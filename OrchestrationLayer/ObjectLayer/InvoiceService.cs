using Flurl;
using Flurl.Http;
using Orchestration.Interfaces;
using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.ObjectLayer;

public class InvoiceService : IInvoiceService
{
    private readonly FlurlClient _client;
    private readonly string invoicePathSeg = "Invoice";

    public InvoiceService(IQueasoBaseUrl url)
    {
        _client = new(url.BaseUrlBuilder(BaseUrlComponent.invoice));
    }

    public async Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
    {
        try
        {
            var result = await _client.BaseUrl
                                      .AppendPathSegments(invoicePathSeg, "UC_301_001_CreateInvoiceHeader")
                                      .PostJsonAsync(input)
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
                                      .AppendPathSegments(invoicePathSeg, "UC_301_002_AddInvoiceLineToHeader")
                                      .PatchJsonAsync(invoiceLineInput)
                                      .ReceiveJson<InvoiceResponse>();
            return result;
        }
        catch { throw; }
    }

    public async Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid getInvoiceByIdInput)
    {
        try
        {
            var result = await _client.BaseUrl
                                      .AppendPathSegments(invoicePathSeg, $"UC_301_003_GetInvoiceByName/{getInvoiceByIdInput}")
                                      .GetJsonAsync<InvoiceDetailResponse>();
            return result;
        }
        catch { throw; }
    }

    public async Task<ArchiveInvoiceJournalEntryResponse> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntry archiveInvoiceJournal)
    {
        try
        {
            var result = await _client.BaseUrl
                                      .AppendPathSegments(invoicePathSeg, "UC_301_004_ArchiveJournalEntryForInvoice")
                                      .PostJsonAsync(archiveInvoiceJournal)
                                      .ReceiveJson<ArchiveInvoiceJournalEntryResponse>();
            return result;
        }
        catch { throw; }
    }

    public async Task<List<InvoiceResponse>> UC_301_005_GetAllInvoicesAsync()
    {
        try
        {
            var result = await _client.BaseUrl
                                      .AppendPathSegments(invoicePathSeg, "UC_301_005_GetAllInvoices")
                                      .GetJsonAsync<List<InvoiceResponse>>();
            return result;
        }
        catch { throw; }
    }
}