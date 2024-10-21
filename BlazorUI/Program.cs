using BlazorUI.Components;
using BlazorUI.Interfaces;
using BlazorUI.ViewModels;
using BlazorUI.Models;
using Flurl.Http;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "http://localhost:7089/Orchestration/" });
builder.Services.AddTransient<IDataModel, DataModel>();
builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();
builder.Services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

bool OnServer = builder.Configuration.GetValue<bool>("OnServer");

if (OnServer)
{
    string url = builder.Configuration["Kestrel:Endpoints:MyHttpEndpoint:Url"] ?? throw new Exception("No url configured");
    app.Run(url);
}
else
{
    app.Run();
}
