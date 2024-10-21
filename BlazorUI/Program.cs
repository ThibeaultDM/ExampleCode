using BlazorUI.Components;
using BlazorUI.Interfaces;
using BlazorUI.ViewModels;
using BlazorUI.Models;
using Flurl.Http;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "http://localhost:7089/Orchestration/" });
builder.Services.AddTransient<IDataModel, DataModel>();
builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();
builder.Services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();

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

app.Run();
