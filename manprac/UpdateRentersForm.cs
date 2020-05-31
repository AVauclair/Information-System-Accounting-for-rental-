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
    public partial class UpdateRentersForm : Form
    {
        public string ConnString = Properties.Settings.Default.ConnectionSting;
        public UpdateRentersForm()
        {
            InitializeComponent();
        }

        private void UpdateRentersForm_Load(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            ActiveControl = newNameBox;
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT Name From Renters WHERE ID_Renters = @ID_Renters", conn);
            command.Parameters.AddWithValue("@ID_Renters", main.DataGridRenters.CurrentRow.Cells[0].Value);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                oldNameBox.Text = reader["Name"].ToString();
            }
            reader.Close();
            conn.Close();
            
        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            if (newNameBox.Text == "")
            {
                MessageBox.Show("Поле пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("UPDATE [Renters] SET [Name] = @Name WHERE [ID_Renters] = @ID_Renters", conn);
            command.Parameters.AddWithValue("@Name", newNameBox.Text);
            command.Parameters.AddWithValue("@ID_Renters", main.DataGridRenters.CurrentRow.Cells[0].Value);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
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

        private void UpdateRentersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            main.DataGridRenters.Rows.Clear();
            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();
            List<string[]> data = new List<string[]>();

            int count = 1;
            while (readerRenters.Read())
            {

                data.Add(new string[3]);
                data[count - 1][0] = readerRenters["ID_Renters"].ToString();
                data[data.Count - 1][1] = count.ToString();
                data[data.Count - 1][2] = readerRenters["Name"].ToString();
                count++;
            }
            foreach (string[] s in data)
                main.DataGridRenters.Rows.Add(s);

            readerRenters.Close();
            conn.Close();
        }
    }
}
