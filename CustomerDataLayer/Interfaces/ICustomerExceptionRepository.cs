using CustomerDataLayer.DataModels;

namespace CustomerDataLayer.Interfaces
{
    public interface ICustomerExceptionRepository
    {
        Task CreateExceptionAsync(DO_CustomerException exceptionToCreate);

        Task<List<DO_CustomerException>> GetAllExceptionsAsync();
    }
}