using AvalonDock;
using CoreModule;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PrismAvalonDock.WpfSampleApp
{
    //https://github.com/alfredoperez/Central/blob/master/Central.Infrastructure/Behaviors/DockingManagerDocumentsSourceSyncBehavior.cs
    internal class AvalonDockSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        public static readonly string BehaviorKey = "DockingManagerDocumentsSourceSyncBehavior";
        private bool _updatingActiveViewsInManagerActiveContentChanged;
        private DockingManager _dockingManager;

        public DependencyObject HostControl
        {
            get => _dockingManager;
            set => _dockingManager = value as DockingManager;
        }

        private readonly ObservableCollection<object> _documents = new ObservableCollection<object>();
        private ReadOnlyObservableCollection<object> _readonlyDocumentsList;
        public ReadOnlyObservableCollection<object> Documents =>
            _readonlyDocumentsList ?? (_readonlyDocumentsList = new ReadOnlyObservableCollection<object>(_documents));

        private readonly ObservableCollection<object> _anchorables = new ObservableCollection<object>();
        private ReadOnlyObservableCollection<object> _readonlyAnchorablesList;
        public ReadOnlyObservableCollection<object> Anchorables =>
            _readonlyAnchorablesList ?? (_readonlyAnchorablesList = new ReadOnlyObservableCollection<object>(_anchorables));

        /// <summary>
        /// Starts to monitor the <see cref="IRegion"/> to keep it in synch with the items of the <see cref="HostControl"/>.
        /// </summary>
        protected override void OnAttach()
        {
            var itemsSourceIsSet = _dockingManager.DocumentsSource != null;

            if (itemsSourceIsSet)
                throw new InvalidOperationException();

            SynchronizeItems();

            _dockingManager.ActiveContentChanged += ManagerActiveContentChanged;
            Region.ActiveViews.CollectionChanged += ActiveViewsCollectionChanged;
            Region.Views.CollectionChanged += ViewsCollectionChanged;
        }

        private void SynchronizeItems()
        {
            BindingOperations.SetBinding(_dockingManager, DockingManager.DocumentsSourceProperty,
                new Binding("Documents") { Source = this });

            BindingOperations.SetBinding(_dockingManager, DockingManager.AnchorablesSourceProperty,
                new Binding("Anchorables") { Source = this });

            foreach (FrameworkElement view in Region.Views)
            {
                if (view is IAnchorable)
                    _anchorables.Add(view);
                else
                    _documents.Add(view);
            }
        }

        private void ViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var startIndex = e.NewStartingIndex;
                    foreach (FrameworkElement newItem in e.NewItems)
                        if (newItem is IAnchorable)
                            _anchorables.Add(newItem);
                        else
                            _documents.Add(newItem);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (FrameworkElement oldItem in e.OldItems)
                        if (oldItem is IAnchorable)
                            _anchorables.Remove(oldItem);
                        else
                            _documents.Remove(oldItem);
                    break;
            }
        }

        private void ActiveViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_updatingActiveViewsInManagerActiveContentChanged)
                return;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (_dockingManager.ActiveContent != null
                    && _dockingManager.ActiveContent != e.NewItems[0]
                    && Region.ActiveViews.Contains(_dockingManager.ActiveContent))
                {
                    Region.Deactivate(_dockingManager.ActiveContent);
                }

                _dockingManager.ActiveContent = e.NewItems[0];
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove &&
                     e.OldItems.Contains(_dockingManager.ActiveContent))
            {
                _dockingManager.ActiveContent = null;
            }
        }

        private void ManagerActiveContentChanged(object sender, EventArgs e)
        {
            try
            {
                _updatingActiveViewsInManagerActiveContentChanged = true;

                if (_dockingManager == sender)
                {
                    var activeContent = _dockingManager.ActiveContent;
                    foreach (var item in Region.ActiveViews.Where(it => it != activeContent))
                    {
                        if (!_documents.Contains(item)) // added this line to lose the main document view
                            Region.Deactivate(item);
                    }

                    if (Region.Views.Contains(activeContent) && !Region.ActiveViews.Contains(activeContent))
                        Region.Activate(activeContent);
                }
            }
            finally
            {
                _updatingActiveViewsInManagerActiveContentChanged = false;
            }
        }
    }
}