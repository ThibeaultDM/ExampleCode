using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Training_UI;
using Training_UI.Interfaces;
using Training_UI.Models;
using Training_UI.ViewModels;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "http://localhost:7089/Orchestration/" });
        builder.Services.AddTransient<IDataModel, DataModel>();
        builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();
        builder.Services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();

        await builder.Build().RunAsync();
    }
}