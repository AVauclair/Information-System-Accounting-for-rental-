﻿namespace manprac
{
    partial class ConnStringForm
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
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.updateConnStringButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Укажите строку подключения:";
            // 
            // stringTextBox
            // 
            this.stringTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stringTextBox.Location = new System.Drawing.Point(278, 8);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(567, 26);
            this.stringTextBox.TabIndex = 4;
            this.stringTextBox.Text = "Data Source=ПК-1;Initial Catalog=RentDB;Integrated Security=True";
            this.stringTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stringTextBox_KeyDown);
            // 
            // updateConnStringButton
            // 
            this.updateConnStringButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateConnStringButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateConnStringButton.Location = new System.Drawing.Point(320, 43);
            this.updateConnStringButton.Name = "updateConnStringButton";
            this.updateConnStringButton.Size = new System.Drawing.Size(182, 28);
            this.updateConnStringButton.TabIndex = 3;
            this.updateConnStringButton.Text = "Подключиться";
            this.updateConnStringButton.UseVisualStyleBackColor = true;
            this.updateConnStringButton.Click += new System.EventHandler(this.updateConnStringButton_Click);
            // 
            // ConnStringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 80);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.updateConnStringButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnStringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подключение";
            this.Load += new System.EventHandler(this.connStringForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.Button updateConnStringButton;
    }
}