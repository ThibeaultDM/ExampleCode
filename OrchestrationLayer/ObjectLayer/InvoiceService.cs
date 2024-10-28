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
        private readonly string invoicePathSeg = "Invoice";

        public InvoiceService(IQueasoBaseUrl url)
        {
            _client = new(url.BaseUrlBuilder(BaseUrlComponent.invoice));
        }

        public async Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments(invoicePathSeg, "CreateInvoiceHeader")
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
                                          .AppendPathSegments(invoicePathSeg, "AddInvoiceLineToInvoiceHeader")
                                          .PostJsonAsync(invoiceLineInput)
                                          .ReceiveJson<InvoiceResponse>();
                // TODO remove this
                if (result.Errors.Count == 0)
                {
                    result.Success = true;
                }

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
                                          .AppendPathSegments(invoicePathSeg, "GetInvoiceById")
                                          .PostJsonAsync(new { InvoiceHeaderId = getInvoiceByIdInput })
                                          .ReceiveJson<InvoiceDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<InvoiceDetailResponse> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntryInput archiveInvoiceJournal)
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments(invoicePathSeg, "ArchiveInvoiceJournalEntry")
                                          .PostJsonAsync(archiveInvoiceJournal)
                                          .ReceiveJson<InvoiceDetailResponse>();
                return result;
            }
            catch { throw; }
        }

        public async Task<List<InvoiceResponse>> UC_301_005_GetAllInvoicesAsync()
        {
            try
            {
                var result = await _client.BaseUrl
                                          .AppendPathSegments(invoicePathSeg, "GetAllInvoices")
                                          .GetJsonAsync<List<InvoiceResponse>>();
                return result;
            }
            catch { throw; }
        }
    }
}