﻿using System.Windows;

namespace WPF_UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<ModuleCustomer.ModuleCustomerModule>();
        moduleCatalog.AddModule<ModuleInvoice.ModuleInvoiceModule>();
    }
}