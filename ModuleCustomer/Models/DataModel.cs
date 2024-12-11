using AutoMapper;
using CustomerBusinessLayer.BusinessModels;
using CustomerBusinessLayer.Interfaces;
using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System.Windows;

namespace ModuleCustomer.Models
{
    public class DataModel : IDataModel
    {
        public DataModel()
        {
        }

        public DataModel(ICustomerUseCases customerUseCases, IMapper mapper)
        {
            Console.WriteLine("DataModel constructor working");
            this._customerUseCases = customerUseCases;
            this._mapper = mapper;
        }

        private List<CustomerResponse> customers;
        private readonly ICustomerUseCases _customerUseCases;
        private readonly IMapper _mapper;

        public List<CustomerResponse> Customers
        {
            get { return customers; }
            private set { customers = value; }
        }

        public List<BO_Customer> CustomerBO { get; set; }

        public async Task GetAllCustomersAsync()
        {
            Console.WriteLine("GetAllCustomersAsync");

            bool tryAgain = true;
            int counter = 0;

            while (tryAgain)
            {
                try
                {
                    CustomerBO = await _customerUseCases.UC_300_002_GetAllCustomerAsync();

                    if (CustomerBO.Count > 0)
                    {
                        Customers = _mapper.Map<List<CustomerResponse>>(CustomerBO);
                    }
                    else
                    {
                        MessageBox.Show("There are no Customers");
                    }

                    tryAgain = false;
                }
                catch (Exception ex)
                {
                    counter++;

                    if (counter > 3)
                    {
                        MessageBox.Show("Restart application");
                        tryAgain = false;
                    }
                    else
                    {
                        if (ex.InnerException.Message != "No connection could be made because the target machine actively refused it. (localhost:7089)")
                        {
                            MessageBox.Show("An error occurred");
                        }
                    }
                }
            }
        }

        public async Task<CustomerDetailResponse> GetDetailResponseAsync(Guid id)
        {
            CustomerDetailResponse response = new();
            try
            {
                BO_Customer customer = await _customerUseCases.UC_300_003_GetCustomerByIdAsync(id);

                response = _mapper.Map<CustomerDetailResponse>(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("an error occurred getting the specific customer");
            }

            return response;
        }
    }
}