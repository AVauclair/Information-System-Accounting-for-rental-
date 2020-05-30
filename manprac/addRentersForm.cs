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
    public partial class AddRentersForm : Form
    {
        public string ConnString = Properties.Settings.Default.ConnectionSting;
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
                MessageBox.Show("Заполните поле \"Название предприятия\" ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Renters] (Name) VALUES (@Name)", conn);
            command.Parameters.AddWithValue("@Name", textBox1.Text);
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
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
