using ModuleCustomer.Models.Response;

namespace ModuleCustomer.Interfaces;

public interface IDataModel
{
    List<CustomerResponse> Customers { get; }

    Task GetAllCustomersAsync();

    Task<CustomerDetailResponse> GetDetailResponseAsync(Guid id);
}