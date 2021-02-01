using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            MainForm main = this.Owner as MainForm;


            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Заполните поле \"Название предприятия\" ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            try
            {
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
                command.ExecuteNonQuery();
                readerRenters.Close();
                MessageBox.Show("Запись была успешно добавлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                List<string[]> dataRenters = new List<string[]>();

                SQLiteCommand loadRenters2 = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                SQLiteDataReader readerRenters2 = loadRenters2.ExecuteReader();

                int countRenters = 1;
                while (readerRenters2.Read())
                {
                    dataRenters.Add(new string[3]);
                    dataRenters[dataRenters.Count - 1][0] = readerRenters2["ID_Renters"].ToString();
                    dataRenters[dataRenters.Count - 1][1] = countRenters.ToString();
                    dataRenters[dataRenters.Count - 1][2] = readerRenters2["Name"].ToString();
                    countRenters++;
                }

                main.dataGridRenters.Rows.Clear();
                foreach (string[] s in dataRenters)
                {
                    main.dataGridRenters.Rows.Add(s);
                }

                readerRenters.Close();
                readerRenters2.Close();

                Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
                SQLiteCommand loadRentersComboBox = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                SQLiteDataReader readerRentersComboBox = loadRentersComboBox.ExecuteReader();

                main.rentersComboBox.Items.Clear();
                main.rentersComboBox.Items.Add("Все");
                main.rentersComboBox.SelectedItem = "Все";
                while (readerRentersComboBox.Read())
                {
                    DebitingRenters.Add(Convert.ToInt32(readerRentersComboBox["ID_Renters"]), Convert.ToString(readerRentersComboBox["Name"]));
                    main.rentersComboBox.Items.Add(readerRentersComboBox["Name"]);
                }
                readerRentersComboBox.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                if (main.dataGridRenters.Rows.Count > 0 && main.dataGridRenters.Visible == true)
                {
                    int columnIndex = main.dataGridRenters.CurrentCell.ColumnIndex;
                    int rowIndex = main.dataGridRenters.CurrentCell.RowIndex;
                    main.dataGridRenters.CurrentCell = main.dataGridRenters[columnIndex, main.dataGridRenters.RowCount - 1];

                }
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

        }
    }
}
