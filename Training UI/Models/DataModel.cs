using Flurl;
using Flurl.Http;
using System.Net.Http.Json;
using Training_UI.Interfaces;
using Training_UI.Models.Input;
using Training_UI.Models.Response;

namespace Training_UI.Models
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


        public async Task FetchAllCustomersAsync()
        {
            Console.WriteLine("FetchAllCustomersAsync");

            customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();

        }

        public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice)
        {
            Console.WriteLine("FetchAllCustomersAsync");

            CustomerDetailResponse customerDetailResponse = new();
            customerDetailResponse = await _client.BaseUrl.AppendPathSegments("UC_300_004_ArchiveCustomerInvoice")
                                           .PostJsonAsync(new { CreateInvoiceInput = createInvoice })
                                           .ReceiveJson<CustomerDetailResponse>();

            return customerDetailResponse;
        }
    }
}
