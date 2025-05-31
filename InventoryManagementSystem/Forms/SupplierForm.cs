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
    public partial class SupplierForm: Form
    {
        private WarehouseDbContext dbcontext;
        private Supplier _selectedSupplier;

        public SupplierForm()
        {
            InitializeComponent();
            dbcontext = new WarehouseDbContext();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            LoadSuppliersIntoComboBox();
            RefreshSupplierGrid();
            ClearFields();
        }

        private void LoadSuppliersIntoComboBox()
        {
            comboBox1.DataSource = dbcontext.Suppliers.ToList();
            comboBox1.DisplayMember = "SupplierName";
            comboBox1.ValueMember = "SupplierId";
            comboBox1.SelectedIndex = -1;
        }

        private void RefreshSupplierGrid()
        {
            dataGridView1.DataSource = dbcontext.Suppliers.Select(s => new
            {
                s.SupplierId,
                s.SupplierName,
                s.PhoneNumber,
                s.Fax,
                s.MobileNumber,
                s.EmailAddress,
                s.Website
            }).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) 
            {
                return;
            }

            _selectedSupplier = comboBox1.SelectedItem as Supplier;

            if (_selectedSupplier != null)
            {
                textBox1.Text = _selectedSupplier.SupplierName;
                textBox2.Text = _selectedSupplier.PhoneNumber;
                textBox3.Text = _selectedSupplier.Fax;
                textBox4.Text = _selectedSupplier.MobileNumber;
                textBox5.Text = _selectedSupplier.EmailAddress;
                textBox6.Text = _selectedSupplier.Website;
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                Supplier newSupplier = new Supplier
                {
                    SupplierName = textBox1.Text,
                    PhoneNumber = textBox2.Text,
                    Fax = textBox3.Text,
                    MobileNumber = textBox4.Text,
                    EmailAddress = textBox5.Text,
                    Website = textBox6.Text
                };

                dbcontext.Suppliers.Add(newSupplier);
                dbcontext.SaveChanges();

                MessageBox.Show("Supplier added successfully.");
                ClearFields();
                LoadSuppliersIntoComboBox();
                RefreshSupplierGrid();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.SelectedIndex = -1;
            _selectedSupplier = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_selectedSupplier == null)
            {
                MessageBox.Show("Please select a supplier to update.");
                return;
            }

            bool hasChanges =
                textBox1.Text != _selectedSupplier.SupplierName ||
                textBox2.Text != _selectedSupplier.PhoneNumber ||
                textBox3.Text != _selectedSupplier.Fax ||
                textBox4.Text != _selectedSupplier.MobileNumber ||
                textBox5.Text != _selectedSupplier.EmailAddress ||
                textBox6.Text != _selectedSupplier.Website;

            if (!hasChanges)
            {
                MessageBox.Show("No changes detected.");
                return;
            }

            var supplierToUpdate = dbcontext.Suppliers
                .Where(s => s.SupplierId == _selectedSupplier.SupplierId)
                .FirstOrDefault();

            if (supplierToUpdate != null)
            {
                supplierToUpdate.SupplierName = textBox1.Text;
                supplierToUpdate.PhoneNumber = textBox2.Text;
                supplierToUpdate.Fax = textBox3.Text;
                supplierToUpdate.MobileNumber = textBox4.Text;
                supplierToUpdate.EmailAddress = textBox5.Text;
                supplierToUpdate.Website = textBox6.Text;

                dbcontext.SaveChanges();

                MessageBox.Show("Supplier updated successfully.");
                ClearFields();
                LoadSuppliersIntoComboBox();
                RefreshSupplierGrid();
            }
            else
            {
                MessageBox.Show("Supplier not found.");
            }
        }
    }
}
