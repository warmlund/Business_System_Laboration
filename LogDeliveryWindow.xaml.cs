using System.Windows;

namespace Business_System_Laboration_4
{
    /// <summary>
    /// Interaction logic for LogDeliveryWindow.xaml
    /// </summary>
    public partial class LogDeliveryWindow : Window
    {
        public LogDeliveryWindow(LogDeliveryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
