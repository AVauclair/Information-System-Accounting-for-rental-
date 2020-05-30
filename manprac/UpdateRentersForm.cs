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
    public partial class UpdateRentersForm : Form
    {
        public UpdateRentersForm()
        {
            InitializeComponent();
        }

        private void UpdateRentersForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox2;
        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
        }
    }
}
