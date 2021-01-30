using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
//using Excel = Microsoft.Office.Interop.Excel;

namespace manprac
{
    public partial class MainForm : Form
    {

        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingAreaType = new Dictionary<int, string>();
        private String dbFileName = "RentDB";
        private string ConnString = "Data Source = RentDB; Version=3";
        #region переменные для загрузки данных в таблицы
        string resultFlatsLoadQueryConst = "SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name";

        string resultFlatsSumQueryConst = "SELECT SUM(Amount_Rent) SumRent, SUM(Amount_Payment) SumPayment FROM Apartaments";
        string resultFlatsSumQuery = "SELECT SUM(Amount_Rent) SumRent, SUM(Amount_Payment) SumPayment FROM Apartaments";
        string resultFlatsLoadQuery = "SELECT Months.ID_Months, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months WHERE Apartament_Status = 2 " +
            " GROUP BY Months.ID_Months, Months.Name";
        string resultOfficesLoadQueryConst = "Select Months.ID_Months, Months.Name Month, SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference" +
                " FROM Offices LEFT JOIN Months on Offices.ID_Month = Months.ID_Month GROUP BY Months.ID_Month, Months.Name";
        string resultOfficesLoadQuery = "Select Months.ID_Months, Months.Name Month, SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent - VAT) Difference " +
                " FROM Offices LEFT JOIN Months on Offices.ID_Month = Months.ID_Months GROUP BY Months.ID_Months, Months.Name";
        string resultOfficesSumQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference FROM Offices";
        string resultOfficesSumQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference FROM Offices";

        string resultFlatsNLoadQueryConst = "SELECT Months.ID_Months, Months.Name Month, sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments " +
              " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month WHERE Apartament_Status = 2 Group By Months.ID_Month, Months.Name";
        string resultFlatsNLoadQuery = "SELECT Months.ID_Months, Months.Name Month, sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments " +
              " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months WHERE Apartament_Status = 1 Group By Months.ID_Months, Months.Name";
        string resultFlatsNSumQueryConst = "SELECT sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments Where Apartament_Status = 1";
        string resultFlatsNSumQuery = "SELECT sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments Where Apartament_Status = 1";

        string resultAllLoadQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Offices";
        string resultAllLoadQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Offices";
        string resultAllSumQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(Amount_Payment) Amount_Payment FROM Apartaments";
        string resultAllSumQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(Amount_Payment) Amount_Payment FROM Apartaments";
        #endregion


        #region методы загрузок таблиц
        public void RentersLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            try
            {
                conn.Open();
                List<string[]> dataRenters = new List<string[]>();
                SQLiteCommand loadRenters = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                SQLiteDataReader readerRenters = loadRenters.ExecuteReader();

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
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при загрузке таблицы. Проверьте существует ли база данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void OfficesLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);

            try
            {
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
                foreach (string[] s in dataOffices)
                {
                    dataGridOffices.Rows.Add(s);
                }

                readerOffices.Close();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при загрузке таблицы. Проверьте существует ли база данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                conn.Close();
            }
        }

        public void FlatsLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            try
            {
                conn.Open();

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
                    if (readerApartaments["Date_Payment"].ToString() != "")
                    {
                        dataApartaments[dataApartaments.Count - 1][9] = Convert.ToDateTime(readerApartaments["Date_Payment"]).ToShortDateString();
                    }
                    dataApartaments[dataApartaments.Count - 1][10] = readerApartaments["Note"].ToString();
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
                MessageBox.Show("Произошла ошибка при загрузке таблицы. Проверьте существует ли база данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ResultFlatsLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();

            SQLiteCommand loadResultFlats = new SQLiteCommand(resultFlatsLoadQuery, conn);
            SQLiteDataReader readerResultFlats = loadResultFlats.ExecuteReader();
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
            readerResultFlats.Close();
            if (dataGridResultFlats.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют записи для составления сводки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand loadResultTotal = new SQLiteCommand(resultFlatsSumQuery, conn);
            SQLiteDataReader readerResultTotal = loadResultTotal.ExecuteReader();
            while (readerResultTotal.Read())
            {
                dataGridResultFlats.Rows.Add("Всего", readerResultTotal["SumRent"].ToString(), readerResultTotal["SumPayment"].ToString());
            }
            readerResultTotal.Close();
            conn.Close();
        }

        public void ResultOfficesLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            SQLiteCommand loadResultOffices = new SQLiteCommand(resultOfficesLoadQuery, conn);
            SQLiteDataReader readerResultOffices = loadResultOffices.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (readerResultOffices.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = readerResultOffices["Month"].ToString();
                data[data.Count - 1][1] = readerResultOffices["Amount_Rent"].ToString();
                data[data.Count - 1][2] = readerResultOffices["VAT"].ToString();
                data[data.Count - 1][3] = readerResultOffices["Difference"].ToString();
            }
            readerResultOffices.Close();
            foreach (string[] s in data)
                dataGridResultOffices.Rows.Add(s);

            if (dataGridResultOffices.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют записи для составления сводки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand loadResultSummary = new SQLiteCommand(resultOfficesSumQuery, conn);
            SQLiteDataReader readerResultSummary = loadResultSummary.ExecuteReader();
            while (readerResultSummary.Read())
            {
                dataGridResultOffices.Rows.Add("Всего", readerResultSummary["Amount_Rent"].ToString(), readerResultSummary["VAT"].ToString(), readerResultSummary["Difference"].ToString());
            }
            readerResultSummary.Close();
            conn.Close();
        }

        public void ResultFlatsNLoad()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            SQLiteCommand loadUninhabitedArea1 = new SQLiteCommand(resultFlatsNLoadQuery, conn);
            SQLiteDataReader readerUninhabitedArea1 = loadUninhabitedArea1.ExecuteReader();
            List<string[]> data1 = new List<string[]>();
            while (readerUninhabitedArea1.Read())
            {
                data1.Add(new string[3]);
                data1[data1.Count - 1][0] = readerUninhabitedArea1["Month"].ToString();
                data1[data1.Count - 1][1] = readerUninhabitedArea1["Amount_Payment"].ToString();
                data1[data1.Count - 1][2] = readerUninhabitedArea1["VAT"].ToString();
            }
            readerUninhabitedArea1.Close();
            foreach (string[] s in data1)
                dataGridUninhabitedArea.Rows.Add(s);

            if (dataGridUninhabitedArea.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют записи для составления сводки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteCommand loadUningabitedArea2 = new SQLiteCommand(resultFlatsNSumQuery, conn);
            SQLiteDataReader readerUninhabitedAre2 = loadUningabitedArea2.ExecuteReader();
            while (readerUninhabitedAre2.Read())
            {
                dataGridUninhabitedArea.Rows.Add("Всего", readerUninhabitedAre2["Amount_Payment"].ToString(), readerUninhabitedAre2["VAT"].ToString());
            }
            readerUninhabitedAre2.Close();
            conn.Close();
        }

        public void ResultAllLoad()
        {

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            conn.Open();
            SQLiteCommand loadCommonSummary1 = new SQLiteCommand(resultAllLoadQuery, conn);
            try
            {
                SQLiteDataReader readerCommonSummary1 = loadCommonSummary1.ExecuteReader();
                List<string[]> data1 = new List<string[]>();
                while (readerCommonSummary1.Read())
                {
                    data1.Add(new string[3]);
                    data1[data1.Count - 1][0] = "Офис";
                    data1[data1.Count - 1][1] = readerCommonSummary1["Amount_Rent"].ToString();
                    data1[data1.Count - 1][2] = readerCommonSummary1["VAT"].ToString();
                }
                foreach (string[] s in data1)
                    dataGridCommonResults.Rows.Add(s);

                for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                {
                    if (dataGridCommonResults.Rows[i].Cells[1].Value.ToString() == "")
                    {
                        MessageBox.Show("Отсутсвуют записи для составления сводки. ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridCommonResults.Rows.Clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SQLiteCommand loadCommonSummary2 = new SQLiteCommand(resultAllSumQuery, conn);
            try
            {
                SQLiteDataReader readerCommonSummary2 = loadCommonSummary2.ExecuteReader();
                List<string[]> data2 = new List<string[]>();
                while (readerCommonSummary2.Read())
                {
                    data2.Add(new string[3]);
                    data2[data2.Count - 1][0] = "Квартиры";
                    data2[data2.Count - 1][1] = readerCommonSummary2["Amount_Rent"].ToString();
                    data2[data2.Count - 1][2] = readerCommonSummary2["Amount_Payment"].ToString();
                }
                readerCommonSummary2.Close();
                foreach (string[] s in data2)
                    dataGridCommonResults.Rows.Add(s);

                for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                {
                    if (dataGridCommonResults.Rows[i].Cells[2].Value.ToString() == "")
                    {
                        MessageBox.Show("Отсутсвуют записи для составления сводки. ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridCommonResults.Rows.Clear();
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            double sumRent = 0;
            double sumVat = 0;
            try
            {
                for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                {
                    sumRent += Convert.ToDouble(dataGridCommonResults.Rows[i].Cells[1].Value);
                    sumVat += Convert.ToDouble(dataGridCommonResults.Rows[i].Cells[2].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            dataGridCommonResults.Rows.Add("Всего", sumRent, sumVat);
        }
        #endregion

        #region методы фильтраций

        public void DateFiltration()
        {
            DateTime datestart = Convert.ToDateTime(datePickerStart.Value);
            DateTime datefinish = Convert.ToDateTime(datePickerFinish.Value);
            datestart.ToString("O: {0:O}");
            datefinish.ToString("O: {0:O}");
            if (dataGridOffices.Visible == true)
            {
                try
                {
                    for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                    {


                        dataGridOffices.Rows[i].Cells[7].DataGridView.DefaultCellStyle.Format = "d";
                        DateTime dt = DateTime.Parse(dataGridOffices.Rows[i].Cells[7].Value.ToString());
                        if (!((dt.Date >= datePickerStart.Value.Date) && (dt.Date <= datePickerFinish.Value.Date)))
                        {
                            dataGridOffices.Rows[i].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (dataGridFlats.Visible == true)
            {
                try
                {
                    for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                    {
                        dataGridFlats.Rows[i].Cells[9].DataGridView.DefaultCellStyle.Format = "d";
                        DateTime dt = DateTime.Parse(dataGridFlats.Rows[i].Cells[9].Value.ToString());
                        if (!((dt.Date >= datePickerStart.Value.Date) && (dt.Date <= datePickerFinish.Value.Date)))
                        {
                            dataGridFlats.Rows[i].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region открытие подменю тулстрип при наведении
            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
            {
                x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
            });
            #endregion

            datePickerFinish.Value = DateTime.Now;
            ChangeHeight();

            #region заполнение комбобоксов
            rentersComboBox.Items.Add("Все");
            monthComboBox.Items.Add("Все");
            areaTypeComboBox.Items.Add("Все");
            #region установка изначальных значений в комбобоксы
            monthComboBox.SelectedItem = "Все";
            rentersComboBox.SelectedItem = "Все";
            areaTypeComboBox.SelectedItem = "Все";

            #endregion

            if (!File.Exists(dbFileName))
            {
                MessageBox.Show("Произошла ошибка. В программе отсутствует база данных. " +
                    "Пожалуйста перейдите в настройки и создайте базу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("SELECT ID_Months, Name FROM Months", conn);
                SQLiteDataReader readerMonth = cmd.ExecuteReader();

                while (readerMonth.Read())
                {
                    DebitingMonth.Add(Convert.ToInt32(readerMonth["ID_Months"]), Convert.ToString(readerMonth["Name"]));
                    monthComboBox.Items.Add(readerMonth["Name"]);
                }
                readerMonth.Close();

                SQLiteCommand loadAreaType = new SQLiteCommand("SELECT ID_Apartament_Status, Name FROM ApartamentStatus", conn);
                SQLiteDataReader readerAreaType = loadAreaType.ExecuteReader();
                while (readerAreaType.Read())
                {
                    DebitingAreaType.Add(Convert.ToInt32(readerAreaType["ID_Apartament_Status"]), Convert.ToString(readerAreaType["Name"]));
                    areaTypeComboBox.Items.Add(readerAreaType["Name"]);
                }
                readerAreaType.Close();

                SQLiteCommand loadRentersComboBox = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                SQLiteDataReader readerRentersComboBox = loadRentersComboBox.ExecuteReader();

                while (readerRentersComboBox.Read())
                {
                    DebitingRenters.Add(Convert.ToInt32(readerRentersComboBox["ID_Renters"]), Convert.ToString(readerRentersComboBox["Name"]));
                    rentersComboBox.Items.Add(readerRentersComboBox["Name"]);
                }
                readerAreaType.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Вероятно база данных была создана некорректно, выполните пересоздание БД. \n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion


        }

        #region переход на другие формы
        private void addRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRentersForm addRentersForm = new AddRentersForm();
            addRentersForm.Owner = this;
            addRentersForm.ShowDialog();
        }

        private void dbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }

        private void updateRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridRenters.Visible == false)
            {
                MessageBox.Show("Выберите запись в таблице \"Арендаторы\", которую хотите изменить.", "Ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridRenters.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют записи в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridRenters.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите изменить.", "Ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridRenters.Rows.Count == 0)
            {
                MessageBox.Show("Отсутствуют записи в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridRenters.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();

                SQLiteCommand readNullRenters = new SQLiteCommand("SELECT  Renters.ID_Renters AS 'FreeRenters'  FROM Renters" +
                   " LEFT JOIN Offices " +
                   " ON Renters.ID_Renters = Offices.ID_Renters  LEFT JOIN Apartaments ON Renters.ID_Renters = Apartaments.ID_Renters " +
                   "WHERE Offices.ID_Renters IS NULL AND Apartaments.ID_Renters IS NULL  AND Renters.ID_Renters = @ID_Renters", conn);
                readNullRenters.Parameters.AddWithValue("@ID_Renters", dataGridRenters.CurrentRow.Cells[0].Value);
                SQLiteDataReader reader = readNullRenters.ExecuteReader();

                if (!reader.HasRows)
                {

                    MessageBox.Show("Выбранного арендатора нельзя удалять, так как он используется в других таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                SQLiteCommand deleteRenters = new SQLiteCommand("DELETE FROM [Renters] WHERE [ID_Renters] = @ID_Renters", conn);
                deleteRenters.Parameters.AddWithValue("@ID_Renters", dataGridRenters.CurrentRow.Cells[0].Value);
                try
                {
                    deleteRenters.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SQLiteCommand loadRenters = new SQLiteCommand("SELECT ID_Renters, Name FROM Renters", conn);
                    SQLiteDataReader readerRenters = loadRenters.ExecuteReader();
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
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridOffices.Rows.Count == 0)
            {
                MessageBox.Show("В таблице отсутсвуют записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridOffices.Rows.Count == 0)
            {
                MessageBox.Show("В таблице отсутсвуют записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridOffices.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();
                SQLiteCommand deleteOffices = new SQLiteCommand("DELETE FROM [Offices] WHERE [ID_Office] = @ID_Office", conn);
                deleteOffices.Parameters.AddWithValue("@ID_Office", dataGridOffices.CurrentRow.Cells[0].Value);
                try
                {
                    deleteOffices.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SQLiteCommand loadOffices = new SQLiteCommand("SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
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
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridFlats.Rows.Count == 0)
            {
                MessageBox.Show("В таблице отсутсвуют записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridFlats.Rows.Count == 0)
            {
                MessageBox.Show("В таблице отсутсвуют записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridFlats.CurrentCell.Selected == false)
            {
                MessageBox.Show("Выберите запись в таблице, которую хотите удалить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();
                SQLiteCommand deleteFlats = new SQLiteCommand("DELETE FROM [Apartaments] WHERE [ID_Apartament] = @ID_Apartament", conn);
                deleteFlats.Parameters.AddWithValue("@ID_Apartament", dataGridFlats.CurrentRow.Cells[0].Value);
                try
                {
                    deleteFlats.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SQLiteCommand loadApartaments = new SQLiteCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT," +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months", conn);
                    SQLiteDataReader readerApartaments = loadApartaments.ExecuteReader();
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
        #endregion

        #region отображение таблиц по нажатию на пункт в меню
        private void rentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Rows.Clear();
            RentersLoad();

            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = true;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            datePickerStart.Enabled = false;
            datePickerFinish.Enabled = false;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            amountRentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            monthComboBox.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;
        }

        private void officesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;
            dataGridOffices.Rows.Clear();
            OfficesLoad();

            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = true;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultRenters.Visible = false;
            dataGridResultOffices.Visible = false;

            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;

            amountVAT_TextBoxStart.Enabled = true;
            amountVAT_TextBoxFinish.Enabled = true;

            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountRentTextBoxFinish.Enabled = true;
            amountRentTextBoxStart.Enabled = true;
            monthComboBox.Enabled = true;
            rentersComboBox.Enabled = true;
            areaTypeComboBox.Enabled = false;
        }

        private void flatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;
            dataGridFlats.Rows.Clear();
            FlatsLoad();
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = true;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;


            amountVAT_TextBoxStart.Enabled = true;
            amountVAT_TextBoxFinish.Enabled = true;
            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountPaymentTextBoxStart.Enabled = true;
            amountPaymentTextBoxFinish.Enabled = true;
            amountRentTextBoxFinish.Enabled = true;
            amountRentTextBoxStart.Enabled = true;
            monthComboBox.Enabled = true;
            rentersComboBox.Enabled = true;
            areaTypeComboBox.Enabled = true;

        }

        private void resultFlatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;

            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = true;
            dataGridResultOffices.Visible = false;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            amountRentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;
            monthComboBox.Enabled = false;

            dataGridResultFlats.Rows.Clear();
            ResultFlatsLoad();
        }
        private void resultOfficesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;


            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = true;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountRentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;

            dataGridResultOffices.Rows.Clear();
            ResultOfficesLoad();
        }

        private void resultAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = true;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            amountRentTextBoxFinish.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;
            monthComboBox.Enabled = false;

            dataGridCommonResults.Rows.Clear();
            ResultAllLoad();
        }

        private void resultFlatsNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;

            dataGridUninhabitedArea.Visible = true;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            amountRentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;
            monthComboBox.Enabled = false;

            dataGridUninhabitedArea.Rows.Clear();
            ResultFlatsNLoad();
        }

        //тоже самое, что и выше через один, но два варианта удобнее имхо
        private void resultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datePickerFinish.Value = DateTime.Now;
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = true;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            amountVAT_TextBoxStart.Enabled = false;
            amountVAT_TextBoxFinish.Enabled = false;
            datePickerStart.Enabled = true;
            datePickerFinish.Enabled = true;
            amountPaymentTextBoxStart.Enabled = false;
            amountPaymentTextBoxFinish.Enabled = false;
            amountRentTextBoxStart.Enabled = false;
            amountRentTextBoxFinish.Enabled = false;
            rentersComboBox.Enabled = false;
            areaTypeComboBox.Enabled = false;
            monthComboBox.Enabled = false;

            dataGridCommonResults.Rows.Clear();
            ResultAllLoad();
        }
        #endregion

        #region фильтрация
        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridOffices.Visible == true)
            {
                Filtration();
            }

            if (dataGridFlats.Visible == true)
            {

                Filtration();
            }
        }

        private void rentersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridOffices.Visible == true)
            {
                Filtration();
            }

            if (dataGridFlats.Visible == true)
            {
                Filtration();
            }

        }

        private void areaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtration();
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            Filtration();
            DateFiltration();

        }

        private void datePickerFinish_ValueChanged(object sender, EventArgs e)
        {
            Filtration();
            DateFiltration();

        }

        private void amountRentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            Filtration();

        }

        private void amountRentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            Filtration();
        }

        private void amountPaymentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            Filtration();
        }

        private void amountPaymentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            Filtration();
        }

        #endregion

        private void yy_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region печать отчетов
        private void предпросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridCommonResults.Visible == true)
            {
                printPreviewDialog1 = new PrintPreviewDialog();
                printPreviewDialog1.Document = printCommonResult;
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
                return;
            }
            if (dataGridResultFlats.Visible == true)
            {
                printPreviewDialog1 = new PrintPreviewDialog();
                printPreviewDialog1.Document = printResultFlats;
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
                return;
            }
            if (dataGridResultOffices.Visible == true)
            {
                printPreviewDialog1 = new PrintPreviewDialog();
                printPreviewDialog1.Document = printResultOffices;
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
                return;

            }
            if (dataGridUninhabitedArea.Visible == true)
            {
                printPreviewDialog1 = new PrintPreviewDialog();
                printPreviewDialog1.Document = printUninhabitedArea;
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
                return;
            }
            else
            {
                MessageBox.Show("Выберите таблицу, с которой хотите работать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string s = "Общий свод по таблицам в период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
            Bitmap btm = new Bitmap(dataGridCommonResults.Size.Width, dataGridCommonResults.Size.Height);
            dataGridCommonResults.DrawToBitmap(btm, new Rectangle(0, 0, dataGridCommonResults.Size.Width + 10, dataGridCommonResults.Size.Height + 10));
            e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, 40);
            e.Graphics.DrawImage(btm, 20, 80);
            e.Graphics.DrawString("Дата: " + dt.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 20, 180);
            btm.Dispose();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridCommonResults.Visible == true)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.Document = printCommonResult;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printCommonResult.Print();
                }
                return;
            }
            if (dataGridResultFlats.Visible == true)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.Document = printResultFlats;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printResultFlats.Print();
                }
                return;
            }
            if (dataGridUninhabitedArea.Visible == true)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.Document = printUninhabitedArea;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printUninhabitedArea.Print();
                }
                return;
            }
            if (dataGridResultOffices.Visible == true)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.Document = printResultOffices;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printResultOffices.Print();
                }
                return;
            }
            else
            {
                MessageBox.Show("Выберите таблицу, с которой хотите работать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void printResultOffices_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string s = "Общий свод по офисам в период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
            Bitmap btm = new Bitmap(dataGridResultOffices.Size.Width, dataGridResultOffices.Size.Height);
            dataGridResultOffices.DrawToBitmap(btm, new Rectangle(0, 0, dataGridResultOffices.Size.Width + 10, dataGridResultOffices.Size.Height));
            e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, 40);
            e.Graphics.DrawImage(btm, 20, 80);
            e.Graphics.DrawString("Дата: " + dt.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 20, 120 + dataGridResultOffices.Rows.GetRowsHeight(DataGridViewElementStates.Visible));
            btm.Dispose();
        }

        private void printResultRenters_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            DateTime dt = DateTime.Now;
            string s = "Общий свод по нежилым квартирам в период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
            Bitmap btm = new Bitmap(dataGridUninhabitedArea.Size.Width, dataGridUninhabitedArea.Size.Height);
            e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 10, 40);
            dataGridUninhabitedArea.DrawToBitmap(btm, new Rectangle(0, 0, dataGridUninhabitedArea.Size.Width + 20, dataGridUninhabitedArea.Size.Height));
            e.Graphics.DrawImage(btm, 20, 80);
            e.Graphics.DrawString("Дата: " + dt.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 20, 120 + dataGridUninhabitedArea.Rows.GetRowsHeight(DataGridViewElementStates.Visible));
            btm.Dispose();
        }

        private void printResultFlats_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            DateTime dt = DateTime.Now;
            string s = "Общий свод по всем квартирам в период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
            Bitmap btm = new Bitmap(dataGridResultFlats.Size.Width, dataGridResultFlats.Size.Height);
            dataGridResultFlats.DrawToBitmap(btm, new Rectangle(0, 0, dataGridResultFlats.Size.Width + 10, dataGridResultFlats.Size.Height));
            e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, 40);
            e.Graphics.DrawImage(btm, 20, 80);
            e.Graphics.DrawString("Дата: " + dt.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 20, 120 + dataGridResultFlats.Rows.GetRowsHeight(DataGridViewElementStates.Visible));
            btm.Dispose();
        }

        private void ExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridCommonResults.Visible == true)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

                ExcelApp.Cells[1, 1] = "Общий свод по таблицам за период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
                ExcelApp.Cells[1, 2] = "";
                ExcelApp.Cells[3, 1] = "Название";
                ExcelApp.Cells[3, 2] = "Сумма аренды";
                ExcelApp.Cells[3, 3] = "Сумма оплаты";

                for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridCommonResults.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 3, j + 1] = dataGridCommonResults.Rows[i].Cells[j].Value;
                        ExcelApp.Cells[i + 3, j + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                    }
                }
                ExcelApp.Cells[7, 1] = "Дата:";
                ExcelApp.Cells[7, 2] = DateTime.Now.ToShortDateString();



                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
                return;
            }
            if (dataGridResultFlats.Visible == true)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                ExcelWorkSheet.Name = "Свод";
                ExcelApp.Cells[1, 1] = "Общий свод по всем квартирам за период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
                ExcelApp.Cells[1, 2] = "";
                for (int i = 0; i < dataGridResultFlats.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridResultFlats.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 3, j + 1] = dataGridResultFlats.Rows[i].Cells[j].Value;
                        ExcelApp.Cells[i + 3, j + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;

                    }
                }
                ExcelApp.Cells[dataGridResultFlats.Rows.Count + 4, 1] = "Дата:";
                ExcelApp.Cells[dataGridResultFlats.Rows.Count + 4, 2] = DateTime.Now.ToShortDateString();
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
                return;
            }
            if (dataGridUninhabitedArea.Visible == true)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                ExcelApp.Cells[1, 1] = "Общий свод по нежилым квартирам за период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
                ExcelApp.Cells[1, 2] = "";
                for (int i = 0; i < dataGridUninhabitedArea.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridUninhabitedArea.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 3, j + 1] = dataGridUninhabitedArea.Rows[i].Cells[j].Value;
                        ExcelApp.Cells[i + 3, j + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                    }
                }
                ExcelApp.Cells[dataGridUninhabitedArea.Rows.Count + 4, 1] = "Дата:";
                ExcelApp.Cells[dataGridUninhabitedArea.Rows.Count + 4, 2] = DateTime.Now.ToShortDateString();
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
                return;
            }
            if (dataGridResultOffices.Visible == true)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                ExcelApp.Cells[1, 1] = "Общий свод по офисам за период с " + datePickerStart.Value.ToShortDateString() + " по " + datePickerFinish.Value.ToShortDateString();
                ExcelApp.Cells[1, 2] = "";
                for (int i = 0; i < dataGridResultOffices.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridResultOffices.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 3, j + 1] = dataGridResultOffices.Rows[i].Cells[j].Value;
                        ExcelApp.Cells[i + 3, j + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                    }
                }
                ExcelApp.Cells[dataGridResultOffices.Rows.Count + 4, 1] = "Дата:";
                ExcelApp.Cells[dataGridResultOffices.Rows.Count + 4, 2] = DateTime.Now.ToShortDateString();
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
                return;
            }
            else
            {
                MessageBox.Show("Выберите таблицу, с которой хотите работать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Адекватное отображение таблицы сводов
        private void ChangeHeight()
        {
            // меняем высоту таблицу по высоте всех строк
            dataGridCommonResults.Height = dataGridCommonResults.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                dataGridCommonResults.ColumnHeadersHeight;
            dataGridResultOffices.Height = dataGridResultOffices.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                dataGridResultOffices.ColumnHeadersHeight;
            dataGridUninhabitedArea.Height = dataGridUninhabitedArea.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                dataGridUninhabitedArea.ColumnHeadersHeight;
            dataGridResultFlats.Height = dataGridResultFlats.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
               dataGridResultFlats.ColumnHeadersHeight;
        }

        private void dataGridCommonResults_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridCommonResults_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridCommonResults_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void dataGridResultOffices_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridResultOffices_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridResultOffices_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void dataGridUninhabitedArea_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridUninhabitedArea_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridUninhabitedArea_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void dataGridResultFlats_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridResultFlats_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeHeight();
        }

        private void dataGridResultFlats_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }
        #endregion

        private void Filtration()
        {
            if (dataGridCommonResults.Visible == true)
            {
                if (datePickerStart.Value > datePickerFinish.Value)
                {
                    MessageBox.Show("Нижняя граница даты не может превышать верхнюю.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataGridCommonResults.Rows.Clear();
                DateTime datestart = Convert.ToDateTime(datePickerStart.Value);
                DateTime datefinish = Convert.ToDateTime(datePickerFinish.Value);

                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();

                string resultAllLoadQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Offices" +
                    " WHERE Date_Payment BETWEEN '" + datestart.ToString("yyyy-MM-dd") + "' AND '" + datefinish.ToString("yyyy-MM-dd") + "'";
                string resultAllSumQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(Amount_Payment) Amount_Payment FROM Apartaments " +
                    " WHERE Date_Payment BETWEEN '" + datestart.ToString("yyyy-MM-dd") + "' AND '" + datefinish.ToString("yyyy-MM-dd") + "'";


                SQLiteCommand loadCommonSummary1 = new SQLiteCommand(resultAllLoadQuery, conn);
                try
                {
                    SQLiteDataReader readerCommonSummary1 = loadCommonSummary1.ExecuteReader();
                    List<string[]> data1 = new List<string[]>();
                    while (readerCommonSummary1.Read())
                    {
                        data1.Add(new string[3]);
                        data1[data1.Count - 1][0] = "Офис";
                        data1[data1.Count - 1][1] = readerCommonSummary1["Amount_Rent"].ToString();
                        data1[data1.Count - 1][2] = readerCommonSummary1["VAT"].ToString();
                    }
                    foreach (string[] s in data1)
                        dataGridCommonResults.Rows.Add(s);

                    for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                    {
                        if (dataGridCommonResults.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            MessageBox.Show("Отсутсвуют записи для выбранного временного периода. ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridCommonResults.Rows.Clear();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SQLiteCommand loadCommonSummary2 = new SQLiteCommand(resultAllSumQuery, conn);
                try
                {
                    SQLiteDataReader readerCommonSummary2 = loadCommonSummary2.ExecuteReader();
                    List<string[]> data2 = new List<string[]>();
                    while (readerCommonSummary2.Read())
                    {
                        data2.Add(new string[3]);
                        data2[data2.Count - 1][0] = "Квартиры";
                        data2[data2.Count - 1][1] = readerCommonSummary2["Amount_Rent"].ToString();
                        data2[data2.Count - 1][2] = readerCommonSummary2["Amount_Payment"].ToString();
                    }
                    readerCommonSummary2.Close();
                    foreach (string[] s in data2)
                        dataGridCommonResults.Rows.Add(s);

                    for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                    {
                        if (dataGridCommonResults.Rows[i].Cells[2].Value.ToString() == "")
                        {
                            MessageBox.Show("Отсутствуют записи для выбранного временного периода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridCommonResults.Rows.Clear();
                            return;
                        }
                    }

                    double sumRent = 0;
                    double sumVat = 0;

                    for (int i = 0; i < dataGridCommonResults.Rows.Count; i++)
                    {
                        sumRent += Convert.ToDouble(dataGridCommonResults.Rows[i].Cells[1].Value);
                        sumVat += Convert.ToDouble(dataGridCommonResults.Rows[i].Cells[2].Value);
                    }

                    dataGridCommonResults.Rows.Add("Всего", sumRent, sumVat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

            }
            if (dataGridResultOffices.Visible == true)
            {
                if (datePickerStart.Value > datePickerFinish.Value)
                {
                    MessageBox.Show("Нижняя граница даты не может превышать верхнюю.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime datestart = Convert.ToDateTime(datePickerStart.Value);
                DateTime datefinish = Convert.ToDateTime(datePickerFinish.Value);

                string resultOfficesLoadQuery = "Select Months.ID_Months, Months.Name Month, SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference" +
                                   " FROM Offices LEFT JOIN Months on Offices.ID_Month = Months.ID_Months " + $"WHERE Date_Payment BETWEEN '{datestart.ToString("yyyy-MM-dd")}' AND '{datefinish.ToString("yyyy-MM-dd")}'" +
                                   $" " + "GROUP BY Months.ID_Months, Months.Name";
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                try
                {
                    conn.Open();
                    SQLiteCommand loadResultOffices = new SQLiteCommand(resultOfficesLoadQuery, conn);
                    SQLiteDataReader readerResultOffices = loadResultOffices.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (readerResultOffices.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = readerResultOffices["Month"].ToString();
                        data[data.Count - 1][1] = readerResultOffices["Amount_Rent"].ToString();
                        data[data.Count - 1][2] = readerResultOffices["VAT"].ToString();
                        data[data.Count - 1][3] = readerResultOffices["Difference"].ToString();
                    }
                    readerResultOffices.Close();
                    dataGridResultOffices.Rows.Clear();
                    foreach (string[] s in data)
                        dataGridResultOffices.Rows.Add(s);

                    double sumRent = 0;
                    double sumVat = 0;
                    double sumDif = 0;
                    for (int i = 0; i < dataGridResultOffices.Rows.Count; i++)
                    {
                        sumRent += Convert.ToDouble(dataGridResultOffices.Rows[i].Cells[1].Value);
                        sumVat += Convert.ToDouble(dataGridResultOffices.Rows[i].Cells[2].Value);

                    }
                    if (dataGridResultOffices.Rows.Count == 0)
                    {
                        MessageBox.Show("Отсутствуют записи для выбранного временного периода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    sumDif = sumRent - sumVat;
                    dataGridResultOffices.Rows.Add("Всего", sumRent, sumVat, sumDif);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (dataGridResultFlats.Visible == true)
            {
                if (datePickerStart.Value > datePickerFinish.Value)
                {
                    MessageBox.Show("Нижняя граница даты не может превышать верхнюю.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime datestart = Convert.ToDateTime(datePickerStart.Value);
                DateTime datefinish = Convert.ToDateTime(datePickerFinish.Value);
                string resultFlatsLoadQuery = "SELECT Months.ID_Months, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
                   "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months " +
                   " " + $"WHERE Date_Payment BETWEEN '{datestart.ToString("yyyy-MM-dd")}' AND '{datefinish.ToString("yyyy-MM-dd")} AND Apartament_Status = 2' " +
                   " GROUP BY Months.ID_Months, Months.Name";
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                try
                {
                    conn.Open();
                    SQLiteCommand loadResultFlats = new SQLiteCommand(resultFlatsLoadQuery, conn);
                    SQLiteDataReader readerResultFlats = loadResultFlats.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    while (readerResultFlats.Read())
                    {
                        data.Add(new string[3]);
                        data[data.Count - 1][0] = readerResultFlats["Month"].ToString();
                        data[data.Count - 1][1] = readerResultFlats["SumRent"].ToString();
                        data[data.Count - 1][2] = readerResultFlats["SumPayment"].ToString();

                    }
                    dataGridResultFlats.Rows.Clear();
                    foreach (string[] s in data)
                    {
                        dataGridResultFlats.Rows.Add(s);
                    }
                    readerResultFlats.Close();

                    if (dataGridResultFlats.Rows.Count == 0)
                    {
                        MessageBox.Show("Отсутствуют записи для выбранного временного периода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    double sumRent = 0;
                    double sumVat = 0;
                    for (int i = 0; i < dataGridResultFlats.Rows.Count; i++)
                    {
                        sumRent += Convert.ToDouble(dataGridResultFlats.Rows[i].Cells[1].Value);
                        sumVat += Convert.ToDouble(dataGridResultFlats.Rows[i].Cells[2].Value);

                    }
                    dataGridResultFlats.Rows.Add("Всего", sumRent, sumVat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (dataGridUninhabitedArea.Visible == true)
            {
                if (datePickerStart.Value > datePickerFinish.Value)
                {
                    MessageBox.Show("Нижняя граница даты не может превышать верхнюю.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime datestart = Convert.ToDateTime(datePickerStart.Value);
                DateTime datefinish = Convert.ToDateTime(datePickerFinish.Value);
                string resultFlatsNLoadQuery = "SELECT Months.ID_Months, Months.Name Month, sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments " +
                      " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Months" +
                      $" WHERE Date_Payment BETWEEN '{datestart.ToString("yyyy-MM-dd")}' AND '{datefinish.ToString("yyyy-MM-dd")}'" + " AND Apartament_Status = 1 " +
                      "Group By Months.ID_Months, Months.Name";
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                try
                {
                    conn.Open();
                    SQLiteCommand loadUninhabitedArea1 = new SQLiteCommand(resultFlatsNLoadQuery, conn);
                    SQLiteDataReader readerUninhabitedArea1 = loadUninhabitedArea1.ExecuteReader();
                    List<string[]> data1 = new List<string[]>();
                    while (readerUninhabitedArea1.Read())
                    {
                        data1.Add(new string[3]);
                        data1[data1.Count - 1][0] = readerUninhabitedArea1["Month"].ToString();
                        data1[data1.Count - 1][1] = readerUninhabitedArea1["Amount_Payment"].ToString();
                        data1[data1.Count - 1][2] = readerUninhabitedArea1["VAT"].ToString();
                    }
                    readerUninhabitedArea1.Close();

                    dataGridUninhabitedArea.Rows.Clear();
                    foreach (string[] s in data1)
                        dataGridUninhabitedArea.Rows.Add(s);


                    if (dataGridUninhabitedArea.Rows.Count == 0)
                    {
                        MessageBox.Show("Отсутствуют записи для выбранного временного периода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    double sumRent = 0;
                    double sumVat = 0;
                    for (int i = 0; i < dataGridUninhabitedArea.Rows.Count; i++)
                    {
                        sumRent += Convert.ToDouble(dataGridUninhabitedArea.Rows[i].Cells[1].Value);
                        sumVat += Convert.ToDouble(dataGridUninhabitedArea.Rows[i].Cells[2].Value);

                    }
                    dataGridUninhabitedArea.Rows.Add("Всего", sumRent, sumVat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (dataGridOffices.Visible == true)
            {
                dataGridOffices.Rows.Clear();
                OfficesLoad();

                if (monthComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                    {
                        if (!(dataGridOffices.Rows[i].Cells[4].Value.ToString().Contains(monthComboBox.Text)))
                        {
                            dataGridOffices.Rows[i].Visible = false;
                        }
                    }
                }
                if (rentersComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                    {
                        if (!(dataGridOffices.Rows[i].Cells[2].Value.ToString().Contains(rentersComboBox.Text)))
                        {
                            dataGridOffices.Rows[i].Visible = false;
                        }
                    }
                }
                if (amountRentTextBoxStart.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridOffices.Rows[i].Cells[5].Value) >= Convert.ToDouble(amountRentTextBoxStart.Text))))
                            {
                                dataGridOffices.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("В поля \"Сумма аренды\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountRentTextBoxStart.Text = amountRentTextBoxStart.Text.Substring(0, amountRentTextBoxStart.Text.Length - 1);
                        amountRentTextBoxStart.SelectionStart = amountRentTextBoxStart.TextLength;
                    }
                }
                if (amountRentTextBoxFinish.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridOffices.Rows[i].Cells[5].Value) <= Convert.ToDouble(amountRentTextBoxFinish.Text))))
                            {
                                dataGridOffices.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма аренды\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountRentTextBoxFinish.Text = amountRentTextBoxFinish.Text.Substring(0, amountRentTextBoxFinish.Text.Length - 1);
                        amountRentTextBoxFinish.SelectionStart = amountRentTextBoxFinish.TextLength;
                    }
                }
                if(amountVAT_TextBoxStart.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridOffices.Rows[i].Cells[6].Value) >= Convert.ToDouble(amountVAT_TextBoxStart.Text))))
                            {
                                dataGridOffices.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма НДС\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountVAT_TextBoxStart.Text = amountVAT_TextBoxStart.Text.Substring(0, amountVAT_TextBoxStart.Text.Length - 1);
                        amountVAT_TextBoxStart.SelectionStart = amountVAT_TextBoxStart.TextLength;
                    }
                }
                if(amountVAT_TextBoxFinish.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridOffices.Rows[i].Cells[6].Value) <= Convert.ToDouble(amountVAT_TextBoxFinish.Text))))
                            {
                                dataGridOffices.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма НДС\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountVAT_TextBoxFinish.Text = amountVAT_TextBoxStart.Text.Substring(0, amountVAT_TextBoxStart.Text.Length - 1);
                        amountVAT_TextBoxFinish.SelectionStart = amountVAT_TextBoxStart.TextLength;
                    }
                }
                DateFiltration();
            }
            if (dataGridFlats.Visible == true)
            {
                dataGridFlats.Rows.Clear();
                FlatsLoad();

                if (monthComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                    {
                        if (!(dataGridFlats.Rows[i].Cells[4].Value.ToString().Contains(monthComboBox.Text)))
                        {
                            dataGridFlats.Rows[i].Visible = false;
                        }
                    }
                }
                if (rentersComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                    {
                        if (!(dataGridFlats.Rows[i].Cells[2].Value.ToString().Contains(rentersComboBox.Text)))
                        {
                            dataGridFlats.Rows[i].Visible = false;
                        }
                    }
                }
                if (areaTypeComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                    {
                        if (!(dataGridFlats.Rows[i].Cells[8].Value.ToString().Contains(areaTypeComboBox.Text)))
                        {
                            dataGridFlats.Rows[i].Visible = false;
                        }
                    }
                }
                if (amountRentTextBoxStart.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value) >= Convert.ToDouble(amountRentTextBoxStart.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма аренды\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountRentTextBoxStart.Text = amountRentTextBoxStart.Text.Substring(0, amountRentTextBoxStart.Text.Length - 1);
                        amountRentTextBoxStart.SelectionStart = amountRentTextBoxStart.TextLength;
                    }
                }
                if (amountRentTextBoxFinish.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value) <= Convert.ToDouble(amountRentTextBoxFinish.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма аренды\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountRentTextBoxFinish.Text = amountRentTextBoxFinish.Text.Substring(0, amountRentTextBoxFinish.Text.Length - 1);
                        amountRentTextBoxFinish.SelectionStart = amountRentTextBoxFinish.TextLength;
                    }
                }
                if (amountPaymentTextBoxStart.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {

                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[6].Value) >= Convert.ToDouble(amountPaymentTextBoxStart.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма оплаты\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountPaymentTextBoxStart.Text = amountPaymentTextBoxStart.Text.Substring(0, amountPaymentTextBoxStart.Text.Length - 1);
                        amountPaymentTextBoxStart.SelectionStart = amountPaymentTextBoxStart.TextLength;
                    }
                   
                }
                if (amountPaymentTextBoxFinish.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {

                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[6].Value) <= Convert.ToDouble(amountPaymentTextBoxFinish.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма оплаты\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountPaymentTextBoxFinish.Text = amountPaymentTextBoxFinish.Text.Substring(0, amountPaymentTextBoxFinish.Text.Length - 1);
                        amountPaymentTextBoxFinish.SelectionStart = amountPaymentTextBoxFinish.TextLength;
                    }
                   
                }
                if (amountVAT_TextBoxStart.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[7].Value) >= Convert.ToDouble(amountVAT_TextBoxStart.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма НДС\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountVAT_TextBoxStart.Text = amountVAT_TextBoxStart.Text.Substring(0, amountVAT_TextBoxStart.Text.Length - 1);
                        amountVAT_TextBoxStart.SelectionStart = amountVAT_TextBoxStart.TextLength;
                    }
                }
                if (amountVAT_TextBoxFinish.Text != "")
                {
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[7].Value) <= Convert.ToDouble(amountVAT_TextBoxFinish.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("В поля \"Сумма НДС\" можно вводить только числа с плавающей запятой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        amountVAT_TextBoxFinish.Text = amountVAT_TextBoxFinish.Text.Substring(0, amountVAT_TextBoxFinish.Text.Length - 1);
                        amountVAT_TextBoxFinish.SelectionStart = amountVAT_TextBoxFinish.TextLength;
                    }
                    DateFiltration();
                }
            }
        }

        private void ClearFiltersButton_Click(object sender, EventArgs e)
        {
            try
            {
                monthComboBox.SelectedIndex = 0;
                rentersComboBox.SelectedIndex = 0;
                areaTypeComboBox.SelectedIndex = 0;
                datePickerStart.Value = Convert.ToDateTime("01.01.2021");
                datePickerFinish.Value = DateTime.Now;
                amountRentTextBoxStart.Text = "1";
                amountRentTextBoxFinish.Text = "1000000";
                amountPaymentTextBoxStart.Text = "1";
                amountRentTextBoxFinish.Text = "1000000";
                amountVAT_TextBoxFinish.Text = "100000";
                amountVAT_TextBoxStart.Text = "0";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void amountVAT_TextBoxStart_TextChanged(object sender, EventArgs e)
        {
            Filtration();
        }

        private void amountVAT_TextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            Filtration();
        }
    }
}



    

