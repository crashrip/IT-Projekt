namespace OLAP_WindowsForms.App
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_hostname = new System.Windows.Forms.TextBox();
            this.label_hostname = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label_username = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_dbName = new System.Windows.Forms.Label();
            this.textBox_dbName = new System.Windows.Forms.TextBox();
            this.button_submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter your login data for the database you want to analyse.";

            // 
            // textBox_hostname
            // 
            this.textBox_hostname.Location = new System.Drawing.Point(94, 32);
            this.textBox_hostname.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_hostname.Name = "textBox_hostname";
            this.textBox_hostname.Size = new System.Drawing.Size(152, 20);
            this.textBox_hostname.TabIndex = 1;

            // 
            // label_hostname
            // 
            this.label_hostname.AutoSize = true;
            this.label_hostname.Location = new System.Drawing.Point(10, 34);
            this.label_hostname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_hostname.Name = "label_hostname";
            this.label_hostname.Size = new System.Drawing.Size(55, 13);
            this.label_hostname.TabIndex = 2;
            this.label_hostname.Text = "Hostname";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(10, 57);
            this.label_port.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(26, 13);
            this.label_port.TabIndex = 4;
            this.label_port.Text = "Port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(94, 54);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(152, 20);
            this.textBox_port.TabIndex = 3;
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(10, 80);
            this.label_username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(55, 13);
            this.label_username.TabIndex = 6;
            this.label_username.Text = "Username";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(94, 77);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(152, 20);
            this.textBox_username.TabIndex = 5;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(10, 102);
            this.label_password.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(53, 13);
            this.label_password.TabIndex = 8;
            this.label_password.Text = "Password";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(94, 100);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(152, 20);
            this.textBox_password.TabIndex = 7;
            // 
            // label_dbName
            // 
            this.label_dbName.AutoSize = true;
            this.label_dbName.Location = new System.Drawing.Point(10, 125);
            this.label_dbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_dbName.Name = "label_dbName";
            this.label_dbName.Size = new System.Drawing.Size(53, 13);
            this.label_dbName.TabIndex = 10;
            this.label_dbName.Text = "DB-Name";
            // 
            // textBox_dbName
            // 
            this.textBox_dbName.Location = new System.Drawing.Point(94, 123);
            this.textBox_dbName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_dbName.Name = "textBox_dbName";
            this.textBox_dbName.Size = new System.Drawing.Size(152, 20);
            this.textBox_dbName.TabIndex = 9;
            // 
            // button_submit
            // 
            this.button_submit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_submit.Location = new System.Drawing.Point(188, 146);
            this.button_submit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(56, 19);
            this.button_submit.TabIndex = 11;
            this.button_submit.Text = "Submit";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button_submit_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 305);
            this.Controls.Add(this.button_submit);
            this.Controls.Add(this.label_dbName);
            this.Controls.Add(this.textBox_dbName);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label_hostname);
            this.Controls.Add(this.textBox_hostname);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_hostname;
        private System.Windows.Forms.Label label_hostname;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label_dbName;
        private System.Windows.Forms.TextBox textBox_dbName;
        private System.Windows.Forms.Button button_submit;
    }
}