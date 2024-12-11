namespace FinalProject
{
    partial class Attendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Attend = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Attend
            // 
            this.Attend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Attend.AutoSize = true;
            this.Attend.BackColor = System.Drawing.Color.White;
            this.Attend.Location = new System.Drawing.Point(184, 96);
            this.Attend.Name = "Attend";
            this.Attend.Size = new System.Drawing.Size(37, 13);
            this.Attend.TabIndex = 0;
            this.Attend.Text = "attend";
            // 
            // Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Attend);
            this.Name = "Attendance";
            this.Text = "Attendance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Attend;
    }
}