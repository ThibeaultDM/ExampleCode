using BlazorUI.ViewModels;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models;
using WinFormsApplication.Views;

namespace WinFormsApplication
{
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

                using var customerView = serviceProvider.GetRequiredService<ICustomerView>() as Form;
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
            services.AddTransient<ICustomerViewModel, CustomerViewModel>();
            services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();
            services.AddTransient<ICustomerView, CustomerView>();
            services.AddTransient<IAddInvoiceView, AddInvoiceView>();
        }
        private static string HandleException(string result, Exception ex)
        {
            if (ex.InnerException != null)
                return HandleException(result, ex.InnerException);

            return result;
        }

    }
}