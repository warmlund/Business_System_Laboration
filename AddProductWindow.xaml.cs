using System.Windows;

namespace Business_System_Laboration_4
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow(AddProductViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
