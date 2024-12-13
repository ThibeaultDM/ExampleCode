using CustomerBusinessLayer.Interfaces;
using CustomerBusinessLayer.UseCases;
using CustomerCommunicationLayer.Models;
using CustomerDataLayer;
using CustomerDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString;
        bool localeDb = builder.Configuration.GetValue<bool>("LocaleDb");

        connectionString = localeDb
            ? builder.Configuration.GetConnectionString("Development") ?? throw new Exception("Connection string not found")
            : builder.Configuration.GetConnectionString("Live") ?? throw new Exception("Connection string not found");

        builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CustomerCommunicationLayer")).EnableSensitiveDataLogging());

        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddTransient<ICustomerExceptionRepository, CustomerExceptionRepository>();
        builder.Services.AddTransient<ICustomerUseCases, CustomerUseCases>();
        builder.Services.AddAutoMapper(typeof(AutomapperConfig));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

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

        // Add URLs from command line args if provided
        if (args.Length > 0)
        {
            Console.WriteLine($"Using URL: {string.Join(", ", args)}");
            app.Urls.Add(args[1]);  // Ensure args[1] contains the correct URL
        }

        string url = builder.Configuration["Kestrel:Endpoints:MyHttpEndpoint:Url"] ?? throw new Exception("No url configured");

        app.Run(url);
    }
}