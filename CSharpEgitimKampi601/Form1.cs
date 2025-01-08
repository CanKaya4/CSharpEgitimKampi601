using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerCity = txtCustomerCity.Text,
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
            };

            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri Ekleme İşlemi Başarılır", "Uyarı", MessageBoxButtons.OK);

        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            var id = txtCustomerId.Text;
            customerOperations.DeleteCustomer(id);
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            var id = txtCustomerId.Text;
            var customer = new Customer()
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
            };
            customerOperations.CustomerUpdate(customer);
            
        }

        private void btnGetIdByCustomer_Click(object sender, EventArgs e)
        {
            var id = txtCustomerId.Text;
            var customer = customerOperations.GetCustomerById(id);
            dataGridView1.DataSource= new List<Customer> { customer};
        }
    }
}
