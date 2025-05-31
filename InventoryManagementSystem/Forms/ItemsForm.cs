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
    public partial class ItemsForm: Form
    {
        private readonly WarehouseDbContext db;
        private Item _itemCopy;

        public ItemsForm()
        {
            InitializeComponent();
            db = new WarehouseDbContext();
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void LoadItems()
        {
            var items = db.Items.ToList();

            // Populate DataGridView
            dataGridView1.DataSource = items.Select(i => new
            {
                i.ItemId,
                i.ItemCode,
                i.ItemName,
                i.UnitsOfMeasurement
            }).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Populate ComboBox
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[]
            {
                "Units",
                "Boxes",
                "Packs",
                "Bottles",
                "Cases"
            });
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = null;
            comboBox2.DataSource = items;
            comboBox2.DisplayMember = "ItemCode";
            comboBox2.ValueMember = "ItemId";
            comboBox2.SelectedIndex = -1;
            textBox1.Clear();
            textBox2.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
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
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var newItem = new Item
            {
                ItemCode = textBox1.Text,
                ItemName = textBox2.Text,
                UnitsOfMeasurement = comboBox1.SelectedItem?.ToString() ?? "Units"
            };

            db.Items.Add(newItem);
            db.SaveChanges();

            MessageBox.Show("Item added successfully");
            ClearForm();
            LoadItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(_itemCopy == null)
            {
                MessageBox.Show("Please Select an Item to update");
                return;
            }
            if (!ValidateInputs()) return;

            bool hasChanges =
                textBox1.Text != _itemCopy.ItemCode ||
                textBox2.Text != _itemCopy.ItemName;

            if (hasChanges)
            {
                MessageBox.Show("No changes detected.");
                return;
            }

            var itemToUpdate = db.Items.FirstOrDefault(i => i.ItemId == _itemCopy.ItemId);

            if (itemToUpdate != null)
            {
                itemToUpdate.ItemCode = textBox1.Text;
                itemToUpdate.ItemName = textBox2.Text;
                itemToUpdate.UnitsOfMeasurement = comboBox1.SelectedItem?.ToString() ?? "Units";
                db.SaveChanges();
                MessageBox.Show("Item updated successfully.");
                ClearForm();
                LoadItems();
            }
            else
            {
                MessageBox.Show("Item not found.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == -1)
            {
                return;
            }

            var selectedItem = comboBox2.SelectedItem as Item;
            if(selectedItem != null)
            {
                _itemCopy = selectedItem;

                textBox1.Text = _itemCopy.ItemCode;
                textBox2.Text = _itemCopy.ItemName;
                comboBox2.SelectedIndex = -1;
            }
        }
    }
}
