using CustomerDataLayer.DataModels;

namespace CustomerDataLayer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<DO_Customer>> GetAllCustomersAsync();

        Task<DO_Customer> GetCustomerByIdAsync(Guid id);

        Task<bool> DeleteCustomerByIdAsync(Guid id);

        Task<DO_Customer> CreateCustomerAsync(DO_Customer customerToCreate);

        Task<DO_Customer> UpdateCustomerAsync(DO_Customer customerToUpdate);
    }
}