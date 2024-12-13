using CustomerBusinessLayer.BusinessModels;

namespace CustomerBusinessLayer.Interfaces;

public interface ICustomerUseCases
{
    Task<BO_Customer> UC_300_001_CreateCustomerAsync(BO_Customer customerToCreate);

    Task<List<BO_Customer>> UC_300_002_GetAllCustomerAsync();

    Task<BO_Customer> UC_300_003_GetCustomerByIdAsync(Guid id);
}