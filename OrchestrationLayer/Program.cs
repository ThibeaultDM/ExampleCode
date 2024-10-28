using Microsoft.OpenApi.Models;
using Orchestration.BusinessLayer;
using Orchestration.Interfaces;
using Orchestration.ObjectLayer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string _OrchestrationCorsPolicy = "_OrchestrationCorsPolicy";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(_OrchestrationCorsPolicy, policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Add services to the container.
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddSingleton<IQueasoBaseUrl, QueasoBaseUrl>();
        builder.Services.AddTransient<ICustomerService, CustomerService>();
        builder.Services.AddTransient<IInvoiceService, InvoiceService>();
        builder.Services.AddScoped<IOrchestrationService, OrchestrationBusinessLayer>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
            opt.SwaggerDoc("OrchestrationService", new OpenApiInfo
            {
                Title = "Orchestration Service",
                Version = "v1"
            })
          );

        var app = builder.Build();

        bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");
        // Configure the HTTP request pipeline.
        if (enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/OrchestrationService/swagger.json", "Orchestration Service");
            });
        }

        app.UseHttpsRedirection();
        app.UseCors(_OrchestrationCorsPolicy);
        app.UseAuthorization();

        app.MapControllers();

        string url = builder.Configuration["Kestrel:Endpoints:MyHttpEndpoint:Url"] ?? throw new Exception("No url configured");

        app.Run(url);
    }
}