using InvoiceBusinessLayer;
using InvoiceBusinessLayer.Interfaces;
using InvoiceCommunicationLayer.Models;
using InvoiceDataLayer;
using InvoiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionstring = builder.Configuration.GetConnectionString("Development");

builder.Services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connectionstring, b => b.MigrationsAssembly("InvoiceCommunicationLayer")).EnableSensitiveDataLogging());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Queaso Services Methology", Version = "v1" });
});

builder.Services.AddTransient<IInvoiceExceptionRepository, InvoiceExceptionRepository>();
builder.Services.AddTransient<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
builder.Services.AddScoped<IInvoiceNumberRepository, InvoiceNumberRepository>();
builder.Services.AddTransient<IInvoiceUseCases, InvoiceUseCases>();

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

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