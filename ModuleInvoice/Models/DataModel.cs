using Flurl;
using Flurl.Http;
using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleInvoice.Models
{
    public class DataModel
    {
        private FlurlClient _client;

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("InvoiceDataModel constructor working");
            _client = client;
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
