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
    public partial class MainForm : Form
    {
        string firstQueryFlats = "SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT," +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string secondQueryFlats = "SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT," +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string helpQueryFlats = "";

        string firstQueryResultFlats = "SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string secondQueryResultFlats = "SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string helpQueryResultFlats = "";

        string firstQueryOffices = "SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
              " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
              " LEFT JOIN Months on Offices.ID_Month = Months.ID_Month WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string secondQueryOffices = "SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
              " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
              " LEFT JOIN Months on Offices.ID_Month = Months.ID_Month WHERE ((Date_Payment BETWEEN '{0:yyyyMMdd}' AND '{1:yyyyMMdd}'))";
        string helpQueryOffices = "";

        string firstQueryResultOffices = "";
        string secondQueryResultOffices = "";
        string helpQueryResultOffices = "";

        string firstQueryResultRenters = "";
        string secondQueryResultRenters = "";
        string helpQueryResultRenters = "";

        string firstQueryResult = "";
        string secondQueryResult = "";
        string helpQueryResult = "";

        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingAreaType = new Dictionary<int, string>();
        public string ConnString = ConnStringForm.connection;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
            {
                x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
            });

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            SqlCommand loadMonth = new SqlCommand("SELECT ID_Month, Name FROM Months", conn);
            SqlDataReader readerMonth = loadMonth.ExecuteReader();
            monthComboBox.Items.Add("Все");
            while (readerMonth.Read())
            {
                DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Month"]), Convert.ToString(readerMonth["Name"]));
                monthComboBox.Items.Add(readerMonth["Name"]);
            }
            readerMonth.Close();

            SqlCommand loadAreaType = new SqlCommand("SELECT ID_Apartment_Status, Name FROM ApartmentStatus", conn);
            SqlDataReader readerAreaType = loadAreaType.ExecuteReader();
            areaTypeComboBox.Items.Add("Любое");
            while (readerAreaType.Read())
            {
                DebitingAreaType.Add(Convert.ToInt32(readerAreaType["ID_Apartment_Status"]), Convert.ToString(readerAreaType["Name"]));
                areaTypeComboBox.Items.Add(readerAreaType["Name"]);
            }
            readerAreaType.Close();

            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();
            rentersComboBox.Items.Add("Все");
            while (readerRenters.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRenters["ID_Renters"]), Convert.ToString(readerRenters["Name"]));
                rentersComboBox.Items.Add(readerRenters["Name"]);
            }

            //Load Table Renters
            #region
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
            foreach (string[] s in dataRenters)
            {
                dataGridRenters.Rows.Add(s);
            }

            readerRenters.Close();
            #endregion
            //Load Table Offices
            #region
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
            foreach (string[] s in dataOffices)
            {
                dataGridOffices.Rows.Add(s);
            }

            readerOffices.Close();
            #endregion
            //Load Table Apartaments
            #region
            SqlCommand loadApartaments = new SqlCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT," +
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
            foreach (string[] s in dataApartaments)
            {
                dataGridFlats.Rows.Add(s);
            }

            readerApartaments.Close();
            #endregion

            conn.Close();

            monthComboBox.SelectedIndex = 0;
            rentersComboBox.SelectedIndex = 0;
            areaTypeComboBox.SelectedIndex = 0;
        }

        private void addRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRentersForm addRentersForm = new AddRentersForm();
            addRentersForm.Owner = this;
            addRentersForm.ShowDialog();
        }

        private void updateRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridRenters.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Арендаторы\", которую хотите измениить.", "Ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridRenters.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите измениить.", "Ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateRentersForm updateRentersForm = new UpdateRentersForm();
            updateRentersForm.Owner = this;
            updateRentersForm.ShowDialog();
        }

        private void deleteRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridRenters.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Арендаторы\", которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridRenters.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();
                SqlCommand deleteRenters = new SqlCommand("DELETE FROM [Renters] WHERE [ID_Renters] = @ID_Renters", conn);
                deleteRenters.Parameters.AddWithValue("@ID_Renters", dataGridRenters.CurrentRow.Cells[0].Value);
                try
                {
                    deleteRenters.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    dataGridRenters.Rows.Clear();
                    foreach (string[] s in data)
                        dataGridRenters.Rows.Add(s);

                    readerRenters.Close();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void addOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOfficesForm addOfficesForm = new AddOfficesForm();
            addOfficesForm.Owner = this;
            addOfficesForm.ShowDialog();
        }

        private void updateOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridOffices.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Офис\", которую хотите изменить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridOffices.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите изменить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateOfficesForm updateOfficesForm = new UpdateOfficesForm();
            updateOfficesForm.Owner = this;
            updateOfficesForm.ShowDialog();
        }

        private void deleteOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridOffices.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Офис\", которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridOffices.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();
                SqlCommand deleteOffices = new SqlCommand("DELETE FROM [Offices] WHERE [ID_Office] = @ID_Office", conn);
                deleteOffices.Parameters.AddWithValue("@ID_Office", dataGridOffices.CurrentRow.Cells[0].Value);
                try
                {
                    deleteOffices.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    dataGridOffices.Rows.Clear();
                    foreach (string[] s in dataOffices)
                        dataGridOffices.Rows.Add(s);

                    readerOffices.Close();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void addFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlatsForm addFlatsForm = new AddFlatsForm();
            addFlatsForm.Owner = this;
            addFlatsForm.ShowDialog();
        }

        private void updateFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridFlats.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Квартиры\", которую хотите изменить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridFlats.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите изменить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateFlatsForm updateFlatsForm = new UpdateFlatsForm();
            updateFlatsForm.Owner = this;
            updateFlatsForm.ShowDialog();
        }

        private void deleteFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridFlats.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Квартиры\", которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridFlats.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотитет удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open();
                SqlCommand deleteFlats = new SqlCommand("DELETE FROM [Apartaments] WHERE [ID_Apartament] = @ID_Apartament", conn);
                deleteFlats.Parameters.AddWithValue("@ID_Apartament", dataGridFlats.CurrentRow.Cells[0].Value);
                try
                {
                    deleteFlats.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlCommand loadApartaments = new SqlCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT," +
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
                    dataGridFlats.Rows.Clear();
                    foreach (string[] s in dataApartaments)
                        dataGridFlats.Rows.Add(s);

                    readerApartaments.Close();
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

        private void connStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnStringForm connStringForm = new ConnStringForm();
            connStringForm.ShowDialog();
        }

        private void rentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Visible = true;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
        }

        private void officesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = true;
        }

        private void flatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = true;
            dataGridOffices.Visible = false;

        }

        private void resultFlatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridResultFlats.Visible = true;
            dataGridResultFlats.Rows.Clear();
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadResultFlats = new SqlCommand("SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name", conn);
            SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (readerResultFlats.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

            }
            foreach (string[] s in data)
            {
                dataGridResultFlats.Rows.Add(s);
            }
        }

        //тут фильтрация начинается
        #region
        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text, 
                    amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(), 
                    areaTypeComboBox.SelectedValue.ToString()), conn);
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
                foreach (string[] s in dataApartaments)
                {
                    dataGridFlats.Rows.Add(s);
                }

                readerApartaments.Close();
            }
            if (dataGridOffices.Visible == true)
            {
                SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                    amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                foreach (string[] s in dataOffices)
                {
                    dataGridOffices.Rows.Add(s);
                }

                readerOffices.Close();
            }
            if (dataGridResultFlats.Visible == true)
            {
                SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                    amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                    areaTypeComboBox.SelectedValue.ToString()), conn);
                SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                List<string[]> data = new List<string[]>();

                while (readerResultFlats.Read())
                {
                    data.Add(new string[3]);
                    data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                    data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                    data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                }
                foreach (string[] s in data)
                {
                    dataGridResultFlats.Rows.Add(s);
                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void datePickerFinish_ValueChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                    amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                    areaTypeComboBox.SelectedValue.ToString()), conn);
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
                foreach (string[] s in dataApartaments)
                {
                    dataGridFlats.Rows.Add(s);
                }

                readerApartaments.Close();
            }
            if (dataGridOffices.Visible == true)
            {
                SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                    amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                foreach (string[] s in dataOffices)
                {
                    dataGridOffices.Rows.Add(s);
                }

                readerOffices.Close();
            }
            if (dataGridResultFlats.Visible == true)
            {
                SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                    amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                    areaTypeComboBox.SelectedValue.ToString()), conn);
                SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                List<string[]> data = new List<string[]>();

                while (readerResultFlats.Read())
                {
                    data.Add(new string[3]);
                    data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                    data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                    data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                }
                foreach (string[] s in data)
                {
                    dataGridResultFlats.Rows.Add(s);
                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void amountRentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (amountRentTextBoxStart.Text == "" || string.IsNullOrWhiteSpace(amountRentTextBoxStart.Text) || amountRentTextBoxStart.Text.Any(char.IsLetter) || amountRentTextBoxStart.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountRentTextBoxStart.Text = "1";
            }
            else if (amountRentTextBoxStart.Text == "" || string.IsNullOrWhiteSpace(amountRentTextBoxStart.Text) || amountRentTextBoxStart.Text.Any(char.IsLetter) || amountRentTextBoxStart.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountRentTextBoxStart.Text = "100000";
            }

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridOffices.Visible == true)
            {
                try
                {
                    if (helpQueryOffices.Length < firstQueryOffices.Length)
                    {
                        secondQueryOffices = firstQueryOffices.Substring(0, firstQueryOffices.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryOffices = secondQueryOffices;
                    }
                    else
                    {
                        secondQueryOffices = helpQueryOffices.Substring(0, helpQueryOffices.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryOffices = secondQueryOffices;
                    }

                    SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataOffices)
                    {
                        dataGridOffices.Rows.Add(s);
                    }

                    readerOffices.Close();
                }
                catch
                {

                }
                
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void amountRentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (amountRentTextBoxFinish.Text == "" || string.IsNullOrWhiteSpace(amountRentTextBoxFinish.Text) || amountRentTextBoxFinish.Text.Any(char.IsLetter) || amountRentTextBoxFinish.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountRentTextBoxFinish.Text = "1";
            }
            else if (amountRentTextBoxFinish.Text == "" || string.IsNullOrWhiteSpace(amountRentTextBoxFinish.Text) || amountRentTextBoxFinish.Text.Any(char.IsLetter) || amountRentTextBoxFinish.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountRentTextBoxFinish.Text = "100000";
            }

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridOffices.Visible == true)
            {
                try
                {
                    if (helpQueryOffices.Length < firstQueryOffices.Length)
                    {
                        secondQueryOffices = firstQueryOffices.Substring(0, firstQueryOffices.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryOffices = secondQueryOffices;
                    }
                    else
                    {
                        secondQueryOffices = helpQueryOffices.Substring(0, helpQueryOffices.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryOffices = secondQueryOffices;
                    }

                    SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataOffices)
                    {
                        dataGridOffices.Rows.Add(s);
                    }

                    readerOffices.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (Amount_Rent BETWEEN `{2}` AND `{3}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void amountPaymentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            if (amountPaymentTextBoxStart.Text == "" || string.IsNullOrWhiteSpace(amountPaymentTextBoxStart.Text) || amountPaymentTextBoxStart.Text.Any(char.IsLetter) || amountPaymentTextBoxStart.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountPaymentTextBoxStart.Text = "1";
            }
            else if (amountPaymentTextBoxStart.Text == "" || string.IsNullOrWhiteSpace(amountPaymentTextBoxStart.Text) || amountPaymentTextBoxStart.Text.Any(char.IsLetter) || amountPaymentTextBoxStart.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountPaymentTextBoxStart.Text = "100000";
            }

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void amountPaymentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            if (amountPaymentTextBoxFinish.Text == "" || string.IsNullOrWhiteSpace(amountPaymentTextBoxFinish.Text) || amountPaymentTextBoxFinish.Text.Any(char.IsLetter) || amountPaymentTextBoxFinish.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountPaymentTextBoxFinish.Text = "1";
            }
            else if (amountPaymentTextBoxFinish.Text == "" || string.IsNullOrWhiteSpace(amountPaymentTextBoxFinish.Text) || amountPaymentTextBoxFinish.Text.Any(char.IsLetter) || amountPaymentTextBoxFinish.Text.Intersect("!@#$%^&*()_-+=|:;\"'`~/?.>,<[]{\\}№ ").Count() != 0)
            {
                amountPaymentTextBoxFinish.Text = "100000";
            }

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (Amount_Payment BETWEEN `{4}` AND `{5}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (Month = `{6}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (Month = `{6}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridOffices.Visible == true)
            {
                try
                {
                    if (helpQueryOffices.Length < firstQueryOffices.Length)
                    {
                        secondQueryOffices = firstQueryOffices.Substring(0, firstQueryOffices.Length - 1) + " AND (Month = `{4}`))";
                        helpQueryOffices = secondQueryOffices;
                    }
                    else
                    {
                        secondQueryOffices = helpQueryOffices.Substring(0, helpQueryOffices.Length - 1) + " AND (Month = `{4}`))";
                        firstQueryOffices = secondQueryOffices;
                    }

                    SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataOffices)
                    {
                        dataGridOffices.Rows.Add(s);
                    }

                    readerOffices.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (Month = `{6}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (Month = `{6}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void rentersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (ID_Renters = `{7}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (ID_Renters = `{7}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridOffices.Visible == true)
            {
                try
                {
                    if (helpQueryOffices.Length < firstQueryOffices.Length)
                    {
                        secondQueryOffices = firstQueryOffices.Substring(0, firstQueryOffices.Length - 1) + " AND (ID_Renters = `{5}`))";
                        helpQueryOffices = secondQueryOffices;
                    }
                    else
                    {
                        secondQueryOffices = helpQueryOffices.Substring(0, helpQueryOffices.Length - 1) + " AND (ID_Renters = `{5}`))";
                        firstQueryOffices = secondQueryOffices;
                    }

                    SqlCommand loadOffices = new SqlCommand(string.Format(secondQueryOffices, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataOffices)
                    {
                        dataGridOffices.Rows.Add(s);
                    }

                    readerOffices.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }

        private void areaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    if (helpQueryFlats.Length < firstQueryFlats.Length)
                    {
                        secondQueryFlats = firstQueryFlats.Substring(0, firstQueryFlats.Length - 1) + " AND (ID_Apartment_Status = `{8}`))";
                        helpQueryFlats = secondQueryFlats;
                    }
                    else
                    {
                        secondQueryFlats = helpQueryFlats.Substring(0, helpQueryFlats.Length - 1) + " AND (ID_Apartment_Status = `{8}`))";
                        firstQueryFlats = secondQueryFlats;
                    }

                    SqlCommand loadApartaments = new SqlCommand(string.Format(secondQueryFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(), rentersComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
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
                    foreach (string[] s in dataApartaments)
                    {
                        dataGridFlats.Rows.Add(s);
                    }

                    readerApartaments.Close();
                }
                catch
                {

                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                try
                {
                    if (helpQueryResultFlats.Length < firstQueryResultFlats.Length)
                    {
                        secondQueryResultFlats = firstQueryResultFlats.Substring(0, firstQueryResultFlats.Length - 1) + " AND (ID_Apartment_Status = `{7}`))";
                        helpQueryResultFlats = secondQueryResultFlats;
                    }
                    else
                    {
                        secondQueryResultFlats = helpQueryResultFlats.Substring(0, helpQueryResultFlats.Length - 1) + " AND (ID_Apartment_Status = `{7}`))";
                        firstQueryResultFlats = secondQueryResultFlats;
                    }

                    SqlCommand loadResultFlats = new SqlCommand(string.Format(secondQueryResultFlats, datePickerStart.Value, datePickerFinish.Value, amountRentTextBoxStart.Text,
                        amountRentTextBoxFinish.Text, amountPaymentTextBoxStart.Text, amountPaymentTextBoxFinish.Text, monthComboBox.SelectedValue.ToString(),
                        areaTypeComboBox.SelectedValue.ToString()), conn);
                    SqlDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                }
                catch
                {

                }
            }
            if (dataGridResultOffices.Visible == true)
            {

            }
            if (dataGridResultRenters.Visible == true)
            {

            }
            if (dataGridResults.Visible == true)
            {

            }

            conn.Close();
        }
        #endregion
    }
}
    

