using ModuleInvoice.Interfaces;
using ModuleInvoice.Models;

namespace ModuleInvoice;

public class ModuleInvoiceModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("InvoiceRegion", typeof(InvoiceView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IDataModel, DataModel>();

        containerRegistry.RegisterForNavigation<InvoiceView>();
    }
}