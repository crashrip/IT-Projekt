namespace OLAP_WindowsForms.App.View
{
    partial class SelectNavigationOperator
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
            this.comboBoxNav = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.ComboBoxCube = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxNav
            // 
            this.comboBoxNav.FormattingEnabled = true;
            this.comboBoxNav.Location = new System.Drawing.Point(41, 33);
            this.comboBoxNav.Name = "comboBoxNav";
            this.comboBoxNav.Size = new System.Drawing.Size(214, 24);
            this.comboBoxNav.TabIndex = 0;
            this.comboBoxNav.SelectedIndexChanged += new System.EventHandler(this.comboBoxNav_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(180, 277);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(41, 277);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 30);
            this.buttonSubmit.TabIndex = 2;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // ComboBoxCube
            // 
            this.ComboBoxCube.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCube.FormattingEnabled = true;
            this.ComboBoxCube.Location = new System.Drawing.Point(41, 98);
            this.ComboBoxCube.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxCube.Name = "ComboBoxCube";
            this.ComboBoxCube.Size = new System.Drawing.Size(214, 24);
            this.ComboBoxCube.TabIndex = 9;
            this.ComboBoxCube.Visible = false;
            this.ComboBoxCube.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCube_SelectedIndexChanged);
            // 
            // SelectNavigationOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 346);
            this.Controls.Add(this.ComboBoxCube);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxNav);
            this.Name = "SelectNavigationOperator";
            this.Text = "Select Navigation Operator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxNav;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.ComboBox ComboBoxCube;
    }
}