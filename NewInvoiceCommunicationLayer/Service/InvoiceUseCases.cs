using AutoMapper;
using NewInvoiceCommunicationLayer.Interfaces;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceServiceLayer.Objects;

namespace NewInvoiceCommunicationLayer.Service
{
    public class InvoiceUseCases : IInvoiceUseCases
    {
        private readonly IInvoiceHeaderRepository _headerRepository;
        private readonly IInvoiceNumberRepository _numberRepository;
        private readonly IInvoiceExceptionRepository _exceptionRepository;
        private readonly IMapper _mapper;

        public InvoiceUseCases(IInvoiceHeaderRepository headerRepository, IInvoiceNumberRepository numberRepository, IInvoiceExceptionRepository exceptionRepository, IMapper mapper)
        {
            _headerRepository = headerRepository;
            _numberRepository = numberRepository;
            _exceptionRepository = exceptionRepository;
            _mapper = mapper;
        }

        public Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            throw new NotImplementedException();
        }

        public Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(Guid invoiceHeaderId, BO_InvoiceLine boInvoiceLine)
        {
            throw new NotImplementedException();
        }

        public Task<BO_InvoiceHeader> UC_301_003_FindInvoiceHeaderAsync(Guid toFind)
        {
            throw new NotImplementedException();
        }

        public Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid companyProxyId, Guid invoiceHeaderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesHeadersAsync()
        {
            throw new NotImplementedException();
        }
    }
}