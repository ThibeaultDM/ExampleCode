using AutoMapper;
using InvoiceBusinessLayer.BusinessObjects;
using InvoiceBusinessLayer.Interfaces;
using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;
using QueasoFramework.BusinessModels.Rules;
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
                throw new Exception("an error occured creating an invoiceHeaderBO");
            }

            return invoiceHeaderBo;
        }

        public async Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(Guid invoiceHeaderId, BO_InvoiceLine boInvoiceLine)
        {
            BO_InvoiceHeader invoiceHeaderBO = new BO_InvoiceHeader();
            invoiceHeaderBO.Id = invoiceHeaderId; // don't see the use of this TODO ask, line 75 asigns again

            invoiceHeaderBO = await UC_301_003_GetInvoiceByNameAsync(invoiceHeaderId);

            invoiceHeaderBO.AddInvoiceLineToHeader(boInvoiceLine);

            try
            {
                //here whyyyyyyyy

                var invoiceHeaderDO = _mapper.Map<BO_InvoiceHeader, DO_InvoiceHeader>(invoiceHeaderBO);

                //foreach (PropertyInfo prop in invoiceHeaderDO.GetType().GetProperties())
                //{
                //	invoiceHeaderBO.GetType().GetProperty(prop.Name).);
                //}

                await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDO);
            }
            catch (Exception)
            {
                throw new Exception("something went wrong updating the header in the database");
            }

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
            BO_InvoiceHeader toCheck = await UC_301_003_GetInvoiceByNameAsync(invoiceHeaderId);

            toCheck.ProxyIdCompany = journalEntryId;
            DO_InvoiceHeader toSave = _mapper.Map<DO_InvoiceHeader>(toCheck);

            await _headerRepository.UpdateInvoiceHeaderAsync(toSave);

            return toCheck;
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