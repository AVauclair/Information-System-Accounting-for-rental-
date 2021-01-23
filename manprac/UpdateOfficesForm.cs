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
    public partial class UpdateOfficesForm : Form
    {
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        private string ConnString = "Data Source = RentDB; Version=3";

        public UpdateOfficesForm()
        {
            InitializeComponent();
        }

        private void UpdateFlatsForm_Load(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;

            ActiveControl = contractTextBox;

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            SQLiteCommand loadRenters = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SQLiteDataReader readerRenters = loadRenters.ExecuteReader();

            while (readerRenters.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRenters["ID_Renters"]), Convert.ToString(readerRenters["Name"]));
                rentersComboBox.Items.Add(readerRenters["Name"]);
            }
            readerRenters.Close();

            SQLiteCommand loadMonth = new SQLiteCommand("SELECT ID_Months, Name FROM Months", conn);
            SQLiteDataReader readerMonth = loadMonth.ExecuteReader();
            while (readerMonth.Read())
            {
                DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Months"]), Convert.ToString(readerMonth["Name"]));
                monthComboBox.Items.Add(readerMonth["Name"]);
            }
            readerMonth.Close();

            int SelectedRenters = 0;
            int SelectedMonth = 0;
            foreach (var item in DebitingRenters)
            {
                if (item.Value == rentersComboBox.Text)
                {
                    SelectedRenters = item.Key;
                }
            }
            foreach (var item in DebitingMonth)
            {
                if (item.Value == monthComboBox.Text)
                {
                    SelectedMonth = item.Key;
                }
            }
            SQLiteCommand SelectedItems = new SQLiteCommand("SELECT ID_Renters, Contract, ID_Month, Amount_Rent, VAT, Date_Payment, Note FROM Offices WHERE ID_Office = @ID_Office", conn);
            SelectedItems.Parameters.AddWithValue("@ID_Office", main.dataGridOffices.CurrentRow.Cells[0].Value);
            SQLiteDataReader readerSelectedItems = SelectedItems.ExecuteReader();
            while(readerSelectedItems.Read())
            {
                rentersComboBox.SelectedItem = DebitingRenters[Convert.ToInt32(readerSelectedItems["ID_Renters"])];
                monthComboBox.SelectedItem = DebitingMonth[Convert.ToInt32(readerSelectedItems["ID_Month"])];
                amountRentBox.Text = readerSelectedItems["Amount_Rent"].ToString();
                contractTextBox.Text = readerSelectedItems["Contract"].ToString();
                amountRentBox.Text = readerSelectedItems["Amount_Rent"].ToString();
                vatTextBox.Text = readerSelectedItems["VAT"].ToString();
                datePicker.Value = Convert.ToDateTime(readerSelectedItems["Date_Payment"]);
                noteTextBox.Text = readerSelectedItems["Note"].ToString();
            }
            readerSelectedItems.Close();
            conn.Close();
            contractTextBox.SelectionStart = 0;
        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            try
            {
                double s = Convert.ToDouble(amountRentBox.Text);
            }
            catch
            {
                MessageBox.Show("В поле \"Сумма аренды\" можно вводить только числа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                double s = Convert.ToDouble(vatTextBox.Text);
            }
            catch
            {
                MessageBox.Show("В поле \"НДС\" можно вводить только числа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int SelectedRenters = 0;
            int SelectedMonth = 0;
            foreach (var item in DebitingRenters)
            {
                if (item.Value == rentersComboBox.Text)
                {
                    SelectedRenters = item.Key;
                }
            }
            foreach (var item in DebitingMonth)
            {
                if (item.Value == monthComboBox.Text)
                {
                    SelectedMonth = item.Key;
                }
            }

            if (contractTextBox.Text == "")
            {
                MessageBox.Show("Заполните поле \"Договор\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (amountRentBox.Text == "")
            {
                MessageBox.Show("Заполните поле \"Сумма аренды\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (vatTextBox.Text == "")
            {
                MessageBox.Show("Заполните поле \"НДС\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE [Offices] SET [ID_Renters] = @ID_Renters, [ID_Month] = @ID_Month, [Contract] = @Contract, [Amount_Rent] = @Amount_Rent, " +
                    "[VAT] = @VAT, [Date_Payment] = @Date_Payment, [Note] = @Note WHERE [ID_Office] = @ID_Office", conn);
                command.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
                command.Parameters.AddWithValue("@ID_Month", SelectedMonth);
                command.Parameters.AddWithValue("@Contract", contractTextBox.Text);
                command.Parameters.AddWithValue("@Amount_Rent", Convert.ToDouble(amountRentBox.Text));
                command.Parameters.AddWithValue("@VAT", Convert.ToDouble(vatTextBox.Text));
                if (noteTextBox.Text == "")
                    command.Parameters.AddWithValue("@Note", "Отсутствует");
                else
                    command.Parameters.AddWithValue("@Note", noteTextBox.Text);
                command.Parameters.AddWithValue("@Date_Payment", datePicker.Value);
                command.Parameters.AddWithValue("@ID_Office", main.dataGridOffices.CurrentRow.Cells[0].Value);
                try
                {
                    command.ExecuteNonQuery();

                    if (MessageBox.Show("Запись успешно изменена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        this.Close();
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

        private void contractTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void amountRentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, true, true, true, true);
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
                updateRecordButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void noteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void rentersComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
        }

        private void monthComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
        }

        private void UpdateOfficesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            int columnIndex = main.dataGridOffices.CurrentCell.ColumnIndex;
            int rowIndex = main.dataGridOffices.CurrentCell.RowIndex;

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();

            SQLiteCommand loadOffices = new SQLiteCommand("SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Months, Amount_Rent, VAT, Date_Payment, Note" +
              " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
              " LEFT JOIN Months on Offices.ID_Month = Months.ID_Months", conn);
            SQLiteDataReader readerOffices = loadOffices.ExecuteReader();
            List<string[]> dataOffices = new List<string[]>();

            int countOffices = 1;
            while (readerOffices.Read())
            {

                dataOffices.Add(new string[9]);
                dataOffices[dataOffices.Count - 1][0] = readerOffices["ID_Office"].ToString();
                dataOffices[dataOffices.Count - 1][1] = countOffices.ToString();
                dataOffices[dataOffices.Count - 1][2] = readerOffices["Renters"].ToString();
                dataOffices[dataOffices.Count - 1][3] = readerOffices["Contract"].ToString();
                dataOffices[dataOffices.Count - 1][4] = readerOffices["Months"].ToString();
                dataOffices[dataOffices.Count - 1][5] = readerOffices["Amount_Rent"].ToString();
                dataOffices[dataOffices.Count - 1][6] = readerOffices["VAT"].ToString();
                if (readerOffices["Date_Payment"].ToString() != "")
                {
                    dataOffices[dataOffices.Count - 1][7] = Convert.ToDateTime(readerOffices["Date_Payment"]).ToShortDateString();
                }
                dataOffices[dataOffices.Count - 1][8] = readerOffices["Note"].ToString();
                countOffices++;
            }
            main.dataGridOffices.Rows.Clear();
            foreach (string[] s in dataOffices)
            {
                main.dataGridOffices.Rows.Add(s);
            }

            main.dataGridOffices.CurrentCell = main.dataGridOffices[columnIndex, rowIndex];

            readerOffices.Close();
            conn.Close();
        }
    }
}
