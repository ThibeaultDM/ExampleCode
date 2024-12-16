using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Controllers;

public class CustomerResponseEventArgs : EventArgs
{
    public List<CustomerResponse> Data { get; }

    public CustomerResponseEventArgs(List<CustomerResponse> data)
    {
        Data = data;
    }
}
