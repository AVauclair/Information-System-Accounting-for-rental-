namespace manprac
{
    partial class AddOfficesForm
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
            this.addRecordButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.contract = new System.Windows.Forms.TextBox();
            this.rentersBox = new System.Windows.Forms.ComboBox();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.amountRentBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.vatBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.noteBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addRecordButton
            // 
            this.addRecordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addRecordButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addRecordButton.Location = new System.Drawing.Point(87, 346);
            this.addRecordButton.Name = "addRecordButton";
            this.addRecordButton.Size = new System.Drawing.Size(182, 32);
            this.addRecordButton.TabIndex = 3;
            this.addRecordButton.Text = "Добавить запись";
            this.addRecordButton.UseVisualStyleBackColor = true;
            this.addRecordButton.Click += new System.EventHandler(this.addOfficesButton_Click);
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
            // contract
            // 
            this.contract.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contract.Location = new System.Drawing.Point(157, 83);
            this.contract.Name = "contract";
            this.contract.Size = new System.Drawing.Size(207, 31);
            this.contract.TabIndex = 8;
            this.contract.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // rentersBox
            // 
            this.rentersBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rentersBox.FormattingEnabled = true;
            this.rentersBox.Location = new System.Drawing.Point(157, 10);
            this.rentersBox.Name = "rentersBox";
            this.rentersBox.Size = new System.Drawing.Size(207, 31);
            this.rentersBox.TabIndex = 6;
            this.rentersBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // monthBox
            // 
            this.monthBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Location = new System.Drawing.Point(157, 46);
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(207, 31);
            this.monthBox.TabIndex = 7;
            this.monthBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox2_KeyDown);
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
            this.amountRentBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
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
            // vatBox
            // 
            this.vatBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vatBox.Location = new System.Drawing.Point(157, 157);
            this.vatBox.Name = "vatBox";
            this.vatBox.Size = new System.Drawing.Size(207, 31);
            this.vatBox.TabIndex = 14;
            this.vatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vatTextBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(35, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Дата оплаты:";
            // 
            // datePicker
            // 
            this.datePicker.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.datePicker.Location = new System.Drawing.Point(157, 194);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(207, 31);
            this.datePicker.TabIndex = 16;
            this.datePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // noteBox
            // 
            this.noteBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noteBox.Location = new System.Drawing.Point(157, 246);
            this.noteBox.Name = "noteBox";
            this.noteBox.Size = new System.Drawing.Size(207, 31);
            this.noteBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(66, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Заметка:";
            // 
            // AddOfficesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 390);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noteBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.vatBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.amountRentBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.monthBox);
            this.Controls.Add(this.rentersBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contract);
            this.Controls.Add(this.addRecordButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOfficesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление записи";
            this.Load += new System.EventHandler(this.AddOfficesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addRecordButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox contract;
        private System.Windows.Forms.ComboBox rentersBox;
        private System.Windows.Forms.ComboBox monthBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox amountRentBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox vatBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.TextBox noteBox;
        private System.Windows.Forms.Label label1;
    }
}