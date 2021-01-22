using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class AddRentersForm : Form
    {
        public string ConnString = "Data Source = RentDB; Version=3";
        public AddRentersForm()
        {
            InitializeComponent();
        }

        private void AddRentersForm_Load(object sender, EventArgs e)
        {
            ActiveControl = nameTextBox;
        }

        private void addRecordButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Заполните поле \"Название предприятия\" ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();

            SQLiteCommand loadRenters = new SQLiteCommand("SELECT  Name FROM Renters WHERE Name= @Name", conn);
            loadRenters.Parameters.AddWithValue("@Name", nameTextBox.Text);
            SQLiteDataReader readerRenters = loadRenters.ExecuteReader();

            if (readerRenters.HasRows)
            {
                MessageBox.Show("Данный арендатор уже существует в базе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                readerRenters.Close();
                conn.Close();
                return;
            }

                SQLiteCommand command = new SQLiteCommand("INSERT INTO [Renters] (Name) VALUES (@Name)", conn);
            command.Parameters.AddWithValue("@Name", nameTextBox.Text);
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись была успешно добавлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               readerRenters.Close();
               conn.Close();
            }
        }

        private void nameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addRecordButton_Click(sender, e);
            }
        }

        private void AddRentersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            yy main = this.Owner as yy;
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            SQLiteCommand loadRenters = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SQLiteDataReader readerRenters = loadRenters.ExecuteReader();
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
