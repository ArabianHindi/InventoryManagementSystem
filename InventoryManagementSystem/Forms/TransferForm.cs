using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem.Forms
{
    public partial class TransferForm: Form
    {
        private readonly WarehouseDbContext _context;
        private List<WarehouseInventory> availableBatches = new List<WarehouseInventory>();
        private List<TransferOrderItem> transferItems = new List<TransferOrderItem>();

        public TransferForm()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            LoadWarehouses();
            InitializeBatchGrid();
            InitializeTransferItemsGrid();
        }

        private void InitializeTransferItemsGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ItemName", "Item");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("Supplier", "Supplier");
            dataGridView1.Columns.Add("ProductionDate", "Production Date");
            dataGridView1.Columns.Add("ExpiryDate", "Expiry Date");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void InitializeBatchGrid()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("ItemName", "Item");
            dataGridView2.Columns.Add("SupplierName", "Supplier");
            dataGridView2.Columns.Add("ProductionDate", "Production Date");
            dataGridView2.Columns.Add("ExpiryDate", "Expiry Date");
            dataGridView2.Columns.Add("CurrentQuantity", "Available Quantity");
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
        }

        private void LoadWarehouses()
        {
            var warehouses = _context.Warehouses.ToList();

            comboBox1.DataSource = warehouses;
            comboBox1.DisplayMember = "WarehouseName";
            comboBox1.ValueMember = "WarehouseId";
            comboBox1.SelectedIndexChanged +=(s,e)=>LoadAvailableBatches();

            comboBox2.DataSource = warehouses.ToList();
            comboBox2.DisplayMember = "WarehouseName";
            comboBox2.ValueMember = "WarehouseId";
            comboBox2.SelectedIndex = -1;
        }

        private void LoadAvailableBatches()
        {
            availableBatches.Clear();
            dataGridView2.Rows.Clear();

            if (comboBox1.SelectedIndex != -1)
            {
                int fromId = (int)comboBox1.SelectedValue;
                availableBatches = _context.WarehouseInventories
                    .Where(i => i.WarehouseId == fromId && i.CurrentQuantity > 0)
                    .Include(i => i.Item)
                    .Include(i => i.Supplier)
                    .ToList();

                foreach (var b in availableBatches)
                {
                    dataGridView2.Rows.Add(
                        b.Item?.ItemName,
                        b.Supplier?.SupplierName,
                        b.ProductionDate.HasValue ? b.ProductionDate.Value.ToShortDateString() : "",
                        b.ExpiryDate.HasValue ? b.ExpiryDate.Value.ToShortDateString() : "",
                        b.CurrentQuantity
                    );
                }
            }
        }

        private void RefreshTransferItemsGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in transferItems)
            {
                dataGridView1.Rows.Add(
                    item.Item?.ItemName,
                    item.Quantity,
                    item.Supplier?.SupplierName,
                    item.ProductionDate.HasValue ? item.ProductionDate.Value.ToShortDateString():"",
                    item.ExpiryDate.HasValue? item.ExpiryDate.Value.ToShortDateString():""
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a batch to transfer.");
                return;
            }

            if (!decimal.TryParse(textBox2.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Enter a valid quantity.");
                return;
            }

            int selectedIndex = dataGridView2.SelectedRows[0].Index;
            var batch = availableBatches[selectedIndex];

            if (batch.CurrentQuantity < quantity)
            {
                MessageBox.Show("Not enough quantity in selected batch.");
                return;
            }

            transferItems.Add(new TransferOrderItem
            {
                ItemId = batch.ItemId,
                Quantity = quantity,
                SupplierId = batch.SupplierId,
                ProductionDate = batch.ProductionDate,
                ExpiryDate = batch.ExpiryDate,
                Item = batch.Item,
                Supplier = batch.Supplier
            });

            RefreshTransferItemsGrid();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(textBox1.Text))
            //{
            //    MessageBox.Show("Transfer number is required.");
            //    return;
            //}

            //if (_context.TransferOrders.Any(t => t.TransferOrderId == textBox1.Text.Trim()))
            //{
            //    MessageBox.Show("Transfer number already exists.");
            //    return;
            //}

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Select source and destination warehouses.");
                return;
            }

            int fromId = (int)comboBox1.SelectedValue;
            int toId = (int)comboBox2.SelectedValue;

            if (fromId == toId)
            {
                MessageBox.Show("Source and destination must be different.");
                return;
            }

            if (transferItems.Count == 0)
            {
                MessageBox.Show("Add at least one item to transfer.");
                return;
            }

            var transferOrder = new TransferOrder
            {
                TransferDate = dateTimePicker3.Value.Date,
                FromWarehouseId = fromId,
                ToWarehouseId = toId,
                TransferOrderItems = transferItems
            };

            foreach (var item in transferItems)
            {
                var fromBatch = _context.WarehouseInventories.FirstOrDefault(i =>
                    i.ItemId == item.ItemId &&
                    i.WarehouseId == fromId &&
                    i.ProductionDate == item.ProductionDate &&
                    i.ExpiryDate == item.ExpiryDate &&
                    i.SupplierId == item.SupplierId);

                if (fromBatch == null || fromBatch.CurrentQuantity < item.Quantity)
                {
                    MessageBox.Show($"Not enough quantity for item {item.Item?.ItemName} in source batch.");
                    return;
                }

                fromBatch.CurrentQuantity -= item.Quantity;

                var toBatch = _context.WarehouseInventories.FirstOrDefault(i =>
                    i.ItemId == item.ItemId &&
                    i.WarehouseId == toId &&
                    i.ProductionDate == item.ProductionDate &&
                    i.ExpiryDate == item.ExpiryDate &&
                    i.SupplierId == item.SupplierId);

                if (toBatch == null)
                {
                    toBatch = new WarehouseInventory
                    {
                        ItemId = item.ItemId,
                        WarehouseId = toId,
                        SupplierId = item.SupplierId,
                        ProductionDate = item.ProductionDate,
                        ExpiryDate = item.ExpiryDate,
                        CurrentQuantity = 0
                    };
                    _context.WarehouseInventories.Add(toBatch);
                }

                toBatch.CurrentQuantity += item.Quantity;
            }

            _context.TransferOrders.Add(transferOrder);
            _context.SaveChanges();

            MessageBox.Show("Transfer completed successfully.");
            ClearForm();
        }

        private void ClearForm()
        {
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            transferItems.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            dateTimePicker3.Value = DateTime.Today;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
