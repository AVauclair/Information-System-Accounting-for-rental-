namespace manprac
{
    partial class Form1
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
            this.menuStrip1.SuspendLayout();
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
            this.updateRentersToolStripMenuItem.Text = "Обновить";
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
            // 
            // addOfficeToolStripMenuItem
            // 
            this.addOfficeToolStripMenuItem.Name = "addOfficeToolStripMenuItem";
            this.addOfficeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addOfficeToolStripMenuItem.Text = "Добавить";
            this.addOfficeToolStripMenuItem.Click += new System.EventHandler(this.addOfficeToolStripMenuItem_Click);
            // 
            // updateOfficeToolStripMenuItem
            // 
            this.updateOfficeToolStripMenuItem.Name = "updateOfficeToolStripMenuItem";
            this.updateOfficeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateOfficeToolStripMenuItem.Text = "Обновить";
            this.updateOfficeToolStripMenuItem.Click += new System.EventHandler(this.updateOfficeToolStripMenuItem_Click);
            // 
            // deleteOfficeToolStripMenuItem
            // 
            this.deleteOfficeToolStripMenuItem.Name = "deleteOfficeToolStripMenuItem";
            this.deleteOfficeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            // 
            // addFlatsPToolStripMenuItem
            // 
            this.addFlatsPToolStripMenuItem.Name = "addFlatsPToolStripMenuItem";
            this.addFlatsPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addFlatsPToolStripMenuItem.Text = "Добавить";
            this.addFlatsPToolStripMenuItem.Click += new System.EventHandler(this.addFlatsPToolStripMenuItem_Click);
            // 
            // updateFlatsPToolStripMenuItem
            // 
            this.updateFlatsPToolStripMenuItem.Name = "updateFlatsPToolStripMenuItem";
            this.updateFlatsPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateFlatsPToolStripMenuItem.Text = "Обновить";
            this.updateFlatsPToolStripMenuItem.Click += new System.EventHandler(this.updateFlatsPToolStripMenuItem_Click);
            // 
            // deleteFlatsPToolStripMenuItem
            // 
            this.deleteFlatsPToolStripMenuItem.Name = "deleteFlatsPToolStripMenuItem";
            this.deleteFlatsPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteFlatsPToolStripMenuItem.Text = "Удалить";
            this.deleteFlatsPToolStripMenuItem.Click += new System.EventHandler(this.deleteFlatsPToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 532);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}

