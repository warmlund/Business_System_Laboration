using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class CustomerViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private int _totalPrice;
        private ObservableCollection<Product> _cartItems;
        public ObservableCollection<Product> CartItems { get { return _cartItems; } set { _cartItems = value; OnPropertyChanged(nameof(CartItems)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public CustomerViewModel()
        {
            throw new System.NotImplementedException();
        }

        public void Checkout()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFromCart()
        {
            throw new System.NotImplementedException();
        }

        public void AddToCart()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}