using Flurl;
using Flurl.Http;
using System.Net.Http.Json;
using Training_UI.Interfaces;
using Training_UI.Models.Response;

namespace Training_UI.Models
{
    public class CustomerModel : ICustomerModel
    {
        private FlurlClient _client;

        public CustomerModel(FlurlClient client)
        {
            Console.WriteLine("CustomerModel constructor working");
            _client = client;
        }

        private List<CustomerResponse> customers;

        public List<CustomerResponse> Customers
        {
            get { return customers; }
            private set { customers = value; }
        }


        public async Task FetchAllCustomersAsync()
        {
            Console.WriteLine("FetchAllCustomersAsync");

            customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();

        }
    }
}
