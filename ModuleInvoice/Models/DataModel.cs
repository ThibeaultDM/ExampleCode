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
            _client.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["OrchestrationUrl"];
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
    }
}