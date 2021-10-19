namespace CoreModule.Views
{
    /// <summary>
    /// Interaction logic for ViewC.xaml
    /// </summary>
    public partial class ViewC : IDocument
    {
        public ViewC()
        {
            InitializeComponent();
        }

        public string Title { get; } = "View C";
    }
}
