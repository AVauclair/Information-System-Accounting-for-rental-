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
    public partial class ConnStringForm : Form
    {
        public ConnStringForm()
        {
            InitializeComponent();
        }

        private void connStringForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox1;
        }

        public static string connection = "Data Source=ПК-1;Initial Catalog=RentDB;Integrated Security=True";

        private void updateConnStringButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                connection = textBox1.Text;
                MessageBox.Show("Строка была успешно обновлена", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateConnStringButton_Click(sender, e);
            }
        }
    }
}
