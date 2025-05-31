using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class CustomerForm: Form
    {
        private WarehouseDbContext dbcontext;
        private Customer _selectedCustomer;
        public CustomerForm()
        {
            InitializeComponent();
            dbcontext = new WarehouseDbContext();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomersIntoComboBox();
            RefreshCustomerGrid();
            ClearFields();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
            _selectedCustomer = null;
        }

        private void RefreshCustomerGrid()
        {
            dataGridView1.DataSource = dbcontext.Customers.Select(c => new
            {
                c.CustomerId,
                c.CustomerName,
                c.PhoneNumber,
                c.Fax,
                c.MobileNumber,
                c.EmailAddress,
                c.Website
            }).ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void LoadCustomersIntoComboBox()
        {
            comboBox1.DataSource = dbcontext.Customers.ToList();
            comboBox1.DisplayMember = "CustomerName";
            comboBox1.ValueMember = "CustomerId";
            comboBox1.SelectedIndex = -1;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }

            _selectedCustomer = comboBox1.SelectedItem as Customer;

            if (_selectedCustomer != null)
            {
                textBox1.Text = _selectedCustomer.CustomerName;
                textBox2.Text = _selectedCustomer.PhoneNumber;
                textBox3.Text = _selectedCustomer.Fax;
                textBox4.Text = _selectedCustomer.MobileNumber;
                textBox5.Text = _selectedCustomer.EmailAddress;
                textBox6.Text = _selectedCustomer.Website;
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                Customer newCustomer = new Customer
                {
                    CustomerName = textBox1.Text,
                    PhoneNumber = textBox2.Text,
                    Fax = textBox3.Text,
                    MobileNumber = textBox4.Text,
                    EmailAddress = textBox5.Text,
                    Website = textBox6.Text
                };

                dbcontext.Customers.Add(newCustomer);
                dbcontext.SaveChanges();

                MessageBox.Show("Customer added successfully.");
                LoadCustomersIntoComboBox();
                RefreshCustomerGrid();
                ClearFields();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("Please select a customer to update.");
                return;
            }

            bool hasChanges =
                textBox1.Text != _selectedCustomer.CustomerName ||
                textBox2.Text != _selectedCustomer.PhoneNumber ||
                textBox3.Text != _selectedCustomer.Fax ||
                textBox4.Text != _selectedCustomer.MobileNumber ||
                textBox5.Text != _selectedCustomer.EmailAddress ||
                textBox6.Text != _selectedCustomer.Website;

            if (!hasChanges)
            {
                MessageBox.Show("No changes detected.");
                return;
            }

            var customerToUpdate = dbcontext.Customers
                .Where(c => c.CustomerId == _selectedCustomer.CustomerId)
                .FirstOrDefault();

            if (customerToUpdate != null)
            {
                customerToUpdate.CustomerName = textBox1.Text;
                customerToUpdate.PhoneNumber = textBox2.Text;
                customerToUpdate.Fax = textBox3.Text;
                customerToUpdate.MobileNumber = textBox4.Text;
                customerToUpdate.EmailAddress = textBox5.Text;
                customerToUpdate.Website = textBox6.Text;

                dbcontext.SaveChanges();

                MessageBox.Show("Customer updated successfully.");
                LoadCustomersIntoComboBox();
                RefreshCustomerGrid();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Customer not found.");
            }
        }
    }
}
