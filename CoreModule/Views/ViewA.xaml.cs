namespace CoreModule.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : IDocument
    {
        public ViewA()
        {
            InitializeComponent();
        }

        public string Title { get; } = "View A";
    }
}
