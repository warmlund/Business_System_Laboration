using System.Windows;

namespace Business_System_Laboration_4
{
    /// <summary>
    /// Interaction logic for RemoveProductWindow.xaml
    /// </summary>
    public partial class RemoveProductWindow : Window
    {
        public RemoveProductWindow(RemoveProductViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

        }
    }
}
