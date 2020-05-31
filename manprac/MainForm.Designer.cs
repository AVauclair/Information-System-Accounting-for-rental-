namespace manprac
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.resultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rentersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRentersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateRentersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRentersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.officesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOfficeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateOfficeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOfficeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFlatsPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFlatsPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFlatsPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataGridRenters = new System.Windows.Forms.DataGridView();
            this.dataGridOffices = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridFlats = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameRenters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRenters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOffices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFlats)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resultToolStripMenuItem,
            this.rentersToolStripMenuItem,
            this.officesToolStripMenuItem,
            this.flatsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(912, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // resultToolStripMenuItem
            // 
            this.resultToolStripMenuItem.Name = "resultToolStripMenuItem";
            this.resultToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.resultToolStripMenuItem.Text = "Свод";
            // 
            // rentersToolStripMenuItem
            // 
            this.rentersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRentersToolStripMenuItem,
            this.updateRentersToolStripMenuItem,
            this.deleteRentersToolStripMenuItem});
            this.rentersToolStripMenuItem.Name = "rentersToolStripMenuItem";
            this.rentersToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.rentersToolStripMenuItem.Text = "Арендаторы";
            this.rentersToolStripMenuItem.Click += new System.EventHandler(this.rentersToolStripMenuItem_Click);
            // 
            // addRentersToolStripMenuItem
            // 
            this.addRentersToolStripMenuItem.Name = "addRentersToolStripMenuItem";
            this.addRentersToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addRentersToolStripMenuItem.Text = "Добавить";
            this.addRentersToolStripMenuItem.Click += new System.EventHandler(this.addRentersToolStripMenuItem_Click);
            // 
            // updateRentersToolStripMenuItem
            // 
            this.updateRentersToolStripMenuItem.Name = "updateRentersToolStripMenuItem";
            this.updateRentersToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.updateRentersToolStripMenuItem.Text = "Изменить";
            this.updateRentersToolStripMenuItem.Click += new System.EventHandler(this.updateRentersToolStripMenuItem_Click);
            // 
            // deleteRentersToolStripMenuItem
            // 
            this.deleteRentersToolStripMenuItem.Name = "deleteRentersToolStripMenuItem";
            this.deleteRentersToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteRentersToolStripMenuItem.Text = "Удалить";
            this.deleteRentersToolStripMenuItem.Click += new System.EventHandler(this.deleteRentersToolStripMenuItem_Click);
            // 
            // officesToolStripMenuItem
            // 
            this.officesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addOfficeToolStripMenuItem,
            this.updateOfficeToolStripMenuItem,
            this.deleteOfficeToolStripMenuItem});
            this.officesToolStripMenuItem.Name = "officesToolStripMenuItem";
            this.officesToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.officesToolStripMenuItem.Text = "Офис";
            this.officesToolStripMenuItem.Click += new System.EventHandler(this.officesToolStripMenuItem_Click);
            // 
            // addOfficeToolStripMenuItem
            // 
            this.addOfficeToolStripMenuItem.Name = "addOfficeToolStripMenuItem";
            this.addOfficeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addOfficeToolStripMenuItem.Text = "Добавить";
            this.addOfficeToolStripMenuItem.Click += new System.EventHandler(this.addOfficeToolStripMenuItem_Click);
            // 
            // updateOfficeToolStripMenuItem
            // 
            this.updateOfficeToolStripMenuItem.Name = "updateOfficeToolStripMenuItem";
            this.updateOfficeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.updateOfficeToolStripMenuItem.Text = "Изменить";
            this.updateOfficeToolStripMenuItem.Click += new System.EventHandler(this.updateOfficeToolStripMenuItem_Click);
            // 
            // deleteOfficeToolStripMenuItem
            // 
            this.deleteOfficeToolStripMenuItem.Name = "deleteOfficeToolStripMenuItem";
            this.deleteOfficeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteOfficeToolStripMenuItem.Text = "Удалить";
            this.deleteOfficeToolStripMenuItem.Click += new System.EventHandler(this.deleteOfficeToolStripMenuItem_Click);
            // 
            // flatsToolStripMenuItem
            // 
            this.flatsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFlatsPToolStripMenuItem,
            this.updateFlatsPToolStripMenuItem,
            this.deleteFlatsPToolStripMenuItem});
            this.flatsToolStripMenuItem.Name = "flatsToolStripMenuItem";
            this.flatsToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.flatsToolStripMenuItem.Text = "Квартиры";
            this.flatsToolStripMenuItem.Click += new System.EventHandler(this.flatsToolStripMenuItem_Click);
            // 
            // addFlatsPToolStripMenuItem
            // 
            this.addFlatsPToolStripMenuItem.Name = "addFlatsPToolStripMenuItem";
            this.addFlatsPToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addFlatsPToolStripMenuItem.Text = "Добавить";
            this.addFlatsPToolStripMenuItem.Click += new System.EventHandler(this.addFlatsPToolStripMenuItem_Click);
            // 
            // updateFlatsPToolStripMenuItem
            // 
            this.updateFlatsPToolStripMenuItem.Name = "updateFlatsPToolStripMenuItem";
            this.updateFlatsPToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.updateFlatsPToolStripMenuItem.Text = "Изменить";
            this.updateFlatsPToolStripMenuItem.Click += new System.EventHandler(this.updateFlatsPToolStripMenuItem_Click);
            // 
            // deleteFlatsPToolStripMenuItem
            // 
            this.deleteFlatsPToolStripMenuItem.Name = "deleteFlatsPToolStripMenuItem";
            this.deleteFlatsPToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteFlatsPToolStripMenuItem.Text = "Удалить";
            this.deleteFlatsPToolStripMenuItem.Click += new System.EventHandler(this.deleteFlatsPToolStripMenuItem_Click);
            // 
            // DataGridRenters
            // 
            this.DataGridRenters.AllowUserToAddRows = false;
            this.DataGridRenters.AllowUserToDeleteRows = false;
            this.DataGridRenters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridRenters.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DataGridRenters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridRenters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column1,
            this.NameRenters});
            this.DataGridRenters.GridColor = System.Drawing.SystemColors.Control;
            this.DataGridRenters.Location = new System.Drawing.Point(12, 27);
            this.DataGridRenters.MultiSelect = false;
            this.DataGridRenters.Name = "DataGridRenters";
            this.DataGridRenters.ReadOnly = true;
            this.DataGridRenters.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DataGridRenters.RowHeadersVisible = false;
            this.DataGridRenters.Size = new System.Drawing.Size(771, 329);
            this.DataGridRenters.TabIndex = 2;
            this.DataGridRenters.Visible = false;
            // 
            // dataGridOffices
            // 
            this.dataGridOffices.AllowUserToAddRows = false;
            this.dataGridOffices.AllowUserToDeleteRows = false;
            this.dataGridOffices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridOffices.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridOffices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOffices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column7,
            this.dataGridViewTextBoxColumn2,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column55,
            this.Column5,
            this.Column6});
            this.dataGridOffices.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridOffices.Location = new System.Drawing.Point(56, 95);
            this.dataGridOffices.MultiSelect = false;
            this.dataGridOffices.Name = "dataGridOffices";
            this.dataGridOffices.ReadOnly = true;
            this.dataGridOffices.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridOffices.RowHeadersVisible = false;
            this.dataGridOffices.Size = new System.Drawing.Size(771, 329);
            this.dataGridOffices.TabIndex = 3;
            this.dataGridOffices.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            this.dataGridViewTextBoxColumn1.HeaderText = "№";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "ID";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Арендатор";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Контракт";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Месяц";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Сумма Аренды";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column55
            // 
            this.Column55.HeaderText = "НДС";
            this.Column55.Name = "Column55";
            this.Column55.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Дата";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Заметка";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // dataGridFlats
            // 
            this.dataGridFlats.AllowUserToAddRows = false;
            this.dataGridFlats.AllowUserToDeleteRows = false;
            this.dataGridFlats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridFlats.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridFlats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFlats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.Column8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.dataGridFlats.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridFlats.Location = new System.Drawing.Point(27, 128);
            this.dataGridFlats.MultiSelect = false;
            this.dataGridFlats.Name = "dataGridFlats";
            this.dataGridFlats.ReadOnly = true;
            this.dataGridFlats.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridFlats.RowHeadersVisible = false;
            this.dataGridFlats.Size = new System.Drawing.Size(873, 329);
            this.dataGridFlats.TabIndex = 4;
            this.dataGridFlats.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Заметка";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "НДС";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Сумма оплаты";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Сумма Аренды";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Месяц";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Контракт";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Арендатор";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 30F;
            this.dataGridViewTextBoxColumn3.HeaderText = "№";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 30;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "ID";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 30F;
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // NameRenters
            // 
            this.NameRenters.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameRenters.HeaderText = "ФИО/ Название организации";
            this.NameRenters.Name = "NameRenters";
            this.NameRenters.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(912, 532);
            this.Controls.Add(this.dataGridFlats);
            this.Controls.Add(this.dataGridOffices);
            this.Controls.Add(this.DataGridRenters);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Давид придумает, Давид умный и очень находчивый ))))))))";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRenters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOffices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFlats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rentersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRentersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateRentersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRentersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem officesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOfficeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateOfficeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteOfficeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFlatsPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateFlatsPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFlatsPToolStripMenuItem;
        public System.Windows.Forms.DataGridView DataGridRenters;
        public System.Windows.Forms.DataGridView dataGridOffices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column55;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        public System.Windows.Forms.DataGridView dataGridFlats;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameRenters;
    }
}

