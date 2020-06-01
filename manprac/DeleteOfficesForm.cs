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
    public partial class DeleteOfficesForm : Form
    {
        public string ConnString = ConnStringForm.connection;
        public DeleteOfficesForm()
        {
            InitializeComponent();
        }

        private void DeleteOfficesForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox1;
        }

        private void deleteRecordButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Запись была успешно удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //  MessageBox.Show("=)");
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                deleteRecordButton_Click(sender, e);
            }
        }
    }
}
