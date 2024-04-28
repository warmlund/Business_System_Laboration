using Business_System_Laboration_4.BaseClasses;
using Business_System_Laboration_4.Products;
using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        private ShoppingCart _cart;
        private InventoryManager _inventoryManager;
        private ProductHandler _prodHandler;

        public Command ConfirmPurchase { get; private set; }
        public Command<Product> AddItemToCart { get; private set; }
        public Command<Product> RemoveItemFromCart { get; private set; }
        public Command AddProduct { get; private set; }
        public Command<Product> RemoveProduct { get; private set; }
        public Command HandleDelivery { get; private set; }

        public InventoryManager Inventory { get { return _inventoryManager; } set { if (_inventoryManager != value) { _inventoryManager = value; OnPropertyChanged(nameof(Inventory)); } } }
        public ShoppingCart Cart { get { return _cart; } set { if (_cart != value) { _cart = value; OnPropertyChanged(nameof(Cart)); } } }
        public ProductHandler ProdHandler { get { return _prodHandler; } set { if (_prodHandler != value) { _prodHandler = value; OnPropertyChanged(nameof(ProdHandler)); } } }

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
            return;
        }

        public bool CanAddProduct()
        {
            return true;
        }

        public void RemoveSelectedProduct(Product product)
        {

        }

        public bool CanRemoveSelectedProduct(Product product)
        {
            return true;
        }

        private bool CanCreateNewDelivery()
        {
            throw new NotImplementedException();
        }

        private void CreateNewDelivery()
        {
            throw new NotImplementedException();
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