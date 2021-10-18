using AvalonDock.Layout;
using PrismAvalonDock.WpfSampleApp.Views;
using System.Windows;
using System.Windows.Controls;

namespace PrismAvalonDock.WpfSampleApp
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FileViewTemplate { get; set; }
        public DataTemplate NotesViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is NotesView)
                return NotesViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
