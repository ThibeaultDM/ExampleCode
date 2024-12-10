using AutoMapper;
using CustomerBusinessLayer.BusinessModels;
using CustomerBusinessLayer.Interfaces;
using CustomerCommunicationLayer.Interfaces;
using CustomerCommunicationLayer.Models.Input;
using CustomerCommunicationLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using QueasoFramework.BusinessModels.Rules;

namespace CustomerCommunicationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller, ICustomerController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerUseCases _customerUseCases;

        public CustomerController(IMapper mapper, ICustomerUseCases customerUseCases)
        {
            _mapper = mapper;
            _customerUseCases = customerUseCases;
        }

        [HttpPost("UC_300_001_CreateCustomer")]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerInput customerToCreate)
        {
            CreateCustomerResponse response = new();

            try
            {
                BO_Customer boCustomer = new();
                boCustomer = _mapper.Map<BO_Customer>(customerToCreate);

                if (boCustomer.Valid)
                {
                    boCustomer.CreditInfo = new BO_CreditInfo();
                    boCustomer = await _customerUseCases.UC_300_001_CreateCustomerAsync(boCustomer);
                    response = _mapper.Map<CreateCustomerResponse>(boCustomer);
                }
                else
                {
                    foreach (BrokenRule brokenRule in boCustomer.BrokenRules)
                    {
                        response.Errors.Add(new ErrorResponse()
                        {
                            ErrorMessage = brokenRule.FailedMessage,
                            PropertyName = brokenRule.PropertyName
                        });
                    }
                }

            }
            catch (Exception ex) when (ex.InnerException.Message == "Requested value 'string' was not found.")
            {
                response.Errors.Add(new() { ErrorMessage = "Gender needs to be Female, Male or Unknown" });
            }
            catch (Exception ex)
            {
                response = HandleException(response, ex);
                throw;
            }

            return Ok(response);
        }

        [HttpGet("UC_300_002_GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            List<BO_Customer> result = await _customerUseCases.UC_300_002_GetAllCustomerAsync();
            List<CustomerResponse> response = new();

            if (result.Count < 1)
            {
                CustomerResponse toAdd = new();
                toAdd.Errors.Add(new() { ErrorMessage = "No Customers Found" });

                response.Add(toAdd);
            }
            else
            {
                response = _mapper.Map<List<CustomerResponse>>(result);
            }

            return Ok(response);
        }

        [HttpPost("UC_300_003_GetCustomerById")]
        public async Task<IActionResult> GetCustomerByIdAsync(GetCustomerByIdInput customerId)
        {
            BO_Customer result = await _customerUseCases.UC_300_003_GetCustomerByIdAsync(customerId.Id);
            GetCustomerByIdResponse response = _mapper.Map<GetCustomerByIdResponse>(result);

            if (response == null)
            {
                response = new GetCustomerByIdResponse();
                response = HandleException(response, new Exception("UC_300_003_GetCustomerByIdAsync,noCustomerWithThisName"));
            }

            return Ok(response);
        }

        private T HandleException<T>(T response, Exception ex) where T : BaseResponse
        {
            if (ex.InnerException != null)
            {
                return HandleException<T>(response, ex.InnerException);
            }
            else
            {
                string[] errorMessage = ex.Message.Split(",");


                response.Errors.Add(new ErrorResponse()
                {
                    ErrorMessage = errorMessage[0],
                    PropertyName = errorMessage[1]
                });

                response.Success = false;
                return response;
            }
        }
    }
}