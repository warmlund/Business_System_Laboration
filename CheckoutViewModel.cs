using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private int _totalPrice;
        private ObservableCollection<Product> _cartItems;
        public ObservableCollection<Product> CartItems { get { return _cartItems; } set { _cartItems = value; OnPropertyChanged(nameof(CartItems)); } }
        public int TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public Command ConfirmPurchase { get; private set; }
        public Command AddItemToCart { get; private set; }
        public Command RemoveItemFromCart { get; private set; }
        public Command IncreaseItemInCart { get; private set; }
        public Command DecreaseItemInCart { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CheckoutViewModel()
        {
            _totalPrice = 0;
            ConfirmPurchase = new Command(Checkout, CanPurchase);
            AddItemToCart = new Command(AddToCart, CanAddToCart);
            RemoveItemFromCart = new Command(RemoveFromCart, CanRemoveFromCart);
            IncreaseItemInCart = new Command(IncreaseItem, CanIncrease);
            DecreaseItemInCart = new Command(DecreaseItem, CanDecrease);
        }

        public void Checkout()
        {
            return;  
        }

        public void RemoveFromCart()
        {
            return;
        }

        public void AddToCart()
        {
            return;
        }

        public void IncreaseItem()
        {
            return;
        }

        public void DecreaseItem()
        {
            return;
        }

        public bool CanPurchase()
        {
            return true;
        }

        public bool CanAddToCart()
        {
            return true;
        }
        public bool CanRemoveFromCart()
        {
            return true;
        }

        public bool CanIncrease()
        {
            return true;
        }

        private bool CanDecrease()
        {
            return true;
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