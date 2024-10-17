using Microsoft.OpenApi.Models;
using Orchestration.BusinessLayer;
using Orchestration.Interfaces;
using Orchestration.ObjectLayer;

var builder = WebApplication.CreateBuilder(args);

string _OrchestrationCorsPolicy = "_OrchestrationCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(_OrchestrationCorsPolicy, policy =>
    {
        policy.WithOrigins("https://localhost:5001") // Specify the Blazor app URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Only allow credentials if necessary
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

// Add URLs from command line args if provided
if (args.Length > 0)
{
    Console.WriteLine($"Using URL: {string.Join(", ", args)}");
    app.Urls.Add(args[1]);  // Ensure args[1] contains the correct URL
}

app.Run();