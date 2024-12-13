using BlazorUI.Components;
using BlazorUI.Interfaces;
using BlazorUI.Models;
using BlazorUI.ViewModels;
using Flurl.Http;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped(sp => new FlurlClient { BaseUrl = "http://localhost:7089/Orchestration/" });
        builder.Services.AddTransient<IDataModel, DataModel>();
        builder.Services.AddTransient<ICustomerViewModel, CustomerViewModel>();
        builder.Services.AddTransient<IAddInvoiceViewModel, AddInvoiceViewModel>();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/Error", createScopeForErrors: true);

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
    }
}