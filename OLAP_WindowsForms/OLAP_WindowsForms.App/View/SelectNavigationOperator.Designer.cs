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
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA = new System.Windows.Forms.ComboBox();
            this.textBox_DN = new System.Windows.Forms.TextBox();
            this.label_DN = new System.Windows.Forms.Label();
            this.ComboBox_Selection3 = new System.Windows.Forms.ComboBox();
            this.ComboBox_Selection4 = new System.Windows.Forms.ComboBox();
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
            this.buttonCancel.Location = new System.Drawing.Point(180, 402);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(41, 401);
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
            this.ComboBox_Selection2.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Selection2_SelectedIndexChanged);
            // 
            // ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA
            // 
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.FormattingEnabled = true;
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Location = new System.Drawing.Point(41, 349);
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Name = "ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA";
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.TabIndex = 11;
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = false;
            this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.SelectedIndexChanged += new System.EventHandler(this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA_SelectedIndexChanged);
            // 
            // textBox_DN
            // 
            this.textBox_DN.Location = new System.Drawing.Point(76, 230);
            this.textBox_DN.Name = "textBox_DN";
            this.textBox_DN.Size = new System.Drawing.Size(179, 22);
            this.textBox_DN.TabIndex = 12;
            this.textBox_DN.Visible = false;
            // 
            // label_DN
            // 
            this.label_DN.AutoSize = true;
            this.label_DN.Location = new System.Drawing.Point(38, 232);
            this.label_DN.Name = "label_DN";
            this.label_DN.Size = new System.Drawing.Size(32, 17);
            this.label_DN.TabIndex = 13;
            this.label_DN.Text = "DN:";
            this.label_DN.Visible = false;
            // 
            // ComboBox_Selection3
            // 
            this.ComboBox_Selection3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Selection3.FormattingEnabled = true;
            this.ComboBox_Selection3.Location = new System.Drawing.Point(41, 228);
            this.ComboBox_Selection3.Name = "ComboBox_Selection3";
            this.ComboBox_Selection3.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_Selection3.TabIndex = 14;
            this.ComboBox_Selection3.Visible = false;
            this.ComboBox_Selection3.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Selection3_SelectedIndexChanged);
            // 
            // ComboBox_Selection4
            // 
            this.ComboBox_Selection4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Selection4.FormattingEnabled = true;
            this.ComboBox_Selection4.Location = new System.Drawing.Point(41, 290);
            this.ComboBox_Selection4.Name = "ComboBox_Selection4";
            this.ComboBox_Selection4.Size = new System.Drawing.Size(214, 24);
            this.ComboBox_Selection4.TabIndex = 15;
            this.ComboBox_Selection4.Visible = false;
            this.ComboBox_Selection4.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Selection4_SelectedIndexChanged);
            // 
            // SelectNavigationOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 482);
            this.Controls.Add(this.ComboBox_Selection4);
            this.Controls.Add(this.ComboBox_Selection3);
            this.Controls.Add(this.label_DN);
            this.Controls.Add(this.textBox_DN);
            this.Controls.Add(this.ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA);
            this.Controls.Add(this.ComboBox_Selection2);
            this.Controls.Add(this.ComboBox_Selection);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.ComboBox_AgsNavstepSchema);
            this.Name = "SelectNavigationOperator";
            this.Text = "Select Navigation Operator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_AgsNavstepSchema;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.ComboBox ComboBox_Selection;
        private System.Windows.Forms.ComboBox ComboBox_Selection2;
        private System.Windows.Forms.ComboBox ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA;
        private System.Windows.Forms.TextBox textBox_DN;
        private System.Windows.Forms.Label label_DN;
        private System.Windows.Forms.ComboBox ComboBox_Selection3;
        private System.Windows.Forms.ComboBox ComboBox_Selection4;
    }
}