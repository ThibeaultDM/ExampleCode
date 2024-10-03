using AutoMapper;
using InvoiceBusinessLayer.BusinessObjects;
using InvoiceCommunicationLayer.Models.Input;
using InvoiceCommunicationLayer.Models.Response;
using InvoiceDataLayer.DataModels;
using QueasoFramework.Exceptions;

namespace InvoiceCommunicationLayer.Models
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BO_InvoiceHeader, DO_InvoiceHeader>().ReverseMap();
            CreateMap<BO_InvoiceLine, DO_InvoiceLine>().ReverseMap();

            CreateMap<FrameworkException, DO_InvoiceException>().ReverseMap();

            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderInput>().ReverseMap();
            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderResponse>().ReverseMap();
            CreateMap<BO_InvoiceLine, AddInvoiceLineToInvoiceHeaderInput>().ReverseMap();
        }
    }
}