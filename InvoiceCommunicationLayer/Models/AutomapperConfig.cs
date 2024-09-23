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

            // TODO break point a method that uses this and look at what is does
            CreateMap<FrameworkException, DO_InvoiceException>()
                .ForMember(dest => dest.NameSpace, opt => opt.MapFrom(src => src.Namespace))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.UseCase))
                .ReverseMap();

            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderInput>().ReverseMap();
            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderResponse>().ReverseMap();
            CreateMap<BO_InvoiceLine, AddInvoiceLineToInvoiceHeaderInput>().ReverseMap();
        }
    }
}