using Flurl;
using Flurl.Http;
using Orchestration.Interfaces;
using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.ObjectLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly FlurlClient _client;

        public CustomerService(IQueasoBaseUrl url)
        {
            _client = new(url.BaseUrlBuilder(BaseUrlComponent.customer));
            // TODO SOLID principe
        }

        public async Task<CustomerDetailResponse> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Customer", "UC_300_001_CreateCustomer")
                                          .PostJsonAsync(customerToCreate)
                                          .ReceiveJson<CustomerDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<List<CustomerResponse>> UC_300_002_GetAllCustomerAsync()
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Customer", "UC_300_002_GetAllCustomers")
                                          .GetJsonAsync<List<CustomerResponse>>();
                return result;
            }
            catch { throw; }
        }

        public async Task<CustomerDetailResponse> UC_300_003_GetCustomerByIdAsync(Guid id)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Customer", "UC_300_003_GetCustomerById")
                                          .PostJsonAsync(new { Id = id })
                                          .ReceiveJson<CustomerDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<List<CustomerResponse>> UC_300_004_ArchiveCustomerInvoice(ArchiveCustomerInvoiceInput customerInput)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Customer", "UC_300_004_ArchiveCustomerInvoice")
                                          .PostJsonAsync(customerInput)
                                          .ReceiveJson<List<CustomerResponse>>();
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}