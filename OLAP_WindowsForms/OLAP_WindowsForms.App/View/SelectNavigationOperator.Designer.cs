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
            this.ComboBox_AgsNavstepSchema = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.ComboBox_Selection = new System.Windows.Forms.ComboBox();
            this.ComboBox_Selection2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ComboBox_AgsNavstepSchema
            // 
            this.ComboBox_AgsNavstepSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_AgsNavstepSchema.FormattingEnabled = true;
            this.ComboBox_AgsNavstepSchema.Location = new System.Drawing.Point(41, 33);
            this.ComboBox_AgsNavstepSchema.Name = "ComboBox_AgsNavstepSchema";
            this.ComboBox_AgsNavstepSchema.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_AgsNavstepSchema.TabIndex = 0;
            this.ComboBox_AgsNavstepSchema.SelectedIndexChanged += new System.EventHandler(this.ComboBox_AgsNavstepSchema_SelectedIndexChanged);
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
            // ComboBox_Selection
            // 
            this.ComboBox_Selection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Selection.FormattingEnabled = true;
            this.ComboBox_Selection.Location = new System.Drawing.Point(41, 98);
            this.ComboBox_Selection.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBox_Selection.Name = "ComboBox_Selection";
            this.ComboBox_Selection.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_Selection.TabIndex = 9;
            this.ComboBox_Selection.Visible = false;
            this.ComboBox_Selection.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Selection_SelectedIndexChanged);
            // 
            // ComboBox_Selection2
            // 
            this.ComboBox_Selection2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Selection2.FormattingEnabled = true;
            this.ComboBox_Selection2.Location = new System.Drawing.Point(41, 163);
            this.ComboBox_Selection2.Name = "ComboBox_Selection2";
            this.ComboBox_Selection2.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_Selection2.TabIndex = 10;
            this.ComboBox_Selection2.Visible = false;
            // 
            // SelectNavigationOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 346);
            this.Controls.Add(this.ComboBox_Selection2);
            this.Controls.Add(this.ComboBox_Selection);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.ComboBox_AgsNavstepSchema);
            this.Name = "SelectNavigationOperator";
            this.Text = "Select Navigation Operator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_AgsNavstepSchema;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.ComboBox ComboBox_Selection;
        private System.Windows.Forms.ComboBox ComboBox_Selection2;
    }
}