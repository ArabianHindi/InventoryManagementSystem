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
    public partial class WarehouseReportForm: Form
    {
        private readonly WarehouseDbContext dbContext;

        public WarehouseReportForm()
        {
            InitializeComponent();
            dbContext = new WarehouseDbContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a warehouse before generating the report.");
                return;
            }

            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be greater than end date.");
                return;
            }

            int warehouseId = (int)comboBox1.SelectedValue;
            endDate = endDate.AddDays(1).AddTicks(-1);

            var supplied = dbContext.SupplyOrders
                .Where(o => o.WarehouseId == warehouseId && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .SelectMany(o => o.SupplyOrderItems)
                .GroupBy(i => i.ItemId)
                .Select(g => new
                {
                    ItemId = g.Key,
                    Supplied = g.Sum(x => x.Quantity)
                });

            var disbursed = dbContext.DisbursementOrders
                .Where(o => o.WarehouseId == warehouseId && o.OrderDate >= startDate && o.OrderDate <= endDate)
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

        private void WarehouseReportForm_Load(object sender, EventArgs e)
        {
            var warehouses = dbContext.Warehouses.ToList();
            comboBox1.DataSource = warehouses;
            comboBox1.DisplayMember = "WarehouseName";
            comboBox1.ValueMember = "WarehouseId";
            comboBox1.SelectedIndex = -1;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Today;
        }
    }
}
