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

            CreateMap<FrameworkException, DO_InvoiceException>().ReverseMap();
        }
    }
}