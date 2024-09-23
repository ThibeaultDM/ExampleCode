using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Training_UI;
using Training_UI.Interfaces;
using Training_UI.Models;
using Training_UI.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "https://localhost:7089/Orchestration/" });
builder.Services.AddTransient<IDataModel, DataModel>();
builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();

await builder.Build().RunAsync();
