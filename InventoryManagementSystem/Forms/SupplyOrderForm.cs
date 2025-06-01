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
    public partial class SupplyOrderForm: Form
    {
        WarehouseDbContext dbContext = new WarehouseDbContext();

        public SupplyOrderForm()
        {
            InitializeComponent();
        }

        private void SupplyOrderForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = dbContext.Warehouses.ToList();
            comboBox1.DisplayMember = "WarehouseName";
            comboBox1.ValueMember = "WarehouseId";
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = dbContext.Suppliers.ToList();
            comboBox2.DisplayMember = "SupplierName";
            comboBox2.ValueMember = "SupplierId";
            comboBox2.SelectedIndex = -1;

            comboBox3.DataSource = dbContext.Items.ToList();
            comboBox3.DisplayMember = "ItemName";
            comboBox3.ValueMember = "ItemId";
            comboBox3.SelectedIndex = -1;

            dataGridView1.Columns.Add("ItemId", "Item ID");
            dataGridView1.Columns.Add("ItemName","Item Name");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("ProductionDate", "Production Date");
            dataGridView1.Columns.Add("ExpiryDate", "Expiry Date");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1 || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please select an item and enter a quantity.");
                return;
            }

            var item = (Item)comboBox3.SelectedItem;
            decimal quantity;

            if(!decimal.TryParse(textBox2.Text, out quantity))
            {
                MessageBox.Show("Invalid Quantity");
                return;
            }

            //DateTime productionDate = dateTimePicker2.Value.Date;
            //DateTime expiryDate = dateTimePicker3.Value.Date;

            //if (productionDate >= expiryDate)
            //{
            //    MessageBox.Show("Production date must be before the expiry date.");
            //    return;
            //}

            DateTime productionDate = dateTimePicker2.Value.Date;
            DateTime? expiryDate = checkBox1.Checked ? (DateTime?)null : dateTimePicker3.Value.Date;

            if (!checkBox1.Checked && productionDate >= expiryDate)
            {
                MessageBox.Show("Production date must be before the expiry date.");
                return;
            }


            dataGridView1.Rows.Add(
                item.ItemId,
                item.ItemName,
                quantity,
                productionDate.ToString("MM/dd/yyyy"),
                expiryDate?.ToString("MM/dd/yyyy") ?? "N/A"
            );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Enter an order number");
                return;
            }

            string orderNumber = textBox1.Text.Trim();
            bool orderExists = dbContext.SupplyOrders.Any(o => o.OrderNumber == orderNumber);
            if (orderExists)
            {
                MessageBox.Show("This order number already exists. Please use a unique order number.");
                return;
            }

            bool hasOrderItems = dataGridView1.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow);
            if (!hasOrderItems)
            {
                MessageBox.Show("Please add at least one item to the order.");
                return;
            }

            int warehouseId = (int)comboBox1.SelectedValue;
            int supplierId = (int)comboBox2.SelectedValue;

            var order = new SupplyOrder
            {
                OrderNumber = orderNumber,
                OrderDate = dateTimePicker1.Value.Date,
                WarehouseId = warehouseId,
                SupplierId = supplierId,
                SupplyOrderItems = new List<SupplyOrderItem>()
            };

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var itemId = Convert.ToInt32(row.Cells["ItemId"].Value);
                var quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);
                var productionDate = DateTime.Parse(row.Cells["ProductionDate"].Value.ToString());
                //var expiryDate = DateTime.Parse(row.Cells["ExpiryDate"].Value.ToString());
                string expiryString = row.Cells["ExpiryDate"].Value.ToString();
                DateTime? expiryDate = expiryString == "N/A" ? (DateTime?)null : DateTime.Parse(expiryString);

                var orderItem = new SupplyOrderItem
                {
                    ItemId = itemId,
                    Quantity = quantity,
                    ProductionDate = productionDate,
                    ExpiryDate = expiryDate
                };

                order.SupplyOrderItems.Add(orderItem);

                var existingInventory = dbContext.WarehouseInventories.FirstOrDefault(inv =>
                     inv.WarehouseId == warehouseId &&
                     inv.ItemId == itemId &&
                     inv.SupplierId == supplierId &&
                     inv.ProductionDate ==  productionDate &&
                     inv.ExpiryDate == expiryDate
                );

                if (existingInventory != null)
                {
                    existingInventory.CurrentQuantity += quantity;
                    existingInventory.LastUpdated = DateTime.Now;
                }
                else
                {
                    dbContext.WarehouseInventories.Add(new WarehouseInventory
                    {
                        WarehouseId = warehouseId,
                        ItemId = itemId,
                        SupplierId = supplierId,
                        CurrentQuantity = quantity,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        LastUpdated = DateTime.Now
                    });
                }

            }

            dbContext.SupplyOrders.Add(order);
            dbContext.SaveChanges();

            MessageBox.Show("Order Saved Successfully");
            ClearForm();
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dataGridView1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Enabled = !checkBox1.Checked;
        }
    }
}