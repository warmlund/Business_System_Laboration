using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using Business_System_Laboration_4.BaseClasses;
using Business_System_Laboration_4.Products;

namespace Business_System_Laboration_4
{
    public class ShoppingCart : NotifyPropertyChangedBase
    {
        private float _totalPrice;
        private int _totalQuantity;
        private ObservableCollection<Product> _cartItems;

        public ObservableCollection<Product> CartItems { get { return _cartItems; } set { if (_cartItems != value) { if (_cartItems != null) { _cartItems.CollectionChanged -= CartItems_CollectionChanged; UnsubscribeFromProductPropertyChanged(); } _cartItems = value; if (_cartItems != null) { _cartItems.CollectionChanged += CartItems_CollectionChanged; SubscribeToProductPropertyChanged(); } OnPropertyChanged(nameof(CartItems)); } } }
        public float TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public int TotalQuantity { get => _totalQuantity; set { _totalQuantity = value; OnPropertyChanged(nameof(TotalQuantity)); } }

        public ShoppingCart()
        {
            _cartItems = new ObservableCollection<Product>();
            _cartItems.CollectionChanged += CartItems_CollectionChanged;
            SubscribeToProductPropertyChanged();
            CalculateTotalPriceAndQuantity();
        }

        private void CalculateTotalPriceAndQuantity()
        {
            float totalPrice = 0f;
            int totalQuantity = 0;

            foreach (Product product in CartItems)
            {
                totalQuantity += product.CartQuantity;
                totalPrice += product.Price * product.CartQuantity;
            }

            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
        }

        public static int CheckProductCountInCart(Product product)
        {
            return product.CartQuantity;
        }

        public void AddToCart(Product product)
        {
            if (product.CartQuantity == 0)
                CartItems.Add(product);

            product.CartQuantity++;
            product.DecreaseStock(1);
            CalculateTotalPriceAndQuantity();
        }

        public void RemoveFromCart(Product product)
        {
            product.CartQuantity--;
            product.IncreaseStock(1);

            if (product.CartQuantity == 0)
                CartItems.Remove(product);

            CalculateTotalPriceAndQuantity();
        }

        public void CheckoutCart()
        {
            MessageBox.Show("Tack för ditt köp!", "Köp genomfört", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            ClearCart();
        }

        private void ClearCart()
        {
            foreach (Product product in CartItems)
            {
                product.CartQuantity = 0;
            }

            CartItems.Clear();
            TotalPrice = 0;
            TotalQuantity = 0;
        }

        public void ReturnItemsToStock()
        {
            if(CartItems.Count > 0)
            {
                foreach (Product product in CartItems)
                {
                    product.IncreaseStock(product.CartQuantity); ;
                }

                ClearCart();
            }
        }
        internal bool IsCartEmpty()
        {
            if (CartItems.Count == 0)
                return true;
            return false;
        }

        private void CartItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CalculateTotalPriceAndQuantity();
            OnPropertyChanged(nameof(CartItems));
        }

        private void SubscribeToProductPropertyChanged()
        {
            foreach (Product product in CartItems)
            {
                product.PropertyChanged += Product_PropertyChanged;
            }
        }

        private void UnsubscribeFromProductPropertyChanged()
        {
            foreach (Product product in CartItems)
            {
                product.PropertyChanged -= Product_PropertyChanged;
            }
        }

        private void Product_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Product.Amount))
            {
                OnPropertyChanged(nameof(CartItems));

            }
        }

    }
}
