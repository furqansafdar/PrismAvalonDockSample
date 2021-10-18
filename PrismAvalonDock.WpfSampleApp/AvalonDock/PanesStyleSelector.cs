using System.Windows;
using System.Windows.Controls;

namespace PrismAvalonDock.WpfSampleApp
{
    class PanesStyleSelector : StyleSelector
    {
        public Style DocumentStyle { get; set; }

        public Style AnchorableStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is IAnchorable)
                return AnchorableStyle;

            return base.SelectStyle(item, container);
        }
    }
}
