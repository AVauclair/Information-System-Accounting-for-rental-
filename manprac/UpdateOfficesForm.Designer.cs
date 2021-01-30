namespace manprac
{
    partial class UpdateOfficesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateOfficesForm));
            this.updateRecordButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.contractTextBox = new System.Windows.Forms.TextBox();
            this.rentersComboBox = new System.Windows.Forms.ComboBox();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.amountRentBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.vatTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // updateRecordButton
            // 
            this.updateRecordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateRecordButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateRecordButton.Location = new System.Drawing.Point(95, 272);
            this.updateRecordButton.Name = "updateRecordButton";
            this.updateRecordButton.Size = new System.Drawing.Size(182, 32);
            this.updateRecordButton.TabIndex = 3;
            this.updateRecordButton.Text = "Обновить запись";
            this.updateRecordButton.UseVisualStyleBackColor = true;
            this.updateRecordButton.Click += new System.EventHandler(this.updateRecordButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(66, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Договор:";
            // 
            // contractTextBox
            // 
            this.contractTextBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contractTextBox.Location = new System.Drawing.Point(157, 83);
            this.contractTextBox.Name = "contractTextBox";
            this.contractTextBox.Size = new System.Drawing.Size(207, 31);
            this.contractTextBox.TabIndex = 8;
            this.contractTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contractTextBox_KeyDown);
            // 
            // rentersComboBox
            // 
            this.rentersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rentersComboBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rentersComboBox.FormattingEnabled = true;
            this.rentersComboBox.Location = new System.Drawing.Point(157, 10);
            this.rentersComboBox.Name = "rentersComboBox";
            this.rentersComboBox.Size = new System.Drawing.Size(207, 31);
            this.rentersComboBox.TabIndex = 6;
            this.rentersComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rentersComboBox_KeyDown);
            // 
            // monthComboBox
            // 
            this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthComboBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Location = new System.Drawing.Point(157, 46);
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(207, 31);
            this.monthComboBox.TabIndex = 7;
            this.monthComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.monthComboBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(83, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Месяц:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(48, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "Арендатор:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Сумма аренды:";
            // 
            // amountRentBox
            // 
            this.amountRentBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.amountRentBox.Location = new System.Drawing.Point(157, 120);
            this.amountRentBox.Name = "amountRentBox";
            this.amountRentBox.Size = new System.Drawing.Size(207, 31);
            this.amountRentBox.TabIndex = 12;
            this.amountRentBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.amountRentTextBox_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(40, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Сумма НДС:";
            // 
            // vatTextBox
            // 
            this.vatTextBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vatTextBox.Location = new System.Drawing.Point(157, 157);
            this.vatTextBox.Name = "vatTextBox";
            this.vatTextBox.Size = new System.Drawing.Size(207, 31);
            this.vatTextBox.TabIndex = 14;
            this.vatTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vatTextBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(30, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Дата оплаты:";
            // 
            // datePicker
            // 
            this.datePicker.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(157, 231);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(207, 31);
            this.datePicker.TabIndex = 17;
            this.datePicker.Value = new System.DateTime(2020, 6, 3, 0, 0, 0, 0);
            this.datePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datePickerStart_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(66, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Заметка:";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noteTextBox.Location = new System.Drawing.Point(157, 194);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(207, 31);
            this.noteTextBox.TabIndex = 16;
            this.noteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noteTextBox_KeyDown);
            // 
            // UpdateOfficesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 315);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.vatTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.amountRentBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.monthComboBox);
            this.Controls.Add(this.rentersComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contractTextBox);
            this.Controls.Add(this.updateRecordButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateOfficesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование записи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateOfficesForm_FormClosing);
            this.Load += new System.EventHandler(this.UpdateFlatsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button updateRecordButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox contractTextBox;
        private System.Windows.Forms.ComboBox rentersComboBox;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox amountRentBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox vatTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox noteTextBox;
    }
}