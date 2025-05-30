using InventoryManagementSystem.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void warehousesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseForm warehouseForm = new WarehouseForm();
            DialogResult result = warehouseForm.ShowDialog();

            if (result == DialogResult.OK)
            {

            }
        }
    }
}
