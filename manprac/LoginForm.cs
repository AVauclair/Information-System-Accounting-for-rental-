using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace manprac
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
               /* string login = loginTextBox.Text;
                string password = passwordTextBox.Text;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * from Members WHERE login = @login AND password = @password", conn);

                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);


                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    MainForm main = new MainForm();
                    main.Show();
                    loginTextBox.Clear();
                    passwordTextBox.Clear();
                    loginTextBox.Select();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }

                conn.Close();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); ;
            }
        }

        private void loginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                loginButton_Click(sender, e);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
