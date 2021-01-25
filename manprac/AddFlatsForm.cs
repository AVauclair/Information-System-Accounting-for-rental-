using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class AddFlatsForm : Form
    {
        public AddFlatsForm()
        {
            InitializeComponent();
        }
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingStatus = new Dictionary<int, string>();
        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        private string ConnString = "Data Source = RentDB; Version=3";
        private void AddFlatsForm_Load(object sender, EventArgs e)
        {
            ActiveControl = contractTextBox;
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            try
            {
                SQLiteCommand loadRenters = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                SQLiteDataReader readerRenter = loadRenters.ExecuteReader();
                while (readerRenter.Read())
                {
                    DebitingRenters.Add(Convert.ToInt32(readerRenter["ID_Renters"]), Convert.ToString(readerRenter["Name"]));
                    rentersComboBox.Items.Add(readerRenter["Name"]);
                }
                readerRenter.Close();

                SQLiteCommand loadMonth = new SQLiteCommand("SELECT ID_Months, Name FROM Months", conn);
                SQLiteDataReader readerMonth = loadMonth.ExecuteReader();
                while (readerMonth.Read())
                {
                    DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Months"]), Convert.ToString(readerMonth["Name"]));
                    monthComboBox.Items.Add(readerMonth["Name"]);
                }
                readerMonth.Close();

                SQLiteCommand loadStatus = new SQLiteCommand("SELECT ID_Apartament_Status, Name FROM ApartamentStatus", conn);
                SQLiteDataReader readerStatus = loadStatus.ExecuteReader();
                while (readerStatus.Read())
                {
                    DebitingStatus.Add(Convert.ToInt32(readerStatus["ID_Apartament_Status"]), Convert.ToString(readerStatus["Name"]));
                    areaTypeComboBox.Items.Add(readerStatus["Name"]);
                }
                readerStatus.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка. "+ ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void addFlatsButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(areaTypeComboBox.Text)) errors.AppendLine("Выберите тип помещения.");
            if (string.IsNullOrWhiteSpace(rentersComboBox.Text)) errors.AppendLine("Выберите арендатора.");
            if (string.IsNullOrWhiteSpace(monthComboBox.Text)) errors.AppendLine("Выберите месяц.");
            if (string.IsNullOrWhiteSpace(contractTextBox.Text)) errors.AppendLine("Заполните поле \"Договор\".");
            if (string.IsNullOrWhiteSpace(amountRentTextBox.Text)) errors.AppendLine("Заполните поле \"Сумма аренды\".");
            if (string.IsNullOrWhiteSpace(amountPaymentTextBox.Text)) errors.AppendLine("Заполните поле \"Сумма оплаты\".");
            if(vatTextBox.Enabled == true)
            {
                if (string.IsNullOrWhiteSpace(vatTextBox.Text)) errors.AppendLine("Заполните поле \"НДС\".");
            }
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                double s1 = Convert.ToDouble(amountRentTextBox.Text);
            }
            catch
            {
                MessageBox.Show("В поле \"Сумма аренды\" можно вводить только числа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                 
            try
            {
                double s3 = Convert.ToDouble(amountPaymentTextBox.Text);
            }
            catch
            {
                MessageBox.Show("В поле \"Сумма оплаты\" можно вводить только числа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (vatTextBox.Enabled == true)
            {
                try
                {
                    double s2 = Convert.ToDouble(vatTextBox.Text);
                    
                }
                catch
                {
                    MessageBox.Show("В поле \"НДС\" можно вводить только числа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            int SelectedRenters = 0;
            int SelectedMonth = 0;
            int SelectedStatus = 0;
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
            foreach (var item in DebitingStatus)
            {
                if(item.Value == areaTypeComboBox.Text)
                {
                    SelectedStatus = item.Key;
                }
            }

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();

            SQLiteCommand checkDublicate = new SQLiteCommand("SELECT ID_Renters, Contract, ID_Month, Apartament_Status FROM Apartaments" +
                " WHERE ID_Renters =@ID_Renters AND Contract = @Contract AND ID_Month = @ID_Month AND Apartament_Status = @Apartament_Status", conn);
            checkDublicate.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
            checkDublicate.Parameters.AddWithValue("@Contract", contractTextBox.Text);
            checkDublicate.Parameters.AddWithValue("@ID_Month", SelectedMonth);
            checkDublicate.Parameters.AddWithValue("@Apartament_Status", SelectedStatus);   
            try
            {
                SQLiteDataReader readerDublicates = checkDublicate.ExecuteReader();
                if(readerDublicates.HasRows)
                {
                    MessageBox.Show("Данная запись уже существует в базе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    readerDublicates.Close();
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           SQLiteCommand InsertInApartament = new SQLiteCommand("INSERT INTO [Apartaments] (ID_Renters, Contract, ID_Month, Amount_Rent, " +
                "VAT, Date_Payment, Apartament_Status, Note, Amount_Payment) VALUES (@ID_Renters, @Contract, @ID_Month, @Amount_Rent, " +
                "@VAT, @Date_Payment, @Apartament_Status, @Note, @Amount_Payment)", conn);
            InsertInApartament.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
            InsertInApartament.Parameters.AddWithValue("@Contract", contractTextBox.Text);
            InsertInApartament.Parameters.AddWithValue("@ID_Month", SelectedMonth);
            InsertInApartament.Parameters.AddWithValue("@Amount_Rent", amountRentTextBox.Text);
            if(vatTextBox.Enabled == true)
            {
                InsertInApartament.Parameters.AddWithValue("@VAT", vatTextBox.Text);
            }
            else InsertInApartament.Parameters.AddWithValue("@VAT", 0);
            InsertInApartament.Parameters.AddWithValue("@Date_Payment", datePicker.Value);
            InsertInApartament.Parameters.AddWithValue("@Apartament_Status", SelectedStatus);

            if (noteTextBox.Text == "")
                InsertInApartament.Parameters.AddWithValue("@Note", "Отсутствует");
            else 
                InsertInApartament.Parameters.AddWithValue("@Note", noteTextBox.Text);
            InsertInApartament.Parameters.AddWithValue("@Amount_Payment", amountPaymentTextBox.Text);
            try
            {
                InsertInApartament.ExecuteNonQuery();
                MessageBox.Show("Запись успешно добавлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SQLiteCommand loadApartaments = new SQLiteCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Months, Amount_Rent, Amount_Payment, VAT, ApartamentStatus.Name ApartamentStatus, " +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months " +
                " LEFT JOIN ApartamentStatus on Apartaments.Apartament_Status = ApartamentStatus.ID_Apartament_Status", conn);
                SQLiteDataReader readerApartaments = loadApartaments.ExecuteReader();
                List<string[]> dataApartaments = new List<string[]>();

                int countApartaments = 1;
                while (readerApartaments.Read())
                {

                    dataApartaments.Add(new string[11]);
                    dataApartaments[dataApartaments.Count - 1][0] = readerApartaments["ID_Apartament"].ToString();
                    dataApartaments[dataApartaments.Count - 1][1] = countApartaments.ToString();
                    dataApartaments[dataApartaments.Count - 1][2] = readerApartaments["Renters"].ToString();
                    dataApartaments[dataApartaments.Count - 1][3] = readerApartaments["Contract"].ToString();
                    dataApartaments[dataApartaments.Count - 1][4] = readerApartaments["Months"].ToString();
                    dataApartaments[dataApartaments.Count - 1][5] = readerApartaments["Amount_Rent"].ToString();
                    dataApartaments[dataApartaments.Count - 1][6] = readerApartaments["Amount_Payment"].ToString();
                    dataApartaments[dataApartaments.Count - 1][7] = readerApartaments["VAT"].ToString();
                    dataApartaments[dataApartaments.Count - 1][8] = readerApartaments["ApartamentStatus"].ToString();
                    dataApartaments[dataApartaments.Count - 1][9] = readerApartaments["Date_Payment"].ToString();///
                    dataApartaments[dataApartaments.Count - 1][10] = readerApartaments["Note"].ToString();
                    countApartaments++;
                }
                main.dataGridFlats.Rows.Clear();
                foreach (string[] s in dataApartaments)
                    main.dataGridFlats.Rows.Add(s);

                readerApartaments.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                conn.Close();
            }
        }

        private void contractTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
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
                addFlatsButton_Click(sender, e);
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

        private void amountPaymentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
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
                addFlatsButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                if (vatTextBox.Enabled != false)
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
                addFlatsButton_Click(sender, e);
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
                addFlatsButton_Click(sender, e);
            }
        }

        private void monthComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void datePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void areaTypeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                addFlatsButton_Click(sender, e);
            }
        }

        private void areaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (areaTypeComboBox.SelectedItem.ToString() == "Нежилое")
            {
                vatTextBox.Enabled = true;
            }
            else
            {
                vatTextBox.Enabled = false;
            }
        }
    }
}
