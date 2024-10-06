using AutoMapper;
using InvoiceBusinessLayer.BusinessObjects;
using InvoiceBusinessLayer.Interfaces;
using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;
using QueasoFramework.Exceptions;

namespace InvoiceBusinessLayer
{
    public class InvoiceUseCases : IInvoiceUseCases
    {
        // TODO comments

        private readonly IInvoiceHeaderRepository _headerRepository;
        private readonly IInvoiceNumberRepository _numberRepository;
        private readonly IInvoiceExceptionRepository _exceptionRepository;
        private readonly IMapper _mapper;

        public InvoiceUseCases(IMapper mapper)
        {
            _mapper = mapper;
        }

        public InvoiceUseCases(IInvoiceHeaderRepository invoiceHeaderRepository, IInvoiceNumberRepository invoiceNumberRepository, IInvoiceExceptionRepository invoiceExceptionRepository, IMapper mapper) : this(mapper)
        {
            _headerRepository = invoiceHeaderRepository;
            _numberRepository = invoiceNumberRepository;
            _exceptionRepository = invoiceExceptionRepository;
        }

        public async Task SaveInvoiceExceptionAsync(FrameworkException exception)
        {
            DO_InvoiceException invoiceException = _mapper.Map<DO_InvoiceException>(exception);

            await _exceptionRepository.CreateInvoiceExceptionsAsync(invoiceException);
            await _exceptionRepository.SaveAsync();
        }

        public async Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            BO_InvoiceHeader invoiceHeaderBo = new BO_InvoiceHeader(vatNumber);
            invoiceHeaderBo.IsPaid = false;
            invoiceHeaderBo.InvoiceNumber = new BO_InvoiceNumber(_numberRepository).LastUsedNumber;

            try
            {
                // logs its creation
                DO_InvoiceHeader invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBo);
                DO_InvoiceHeader result = await _headerRepository.CreateInvoiceHeaderAsync(invoiceHeaderDo);

                invoiceHeaderBo = _mapper.Map<BO_InvoiceHeader>(result);
            }
            catch (Exception)
            {
                throw new Exception("an error occurred creating an invoiceHeaderBO");
            }

            return invoiceHeaderBo;
        }

        public async Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(Guid invoiceHeaderId, BO_InvoiceLine boInvoiceLine)
        {
            BO_InvoiceHeader invoiceHeaderBO = new BO_InvoiceHeader();

            invoiceHeaderBO = await UC_301_003_GetInvoiceByNameAsync(invoiceHeaderId);

            invoiceHeaderBO.AddInvoiceLineToHeader(boInvoiceLine);

            var invoiceHeaderDO = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBO);

            try
            {
                invoiceHeaderDO = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDO, invoiceHeaderDO.InvoiceLines.Last());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDO);
            return invoiceHeaderBO;
        }

        /// <summary>
        /// Get the invoice header by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BO_InvoiceHeader> UC_301_003_GetInvoiceByNameAsync(Guid id)
        {
            // null check happens in repo

            DO_InvoiceHeader doInvoiceHeader = await _headerRepository.GetInvoiceHeaderAsync(id);

            BO_InvoiceHeader boInvoiceHeader = _mapper.Map<BO_InvoiceHeader>(doInvoiceHeader);

            return boInvoiceHeader;
        }

        // where do I find this ulm schematic
        public async Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid journalEntryId, Guid invoiceHeaderId)
        {
            BO_InvoiceHeader invoiceHeaderBO = await UC_301_003_GetInvoiceByNameAsync(invoiceHeaderId);

            invoiceHeaderBO.ProxyIdCompany = journalEntryId;
            DO_InvoiceHeader invoiceHeaderDO = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBO);

            invoiceHeaderDO = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDO);
            invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDO);

            return invoiceHeaderBO;
        }

        // where do I find this ulm schematic
        public async Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesAsync()
        {
            List<DO_InvoiceHeader> doInvoiceLines = await _headerRepository.GetInvoiceHeadersAsync();

            List<BO_InvoiceHeader> boInvoiceHeaders = _mapper.Map<List<DO_InvoiceHeader>, List<BO_InvoiceHeader>>(doInvoiceLines);

            return boInvoiceHeaders;
        }
    }
}