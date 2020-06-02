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
        //public string ConnString = ConnStringForm.connection;
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
            else
            {
                MessageBox.Show("Запись была успешно добавлена", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [Renters] (Name) VALUES (@Name)", conn);
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
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

        private void AddRentersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();
            List<string[]> dataRenters = new List<string[]>();

            int countRenters = 1;
            while (readerRenters.Read())
            {

                dataRenters.Add(new string[3]);
                dataRenters[dataRenters.Count - 1][0] = readerRenters["ID_Renters"].ToString();
                dataRenters[dataRenters.Count - 1][1] = countRenters.ToString();
                dataRenters[dataRenters.Count - 1][2] = readerRenters["Name"].ToString();
                countRenters++;
            }
            main.dataGridRenters.Rows.Clear();
            foreach (string[] s in dataRenters)
                main.dataGridRenters.Rows.Add(s);

            readerRenters.Close();
            conn.Close();
        }
    }
}
