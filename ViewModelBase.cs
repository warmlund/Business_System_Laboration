using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        private ShoppingCart _cart;
        private InventoryManager _inventoryManager;
        private ProductHandler _prodHandler;
        private AddProductViewModel _productViewModel;
        private AddProductWindow _addProductWindow;

        public Command ConfirmPurchase { get; private set; }
        public Command<Product> AddItemToCart { get; private set; }
        public Command<Product> RemoveItemFromCart { get; private set; }
        public Command AddProduct { get; private set; }
        public Command<Product> RemoveProduct { get; private set; }
        public Command HandleDelivery { get; private set; }
        public InventoryManager Inventory { get { return _inventoryManager; } set { if (_inventoryManager != value) { _inventoryManager = value; OnPropertyChanged(nameof(Inventory)); } } }
        public ShoppingCart Cart { get { return _cart; } set { if (_cart != value) { _cart = value; OnPropertyChanged(nameof(Cart)); } } }
        public ProductHandler ProdHandler { get { return _prodHandler; } set { if (_prodHandler != value) { _prodHandler = value; OnPropertyChanged(nameof(ProdHandler)); } } }

        public AddProductViewModel AddProductViewModel { get { return _productViewModel; } set { if (_productViewModel != value) { _productViewModel = value; OnPropertyChanged(nameof(AddProductViewModel)); } } }

        public ViewModelBase()
        {
            ConfirmPurchase = new Command(Checkout, CanPurchase);
            AddItemToCart = new Command<Product>(AddToCart, CanAddToCart);
            RemoveItemFromCart = new Command<Product>(RemoveFromCart, CanRemoveFromCart);
            AddProduct = new Command(AddNewProduct, CanAddProduct);
            RemoveProduct = new Command<Product>(RemoveSelectedProduct, CanRemoveSelectedProduct);
            HandleDelivery = new Command(CreateNewDelivery, CanCreateNewDelivery);

            _cart = new ShoppingCart();
            _cart.PropertyChanged += ProductPropertyChanged;
            _inventoryManager = new InventoryManager();
            _prodHandler = new ProductHandler();
            _prodHandler.ModelBase = this;
            _productViewModel = new AddProductViewModel(_prodHandler);
            
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
            _addProductWindow = new AddProductWindow(_productViewModel);
            _addProductWindow.ShowDialog();
        }

        public bool CanAddProduct()
        {
            return true;
        }

        public void RemoveSelectedProduct(Product product)
        {
            return;
        }

        public bool CanRemoveSelectedProduct(Product product)
        {
            return true;
        }

        private bool CanCreateNewDelivery()
        {
            return true;
        }

        private void CreateNewDelivery()
        {
            return;
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