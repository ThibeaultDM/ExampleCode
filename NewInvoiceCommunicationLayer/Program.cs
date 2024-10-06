using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewInvoiceCommunicationLayer.Interfaces;
using NewInvoiceCommunicationLayer.Service;
using NewInvoiceDataLayer;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Development") ?? throw new Exception("Connection string not found");

builder.Services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("NewInvoiceCommunicationLayer")).EnableSensitiveDataLogging());

// Add services to the container.

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
builder.Services.AddScoped<IInvoiceNumberRepository, InvoiceNumberRepository>();

builder.Services.AddTransient<IInvoiceUseCases, InvoiceUseCases>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

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