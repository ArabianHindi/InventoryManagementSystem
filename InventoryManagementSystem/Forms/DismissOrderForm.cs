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
    public partial class DismissOrderForm: Form
    {
        private readonly WarehouseDbContext _context;
        private List<DisbursementOrderItem> orderItems = new List<DisbursementOrderItem>();

        public DismissOrderForm()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();

            LoadWarehouses();
            comboBox1.SelectedIndexChanged+= ComboBox1_SelectedIndexChanged;
            LoadCustomers();
            LoadItems();
            comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;

            IntializeGridColumns();
        }

        private void IntializeGridColumns()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("ItemId", "Item ID");
            dataGridView1.Columns.Add("ItemName", "Item Name");
            dataGridView1.Columns.Add("Quantity", "Quantity");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadWarehouses()
        {
            comboBox1.DataSource = _context.Warehouses.ToList();
            comboBox1.DisplayMember = "WarehouseName";
            comboBox1.ValueMember = "WarehouseId";
            comboBox1.SelectedIndex = -1;
        }

        private void LoadCustomers()
        {
            comboBox2.DataSource = _context.Customers.ToList();
            comboBox2.DisplayMember = "CustomerName";
            comboBox2.ValueMember = "CustomerId";
            comboBox2.SelectedIndex = -1;
        }

        private void LoadItems()
        {
            comboBox3.DataSource = _context.Items.ToList();
            comboBox3.DisplayMember = "ItemName";
            comboBox3.ValueMember = "ItemId";
            comboBox3.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox3.SelectedItem == null || !decimal.TryParse(textBox2.Text,out decimal quantity)|| quantity <= 0)
            {
                MessageBox.Show("Please select an item and enter a valid quantity greater than zero.");
                return;
            }

            int itemId = (int)comboBox3.SelectedValue;
            int warehouseId = (int)comboBox1.SelectedValue;

            if(orderItems.Any(i => i.ItemId == itemId))
            {
                MessageBox.Show("This item is already added to the order.");
                return;
            }

            if (!CheckInventoryAvailability(itemId, warehouseId, quantity))
            {
                MessageBox.Show("Insufficient quantity in warehouse inventory.");
                return;
            }

            var item = _context.Items.Find(itemId);

            var newItem = new DisbursementOrderItem
            {
                ItemId = itemId,
                Quantity = quantity,
                Item = item
            };

            orderItems.Add(newItem);
            refreshGrid();
        }

        private bool CheckInventoryAvailability(int itemId, int warehouseId, decimal quantity)
        {
            var inventory = _context.WarehouseInventories
                                    .FirstOrDefault(i => i.ItemId == itemId && i.WarehouseId == warehouseId);

            return inventory != null && inventory.CurrentQuantity >= quantity;
        }

        private void refreshGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (var item in orderItems)
            {
                dataGridView1.Rows.Add(
                    item.ItemId,
                    item.Item?.ItemName ?? "",
                    item.Quantity
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Order number is required.");
                return;
            }

            if (_context.DisbursementOrders.Any(o => o.OrderNumber == textBox1.Text))
            {
                MessageBox.Show("Order number already exists.");
                return;
            }

            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both warehouse and customer.");
                return;
            }

            if (orderItems.Count == 0)
            {
                MessageBox.Show("Add at least one item.");
                return;
            }

            var newOrder = new DisbursementOrder
            {
                OrderNumber = textBox1.Text.Trim(),
                OrderDate = dateTimePicker1.Value.Date,
                WarehouseId = (int)comboBox1.SelectedValue,
                CustomerId = (int)comboBox2.SelectedValue,
                DisbursementOrderItems = orderItems
            };

            foreach (var item in orderItems)
            {
                var inventory = _context.WarehouseInventories
                                        .FirstOrDefault(i => i.ItemId == item.ItemId && i.WarehouseId == newOrder.WarehouseId);

                if (inventory != null)
                {
                    inventory.CurrentQuantity -= item.Quantity;
                }
            }

            _context.DisbursementOrders.Add(newOrder);
            _context.SaveChanges();

            MessageBox.Show("Order saved successfully.");
            ClearForm();
        }

        private void ClearForm()
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox2.Clear();
            orderItems.Clear();
            refreshGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            int warehouseId = (int)comboBox1.SelectedValue;

            var availableItems = _context.WarehouseInventories
                .Where(i => i.WarehouseId == warehouseId && i.CurrentQuantity > 0)
                .Select(i => i.Item)
                .Distinct()
                .ToList();

            comboBox3.DataSource = availableItems;
            comboBox3.DisplayMember = "ItemName";
            comboBox3.ValueMember = "ItemId";
            comboBox3.SelectedIndex = -1;

            textBox2.Clear();
            label7.Text = "Available Quantity:  —";

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                label7.Text = "Available Quantity: —";
                return;
            }

            int warehouseId = (int)comboBox1.SelectedValue;
            int itemId = (int)comboBox3.SelectedValue;

            var inventory = _context.WarehouseInventories
                .FirstOrDefault(i => i.WarehouseId == warehouseId && i.ItemId == itemId);

            if (inventory != null)
            {
                label7.Text = $"Available Quantity: {inventory.CurrentQuantity}";
            }
            else
            {
                label7.Text = "Available Quantity: 0";
            }
        }

    }
}
