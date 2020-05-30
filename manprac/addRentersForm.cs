using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class AddRentersForm : Form
    {
        public AddRentersForm()
        {
            InitializeComponent();
        }

        private void AddRentersForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox1;
        }

        private void addRecordButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addRecordButton_Click(sender, e);
            }
        }
    }
}
