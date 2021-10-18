using MahApps.Metro.Controls;
using Prism.Regions;
using System.Collections.Specialized;

namespace PrismAvalonDock.WpfSampleApp
{
    //https://stackoverflow.com/questions/45154263/mahapps-prism-hamburgermenu
    public class HamburgerMenuItemCollectionRegionAdapter : RegionAdapterBase<HamburgerMenuItemCollection>
    {       
        public HamburgerMenuItemCollectionRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, HamburgerMenuItemCollection regionTarget)
        {
            region.ActiveViews.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    foreach (HamburgerMenuItem element in e.NewItems)
                        regionTarget.Add(element);
            };
        }

        protected override IRegion CreateRegion() => new AllActiveRegion();
    }
}
