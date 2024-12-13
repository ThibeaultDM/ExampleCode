using BlazorUI.Interfaces;
using BlazorUI.Models.Input;
using BlazorUI.Models.Response;
using Flurl;
using Flurl.Http;

namespace BlazorUI.Models;

public class DataModel : IDataModel
{
    private FlurlClient _client;

    public DataModel(FlurlClient client)
    {
        Console.WriteLine("DataModel constructor working");
        _client = client;
    }

    public List<CustomerResponse> Customers { get; private set; }

    public async Task GetAllCustomersAsync()
    {
        Console.WriteLine("GetAllCustomersAsync");

        Customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();
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