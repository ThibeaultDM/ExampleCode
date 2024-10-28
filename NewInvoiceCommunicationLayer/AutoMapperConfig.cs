using AutoMapper;
using NewInvoiceCommunicationLayer.Models.Input;
using NewInvoiceCommunicationLayer.Models.Response;
using NewInvoiceDataLayer.Objects;
using NewInvoiceServiceLayer.Objects;
using QueasoFramework.Exceptions;

namespace NewInvoiceCommunicationLayer
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region Service objects

            CreateMap<BO_InvoiceHeader, DO_InvoiceHeader>().ReverseMap();
            CreateMap<BO_InvoiceLine, DO_InvoiceLine>().ReverseMap();

            #endregion Service objects

            #region Input models

            CreateMap<FrameworkException, DO_InvoiceException>();
            CreateMap<AddInvoiceLineToInvoiceHeaderInput, BO_InvoiceLine>();

            #endregion Input models

            #region Response models

            CreateMap<BO_InvoiceHeader, CreateInvoiceHeaderResponse>();

            #endregion Response models
        }
    }
}