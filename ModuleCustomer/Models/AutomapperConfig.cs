using AutoMapper;
using CustomerBusinessLayer.BusinessModels;
using CustomerDataLayer.DataModels.Enums;
using CustomerDataLayer.DataModels;
using QueasoFramework.Exceptions;
using ModuleCustomer.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCustomer.Models
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //Address
            CreateMap<BO_Address, DO_Address>().ReverseMap();
            CreateMap<BO_Address, AddressResponse>().ReverseMap();

            //Customer
            CreateMap<BO_Customer, DO_Customer>().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses)).ReverseMap();
            CreateMap<BO_Customer, CustomerResponse>().ReverseMap();
            CreateMap<BO_Customer, CustomerDetailResponse>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));

            CreateMap<Gender, string>().ConvertUsing(src => src.ToString());

            CreateMap<FrameworkException, DO_CustomerException>().ReverseMap();

            CreateMap<BO_CreditInfo, CreditResponse>().ReverseMap();
            CreateMap<BO_CreditInfo, DO_CreditInfo>().ReverseMap();

            CreateMap<BO_Company, CompanyResponse>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));
            CreateMap<BO_Company, DO_Company>().ReverseMap().ForMember(desc => desc.Addresses, opt => opt.MapFrom(src => src.Addresses));
        }
    }
}