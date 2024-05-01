using System.Windows;

namespace Business_System_Laboration_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelBase _viewModelBase;
        public MainWindow()
        {
            InitializeComponent();
            _viewModelBase = new ViewModelBase();
            DataContext = _viewModelBase;

        }
    }
}
