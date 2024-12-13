using AutoMapper;
using CustomerBusinessLayer.BusinessModels;
using CustomerCommunicationLayer.Models.Input;
using CustomerCommunicationLayer.Models.Response;
using CustomerDataLayer.DataModels;
using CustomerDataLayer.DataModels.Enums;
using QueasoFramework.Exceptions;

namespace CustomerCommunicationLayer.Models;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        //Address
        CreateMap<BO_Address, DO_Address>().ReverseMap();
        CreateMap<BO_Address, AddressResponse>().ReverseMap();
        CreateMap<BO_Address, CustomerAddressInput>().ReverseMap();
        CreateMap<BO_Address, CreateAddressInput>().ReverseMap();

        //Customer
        CreateMap<BO_Customer, DO_Customer>().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses)).ReverseMap();
        CreateMap<CreateCustomerInput, BO_Customer>();
        CreateMap<BO_Customer, UpdateCustomerInput>().ReverseMap();
        CreateMap<BO_Customer, CreateCustomerResponse>().ReverseMap();
        CreateMap<BO_Customer, CustomerResponse>().ReverseMap();
        CreateMap<BO_Customer, GetCustomerByIdResponse>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));

        CreateMap<Gender, string>().ConvertUsing(src => src.ToString());

        CreateMap<FrameworkException, DO_CustomerException>().ReverseMap();

        CreateMap<BO_CreditInfo, CreditResponse>().ReverseMap();
        CreateMap<BO_CreditInfo, DO_CreditInfo>().ReverseMap();

        CreateMap<BO_Company, CreateCompanyInput>().ReverseMap();
        CreateMap<BO_Company, CompanyResponse>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));
        CreateMap<BO_Company, DO_Company>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));
    }
}