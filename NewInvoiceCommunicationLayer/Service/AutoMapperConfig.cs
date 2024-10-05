using AutoMapper;
using NewInvoiceDataLayer.Objects;
using NewInvoiceServiceLayer.Objects;

namespace NewInvoiceCommunicationLayer.Service
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BO_InvoiceHeader, DO_InvoiceHeader>().ReverseMap();
            CreateMap<BO_InvoiceLine, DO_InvoiceLine>().ReverseMap();
        }
    }
}