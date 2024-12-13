using AutoMapper;
using CustomerBusinessLayer.BusinessModels;
using CustomerBusinessLayer.Interfaces;
using CustomerDataLayer.DataModels;
using CustomerDataLayer.Interfaces;
using QueasoFramework.BusinessModels.Rules;
using QueasoFramework.Enums;
using QueasoFramework.Exceptions;

namespace CustomerBusinessLayer.UseCases;

public class CustomerUseCases : ICustomerUseCases
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICustomerExceptionRepository _exceptionRepository;

    public CustomerUseCases(ICustomerRepository customerRepository, IMapper mapper, ICustomerExceptionRepository exceptionRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _exceptionRepository = exceptionRepository;
    }

    public async Task<BO_Customer> UC_300_001_CreateCustomerAsync(BO_Customer customerToCreate)
    {
        try
        {
            if (customerToCreate.Valid)
            {
                DO_Customer doCustomer = _mapper.Map<DO_Customer>(customerToCreate);
                doCustomer = await _customerRepository.CreateCustomerAsync(doCustomer);

                customerToCreate = _mapper.Map<BO_Customer>(customerToCreate);
            }
            else if (customerToCreate.BusinessRules.Count > 0)
            {
                foreach (BrokenRule brokenRule in customerToCreate.BrokenRules)
                {
                    FrameworkException exception = new(customerToCreate, "UC_300_001_CreateCustomerAsync", "", FrameworkExceptionType.BusinessRuleViolation);
                    SaveCustomerException(exception);
                }
            }
            return customerToCreate;
        }
        catch (Exception ex)
        {
            FrameworkException exception = new("UC_300_001_CreateCustomerAsync", ex.Message, ex, FrameworkExceptionType.Error);
            SaveCustomerException(exception);

            throw exception;
        }
    }

    public async Task<List<BO_Customer>> UC_300_002_GetAllCustomerAsync()
    {
        try
        {
            List<DO_Customer> doCustomers = await _customerRepository.GetAllCustomersAsync();

            List<BO_Customer> boCustomers = _mapper.Map<List<BO_Customer>>(doCustomers);

            return boCustomers;
        }
        catch (Exception ex)
        {
            FrameworkException exception = new("UC_300_002_GetAllCustomerAsync", ex.Message, ex, FrameworkExceptionType.Error);
            SaveCustomerException(exception);

            throw exception;
        }
    }

    public async Task<BO_Customer> UC_300_003_GetCustomerByIdAsync(Guid id)
    {
        try
        {
            DO_Customer doCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            BO_Customer boCustomer = _mapper.Map<BO_Customer>(doCustomer);

            return boCustomer;
        }
        catch (Exception ex)
        {
            FrameworkException exception = new("UC_300_003_GetCustomerByIdAsync", ex.Message, ex, FrameworkExceptionType.Error);
            SaveCustomerException(exception);

            throw exception;
        }
    }

    private async Task SaveCustomerException(FrameworkException frameworkException)
    {
        DO_CustomerException doCustomerException = _mapper.Map<DO_CustomerException>(frameworkException);

        doCustomerException.IsDeleted = false;
        doCustomerException.DeletedBy = Environment.UserName;
        doCustomerException.DeletedOn = DateTime.Now;

        await _exceptionRepository.CreateExceptionAsync(doCustomerException);
    }
}