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
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class WarehouseForm : Form
    {
        private readonly WarehouseDbContext dbContext;
        private Warehouse _warehouseCopy;

        public WarehouseForm()
        {
            dbContext = new WarehouseDbContext();
            InitializeComponent();
        }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
        }

        private void LoadWarehouses()
        {
            var warehouses = dbContext.Warehouses.ToList();

            // Populate DataGridView
            dataGridView1.DataSource = warehouses.Select(w => new
            {
                w.WarehouseId,
                w.WarehouseName,
                w.Address,
                w.PersonInCharge
            }).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Populate ComboBox
            comboBox1.DataSource = null;
            comboBox1.DataSource = warehouses;
            comboBox1.DisplayMember = "WarehouseName";
            comboBox1.ValueMember = "WarehouseId";
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var newWarehouse = new Warehouse
            {
                WarehouseName = textBox1.Text,
                Address = textBox2.Text,
                PersonInCharge = textBox3.Text
            };

            dbContext.Warehouses.Add(newWarehouse);
            dbContext.SaveChanges();

            MessageBox.Show("Warehouse added successfully.");
            ClearForm();
            LoadWarehouses();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_warehouseCopy == null)
            {
                MessageBox.Show("Please select a warehouse to update.");
                return;
            }

            if (!ValidateInputs()) return;

            bool hasChanges =
                textBox1.Text != _warehouseCopy.WarehouseName ||
                textBox2.Text != _warehouseCopy.Address ||
                textBox3.Text != _warehouseCopy.PersonInCharge;

            if (!hasChanges)
            {
                MessageBox.Show("No changes detected.");
                return;
            }

            var warehouseToUpdate = dbContext.Warehouses
                .FirstOrDefault(w => w.WarehouseId == _warehouseCopy.WarehouseId);

            if (warehouseToUpdate != null)
            {
                warehouseToUpdate.WarehouseName = textBox1.Text;
                warehouseToUpdate.Address = textBox2.Text;
                warehouseToUpdate.PersonInCharge = textBox3.Text;

                dbContext.SaveChanges();

                MessageBox.Show("Warehouse updated successfully.");
                ClearForm();
                LoadWarehouses();
            }
            else
            {
                MessageBox.Show("Warehouse not found in database.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }

            var selectedWarehouse = comboBox1.SelectedItem as Warehouse;
            if (selectedWarehouse != null)
            {
                _warehouseCopy = selectedWarehouse;

                textBox1.Text = _warehouseCopy.WarehouseName;
                textBox2.Text = _warehouseCopy.Address;
                textBox3.Text = _warehouseCopy.PersonInCharge;
                comboBox1.SelectedIndex = -1;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            _warehouseCopy = null;
        }
    }
}
