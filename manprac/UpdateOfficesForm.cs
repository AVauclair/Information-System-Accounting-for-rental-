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
    public partial class UpdateOfficesForm : Form
    {
        public string ConnString = ConnStringForm.connection;
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        public UpdateOfficesForm()
        {
            InitializeComponent();
        }

        private void UpdateFlatsForm_Load(object sender, EventArgs e)
        {
            yy main = this.Owner as yy;

            ActiveControl = contractTextBox;

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();

            while (readerRenters.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRenters["ID_Renters"]), Convert.ToString(readerRenters["Name"]));
                rentersComboBox.Items.Add(readerRenters["Name"]);
            }
            readerRenters.Close();

            SqlCommand loadMonth = new SqlCommand("SELECT ID_Month, Name FROM Months", conn);
            SqlDataReader readerMonth = loadMonth.ExecuteReader();
            while (readerMonth.Read())
            {
                DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Month"]), Convert.ToString(readerMonth["Name"]));
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
            SqlCommand SelectedItems = new SqlCommand("SELECT ID_Renters, Contract, ID_Month, Amount_Rent, VAT, Date_Payment, Note FROM Offices WHERE ID_Office = @ID_Office", conn);
            SelectedItems.Parameters.AddWithValue("@ID_Office", main.dataGridOffices.CurrentRow.Cells[0].Value);
            SqlDataReader readerSelectedItems = SelectedItems.ExecuteReader();
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
            yy main = this.Owner as yy;
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
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();
                SqlCommand command = new SqlCommand("UPDATE [Offices] SET [ID_Renters] = @ID_Renters, [ID_Month] = @ID_Month, [Contract] = @Contract, [Amount_Rent] = @Amount_Rent, " +
                    "[VAT] = @VAT, [Date_Payment] = @Date_Payment, [Note] = @Note WHERE [ID_Office] = @ID_Office", conn);
                command.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
                command.Parameters.AddWithValue("@ID_Month", SelectedMonth);
                command.Parameters.AddWithValue("@Contract", contractTextBox.Text);
                command.Parameters.AddWithValue("@Amount_Rent", Convert.ToDouble(amountRentBox.Text));
                command.Parameters.AddWithValue("@VAT", Convert.ToDouble(vatTextBox.Text));
                command.Parameters.AddWithValue("@Note", noteTextBox.Text);
                command.Parameters.AddWithValue("@Date_Payment", datePicker.Value);
                command.Parameters.AddWithValue("@ID_Office", main.dataGridOffices.CurrentRow.Cells[0].Value);
                try
                {
                    command.ExecuteNonQuery();

                    SqlCommand loadOffices = new SqlCommand("SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
              " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
              " LEFT JOIN Months on Offices.ID_Month = Months.ID_Month", conn);
                    SqlDataReader readerOffices = loadOffices.ExecuteReader();
                    List<string[]> dataOffices = new List<string[]>();

                    int countOffices = 1;
                    while (readerOffices.Read())
                    {

                        dataOffices.Add(new string[9]);
                        dataOffices[dataOffices.Count - 1][0] = readerOffices["ID_Office"].ToString();
                        dataOffices[dataOffices.Count - 1][1] = countOffices.ToString();
                        dataOffices[dataOffices.Count - 1][2] = readerOffices["Renters"].ToString();
                        dataOffices[dataOffices.Count - 1][3] = readerOffices["Contract"].ToString();
                        dataOffices[dataOffices.Count - 1][4] = readerOffices["Month"].ToString();
                        dataOffices[dataOffices.Count - 1][5] = readerOffices["Amount_Rent"].ToString();
                        dataOffices[dataOffices.Count - 1][6] = readerOffices["VAT"].ToString();
                        dataOffices[dataOffices.Count - 1][7] = readerOffices["Date_Payment"].ToString();
                        dataOffices[dataOffices.Count - 1][8] = readerOffices["Note"].ToString();
                        countOffices++;
                    }
                    main.dataGridOffices.Rows.Clear();
                    foreach (string[] s in dataOffices)
                        main.dataGridOffices.Rows.Add(s);

                    readerOffices.Close();
                    MessageBox.Show("Запись была успешно обновлена", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
           
        }
    }
}
