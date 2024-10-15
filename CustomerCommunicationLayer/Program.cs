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
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        string connectionstring = builder.Configuration.GetConnectionString("Development");

        builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionstring, b => b.MigrationsAssembly("CustomerCommunicationLayer")).EnableSensitiveDataLogging());

        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddTransient<ICustomerExceptionRepository, CustomerExceptionRepository>();
        builder.Services.AddTransient<ICustomerUseCases, CustomerUseCases>();
        builder.Services.AddAutoMapper(typeof(AutomapperConfig));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}