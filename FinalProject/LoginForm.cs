using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginBttn_Click(object sender, EventArgs e)
        {
            string username = LoginTxtBox.Text;
            string password = PasswordTxtBox.Text;

            var result = AuthenticateUser(username, password);

            if (result != null)
            {
                int userId = result.Value.userId;
                int accessLevel = result.Value.accessLevel;

                if (accessLevel == 2)
                {
                    MessageBox.Show("Welcome, Manager!");
                    Lvl1 managerForm = new Lvl1();
                    managerForm.Show();
                    this.Hide(); //hides 
                }
                else if (accessLevel == 3)
                {
                    MessageBox.Show("Welcome,  User!");
                    Clockinout regularUserForm = new Clockinout();
                    regularUserForm.Show();
                    this.Hide(); // hides
                }
                else
                {
                    MessageBox.Show("Access level not recognized.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        

        public (int userId, int accessLevel)? AuthenticateUser(string username, string password)
            {
                string connectionString = "Data Source=localhost; Initial Catalog=Project1; Integrated Security = true ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                      string query = "SELECT user_id, role_id FROM users WHERE email = @username AND password = @password";

                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password); 

                            connection.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                                {
                                    int userId = reader.GetInt32(0);
                                    int accessLevel = reader.GetInt32(1);
                                    return (userId, accessLevel);
                                }
                       }
                return null; 
            }

     }
}
