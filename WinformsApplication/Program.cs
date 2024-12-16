using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Controllers;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models;
using WinFormsApplication.Views;

namespace WinFormsApplication;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        try
        {
            ServiceCollection services = new();
            ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            using Form? customerView = serviceProvider.GetRequiredService<ICustomerView>() as Form;
            Application.Run(customerView);

        }
        catch (Exception ex)
        {
            string error = "";
            error = HandleException(error, ex);
            MessageBox.Show($"{error}");
        }
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddScoped(sp => new FlurlClient { BaseUrl = "http://localhost:7089/Orchestration/" });

        services.AddTransient<IDataModel, DataModel>();

        services.AddTransient<ICustomerController, CustomerController>();
        services.AddTransient<IAddInvoiceController, AddInvoiceController>();

        services.AddTransient<ICustomerView, CustomerView>();
        services.AddTransient<IAddInvoiceView, AddInvoiceView>();
    }
    private static string HandleException(string result, Exception ex)
    {
        return ex.InnerException != null ? HandleException(result, ex.InnerException) : result;
    }

}