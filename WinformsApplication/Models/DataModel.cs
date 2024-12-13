using Flurl;
using Flurl.Http;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Models;

public class DataModel : IDataModel
{
    private FlurlClient _client;

    public DataModel(FlurlClient client)
    {
        Console.WriteLine("DataModel constructor working");
        _client = client;
    }

    private List<CustomerResponse> customers;

    public List<CustomerResponse> Customers
    {
        get { return customers; }
        private set { customers = value; }
    }

    public async Task GetAllCustomersAsync()
    {
        Console.WriteLine("GetAllCustomersAsync");

        bool tryAgain = true;
        int counter = 0;

        while (tryAgain)
        {
            try
            {
                Customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();

                if (Customers.Count < 1)
                {
                    MessageBox.Show("There are no customers");
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

    public async Task<CustomerDetailResponse> GetCustomerAsync(string customerId)
    {
        Console.WriteLine("GetCustomerAsync");

        CustomerDetailResponse customer = await _client.BaseUrl.AppendPathSegments("UC_300_003_GetCustomerByName")
                                                               .SetQueryParam("customerId", customerId)
                                                               .PostJsonAsync(customerId)
                                                               .ReceiveJson<CustomerDetailResponse>();

        return customer;
    }

    public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice)
    {
        Console.WriteLine("GetAllCustomersAsync");

        CustomerDetailResponse customerDetailResponse = new();
        customerDetailResponse = await _client.BaseUrl.AppendPathSegments("UC_200_002_SaveInvoiceForCustomer")
                                       .PostJsonAsync(createInvoice)
                                       .ReceiveJson<CustomerDetailResponse>();

        return customerDetailResponse;
    }
}