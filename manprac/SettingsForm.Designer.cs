
namespace manprac
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.savaDB_Button = new System.Windows.Forms.Button();
            this.loadDB_Button = new System.Windows.Forms.Button();
            this.createDB_Button = new System.Windows.Forms.Button();
            this.clearRentersButton = new System.Windows.Forms.Button();
            this.clearOfficesButton = new System.Windows.Forms.Button();
            this.clearFlatsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RecreateDB_Button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.helpLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // savaDB_Button
            // 
            this.savaDB_Button.Location = new System.Drawing.Point(6, 29);
            this.savaDB_Button.Name = "savaDB_Button";
            this.savaDB_Button.Size = new System.Drawing.Size(203, 36);
            this.savaDB_Button.TabIndex = 0;
            this.savaDB_Button.Text = "Сохранить базу";
            this.toolTip1.SetToolTip(this.savaDB_Button, "убей меня");
            this.savaDB_Button.UseVisualStyleBackColor = true;
            this.savaDB_Button.Click += new System.EventHandler(this.savaDB_Button_Click);
            // 
            // loadDB_Button
            // 
            this.loadDB_Button.Location = new System.Drawing.Point(6, 83);
            this.loadDB_Button.Name = "loadDB_Button";
            this.loadDB_Button.Size = new System.Drawing.Size(203, 36);
            this.loadDB_Button.TabIndex = 1;
            this.loadDB_Button.Text = "Загрузить базу";
            this.loadDB_Button.UseVisualStyleBackColor = true;
            this.loadDB_Button.Click += new System.EventHandler(this.loadDB_Button_Click);
            // 
            // createDB_Button
            // 
            this.createDB_Button.Location = new System.Drawing.Point(6, 35);
            this.createDB_Button.Name = "createDB_Button";
            this.createDB_Button.Size = new System.Drawing.Size(197, 36);
            this.createDB_Button.TabIndex = 2;
            this.createDB_Button.Text = "Создать базу данных ";
            this.toolTip1.SetToolTip(this.createDB_Button, "Создание базы данных с таблицами. Если база данных уже существует в проекте,\r\n то" +
        "  новая создана не будет.");
            this.createDB_Button.UseVisualStyleBackColor = true;
            this.createDB_Button.Click += new System.EventHandler(this.createDB_Button_Click);
            // 
            // clearRentersButton
            // 
            this.clearRentersButton.Location = new System.Drawing.Point(6, 28);
            this.clearRentersButton.Name = "clearRentersButton";
            this.clearRentersButton.Size = new System.Drawing.Size(292, 36);
            this.clearRentersButton.TabIndex = 3;
            this.clearRentersButton.Text = "Стереть данные в таблице \"Арендаторы\"";
            this.clearRentersButton.UseVisualStyleBackColor = true;
            this.clearRentersButton.Click += new System.EventHandler(this.clearRentersButton_Click);
            // 
            // clearOfficesButton
            // 
            this.clearOfficesButton.Location = new System.Drawing.Point(6, 75);
            this.clearOfficesButton.Name = "clearOfficesButton";
            this.clearOfficesButton.Size = new System.Drawing.Size(292, 36);
            this.clearOfficesButton.TabIndex = 4;
            this.clearOfficesButton.Text = "Стереть данные в таблице \"Офисы\"";
            this.clearOfficesButton.UseVisualStyleBackColor = true;
            this.clearOfficesButton.Click += new System.EventHandler(this.clearOfficesButton_Click);
            // 
            // clearFlatsButton
            // 
            this.clearFlatsButton.Location = new System.Drawing.Point(6, 121);
            this.clearFlatsButton.Name = "clearFlatsButton";
            this.clearFlatsButton.Size = new System.Drawing.Size(292, 36);
            this.clearFlatsButton.TabIndex = 5;
            this.clearFlatsButton.Text = "Стереть данные в таблице \"Квартиры\"";
            this.clearFlatsButton.UseVisualStyleBackColor = true;
            this.clearFlatsButton.Click += new System.EventHandler(this.clearFlatsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearRentersButton);
            this.groupBox1.Controls.Add(this.clearFlatsButton);
            this.groupBox1.Controls.Add(this.clearOfficesButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 178);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Очистка таблиц";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RecreateDB_Button);
            this.groupBox2.Controls.Add(this.createDB_Button);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Создание базы данных";
            // 
            // RecreateDB_Button
            // 
            this.RecreateDB_Button.Location = new System.Drawing.Point(353, 35);
            this.RecreateDB_Button.Name = "RecreateDB_Button";
            this.RecreateDB_Button.Size = new System.Drawing.Size(197, 36);
            this.RecreateDB_Button.TabIndex = 3;
            this.RecreateDB_Button.Text = "Пересоздать базу данных ";
            this.toolTip1.SetToolTip(this.RecreateDB_Button, "Полное удаление старой БД и создание новой.\r\nНеобходимо использовать в случае, ес" +
        "ли БД была\r\nповреждена или неправильно создана");
            this.RecreateDB_Button.UseVisualStyleBackColor = true;
            this.RecreateDB_Button.Click += new System.EventHandler(this.RecreateDB_Button_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.helpLabel);
            this.groupBox3.Controls.Add(this.savaDB_Button);
            this.groupBox3.Controls.Add(this.loadDB_Button);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(12, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 165);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Копирование базы/ загрузка данных из копии";
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.helpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.helpLabel.ForeColor = System.Drawing.Color.MediumBlue;
            this.helpLabel.Location = new System.Drawing.Point(6, 134);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(261, 15);
            this.helpLabel.TabIndex = 2;
            this.helpLabel.Text = "Я не могу загрузить копию БД. Что делать?";
            this.helpLabel.Click += new System.EventHandler(this.helpLabel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 480);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ISAFR: Настройки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button savaDB_Button;
        private System.Windows.Forms.Button loadDB_Button;
        private System.Windows.Forms.Button createDB_Button;
        private System.Windows.Forms.Button clearRentersButton;
        private System.Windows.Forms.Button clearOfficesButton;
        private System.Windows.Forms.Button clearFlatsButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button RecreateDB_Button;
        private System.Windows.Forms.Label helpLabel;
    }
}