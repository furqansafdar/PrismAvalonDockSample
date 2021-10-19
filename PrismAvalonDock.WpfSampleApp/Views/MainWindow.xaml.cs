using CoreModule;
using MahApps.Metro.Controls;
using Prism.Regions;
using System;

namespace PrismAvalonDock.WpfSampleApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;

            //RegionManager.SetRegionName(HamburgerMenuItemCollection, RegionNames.MenuRegion);
            //RegionManager.SetRegionManager(HamburgerMenuItemCollection, _regionManager);

            RegionManager.SetRegionName(DockingManager, RegionNames.TabRegion);
            RegionManager.SetRegionManager(DockingManager, _regionManager);
        }

        private void MetroWindow_ContentRendered(object sender, System.EventArgs e)
        {
            if (HamburgerMenu.Items.Count > 0)
            {
                // Ideally this piece of logic should execute after module initialization
                // HACK to load the content for first item
                LoadContent((HamburgerMenuGlyphItem)HamburgerMenu.Items[0]);
            }
        }

        private void HamburgerMenu_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            //https://stackoverflow.com/questions/7862824/activating-views-in-regions-in-prism
            //HamburgerMenu.Content = e.InvokedItem;
            if (e.InvokedItem is HamburgerMenuGlyphItem menuGlyphItem)
                LoadContent(menuGlyphItem);
        }

        private void LoadContent(HamburgerMenuGlyphItem option)
        {
            var viewType = (Type)option.Tag;
            var viewName = viewType.Name;
            var region = _regionManager.Regions[RegionNames.TabRegion];
            region?.RequestNavigate(viewName, NavigationCallback);

            //var view = AppWide.Ioc.Resolve(viewType);
            //if (view is FrameworkElement fe)
            //    _ribbonMainTab.DataContext = fe.DataContext;
        }

        private void NavigationCallback(NavigationResult obj)
        {

        }
    }
}
