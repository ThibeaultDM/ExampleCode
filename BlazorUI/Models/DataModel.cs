using Flurl;
using Flurl.Http;
using BlazorUI.Interfaces;
using BlazorUI.Models.Input;
using BlazorUI.Models.Response;

namespace BlazorUI.Models
{
    public class DataModel : IDataModel
    {
        private FlurlClient _client;

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("DataModel constructor working");
            _client = client;
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

        public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice)
        {
            Console.WriteLine("GetAllCustomersAsync");

            CustomerDetailResponse customerDetailResponse = new();
            customerDetailResponse = await _client.BaseUrl.AppendPathSegments("UC_300_004_ArchiveCustomerInvoice")
                                           .PostJsonAsync(createInvoice)
                                           .ReceiveJson<CustomerDetailResponse>();

            return customerDetailResponse;
        }
    }
}