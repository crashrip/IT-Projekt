﻿namespace OLAP_WindowsForms.App.View
{
    partial class LoadForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.create_new_schema = new System.Windows.Forms.Button();
            this.initiate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Linksklick auf die ASS_SID um das Analyse Schema zu laden";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 49);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(624, 327);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // create_new_schema
            // 
            this.create_new_schema.Location = new System.Drawing.Point(493, 14);
            this.create_new_schema.Name = "create_new_schema";
            this.create_new_schema.Size = new System.Drawing.Size(145, 23);
            this.create_new_schema.TabIndex = 6;
            this.create_new_schema.Text = "create new schema";
            this.create_new_schema.UseVisualStyleBackColor = true;
            this.create_new_schema.Click += new System.EventHandler(this.create_new_schema_Click);
            // 
            // initiate
            // 
            this.initiate.Location = new System.Drawing.Point(510, 385);
            this.initiate.Name = "initiate";
            this.initiate.Size = new System.Drawing.Size(128, 23);
            this.initiate.TabIndex = 8;
            this.initiate.Text = "initiateButton";
            this.initiate.UseVisualStyleBackColor = true;
            this.initiate.Click += new System.EventHandler(this.initiateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(12, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Rechtsklick auf die ASS_SID um das Analyse Schema zu löschen";
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 420);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.initiate);
            this.Controls.Add(this.create_new_schema);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "LoadForm";
            this.Text = "LoadForm";
            this.Click += new System.EventHandler(this.LoadForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button create_new_schema;
        private System.Windows.Forms.Button initiate;
        private System.Windows.Forms.Label label2;
    }
}