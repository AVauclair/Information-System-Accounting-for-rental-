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

        Dictionary<int, string> DebitingMonth = new Dictionary<int, string>();
        Dictionary<int, string> DebitingRenters = new Dictionary<int, string>();
        Dictionary<int, string> DebitingAreaType = new Dictionary<int, string>();
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
                dataOffices[dataOffices.Count - 1][7] = readerOffices["Date_Payment"].ToString();
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

            conn.Close();
        }

        public void ResultFlatsLoad()
        {
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

            readerResultFlats.Close();

            conn.Close();
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

        private void connStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnStringForm connStringForm = new ConnStringForm();
            connStringForm.ShowDialog();
        }
        #endregion

        #region отображение таблиц по нажатию на пункт в меню
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

        //не доделал фильтрацию у квартир и свода квартир так как хз как - типа помещения в таблиц нет
        private void areaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            conn.Close();
        }
        #endregion
    }
}
    

