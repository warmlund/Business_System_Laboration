using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        private ShoppingCart _cart;
        private ProductHandler _prodHandler;
        private AddProductViewModel _productViewModel;
        private AddProductWindow _addProductWindow;
        private RemoveProductViewModel _removeProductViewModel;
        private RemoveProductWindow _removeProductWindow;
        private LogDeliveryViewModel _logDeliveryViewModel;
        private LogDeliveryWindow _logDeliveryWindow;

        public Command ConfirmPurchase { get; private set; }
        public Command<Product> AddItemToCart { get; private set; }
        public Command<Product> RemoveItemFromCart { get; private set; }
        public Command AddProduct { get; private set; }
        public Command RemoveProduct { get; private set; }
        public Command HandleDelivery { get; private set; }
        public Command SyncWithCentral { get; private set; }
        public ShoppingCart Cart { get { return _cart; } set { if (_cart != value) { _cart = value; OnPropertyChanged(nameof(Cart)); } } }
        public ProductHandler ProdHandler { get { return _prodHandler; } set { if (_prodHandler != value) { _prodHandler = value; OnPropertyChanged(nameof(ProdHandler)); } } }

        public ViewModelBase()
        {
            ConfirmPurchase = new Command(Checkout, CanPurchase);
            AddItemToCart = new Command<Product>(AddToCart, CanAddToCart);
            RemoveItemFromCart = new Command<Product>(RemoveFromCart, CanRemoveFromCart);
            AddProduct = new Command(AddNewProduct, CanAddProduct);
            RemoveProduct = new Command(RemoveSelectedProduct, CanRemoveProduct);
            HandleDelivery = new Command(CreateNewDelivery, CanCreateNewDelivery);
            SyncWithCentral = new Command(SyncToCentral, CanSyncToCentral);

            _cart = new ShoppingCart();
            _cart.PropertyChanged += ProductPropertyChanged;
            _prodHandler = new ProductHandler();
            _prodHandler.ModelBase = this;
        }

        #region Shoppingcart
        public void Checkout()
        {
            Cart.CheckoutCart();
        }

        public void RemoveFromCart(Product product)
        {
            Cart.RemoveFromCart(product);
        }

        public void AddToCart(Product product)
        {
            Cart.AddToCart(product);
        }

        public bool CanPurchase()
        {
            if (Cart.IsCartEmpty())
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public static bool CanAddToCart(Product product)
        {
            if (product.Amount == 0)
                return false;

            else
                return true;
        }
        public bool CanRemoveFromCart(Product product)
        {
            return product != null && Cart.CartItems.Contains(product);
        }
        #endregion ShoppingCart

        #region Inventory

        public void AddNewProduct()
        {
            _productViewModel = new AddProductViewModel(_prodHandler);
            _addProductWindow = new AddProductWindow(_productViewModel);
            _addProductWindow.ShowDialog();
        }

        public static bool CanAddProduct()
        {
            return true;
        }

        public void RemoveSelectedProduct()
        {
            _removeProductViewModel = new RemoveProductViewModel(_prodHandler);
            _removeProductWindow = new RemoveProductWindow(_removeProductViewModel);
            _removeProductWindow.ShowDialog();
        }

        public static bool CanRemoveProduct()
        {
            return true;
        }

        private void CreateNewDelivery()
        {
            _logDeliveryViewModel = new LogDeliveryViewModel(_prodHandler);
            _logDeliveryWindow = new LogDeliveryWindow(_logDeliveryViewModel);
            _logDeliveryWindow.ShowDialog();
        }


        private bool CanCreateNewDelivery()
        {
            return true;
        }

        private async void SyncToCentral()
        {
            string responseData = await ApiHandler.SyncWithWebAPI();
            _prodHandler.SyncProductDataWithCentral(responseData);
        }

        private bool CanSyncToCentral()
        {
            return true;
        }

        #endregion

        internal void ProductPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Product.Amount) || e.PropertyName == nameof(ShoppingCart.CartItems))
            {
                ConfirmPurchase.RaiseCanExecuteChanged();
                AddItemToCart.RaiseCanExecuteChanged();
                RemoveItemFromCart.RaiseCanExecuteChanged();
            }
        }
    }
}