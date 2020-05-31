namespace manprac
{
    partial class UpdateRentersForm
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
            this.oldNameBox = new System.Windows.Forms.TextBox();
            this.updateRecordButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.newNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(69, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Старое название";
            // 
            // oldNameBox
            // 
            this.oldNameBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.oldNameBox.Enabled = false;
            this.oldNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oldNameBox.Location = new System.Drawing.Point(227, 9);
            this.oldNameBox.Name = "oldNameBox";
            this.oldNameBox.Size = new System.Drawing.Size(207, 26);
            this.oldNameBox.TabIndex = 4;
            // 
            // updateRecordButton
            // 
            this.updateRecordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateRecordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateRecordButton.Location = new System.Drawing.Point(131, 74);
            this.updateRecordButton.Name = "updateRecordButton";
            this.updateRecordButton.Size = new System.Drawing.Size(182, 28);
            this.updateRecordButton.TabIndex = 3;
            this.updateRecordButton.Text = "Обновить запись";
            this.updateRecordButton.UseVisualStyleBackColor = true;
            this.updateRecordButton.Click += new System.EventHandler(this.updateRecordButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(75, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Новое название";
            // 
            // newNameBox
            // 
            this.newNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newNameBox.Location = new System.Drawing.Point(227, 42);
            this.newNameBox.Name = "newNameBox";
            this.newNameBox.Size = new System.Drawing.Size(207, 26);
            this.newNameBox.TabIndex = 6;
            this.newNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // UpdateRentersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 108);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oldNameBox);
            this.Controls.Add(this.updateRecordButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateRentersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование записи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateRentersForm_FormClosing);
            this.Load += new System.EventHandler(this.UpdateRentersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oldNameBox;
        private System.Windows.Forms.Button updateRecordButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newNameBox;
    }
}