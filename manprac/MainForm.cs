using Microsoft.Office.Interop.Excel;
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
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
//using Excel = Microsoft.Office.Interop.Excel;

namespace manprac
{
    public partial class yy : Form
    {

        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingAreaType = new Dictionary<int, string>();

        #region переменные для загрузки данных в таблицы
        string resultFlatsLoadQueryConst = "SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name";
        string resultFlatsLoadQuery = "SELECT Months.ID_Month, Months.Name Month, SUM (Amount_Rent) as 'SumRent', " +
            "SUM(Amount_Payment) as 'SumPayment' FROM Apartaments LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
            " GROUP BY Months.ID_Month, Months.Name";
        string resultFlatsSumQueryConst = "SELECT SUM(Amount_Rent) SumRent, SUM(Amount_Payment) SumPayment FROM Apartaments";
        string resultFlatsSumQuery = "SELECT SUM(Amount_Rent) SumRent, SUM(Amount_Payment) SumPayment FROM Apartaments";

        string resultOfficesLoadQueryConst = "Select Months.ID_Month, Months.Name Month, SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference" +
                " FROM Offices LEFT JOIN Months on Offices.ID_Month = Months.ID_Month GROUP BY Months.ID_Month, Months.Name";
        string resultOfficesLoadQuery = "Select Months.ID_Month, Months.Name Month, SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference" +
                " FROM Offices LEFT JOIN Months on Offices.ID_Month = Months.ID_Month GROUP BY Months.ID_Month, Months.Name";
        string resultOfficesSumQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference FROM Offices";
        string resultOfficesSumQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT, SUM(Amount_Rent-VAT) Difference FROM Offices";

        string resultFlatsNLoadQueryConst = "SELECT Months.ID_Month, Months.Name Month, sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments " +
              " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month WHERE Apartament_Status = 2 Group By Months.ID_Month, Months.Name";
        string resultFlatsNLoadQuery = "SELECT Months.ID_Month, Months.Name Month, sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments " +
              " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month WHERE Apartament_Status = 2 Group By Months.ID_Month, Months.Name";
        string resultFlatsNSumQueryConst = "SELECT sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments Where Apartament_Status = 2";
        string resultFlatsNSumQuery = "SELECT sum(Amount_Payment) Amount_Payment, sum(VAT) VAT FROM Apartaments Where Apartament_Status = 2";

        string resultAllLoadQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Offices";
        string resultAllLoadQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Offices";
        string resultAllSumQuery = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Apartaments";
        string resultAllSumQueryConst = "Select SUM(Amount_Rent) Amount_Rent, SUM(VAT) VAT FROM Apartaments";
        #endregion

        public string ConnString = ConnStringForm.connection;

        #region методы загрузок таблиц
        public void RentersLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            List<string[]> dataRenters = new List<string[]>();

            SqlCommand loadRenters = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRenters = loadRenters.ExecuteReader();

            rentersComboBox.Items.Add("Любой");
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

            conn.Close();
        }

        public void OfficesLoad()
        {

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

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

            conn.Close();
        }

        public void FlatsLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            SqlCommand loadApartaments = new SqlCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payment, VAT, ApartmentStatus.Name ApartamentStatus, " +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month " +
                " LEFT JOIN ApartmentStatus on Apartaments.Apartament_Status = ApartmentStatus.ID_Apartment_Status", conn);
            SqlDataReader readerApartaments = loadApartaments.ExecuteReader();
            List<string[]> dataApartaments = new List<string[]>();
            int countApartaments = 1;
            while (readerApartaments.Read())
            {

                dataApartaments.Add(new string[11]);
                dataApartaments[dataApartaments.Count - 1][0] = readerApartaments["ID_Apartament"].ToString();
                dataApartaments[dataApartaments.Count - 1][1] = countApartaments.ToString();
                dataApartaments[dataApartaments.Count - 1][2] = readerApartaments["Renters"].ToString();
                dataApartaments[dataApartaments.Count - 1][3] = readerApartaments["Contract"].ToString();
                dataApartaments[dataApartaments.Count - 1][4] = readerApartaments["Month"].ToString();
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

            conn.Close();
        }

        public void ResultFlatsLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadResultFlats = new SqlCommand(resultFlatsLoadQuery, conn);
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
            readerResultFlats.Close();

            SqlCommand loadResultTotal = new SqlCommand(resultFlatsSumQuery, conn);
            SqlDataReader readerResultTotal = loadResultTotal.ExecuteReader();
            while (readerResultTotal.Read())
            {
                dataGridResultFlats.Rows.Add("Всего", readerResultTotal["SumRent"].ToString(), readerResultTotal["SumPayment"].ToString());
            }
            readerResultTotal.Close();
            conn.Close();
        }

        public void ResultOfficesLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadResultOffices = new SqlCommand(resultOfficesLoadQuery, conn);
            SqlDataReader readerResultOffices = loadResultOffices.ExecuteReader();
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

            SqlCommand loadResultSummary = new SqlCommand(resultOfficesSumQuery, conn);
            SqlDataReader readerResultSummary = loadResultSummary.ExecuteReader();
            while (readerResultSummary.Read())
            {
                dataGridResultOffices.Rows.Add("Всего", readerResultSummary["Amount_Rent"].ToString(), readerResultSummary["VAT"].ToString(), readerResultSummary["Difference"].ToString());
            }
            readerResultSummary.Close();
            conn.Close();
        }

        public void ResultFlatsNLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadUninhabitedArea1 = new SqlCommand(resultFlatsNLoadQuery, conn);
            SqlDataReader readerUninhabitedArea1 = loadUninhabitedArea1.ExecuteReader();
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

            SqlCommand loadUningabitedArea2 = new SqlCommand(resultFlatsNSumQuery, conn);
            SqlDataReader readerUninhabitedAre2 = loadUningabitedArea2.ExecuteReader();
            while (readerUninhabitedAre2.Read())
            {
                dataGridUninhabitedArea.Rows.Add("Всего", readerUninhabitedAre2["Amount_Payment"].ToString(), readerUninhabitedAre2["VAT"].ToString());
            }
            readerUninhabitedAre2.Close();
            conn.Close();
        }

        public void ResultAllLoad()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand loadCommonSummary1 = new SqlCommand(resultAllLoadQuery, conn);
            SqlDataReader readerCommonSummary1 = loadCommonSummary1.ExecuteReader();
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
            readerCommonSummary1.Close();

            SqlCommand loadCommonSummary2 = new SqlCommand(resultAllSumQuery, conn);
            SqlDataReader readerCommonSummary2 = loadCommonSummary2.ExecuteReader();
            List<string[]> data2 = new List<string[]>();
            while (readerCommonSummary2.Read())
            {
                data2.Add(new string[3]);
                data2[data2.Count - 1][0] = "Квартиры";
                data2[data2.Count - 1][1] = readerCommonSummary2["Amount_Rent"].ToString();
                data2[data2.Count - 1][2] = readerCommonSummary2["VAT"].ToString();
            }
            readerCommonSummary2.Close();
            foreach (string[] s in data2)
                dataGridCommonResults.Rows.Add(s);
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
            catch
            {

            }
            dataGridCommonResults.Rows.Add("Всего", sumRent, sumVat);
        }
        #endregion

        #region методы фильтраций

        public void AmountRentFiltration()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridOffices.Visible == true)
            {
                if (amountRentTextBoxStart.Text != "" && amountRentTextBoxFinish.Text != "")
                {
                    dataGridOffices.Rows.Clear();
                    OfficesLoad();
                    try
                    {
                        for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                        {
                            if (dataGridFlats.Rows[i].Cells[5].Value.ToString() == "")
                            {
                                dataGridFlats.Rows[i].Cells[5].Value = 0;
                            }

                            if (!((Convert.ToDouble(dataGridOffices.Rows[i].Cells[5].Value) >= Convert.ToDouble(amountRentTextBoxStart.Text)) &&
                                (Convert.ToDouble(dataGridOffices.Rows[i].Cells[5].Value) <= Convert.ToDouble(amountRentTextBoxFinish.Text))))
                            {
                                dataGridOffices.Rows[i].Visible = false;
                            }

                            if (Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value.ToString()) == 0)
                            {
                                dataGridFlats.Rows[i].Cells[5].Value = "";
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }

            if (dataGridFlats.Visible == true)
            {
                if (amountRentTextBoxStart.Text != "" && amountRentTextBoxFinish.Text != "")
                {
                    dataGridFlats.Rows.Clear();
                    FlatsLoad();
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (dataGridFlats.Rows[i].Cells[5].Value.ToString() == "")
                            {
                                dataGridFlats.Rows[i].Cells[5].Value = 0;
                            }

                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value) >= Convert.ToDouble(amountRentTextBoxStart.Text)) &&
                                 (Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value) <= Convert.ToDouble(amountRentTextBoxFinish.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }

                            if (Convert.ToDouble(dataGridFlats.Rows[i].Cells[5].Value.ToString()) == 0)
                            {
                                dataGridFlats.Rows[i].Cells[5].Value = "";
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }

            conn.Close();
        }

        public void AmountPaymentFiltration()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                if (amountPaymentTextBoxStart.Text != "" && amountPaymentTextBoxFinish.Text != "")
                {
                    dataGridFlats.Rows.Clear();
                    FlatsLoad();
                    try
                    {
                        for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                        {
                            if (dataGridFlats.Rows[i].Cells[6].Value.ToString() == "")
                            {
                                dataGridFlats.Rows[i].Cells[6].Value = 0;
                            }

                            if (!((Convert.ToDouble(dataGridFlats.Rows[i].Cells[6].Value) >= Convert.ToDouble(amountPaymentTextBoxStart.Text)) &&
                                 (Convert.ToDouble(dataGridFlats.Rows[i].Cells[6].Value) <= Convert.ToDouble(amountPaymentTextBoxFinish.Text))))
                            {
                                dataGridFlats.Rows[i].Visible = false;
                            }

                            if (Convert.ToDouble(dataGridFlats.Rows[i].Cells[6].Value) == 0)
                            {
                                dataGridFlats.Rows[i].Cells[6].Value = "";
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }

            conn.Close();
        }

        public void DateFiltration()
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            string datestart = string.Format("{0:yyyy-MM-dd}", $"'{datePickerStart.Value}'");
            string datefinish = string.Format("{0:yyyy-MM-dd}", $"'{datePickerFinish.Value}'");

            if (dataGridOffices.Visible == true)
            {
                dataGridOffices.Rows.Clear();
                OfficesLoad();
                try
                {
                    for (int i = 0; i < dataGridOffices.Rows.Count; i++)
                    {
                        if (dataGridFlats.Rows[i].Cells[7].Value.ToString() == "")
                        {
                            dataGridFlats.Rows[i].Cells[7].Value = 0;
                        }

                        dataGridOffices.Rows[i].Cells[7].DataGridView.DefaultCellStyle.Format = "d";
                        DateTime dt = DateTime.Parse(dataGridOffices.Rows[i].Cells[7].Value.ToString());
                        if (!((dt.Date >= datePickerStart.Value.Date) && (dt.Date <= datePickerFinish.Value.Date)))
                        {
                            dataGridOffices.Rows[i].Visible = false;
                        }

                        if (Convert.ToDouble(dataGridFlats.Rows[i].Cells[7].Value) == 0)
                        {
                            dataGridFlats.Rows[i].Cells[7].Value = "";
                        }
                    }
                }
                catch
                {

                }
            }

            if (dataGridFlats.Visible == true)
            {
                dataGridFlats.Rows.Clear();
                FlatsLoad();
                try
                {
                    for (int i = 0; i < dataGridFlats.Rows.Count; i++)
                    {

                        if (dataGridFlats.Rows[i].Cells[9].Value == null)
                        {
                            dataGridFlats.Rows[i].Cells[9].Value = "01.01.2001";
                        }

                        dataGridFlats.Rows[i].Cells[9].DataGridView.DefaultCellStyle.Format = "d";
                        DateTime dt = DateTime.Parse(dataGridFlats.Rows[i].Cells[9].Value.ToString());
                        if (!((dt.Date >= datePickerStart.Value.Date) && (dt.Date <= datePickerFinish.Value.Date)))
                        {
                            dataGridFlats.Rows[i].Visible = false;
                        }

                        if (dataGridFlats.Rows[i].Cells[9].Value.ToString() == "01.01.2001")
                        {
                            dataGridFlats.Rows[i].Cells[9].Value = null;
                        }
                    }
                }
                catch
                {

                }
            }

            if (dataGridResultFlats.Visible == true)
            {
                if (areaTypeComboBox.SelectedIndex == 1)
                {
                    resultFlatsLoadQuery = resultFlatsLoadQueryConst.Insert(187, $"WHERE ((Date_Payment BETWEEN {datestart} AND {datefinish}) AND Apartament_Status = 1)");
                    resultFlatsSumQuery = resultFlatsSumQueryConst + $" WHERE ((Date_Payment BETWEEN {datestart} AND {datefinish}) AND Apartament_Status = 1)";
                    dataGridResultFlats.Rows.Clear();
                    ResultFlatsLoad();
                }
                else if (areaTypeComboBox.SelectedIndex == 2)
                {
                    resultFlatsLoadQuery = resultFlatsLoadQueryConst.Insert(187, $"WHERE ((Date_Payment BETWEEN {datestart} AND {datefinish}) AND Apartament_Status = 2)");
                    resultFlatsSumQuery = resultFlatsSumQueryConst + $" WHERE ((Date_Payment BETWEEN {datestart} AND {datefinish}) AND Apartament_Status = 2)";
                    dataGridResultFlats.Rows.Clear();
                    ResultFlatsLoad();
                }
            }

            if (dataGridResultOffices.Visible == true)
            {
                resultOfficesLoadQuery = resultOfficesLoadQueryConst.Insert(187, $"WHERE Date_Payment BETWEEN {datestart} AND {datefinish}");
                resultOfficesSumQuery = resultOfficesSumQueryConst + $" WHERE Date_Payment BETWEEN {datestart} AND {datefinish}";
                dataGridResultOffices.Rows.Clear();
                ResultOfficesLoad();
            }

            if (dataGridUninhabitedArea.Visible == true)
            {
                resultFlatsNLoadQuery = resultFlatsNLoadQueryConst.Insert(197, $"AND (Date_Payment BETWEEN {datestart} AND {datefinish})");
                resultFlatsNSumQuery = resultFlatsNSumQueryConst + $" AND (Date_Payment BETWEEN {datestart} AND {datefinish})";
                dataGridUninhabitedArea.Rows.Clear();
                ResultFlatsNLoad();
            }

            if (dataGridCommonResults.Visible == true)
            {
                resultAllLoadQuery = resultAllLoadQueryConst.Insert(62, $" WHERE Date_Payment BETWEEN {datestart} AND {datefinish}");
                resultAllSumQuery = resultAllSumQueryConst.Insert(66, $" WHERE Date_Payment BETWEEN {datestart} AND {datefinish}");
                dataGridCommonResults.Rows.Clear();
                ResultAllLoad();
            }

            conn.Close();
        }

        #endregion

        public yy()
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

            #region заполнение комбобоксов
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
            areaTypeComboBox.Items.Add("Все");
            while (readerAreaType.Read())
            {
                DebitingAreaType.Add(Convert.ToInt32(readerAreaType["ID_Apartment_Status"]), Convert.ToString(readerAreaType["Name"]));
                areaTypeComboBox.Items.Add(readerAreaType["Name"]);
            }
            readerAreaType.Close();

            SqlCommand loadRentersComboBox = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader readerRentersComboBox = loadRentersComboBox.ExecuteReader();
            rentersComboBox.Items.Add("Любое");
            while (readerRentersComboBox.Read())
            {
                DebitingRenters.Add(Convert.ToInt32(readerRentersComboBox["ID_Renters"]), Convert.ToString(readerRentersComboBox["Name"]));
                rentersComboBox.Items.Add(readerRentersComboBox["Name"]);
            }
            readerAreaType.Close();

            conn.Close();
            #endregion

            #region загрузка данных в таблицы
            RentersLoad();
            OfficesLoad();
            FlatsLoad();
            #endregion

            #region установка изначальных значений в комбобоксы
            monthComboBox.SelectedIndex = 0;
            rentersComboBox.SelectedIndex = 0;
            areaTypeComboBox.SelectedIndex = 0;
            #endregion

            ChangeHeight();
        }

        #region переход на другие формы
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
                MessageBox.Show("Выберите запись в таблице \"Арендаторы\", которую хотите изменить.", "Ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

        #region отображение таблиц по нажатию на пункт в меню
        private void rentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = true;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;


            dateLabel.Visible = false;
            datePickerStart.Visible = false;
            datePickerFinish.Visible = false;
            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;
            amountRentTextBoxFinish.Visible = false;
            amountRentTextBoxStart.Visible = false;
            monthLabel.Visible = false;
            monthComboBox.Visible = false;
            rentersLabel.Visible = false;
            rentersComboBox.Visible = false;
            areaTypeLabel.Visible = false;
            areaTypeComboBox.Visible = false;
            rentLabel.Visible = false;
            paymentLabel.Visible = false;
        }

        private void officesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = true;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultRenters.Visible = false;
            dataGridResultOffices.Visible = false;

            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountRentTextBoxFinish.Visible = true;
            amountRentTextBoxStart.Visible = true;
            monthLabel.Visible = true;
            monthComboBox.Visible = true;
            rentersLabel.Visible = true;
            rentersComboBox.Visible = true;
            areaTypeLabel.Visible = true;
            areaTypeComboBox.Visible = true;
            rentLabel.Visible = true;
        }

        private void flatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = true;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountPaymentTextBoxStart.Visible = true;
            amountPaymentTextBoxFinish.Visible = true;
            amountRentTextBoxFinish.Visible = true;
            amountRentTextBoxStart.Visible = true;
            monthLabel.Visible = true;
            monthComboBox.Visible = true;
            rentersLabel.Visible = true;
            rentersComboBox.Visible = true;
            areaTypeLabel.Visible = true;
            areaTypeComboBox.Visible = true;

            rentLabel.Visible = true;
            paymentLabel.Visible = true;
        }

        private void resultFlatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = true;
            dataGridResultOffices.Visible = false;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;
            amountRentTextBoxFinish.Visible = false;
            amountRentTextBoxStart.Visible = false;
            rentersLabel.Visible = false;
            rentersComboBox.Visible = false;
            areaTypeLabel.Visible = true;
            areaTypeComboBox.Visible = true;
            rentLabel.Visible = false;
            paymentLabel.Visible = false;

            dataGridResultFlats.Rows.Clear();
            ResultFlatsLoad();
        }

        private void resultOfficesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = true;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;
            amountRentTextBoxFinish.Visible = false;
            amountRentTextBoxStart.Visible = false;
            rentersLabel.Visible = false;
            rentersComboBox.Visible = false;
            areaTypeLabel.Visible = false;
            areaTypeComboBox.Visible = false;
            rentLabel.Visible = false;
            paymentLabel.Visible = false;

            dataGridResultOffices.Rows.Clear();
            ResultOfficesLoad();
        }

        private void resultAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = true;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;
            amountRentTextBoxFinish.Visible = false;
            amountRentTextBoxStart.Visible = false;
            rentersLabel.Visible = false;
            rentersComboBox.Visible = false;
            areaTypeLabel.Visible = false;
            areaTypeComboBox.Visible = false;
            rentLabel.Visible = false;
            paymentLabel.Visible = false;

            monthLabel.Visible = true;
            monthComboBox.Visible = true;

            dataGridCommonResults.Rows.Clear();
            ResultAllLoad();
        }

        private void resultFlatsNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridUninhabitedArea.Visible = false;
            dataGridUninhabitedArea.Visible = true;
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = false;
            dataGridCommonResults.Visible = false;
            dataGridResultFlats.Visible = false;
            dataGridResultOffices.Visible = false;

            dateLabel.Visible = true;
            datePickerStart.Visible = true;
            datePickerFinish.Visible = true;
            amountPaymentTextBoxStart.Visible = false;
            amountPaymentTextBoxFinish.Visible = false;
            amountRentTextBoxFinish.Visible = false;
            amountRentTextBoxStart.Visible = false;
            rentersLabel.Visible = false;
            rentersComboBox.Visible = false;
            areaTypeLabel.Visible = false;
            areaTypeComboBox.Visible = false;
            monthLabel.Visible = true;
            monthComboBox.Visible = true;

            ResultFlatsNLoad();
        }
        #endregion

        #region фильтрация
        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

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
            }

            if (dataGridResultFlats.Visible == true)
            {
                dataGridResultFlats.Rows.Clear();
                ResultFlatsLoad();
                if (monthComboBox.SelectedIndex != 0)
                {
                    for (int i = 0; i < dataGridResultFlats.Rows.Count; i++)
                    {
                        if (!(dataGridResultFlats.Rows[i].Cells[0].Value.ToString().Contains(monthComboBox.Text)))
                        {
                            dataGridResultFlats.Rows[i].Visible = false;
                        }
                    }
                }
            }

            conn.Close();
        }

        private void rentersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridOffices.Visible == true)
            {
                dataGridOffices.Rows.Clear();
                OfficesLoad();
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
            }

            if (dataGridFlats.Visible == true)
            {
                dataGridFlats.Rows.Clear();
                FlatsLoad();
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
            }

            conn.Close();
        }

        private void areaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            if (dataGridFlats.Visible == true)
            {
                dataGridFlats.Rows.Clear();
                FlatsLoad();
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
            }

            conn.Close();
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            DateFiltration();
        }

        private void datePickerFinish_ValueChanged(object sender, EventArgs e)
        {
            DateFiltration();
        }

        private void amountRentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            AmountRentFiltration();
        }

        private void amountRentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            AmountRentFiltration();
        }

        private void amountPaymentTextBoxStart_TextChanged(object sender, EventArgs e)
        {
            AmountPaymentFiltration();
        }

        private void amountPaymentTextBoxFinish_TextChanged(object sender, EventArgs e)
        {
            AmountPaymentFiltration();
        }

        #endregion

        #region дамп бд
        private void saveDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DBname = "";
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand NameDB = new SqlCommand("select db_name() Name", conn);
            SqlDataReader readerNameBD = NameDB.ExecuteReader();
            while (readerNameBD.Read())
            {
                DBname = readerNameBD["Name"].ToString();
            }
            readerNameBD.Close();
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = DBname + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                dlg.DefaultExt = ".bak";
                dlg.Filter = "Базы данных (*.bak)|*.bak|Все файлы (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string filename = dlg.FileName;
                    SqlConnection connection = new SqlConnection(ConnString);

                    string comm = "USE [master] " +
                        $"BACKUP DATABASE [{DBname}] TO DISK = N'{filename}'";
                    SqlCommand command = new SqlCommand(comm, connection);
                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Резервное сохранение базы данных успешно создано.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void loadDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DBname = "";

            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand NameDB = new SqlCommand("select db_name() Name", conn);
            SqlDataReader readerNameBD = NameDB.ExecuteReader();
            while (readerNameBD.Read())
            {
                DBname = readerNameBD["Name"].ToString();
            }
            readerNameBD.Close();

            try
            {
                if (MessageBox.Show("После выбора файла несохраненные данные будут утеряны. Программа будет перезагружена. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.DefaultExt = ".bak";
                    dlg.Filter = "Базы данных (*.bak)|*.bak|Все файлы (*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string filename = dlg.FileName;
                        SqlConnection connection = new SqlConnection(ConnString);
                        string comm = "USE [master] " +
                            $"ALTER DATABASE[{DBname}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                            $"RESTORE DATABASE[{DBname}] FROM DISK = N'{filename}' WITH REPLACE " +
                            $"ALTER DATABASE[{DBname}] SET MULTI_USER";
                        SqlCommand command = new SqlCommand(comm, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        Application.Restart();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            dataGridCommonResults.DrawToBitmap(btm, new Rectangle(0, 0, dataGridCommonResults.Size.Width + 10, dataGridCommonResults.Size.Height +10 ));
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
            e.Graphics.DrawString("Дата: " + dt.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 20, 120 + dataGridResultOffices.Rows.GetRowsHeight(DataGridViewElementStates.Visible) );
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
                ExcelApp.Cells[dataGridResultFlats.Rows.Count + 4, 1] = "Дата:" ;   
                ExcelApp.Cells[dataGridResultFlats.Rows.Count +4, 2] = DateTime.Now.ToShortDateString();
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
    }

}

    

