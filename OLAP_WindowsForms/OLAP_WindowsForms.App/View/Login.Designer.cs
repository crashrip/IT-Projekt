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
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter your login data for the database you want to analyse.";
            // 
            // textBox_hostname
            // 
            this.textBox_hostname.Location = new System.Drawing.Point(125, 53);
            this.textBox_hostname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_hostname.Name = "textBox_hostname";
            this.textBox_hostname.Size = new System.Drawing.Size(201, 22);
            this.textBox_hostname.TabIndex = 1;
            // 
            // label_hostname
            // 
            this.label_hostname.AutoSize = true;
            this.label_hostname.Location = new System.Drawing.Point(13, 56);
            this.label_hostname.Name = "label_hostname";
            this.label_hostname.Size = new System.Drawing.Size(72, 17);
            this.label_hostname.TabIndex = 2;
            this.label_hostname.Text = "Hostname";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(13, 84);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(34, 17);
            this.label_port.TabIndex = 4;
            this.label_port.Text = "Port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(125, 80);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(201, 22);
            this.textBox_port.TabIndex = 3;
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(13, 112);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(73, 17);
            this.label_username.TabIndex = 6;
            this.label_username.Text = "Username";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(125, 109);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(201, 22);
            this.textBox_username.TabIndex = 5;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(13, 140);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(69, 17);
            this.label_password.TabIndex = 8;
            this.label_password.Text = "Password";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(125, 137);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(201, 22);
            this.textBox_password.TabIndex = 7;
            // 
            // label_dbName
            // 
            this.label_dbName.AutoSize = true;
            this.label_dbName.Location = new System.Drawing.Point(13, 168);
            this.label_dbName.Name = "label_dbName";
            this.label_dbName.Size = new System.Drawing.Size(69, 17);
            this.label_dbName.TabIndex = 10;
            this.label_dbName.Text = "DB-Name";
            // 
            // textBox_dbName
            // 
            this.textBox_dbName.Location = new System.Drawing.Point(125, 165);
            this.textBox_dbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_dbName.Name = "textBox_dbName";
            this.textBox_dbName.Size = new System.Drawing.Size(201, 22);
            this.textBox_dbName.TabIndex = 9;
            // 
            // button_submit
            // 
            this.button_submit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_submit.Location = new System.Drawing.Point(251, 217);
            this.button_submit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(75, 23);
            this.button_submit.TabIndex = 11;
            this.button_submit.Text = "Submit";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.Submit);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 279);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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