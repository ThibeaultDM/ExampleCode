using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDetailResponse> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate);

        Task<List<CustomerResponse>> UC_300_002_GetAllCustomerAsync();

        Task<CustomerDetailResponse> UC_300_003_GetCustomerByIdAsync(Guid id);
    }
}