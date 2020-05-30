using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manprac
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRentersForm addRentersForm = new AddRentersForm();
            addRentersForm.ShowDialog();
        }

        private void updateRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateRentersForm updateRentersForm = new UpdateRentersForm();
            updateRentersForm.ShowDialog();
        }

        private void deleteRentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRentersForm deleteRentersForm = new DeleteRentersForm();
            deleteRentersForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
            {
                x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
            });
        }

        private void addOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOfficesForm addOfficesForm = new AddOfficesForm();
            addOfficesForm.ShowDialog();
        }

        private void updateOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateOfficesForm updateOfficesForm = new UpdateOfficesForm();
            updateOfficesForm.ShowDialog();
        }

        private void deleteOfficeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteOfficesForm deleteOfficesForm = new DeleteOfficesForm();
            deleteOfficesForm.ShowDialog();
        }

        private void addFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteFlatsPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
