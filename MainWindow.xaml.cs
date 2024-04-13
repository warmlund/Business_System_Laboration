using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            _viewModelBase=new ViewModelBase();
            DataContext=_viewModelBase;

            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new Uri("Styles.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}