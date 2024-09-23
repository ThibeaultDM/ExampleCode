using AutoMapper;
using InvoiceBusinessLayer.BusinessObjects;
using InvoiceCommunicationLayer.Models.Input;

namespace InvoiceCommunicationLayer.Models.Mapping
{
    public class InvoiceCommunicationLayerObjectsMapping : Profile
    {
        public InvoiceCommunicationLayerObjectsMapping()
        {
            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderInput>().ReverseMap();

            CreateMap<BO_InvoiceLine, AddInvoiceLineToInvoiceHeaderInput>().ReverseMap(); // TODO doen't work don't understand why
        }
    }
}