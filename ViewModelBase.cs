using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : Command, INotifyPropertyChanged
    {
        private List<Product> _products;

        public CustomerViewModel CustomerViewModel { get; }
        public StaffViewModel StaffViewModel { get; }

        public ViewModelBase()
        {
            CustomerViewModel = new CustomerViewModel();
            StaffViewModel = new StaffViewModel();
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void LoadProducts()
        {
        }
    }
}