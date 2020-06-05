﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class UpdateFlatsForm : Form
    {
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingStatus = new Dictionary<int, string>();
        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        public string ConnString = ConnStringForm.connection;
        public UpdateFlatsForm()
        {
            InitializeComponent();
        }

        private void UpdateFlatsForm_Load(object sender, EventArgs e)
        {
            ActiveControl = contractTextBox;
            contractTextBox.SelectionStart = 0;

            MainForm main = this.Owner as MainForm;
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenter = loadRenters.ExecuteReader();
            while (readerRenter.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRenter["ID_Renters"]), Convert.ToString(readerRenter["Name"]));
                rentersComboBox.Items.Add(readerRenter["Name"]);
            }
            readerRenter.Close();

            SqlCommand loadMonth = new SqlCommand("SELECT ID_Month, Name FROM Months", conn);
            SqlDataReader readerMonth = loadMonth.ExecuteReader();
            while (readerMonth.Read())
            {
                DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Month"]), Convert.ToString(readerMonth["Name"]));
                monthComboBox.Items.Add(readerMonth["Name"]);
            }
            readerMonth.Close();

            SqlCommand loadStatus = new SqlCommand("SELECT ID_Apartment_Status, Name FROM ApartmentStatus", conn);
            SqlDataReader readerStatus = loadStatus.ExecuteReader();
            while (readerStatus.Read())
            {
                DebitingStatus.Add(Convert.ToInt32(readerStatus["ID_Apartment_Status"]), Convert.ToString(readerStatus["Name"]));
                areaTypeComboBox.Items.Add(readerStatus["Name"]);
            }
            readerStatus.Close();

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
                if (item.Value == areaTypeComboBox.Text)
                {
                    SelectedStatus = item.Key;
                }
            }

            SqlCommand selectedItems = new SqlCommand("SELECT ID_Renters, Contract, ID_Month, Amount_Rent, VAT, Date_Payment, " +
                "Apartament_Status, Note, Amount_Payment FROM Apartaments WHERE ID_Apartament = @ID_Apartament", conn);
            selectedItems.Parameters.AddWithValue("@ID_Apartament", main.dataGridFlats.CurrentRow.Cells[0].Value);
            SqlDataReader readerItems = selectedItems.ExecuteReader();
            while(readerItems.Read())
            {
                areaTypeComboBox.SelectedItem = DebitingStatus[Convert.ToInt32(readerItems["Apartament_Status"])];
                if (areaTypeComboBox.SelectedItem.ToString() == "Жилое")
                {
                    vatTextBox.Enabled = false;
                }
                else
                {
                    vatTextBox.Enabled = true;
                }
                rentersComboBox.SelectedItem = DebitingRenters[Convert.ToInt32(readerItems["ID_Renters"])];
                monthComboBox.SelectedItem = DebitingMonth[Convert.ToInt32(readerItems["ID_Month"])];
                contractTextBox.Text = readerItems["Contract"].ToString();
                amountRentTextBox.Text = readerItems["Amount_Rent"].ToString();
                amountPaymentTextBox.Text = readerItems["Amount_Payment"].ToString();
                noteTextBox.Text = readerItems["Note"].ToString();
                vatTextBox.Text = readerItems["VAT"].ToString();
                datePicker.Value = Convert.ToDateTime(readerItems["Date_Payment"]);


            }
            readerItems.Close();
            conn.Close();

            contractTextBox.SelectionStart = 0;
        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(areaTypeComboBox.Text)) errors.AppendLine("Выберите тип помещения.");
            if (string.IsNullOrWhiteSpace(rentersComboBox.Text)) errors.AppendLine("Выберите арендатора.");
            if (string.IsNullOrWhiteSpace(monthComboBox.Text)) errors.AppendLine("Выберите месяц.");
            if (string.IsNullOrWhiteSpace(contractTextBox.Text)) errors.AppendLine("Заполните поле \"Договор\".");
            if (string.IsNullOrWhiteSpace(amountRentTextBox.Text)) errors.AppendLine("Заполните поле \"Сумма аренды\".");
            if (string.IsNullOrWhiteSpace(amountPaymentTextBox.Text)) errors.AppendLine("Заполните поле \"Сумма оплаты\".");
            if (vatTextBox.Enabled == true)
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

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand updateApartament = new SqlCommand("UPDATE Apartaments SET  [ID_Renters] = @ID_Renters, [Contract] =@Contract, [ID_Month] =@ID_Month," +
                " [Amount_Rent]=@Amount_Rent, [VAT] =@VAT, [Date_Payment] =@Date_Payment, [Apartament_Status]= @Apartament_Status, [Note] =@Note," +
                " [Amount_Payment]= @Amount_Payment WHERE [ID_Apartament] = @ID_Apartament", conn);
            updateApartament.Parameters.AddWithValue("@ID_Renters", SelectedRenters);
            updateApartament.Parameters.AddWithValue("@Contract", contractTextBox.Text);
            updateApartament.Parameters.AddWithValue("@ID_Month", SelectedMonth);
            updateApartament.Parameters.AddWithValue("@Amount_Rent", amountRentTextBox.Text);
            if (vatTextBox.Enabled == true)
            {
                updateApartament.Parameters.AddWithValue("@VAT", vatTextBox.Text);
            }
            else updateApartament.Parameters.AddWithValue("@VAT", 0);
            updateApartament.Parameters.AddWithValue("@Date_Payment", datePicker.Value);
            updateApartament.Parameters.AddWithValue("@Apartament_Status", SelectedStatus);
            updateApartament.Parameters.AddWithValue("@Note", noteTextBox.Text);
            updateApartament.Parameters.AddWithValue("@Amount_Payment", amountPaymentTextBox.Text);
            updateApartament.Parameters.AddWithValue("@ID_Apartament", main.dataGridFlats.CurrentRow.Cells[0].Value);
            try
            {
                updateApartament.ExecuteNonQuery();
                MessageBox.Show("Запись успешно изменена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand loadApartaments = new SqlCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month," +
                    " Amount_Rent, Amount_Payment, VAT," +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month", conn);
                SqlDataReader readerApartaments = loadApartaments.ExecuteReader();
                List<string[]> dataApartaments = new List<string[]>();

                int countApartaments = 1;
                while (readerApartaments.Read())
                {

                    dataApartaments.Add(new string[10]);
                    dataApartaments[dataApartaments.Count - 1][0] = readerApartaments["ID_Apartament"].ToString();
                    dataApartaments[dataApartaments.Count - 1][1] = countApartaments.ToString();
                    dataApartaments[dataApartaments.Count - 1][2] = readerApartaments["Renters"].ToString();
                    dataApartaments[dataApartaments.Count - 1][3] = readerApartaments["Contract"].ToString();
                    dataApartaments[dataApartaments.Count - 1][4] = readerApartaments["Month"].ToString();
                    dataApartaments[dataApartaments.Count - 1][5] = readerApartaments["Amount_Rent"].ToString();
                    dataApartaments[dataApartaments.Count - 1][6] = readerApartaments["Amount_Payment"].ToString();
                    dataApartaments[dataApartaments.Count - 1][7] = readerApartaments["VAT"].ToString();
                    dataApartaments[dataApartaments.Count - 1][8] = readerApartaments["Date_Payment"].ToString();
                    dataApartaments[dataApartaments.Count - 1][9] = readerApartaments["Note"].ToString();
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

        private void amountPaymentTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void datePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
            }
        }

        private void areaTypeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                updateRecordButton_Click(sender, e);
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
