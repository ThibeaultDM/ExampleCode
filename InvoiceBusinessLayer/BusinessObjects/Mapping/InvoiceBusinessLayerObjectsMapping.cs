using AutoMapper;
using InvoiceDataLayer.DataModels;
using QueasoFramework.Exceptions;

namespace InvoiceBusinessLayer.BusinessObjects.Mapping
{
    public class InvoiceBusinessLayerObjectsMapping : Profile
    {
        // maps the related objects
        public InvoiceBusinessLayerObjectsMapping()
        {
            CreateMap<BO_InvoiceHeader, DO_InvoiceHeader>().ReverseMap();
            CreateMap<BO_InvoiceLine, DO_InvoiceLine>().ReverseMap();

            // TODO break point a method that uses this and look at what is does
            CreateMap<FrameworkException, DO_InvoiceException>()
                .ForMember(dest => dest.NameSpace, opt => opt.MapFrom(src => src.Namespace))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.UseCase))
                .ReverseMap();
        }
    }
}