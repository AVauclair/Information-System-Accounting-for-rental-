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
    public partial class AddOfficesForm : Form
    {
        public string ConnString = ConnStringForm.connection;
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        public AddOfficesForm()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();

            while (readerRenters.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRenters["ID_Renters"]), Convert.ToString(readerRenters["Name"]));
                rentersBox.Items.Add(readerRenters["Name"]);
            }
            readerRenters.Close();

            SqlCommand loadMonth = new SqlCommand("SELECT ID_Month, Name FROM Months", conn);
            SqlDataReader readerMonth = loadMonth.ExecuteReader();
            while(readerMonth.Read())
            {
                DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Month"]), Convert.ToString(readerMonth["Name"]));
                monthBox.Items.Add(readerMonth["Name"]);
            }
            readerMonth.Close();
            conn.Close();

        }

        private void AddOfficesForm_Load(object sender, EventArgs e)
        {
            ActiveControl = contract;
        }

        private void addOfficesButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(rentersBox.Text)) errors.AppendLine("Выберите арендатора.");
            if (string.IsNullOrWhiteSpace(monthBox.Text)) errors.AppendLine("Выберите месяц.");
            if (string.IsNullOrWhiteSpace(contract.Text)) errors.AppendLine("Заполните поле \"Контракт\".");
            if (string.IsNullOrWhiteSpace(amountRentBox.Text)) errors.AppendLine("Заполните поле \"Сумма аренды\".");
            if (string.IsNullOrWhiteSpace(vatBox.Text)) errors.AppendLine("Заполниете поле \"НДС\".");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                double s = Convert.ToDouble(vatBox.Text);
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
                if (item.Value == rentersBox.Text)
                {
                    SelectedRenters = item.Key;
                }
            }
            foreach (var item in DebitingMonth)
            {
                if (item.Value == monthBox.Text)
                {
                    SelectedMonth = item.Key;
                }
            }

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand InsertInOffices = new SqlCommand("INSERT INTO [Offices] (ID_Renters, Contract, ID_Month, Amount_Rent, VAT, Date_Payment, Note) VALUES (@ID_Renters, @Contract, @ID_Month, @Amount_Rent, @VAT, @Date_Payment, @Note)", conn);
            InsertInOffices.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
            InsertInOffices.Parameters.AddWithValue("@Contract", contract.Text);
            InsertInOffices.Parameters.AddWithValue("@ID_Month", SelectedMonth);
            InsertInOffices.Parameters.AddWithValue("@Amount_Rent", amountRentBox.Text);
            InsertInOffices.Parameters.AddWithValue("@VAT", vatBox.Text);
            InsertInOffices.Parameters.AddWithValue("@Date_Payment", datePicker.Value);
            InsertInOffices.Parameters.AddWithValue("@Note", noteBox.Text);
            try
            {
                InsertInOffices.ExecuteNonQuery();
                MessageBox.Show("Запись успешно добавлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand loadOffices = new SqlCommand("SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
              " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
              " LEFT JOIN Months on Offices.ID_Month = Months.ID_Month", conn);
                SqlDataReader readerOffices = loadOffices.ExecuteReader();
                List<string[]> dataOffices = new List<string[]>();

                int countOffices = 1;
                while (readerOffices.Read())
                {

                    dataOffices.Add(new string[9]);
                    dataOffices[dataOffices.Count - 1][0] = countOffices.ToString();
                    dataOffices[dataOffices.Count - 1][1] = readerOffices["ID_Office"].ToString();
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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addOfficesButton_Click(sender, e);
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
                addOfficesButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                if (vatBox.Enabled != false)
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
                addOfficesButton_Click(sender, e);
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
                addOfficesButton_Click(sender, e);
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addOfficesButton_Click(sender, e);
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addOfficesButton_Click(sender, e);
            }
        }
    }
}
