using CustomerCommunicationLayer.Controllers;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System.Windows;

namespace ModuleCustomer.Models
{
    public class DataModel : IDataModel
    {
        private FlurlClient _client;

        public DataModel()
        {
        }

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("DataModel constructor working");
        }

        private List<CustomerResponse> customers;

        public List<CustomerResponse> Customers
        {
            get { return customers; }
            private set { customers = value; }
        }

        public async Task GetAllCustomersAsync()
        {
            Console.WriteLine("GetAllCustomersAsync");

            bool tryAgain = true;

            while (tryAgain)
            {
                try
                {
                    //TODO fix this
                    // I'm trying to startup the entire Customer component so I just need to do the call
                    using (var context = CustomerCommunicationLayer.Program)
                    {
                        CustomerController customerController = new CustomerController(context);

                        await customerController.GetAllCustomersAsync();
                        var test = await customerController.GetAllCustomersAsync() as OkObjectResult;

                        customers = (List<CustomerResponse>)test.Value;
                        tryAgain = false;

                    }
                }
                catch (Exception ex)
                {

                    if (ex.InnerException.Message != "No connection could be made because the target machine actively refused it. (localhost:7089)")
                    {
                        MessageBox.Show("An error occurred");
                    }
                }
            }
        }
    }
}

