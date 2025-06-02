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
    public partial class StockAgingReportForm: Form
    {
        private readonly WarehouseDbContext _db;
        public StockAgingReportForm()
        {
            InitializeComponent();
            _db = new WarehouseDbContext();
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

            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be after end date.");
                return;
            }

            endDate = endDate.AddDays(1).AddTicks(-1);

            // Supplied items in period
            var disbursedItems = _db.DisbursementOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId) && o.OrderDate >= startDate)
                .SelectMany(o => o.DisbursementOrderItems.Select(i => new
                {
                    i.ItemId,
                    o.OrderDate
                }))
                .ToList();

            // Step 2: Supplied Items (load into memory)
            var suppliedItems = _db.SupplyOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId) && o.OrderDate <= endDate)
                .SelectMany(o => o.SupplyOrderItems.Select(i => new
                {
                    i.ItemId,
                    i.Item.ItemName,
                    o.OrderDate,
                    i.Quantity,
                    WarehouseName = o.Warehouse.WarehouseName
                }))
                .ToList();

            // Step 3: Group and Filter in memory
            var agingItems = suppliedItems
                .GroupBy(i => new { i.ItemId, i.WarehouseName, i.ItemName })
                .Where(g =>
                    !disbursedItems.Any(d =>
                        d.ItemId == g.Key.ItemId &&
                        d.OrderDate >= startDate && d.OrderDate <= endDate)
                )
                .Select(g => new
                {
                    ItemId = g.Key.ItemId,
                    ItemName = g.Key.ItemName,
                    LastSuppliedDate = g.Max(x => x.OrderDate),
                    TotalSupplied = g.Sum(x => x.Quantity),
                    Warehouse = g.Key.WarehouseName
                })
                .OrderBy(x => x.LastSuppliedDate)
                .ToList();

            // Step 4: Show in grid
            dataGridView1.DataSource = agingItems;
        }

        private void StockAgingReportForm_Load(object sender, EventArgs e)
        {
            var warehouses = _db.Warehouses.ToList();
            checkedListBox1.DataSource = warehouses;
            checkedListBox1.DisplayMember = "WarehouseName";
            checkedListBox1.ValueMember = "WarehouseId";

            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Today;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
