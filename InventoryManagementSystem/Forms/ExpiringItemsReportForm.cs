using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class ExpiringItemsReportForm: Form
    {
        private readonly WarehouseDbContext _db = new WarehouseDbContext();
        public ExpiringItemsReportForm()
        {
            InitializeComponent();
        }

        private void ExpiringItemsReportForm_Load(object sender, EventArgs e)
        {
            var warehouses = _db.Warehouses.ToList();
            checkedListBox1.DataSource = warehouses;
            checkedListBox1.DisplayMember = "WarehouseName";
            checkedListBox1.ValueMember = "WarehouseId";

            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Today;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedWarehouseIds = checkedListBox1.CheckedItems
                .Cast<Warehouse>()
                .Select(w => w.WarehouseId)
                .ToList();

            if (selectedWarehouseIds.Count == 0)
            {
                MessageBox.Show("Please select at least one warehouse.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out int thresholdDays) || thresholdDays <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for days threshold.");
                return;
            }

            DateTime today = DateTime.Today;
            DateTime thresholdDate = today.AddDays(thresholdDays);

            var expiringItems = _db.SupplyOrders
                .Include(o => o.Warehouse)
                .Include(o => o.SupplyOrderItems)
                    .ThenInclude(i => i.Item)
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId))
                .AsEnumerable()
                .SelectMany(o => o.SupplyOrderItems
                    .Where(i => i.ExpiryDate.HasValue &&
                                i.ExpiryDate.Value >= today &&
                                i.ExpiryDate.Value <= thresholdDate)
                    .Select(i => new
                    {
                        ItemName = i.Item.ItemName,
                        Quantity = i.Quantity,
                        ExpiryDate = i.ExpiryDate.Value,
                        DaysUntilExpiry = (i.ExpiryDate.Value - today).Days,
                        WarehouseName = o.Warehouse.WarehouseName
                    })
                )
                .OrderBy(x => x.ExpiryDate)
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = expiringItems;
        }
    }
}
