﻿using ModuleCustomer.Interfaces;
using ModuleCustomer.Models;

namespace ModuleCustomer
{
    public class ModuleCustomerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(CustomerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataModel, DataModel>();

            containerRegistry.RegisterForNavigation<CustomerView>();
        }
    }
}