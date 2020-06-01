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
        }

        private void addRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRentersForm addRentersForm = new AddRentersForm();
            addRentersForm.Owner = this;
            addRentersForm.ShowDialog();
        }

        private void updateRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateRentersForm updateRentersForm = new UpdateRentersForm();
            updateRentersForm.Owner = this;
            updateRentersForm.ShowDialog();
        }

        private void deleteRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRentersForm deleteRentersForm = new DeleteRentersForm();
            deleteRentersForm.Owner = this;
            deleteRentersForm.ShowDialog();
        }

        private void addOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOfficesForm addOfficesForm = new AddOfficesForm();
            addOfficesForm.Owner = this;
            addOfficesForm.ShowDialog();
        }

        private void updateOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateOfficesForm updateOfficesForm = new UpdateOfficesForm();
            updateOfficesForm.Owner = this;
            updateOfficesForm.ShowDialog();
        }

        private void deleteOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteOfficesForm deleteOfficesForm = new DeleteOfficesForm();
            deleteOfficesForm.Owner = this;
            deleteOfficesForm.ShowDialog();
        }

        private void addFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlatsForm addFlatsForm = new AddFlatsForm();
            addFlatsForm.Owner = this;
            addFlatsForm.ShowDialog();
        }

        private void updateFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFlatsForm updateFlatsForm = new UpdateFlatsForm();
            updateFlatsForm.Owner = this;
            updateFlatsForm.ShowDialog();
        }

        private void deleteFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFlatsForm deleteFlatsForm = new DeleteFlatsForm();
            deleteFlatsForm.Owner = this;
            deleteFlatsForm.ShowDialog();
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
            dataGridRenters.Rows.Clear();
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Renters, Name FROM Renters", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            int count = 1;
            while (reader.Read())
            {

                data.Add(new string[3]);
                data[count - 1][0] = reader["ID_Renters"].ToString();
                data[data.Count - 1][1] = count.ToString();
                data[data.Count - 1][2] = reader["Name"].ToString();
                count++;
            }
            foreach (string[] s in data)
                dataGridRenters.Rows.Add(s);

            reader.Close();
            conn.Close();
        }

        private void officesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = false;
            dataGridOffices.Visible = true;
            dataGridOffices.Rows.Clear();
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Office, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, VAT, Date_Payment, Note" +
                " FROM Offices LEFT JOIN Renters on Offices.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Offices.ID_Month = Months.ID_Month", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            int count = 1;
            while (reader.Read())
            {

                data.Add(new string[9]);
                data[data.Count - 1][0] = count.ToString();
                data[data.Count - 1][1] = reader["ID_Office"].ToString();
                data[data.Count - 1][2] = reader["Renters"].ToString();
                data[data.Count - 1][3] = reader["Contract"].ToString();
                data[data.Count - 1][4] = reader["Month"].ToString();
                data[data.Count - 1][5] = reader["Amount_Rent"].ToString();
                data[data.Count - 1][6] = reader["VAT"].ToString();
                data[data.Count - 1][7] = reader["Date_Payment"].ToString();
                data[data.Count - 1][8] = reader["Note"].ToString();
                count++;
            }
            foreach (string[] s in data)
                dataGridOffices.Rows.Add(s);

            reader.Close();
            conn.Close();
        }

        private void flatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridRenters.Visible = false;
            dataGridFlats.Visible = true;
            dataGridOffices.Visible = false;
            dataGridFlats.Rows.Clear();
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Apartament, Renters.Name Renters, Contract, Months.Name Month, Amount_Rent, Amount_Payemnt, VAT," +
                " Date_Payment, Note FROM Apartaments LEFT JOIN Renters on Apartaments.ID_Renters = Renters.ID_Renters " +
                " LEFT JOIN Months on Apartaments.ID_Month = Months.ID_Month", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            int count = 1;
            while (reader.Read())
            {

                data.Add(new string[10]);
                data[data.Count - 1][0] = count.ToString();
                data[data.Count - 1][1] = reader["ID_Apartament"].ToString();
                data[data.Count - 1][2] = reader["Renters"].ToString();
                data[data.Count - 1][3] = reader["Contract"].ToString();
                data[data.Count - 1][4] = reader["Month"].ToString();
                data[data.Count - 1][5] = reader["Amount_Rent"].ToString();
                data[data.Count - 1][6] = reader["Amount_Payemnt"].ToString();
                data[data.Count - 1][7] = reader["VAT"].ToString();
                data[data.Count - 1][8] = reader["Date_Payment"].ToString();
                data[data.Count - 1][9] = reader["Note"].ToString();
                count++;
            }
            foreach (string[] s in data)
                dataGridFlats.Rows.Add(s);

            reader.Close();
            conn.Close();
        }
    }
}
    

