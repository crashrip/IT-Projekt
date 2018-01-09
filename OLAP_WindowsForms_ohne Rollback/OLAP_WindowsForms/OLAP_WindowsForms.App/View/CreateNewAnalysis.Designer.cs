namespace OLAP_WindowsForms.App.View
{
    partial class CreateNewAnalysis
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
            this.AGS_NAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AGS_DESCRITPION = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bitte tragen Sie einen Namen für das neue Analyse-Schema ein:";
            // 
            // AGS_NAME
            // 
            this.AGS_NAME.Location = new System.Drawing.Point(15, 58);
            this.AGS_NAME.Name = "AGS_NAME";
            this.AGS_NAME.Size = new System.Drawing.Size(411, 20);
            this.AGS_NAME.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Beschreibung:";
            // 
            // AGS_DESCRITPION
            // 
            this.AGS_DESCRITPION.Location = new System.Drawing.Point(15, 130);
            this.AGS_DESCRITPION.Multiline = true;
            this.AGS_DESCRITPION.Name = "AGS_DESCRITPION";
            this.AGS_DESCRITPION.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AGS_DESCRITPION.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AGS_DESCRITPION.Size = new System.Drawing.Size(411, 189);
            this.AGS_DESCRITPION.TabIndex = 6;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(115, 329);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(259, 329);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CreateNewAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 364);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.AGS_DESCRITPION);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AGS_NAME);
            this.Controls.Add(this.label1);
            this.Name = "CreateNewAnalysis";
            this.Text = "CreateNewSchema";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AGS_NAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AGS_DESCRITPION;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}