using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewInvoiceCommunicationLayer.Service;
using NewInvoiceDataLayer;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionstring = builder.Configuration.GetConnectionString("Development");

builder.Services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connectionstring, b => b.MigrationsAssembly("InvoiceCommunicationLayer")).EnableSensitiveDataLogging());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v2", new OpenApiInfo() { Title = "Queaso Services Methodology", Version = "v2" });
});

builder.Services.AddTransient<IInvoiceExceptionRepository, InvoiceExceptionRepository>();
builder.Services.AddTransient<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
builder.Services.AddScoped<IInvoiceNumberRepository, InvoiceNumberRepository>();

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