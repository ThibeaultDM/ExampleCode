using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewInvoiceCommunicationLayer;
using NewInvoiceDataLayer;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Repositories;
using NewInvoiceServiceLayer.Interfaces;
using NewInvoiceServiceLayer.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connectionString;
        bool localeDb = builder.Configuration.GetValue<bool>("LocaleDb");

        if (localeDb)
        {
            connectionString = builder.Configuration.GetConnectionString("Development") ?? throw new Exception("Connection string not found");
        }
        else
        {
            connectionString = builder.Configuration.GetConnectionString("Live") ?? throw new Exception("Connection string not found");
        }

        builder.Services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("NewInvoiceCommunicationLayer")).EnableSensitiveDataLogging());

        // AddAsync services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Queaso Services Methodology", Version = "v1" });
        });

        builder.Services.AddScoped<IInvoiceDbContext, InvoiceDbContext>();
        builder.Services.AddTransient<IInvoiceExceptionRepository, InvoiceExceptionRepository>();
        builder.Services.AddTransient<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
        builder.Services.AddTransient<IJournalEntryRepository, JournalEntryRepository>();
        builder.Services.AddScoped<IInvoiceNumberRepository, InvoiceNumberRepository>();

        builder.Services.AddTransient<IInvoiceUseCases, InvoiceUseCases>();

        builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

        var app = builder.Build();

        bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");
        // Configure the HTTP request pipeline.
        if (enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        string url = builder.Configuration["Kestrel:Endpoints:MyHttpEndpoint:Url"] ?? throw new Exception("No url configured");

        app.Run(url);
    }
}