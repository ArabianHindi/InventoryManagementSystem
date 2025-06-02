using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class ItemMovementReportForm: Form
    {
        private readonly WarehouseDbContext dbContext;

        public ItemMovementReportForm()
        {
            InitializeComponent();
            dbContext = new WarehouseDbContext();
        }

        private void ItemMovementReportForm_Load(object sender, EventArgs e)
        {
            var warehouses = dbContext.Warehouses.ToList();
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

            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be after end date.");
                return;
            }

            endDate = endDate.AddDays(1).AddTicks(-1);

            // Supply movements
            var supplyMovements = dbContext.SupplyOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId)
                         && o.OrderDate >= startDate
                         && o.OrderDate <= endDate)
                .SelectMany(o => o.SupplyOrderItems.Select(i => new
                {
                    MovementDate = o.OrderDate,
                    ItemName = i.Item.ItemName,
                    Quantity = i.Quantity,
                    MovementType = "Supplied",
                    WarehouseName = o.Warehouse.WarehouseName
                }));

            // Disbursement movements
            var disbursementMovements = dbContext.DisbursementOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId)
                         && o.OrderDate >= startDate
                         && o.OrderDate <= endDate)
                .SelectMany(o => o.DisbursementOrderItems.Select(i => new
                {
                    MovementDate = o.OrderDate,
                    ItemName = i.Item.ItemName,
                    Quantity = i.Quantity,
                    MovementType = "Disbursed",
                    WarehouseName = o.Warehouse.WarehouseName
                }));

            // Merge and sort by date
            var allMovements = supplyMovements
                .Concat(disbursementMovements)
                .OrderBy(m => m.MovementDate)
                .ToList();

            dataGridView1.DataSource = allMovements;
        }
    }
}
