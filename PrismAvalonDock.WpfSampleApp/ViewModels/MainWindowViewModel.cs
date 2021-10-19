using AvalonDock.Themes;
using Prism.Mvvm;
using PrismAvalonDock.WpfSampleApp.Views;
using System.Collections.Generic;
using CoreModule;

namespace PrismAvalonDock.WpfSampleApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Notes";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Theme SelectedTheme => new Vs2013LightTheme();

        private List<IAnchorable> _anchorables = new List<IAnchorable> { new NotesView() };
        public List<IAnchorable> Anchorables
        {
            get => _anchorables;
            set => SetProperty(ref _anchorables, value);
        }

        public MainWindowViewModel()
        {
        }
    }
}
