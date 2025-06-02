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

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemsForm itemsForm = new ItemsForm();
            DialogResult result = itemsForm.ShowDialog();

            if (result == DialogResult.OK)
            {
            }
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm = new SupplierForm();
            DialogResult result = supplierForm.ShowDialog();

            if (result == DialogResult.OK)
            {
            }    
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            DialogResult result = customerForm.ShowDialog();

            if (result == DialogResult.OK)
            {
            }
        }

        private void supplyOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplyOrderForm supplyOrderForm = new SupplyOrderForm();
            DialogResult result = supplyOrderForm.ShowDialog();

            if(result == DialogResult.OK)
            {
            }
        }

        private void dismissOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DismissOrderForm dismissOrderForm = new DismissOrderForm();
            DialogResult result = dismissOrderForm.ShowDialog();

            if(result == DialogResult.OK)
            {
            }
        }

        private void transferItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferForm transferForm = new TransferForm();
            DialogResult result = transferForm.ShowDialog();

            if(result == DialogResult.OK)
            {
            }
        }

        private void warehouseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReportForm warehouseReportForm = new WarehouseReportForm();
            DialogResult result = warehouseReportForm.ShowDialog();

            if(result == DialogResult.OK)
            {
            }
        }

        private void itemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemReportForm itemReportForm = new ItemReportForm();
            DialogResult result = itemReportForm.ShowDialog();

            if(result != DialogResult.OK)
            {
            }
        }
    }
}
