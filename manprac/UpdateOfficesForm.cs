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
            MainForm main = this.Owner as MainForm;

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
            string selectedItem = "sss";
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
            }
            readerSelectedItems.Close();
            conn.Close();
            contractTextBox.SelectionStart = 0;
        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            if (contractTextBox.Text == "")
            {
                MessageBox.Show("Есть пустые поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Запись была успешно обновлена", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
