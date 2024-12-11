using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            
        }

        public DataTable GetPivotedScheduleForWeek(DateTime weekStart, DateTime weekEnd)
        {
            string connectionString = "Data Source=localhost; Initial Catalog=Project1; Integrated Security = true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPivotedScheduleForWeek", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WeekStart", weekStart);
                cmd.Parameters.AddWithValue("@WeekEnd", weekEnd);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadScheduleForNextWeek()
        {
            DateTime today = DateTime.Now;
            DateTime startOfNextWeek = today.AddDays(7 - (int)today.DayOfWeek).Date; 
            DateTime endOfNextWeek = startOfNextWeek.AddDays(6); 

            DataTable scheduleData = GetPivotedScheduleForWeek(startOfNextWeek, endOfNextWeek);
            dataGridView1.DataSource = scheduleData;
        }

        

        private void btnCreateNextSchedule_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;

           
            DateTime weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); // Sunday
            DateTime weekEnd = weekStart.AddDays(6); // Saturday

            
            DataTable selectedWeekSchedule = GetPivotedScheduleForWeek(weekStart, weekEnd);

            
            dataGridView1.DataSource = selectedWeekSchedule;

            
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void SaveSchedule(DateTime weekStart, DateTime weekEnd)
        {
            // datatable for tvp
            DataTable scheduleData = new DataTable();
            scheduleData.Columns.Add("user_id", typeof(int));
            scheduleData.Columns.Add("shift_date", typeof(DateTime));
            scheduleData.Columns.Add("shift_code", typeof(string));


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; 

                int userId = Convert.ToInt32(row.Cells["user_id"].Value);

                for (int i = 1; i < dataGridView1.Columns.Count; i++) 
                {
                    string columnName = dataGridView1.Columns[i].HeaderText;
                    if (DateTime.TryParse(columnName, out DateTime shiftDate)) 
                    {
                        string shiftTime = row.Cells[i].Value?.ToString();
                        if (!string.IsNullOrEmpty(shiftTime) && shiftTime.Contains("-"))
                        {
                            try
                            {
                                
                                string[] times = shiftTime.Split('-');
                                TimeSpan shiftStart = TimeSpan.Parse(times[0].Trim());
                                TimeSpan shiftEnd = TimeSpan.Parse(times[1].Trim());

                                scheduleData.Rows.Add(userId, shiftDate, shiftStart, shiftEnd);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error parsing shift time in row {row.Index + 1}, column {columnName}: {ex.Message}");
                            }
                        }
                    }
                }
            }

            
            string connectionString = "Data Source=localhost; Initial Catalog=Project1; Integrated Security = true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveSchedule", connection))
                {
                    if (scheduleData.Rows.Count == 0)
                    {
                        MessageBox.Show("No schedule data to save.");
                    }

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WeekStart", weekStart);
                    cmd.Parameters.AddWithValue("@WeekEnd", weekEnd);

                   
                    SqlParameter scheduleParam = new SqlParameter("@ScheduleData", SqlDbType.Structured)
                    {
                        TypeName = "dbo.ScheduleTableType", 
                        Value = scheduleData
                    };
                    cmd.Parameters.Add(scheduleParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Schedule saved successfully!");
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;
            DateTime weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); 
            DateTime weekEnd = weekStart.AddDays(6); 

            SaveSchedule(weekStart, weekEnd);
        }
    }
}
