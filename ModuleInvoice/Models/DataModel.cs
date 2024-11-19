using Flurl;
using Flurl.Http;
using ModuleInvoice.Interfaces;
using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;

namespace ModuleInvoice.Models
{
    public class DataModel : IDataModel
    {
        private FlurlClient _client;

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("DataModel constructor working");
            _client = client;
            _client.BaseUrl = "http://localhost:7089/Orchestration/";
        }

        public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice)
        {
            Console.WriteLine("GetAllCustomersAsync");

            CustomerDetailResponse customerDetailResponse = new();
            customerDetailResponse = await _client.BaseUrl.AppendPathSegments("UC_200_002_SaveInvoiceForCustomer")
                                           .PostJsonAsync(createInvoice)
                                           .ReceiveJson<CustomerDetailResponse>();

            return customerDetailResponse;
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