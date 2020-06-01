using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class AddFlatsForm : Form
    {
        public AddFlatsForm()
        {
            InitializeComponent();
        }

        public string ConnString = ConnStringForm.connection;
        private void AddFlatsForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBox2;
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadRenters = new SqlCommand("SELECT Name FROM Renters", conn);
            SqlDataReader readerRenter = loadRenters.ExecuteReader();
            while(readerRenter.Read())
            {
                rentersBox.Items.Add(readerRenter["Name"]);
            }
            readerRenter.Close();

            SqlCommand loadMounth = new SqlCommand("SELECT Name FROM Months", conn);
            SqlDataReader readerMonth = loadMounth.ExecuteReader();
            while(readerMonth.Read())
            {
                monthBox.Items.Add(readerMonth["Name"]);
            }
            readerMonth.Close();
            conn.Close();
        }

        private void addFlatsButton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Есть пустые поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Запись была успешно добавлена", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                if (vatTextBox.Enabled != false)
                {
                    e.Handled = true;
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void vatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }
    }
}
