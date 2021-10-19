using System;
using AvalonDock;
using MahApps.Metro.Controls;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismAvalonDock.WpfSampleApp.Views;
using System.Windows;
using CoreModule;
using CoreModule.Views;

namespace PrismAvalonDock.WpfSampleApp
{
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
            containerRegistry.RegisterForNavigation<NotesView>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            //RequestNavigate<NotesView>(RegionNames.TabRegion);
        }

        protected void RequestNavigate<T>(string regionName) =>
            Container.Resolve<IRegionManager>().RequestNavigate(regionName, new Uri(typeof(T).Name, UriKind.Relative));

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<CoreModule.CoreModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            // register custom region adapters
            regionAdapterMappings.RegisterMapping(typeof(DockingManager), Container.Resolve<DockingManagerRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(HamburgerMenuItemCollection), Container.Resolve<HamburgerMenuItemCollectionRegionAdapter>());
        }
    }
}
