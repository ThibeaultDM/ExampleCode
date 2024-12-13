using CustomerCommunicationLayer.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCommunicationLayer.Interfaces;

public interface ICustomerController
{
    Task<IActionResult> CreateCustomerAsync(CreateCustomerInput customerToCreate);

    Task<IActionResult> GetAllCustomersAsync();

    Task<IActionResult> GetCustomerByIdAsync(GetCustomerByIdInput customerId);
}