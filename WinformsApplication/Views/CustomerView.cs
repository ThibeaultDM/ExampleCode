﻿using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Views
{
    public partial class CustomerView : Form, ICustomerView
    {
        private readonly ICustomerViewModel _customerViewModel;

        public CustomerView(ICustomerViewModel customerViewModel)
        {
            InitializeComponent();
            this._customerViewModel = customerViewModel;
        }

        private async void CustomerView_Load(object sender, EventArgs e)
        {
            try
            {
                await _customerViewModel.GetCustomersAsync();

                comboBoxCustomers.Items.Insert(0, "Select a customer");
                comboBoxCustomers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void comboBoxCustomers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomers.SelectedValue != null)
                _customerViewModel.SelectedCustomer = comboBoxCustomers.SelectedValue as CustomerResponse;

            if (_customerViewModel.SelectedCustomer != null)
            {
                textBoxFirstName.Text = _customerViewModel.SelectedCustomer.FirstName;
                textBoxLastName.Text = _customerViewModel.SelectedCustomer.FamilyName;
                textBoxGender.Text = _customerViewModel.SelectedCustomer.Gender;
            }
        }

        private void comboBoxCustomers_DropDown(object sender, EventArgs e)
        {
            comboBoxCustomers.DataSource = _customerViewModel.ListCustomers;
            buttonAddInvoice.Enabled = true;
        }

        private void buttonAddInvoice_Click(object sender, EventArgs e)
        {
        }

    }
}