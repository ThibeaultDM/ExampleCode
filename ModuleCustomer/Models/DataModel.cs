using Flurl;
using Flurl.Http;
using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;

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
            _client = client;
            _client.BaseUrl = "http://localhost:7089/Orchestration/";
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

            customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();
        }

        public async Task<CustomerDetailResponse> GetCustomerAsync(string customerId)
        {
            Console.WriteLine("GetCustomerAsync");

            CustomerDetailResponse customer = await _client.BaseUrl.AppendPathSegments("UC_300_003_GetCustomerByName")
                                                                   .SetQueryParam("customerId", customerId)
                                                                   .PostJsonAsync(customerId)
                                                                   .ReceiveJson<CustomerDetailResponse>();
            return customer;
        }
    }
}