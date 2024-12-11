using AutoMapper;
using CustomerBusinessLayer.Interfaces;
using CustomerBusinessLayer.UseCases;
using CustomerDataLayer;
using CustomerDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModuleCustomer.Interfaces;
using ModuleCustomer.Models;

namespace ModuleCustomer
{
    public class ModuleCustomerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("CustomerRegion", typeof(CustomerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataModel, DataModel>();
            containerRegistry.RegisterForNavigation<CustomerDetailsView>();

            containerRegistry.Register<ICustomerRepository, CustomerRepository>();
            containerRegistry.Register<ICustomerExceptionRepository, CustomerExceptionRepository>();
            containerRegistry.Register<ICustomerUseCases, CustomerUseCases>();
            containerRegistry.RegisterInstance<IMapper>(CreateMapper());

            string connectionString;
            string localeDb = System.Configuration.ConfigurationManager.AppSettings["localeDb"];

            if (localeDb == "true")
            {
                connectionString = System.Configuration.ConfigurationManager.AppSettings["Development"];
            }
            else
            {
                connectionString = System.Configuration.ConfigurationManager.AppSettings["Live"];
            }

            containerRegistry.Register<CustomerDbContext>(provider =>
            {
                DbContextOptionsBuilder<CustomerDbContext> optionsBuilder = new();
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("CustomerCommunicationLayer")).EnableSensitiveDataLogging();
                return new CustomerDbContext(optionsBuilder.Options);
            });
        }

        private IMapper CreateMapper()
        {
            MapperConfiguration config = new(cfg => { cfg.AddProfile<AutomapperConfig>(); });

            return config.CreateMapper();
        }
    }
}