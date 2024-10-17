using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Training_UI;
using Training_UI.Interfaces;
using Training_UI.Models;
using Training_UI.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "https://0.0.0.0:7089/Orchestration/" });
builder.Services.AddTransient<IDataModel, DataModel>();
builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();
builder.Services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();

await builder.Build().RunAsync();