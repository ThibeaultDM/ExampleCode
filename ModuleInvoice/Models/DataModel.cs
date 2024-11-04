using Flurl;
using Flurl.Http;
using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;

namespace ModuleInvoice.Models
{
    public class DataModel
    {
        private FlurlClient _client;

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("DataModel constructor working");
            _client = client;
            _client.BaseUrl = "https://localhost:7089/Orchestration/";
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