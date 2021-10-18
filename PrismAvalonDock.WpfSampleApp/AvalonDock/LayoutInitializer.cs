﻿using System.Linq;
using AvalonDock.Layout;

namespace PrismAvalonDock.WpfSampleApp
{
    class LayoutInitializer : ILayoutUpdateStrategy
    {
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            //AD wants to add the anchorable into destinationContainer
            //just for test provide a new anchorablepane 
            //if the pane is floating let the manager go ahead
            var destPane = destinationContainer as LayoutAnchorablePane;
            if (destinationContainer?.FindParent<LayoutFloatingWindow>() != null)
                return false;

            var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>()
                .FirstOrDefault(d => d.Name == "ToolsPane");

            if (toolsPane != null)
            {
                // do not allow this as Tabbed Document
                anchorableToShow.CanDockAsTabbedDocument = false;
                //if (anchorableToShow.IsAutoHidden)
                //    anchorableToShow.IsActive = false;
                toolsPane.Children.Add(anchorableToShow);
                //anchorableToShow.ToggleAutoHide();
                //anchorableToShow.Hide();
                return true;
            }

            return false;
        }

        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
        }
    }
}
