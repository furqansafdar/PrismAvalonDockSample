using CoreModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace CoreModule
{
    public class CoreModule : IModule
    {
        protected readonly IRegionManager RegionManager;

        protected CoreModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterViewWithRegion<RuleMenuOptionItem>(RegionNames.MenuRegion);
            RegisterViewWithRegion<EntityMenuOptionItem>(RegionNames.MenuRegion);
            RegisterViewWithRegion<DataMenuOptionItem>(RegionNames.MenuRegion);

            RequestNavigate<ViewA>(RegionNames.TabRegion);
            RequestNavigate<ViewB>(RegionNames.TabRegion);
            RequestNavigate<ViewC>(RegionNames.TabRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<ViewA>();
            //containerRegistry.RegisterSingleton<ViewB>();
            //containerRegistry.RegisterSingleton<ViewC>(); 

            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<ViewC>();
        }
        
        protected void RegisterViewWithRegion<T>(string regionName) =>
            RegionManager.RegisterViewWithRegion(regionName, typeof(T));

        protected void RequestNavigate<T>(string regionName) =>
            RegionManager.RequestNavigate(regionName, new Uri(typeof(T).Name, UriKind.Relative));
    }

    public class RuleMenuOptionItem : HamburgerMenuItemBase
    {
        public RuleMenuOptionItem() : base("rules")
        {
            Glyph = "BitmapDefault48";
            Tag = typeof(ViewA);
        }
    }

    public class EntityMenuOptionItem : HamburgerMenuItemBase
    {
        public EntityMenuOptionItem() : base("entities")
        {
            Glyph = "BitmapEntity48";
            Tag = typeof(ViewB);
        }
    }

    public class DataMenuOptionItem : HamburgerMenuItemBase
    {
        public DataMenuOptionItem() : base("data")
        {
            Glyph = "BitmapDatabaseConnection48";
            Tag = typeof(ViewC);
        }
    }
}