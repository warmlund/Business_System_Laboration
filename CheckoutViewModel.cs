using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private float _totalPrice;
        private ObservableCollection<Product> _cartItems;
        public ObservableCollection<Product> CartItems { get { return _cartItems; } set { _cartItems = value; OnPropertyChanged(nameof(CartItems)); } }
        public float TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public Command ConfirmPurchase { get; private set; }
        public Command<Product> AddItemToCart { get; private set; }
        public Command<Product> RemoveItemFromCart { get; private set; }
        public Command<Product> IncreaseItemInCart { get; private set; }
        public Command<Product> DecreaseItemInCart { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CheckoutViewModel()
        {
            _totalPrice = 0;
            ConfirmPurchase = new Command(Checkout, CanPurchase);
            AddItemToCart = new Command<Product>(AddToCart, CanAddToCart);
            RemoveItemFromCart = new Command<Product>(RemoveFromCart, CanRemoveFromCart);
            IncreaseItemInCart = new Command<Product>(IncreaseItem, CanIncrease);
            DecreaseItemInCart = new Command<Product>(DecreaseItem, CanDecrease);
            _cartItems = new ObservableCollection<Product>();
        }

        public void Checkout()
        {
            return;  
        }

        public void RemoveFromCart(Product product)
        {
            return;
        }

        public void AddToCart(Product product)
        {
           CartItems.Add(product);
            TotalPrice+= product.Price; 
        }

        public void IncreaseItem(Product product)
        {
            return;
        }

        public void DecreaseItem(Product product)
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