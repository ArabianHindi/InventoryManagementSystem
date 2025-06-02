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
    public partial class ItemReportForm: Form
    {
        private readonly WarehouseDbContext dbContext;
        public ItemReportForm()
        {
            InitializeComponent();
            dbContext = new WarehouseDbContext();
        }

        private void ItemReportForm_Load(object sender, EventArgs e)
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
                MessageBox.Show("Please select at least one warehouse.", "Validation Error");
                return;
            }

            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be greater than end date.", "Validation Error");
                return;
            }

            endDate = endDate.AddDays(1).AddTicks(-1);

            var supplied = dbContext.SupplyOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId) && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .SelectMany(o => o.SupplyOrderItems)
                .GroupBy(i => i.ItemId)
                .Select(g => new
                {
                    ItemId = g.Key,
                    Supplied = g.Sum(x => x.Quantity)
                });

            var disbursed = dbContext.DisbursementOrders
                .Where(o => selectedWarehouseIds.Contains(o.WarehouseId) && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .SelectMany(o => o.DisbursementOrderItems)
                .GroupBy(i => i.ItemId)
                .Select(g => new
                {
                    ItemId = g.Key,
                    Disbursed = g.Sum(x => x.Quantity)
                });

            var items = dbContext.Items.ToList();

            var report = from item in items
                         join s in supplied on item.ItemId equals s.ItemId into sup
                         from s in sup.DefaultIfEmpty()
                         join d in disbursed on item.ItemId equals d.ItemId into dis
                         from d in dis.DefaultIfEmpty()
                         where (s != null && s.Supplied > 0) || (d != null && d.Disbursed > 0)
                         select new
                         {
                             ItemName = item.ItemName,
                             SuppliedIn = s?.Supplied ?? 0,
                             DisbursedOut = d?.Disbursed ?? 0,
                             NetMovement = (s?.Supplied ?? 0) - (d?.Disbursed ?? 0)
                         };

            dataGridView1.DataSource = report.ToList();
        }
    }
}
