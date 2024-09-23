using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Interfaces;
using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.ObjectLayer
{
    public class InvoiceService : IInvoiceService
    {
        private readonly FlurlClient _client;

        public InvoiceService(IQueasoBaseUrl url)
        {
            _client = new(url.BaseUrlBuilder(BaseUrlComponent.invoice));
        }

        public async Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Invoice", "CreateInvoiceHeader")
                                          .PostJsonAsync(new { VatNumber = vatNumber })
                                          .ReceiveJson<InvoiceDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Invoice", "AddInvoiceLineToInvoiceHeader")
                                          .PostJsonAsync(invoiceLineInput)
                                          .ReceiveJson<InvoiceResponse>();
                return result;
            }
            catch { throw; }
        }

        [HttpPost]
        public async Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid getInvoiceByIdInput)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Invoice", "GetInvoiceById")
                                          .PostJsonAsync(new { InvoiceHeaderId = getInvoiceByIdInput })
                                          .ReceiveJson<InvoiceDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<List<InvoiceResponse>> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntryInput archiveInvoiceJournal)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Invoice", "ArchiveInvoiceJournalEntry")
                                          .PostJsonAsync(archiveInvoiceJournal)
                                          .ReceiveJson<List<InvoiceResponse>>();
                return result;
            }
            catch { throw; }
        }

        public async Task<List<InvoiceResponse>> UC_301_005_GetAllInvoicesAsync()
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments("Invoice", "GetAllInvoices")
                                          .GetJsonAsync<List<InvoiceResponse>>();
                return result;
            }
            catch { throw; }
        }
    }
}