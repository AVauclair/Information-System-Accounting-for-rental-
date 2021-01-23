using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        int formLoading = 0;

        private void connStringForm_Load(object sender, EventArgs e)
        {
            ActiveControl = stringTextBox;
            stringTextBox.Text = Properties.Settings.Default.ConnectionString;
        }

        public static string connection = /*"Data Source=DESKTOP-U4D9RVF;Initial Catalog=RentDB;Integrated Security=True";//*/Properties.Settings.Default.ConnectionString;

        private void updateConnStringButton_Click(object sender, EventArgs e)
        {
            if (stringTextBox.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connection = stringTextBox.Text;
                    SqlConnection conn = new SqlConnection(connection);
                    conn.Open();
                    conn.Close();
                    formLoading = 1;
                }
                catch
                {
                    MessageBox.Show("Отсутствует подключение. Убедитесь, что строка подключения введена правильно.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formLoading = 0;
                }

                if (formLoading == 1)
                {
                    MessageBox.Show("Строка была успешно обновлена", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Properties.Settings.Default.ConnectionString = stringTextBox.Text;
                    Properties.Settings.Default.Save();

                    /*LoginForm loginForm = new LoginForm();
                    loginForm.Show();*/
                    MainForm form = new MainForm();
                    form.Show();
                    Hide();
                }
            }
        }

        private void stringTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateConnStringButton_Click(sender, e);
            }
        }
    }
}
