using System.Linq;
using System.Windows;

namespace Business_System_Laboration_4
{
    public class RemoveProductViewModel : NotifyPropertyChangedBase, ICloseWindow
    {
        private Product _selectedProduct;
        private ProductHandler _prodHandler;
        private IEnumerable<Product> _availableProducts;

        public Action Close { get; set; }

        public Product SelectedProduct { get { return _selectedProduct; } set { if (_selectedProduct != value) { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); CanRemoveProduct(); ConfirmRemoveProduct.RaiseCanExecuteChanged(); } } }
        public IEnumerable<Product> AvailableProducts { get { return _availableProducts; } set { if (_availableProducts != value) { _availableProducts = value; OnPropertyChanged(nameof(AvailableProducts)); } } }

        public Command ConfirmRemoveProduct { get; private set; }
        public Command AbortRemoveProduct { get; private set; }


        public RemoveProductViewModel(ProductHandler productHandler)
        {
            ConfirmRemoveProduct = new Command(RemoveProduct, CanRemoveProduct);
            AbortRemoveProduct = new Command(AbortRemove, CanAbortRemove);
            _prodHandler = productHandler;
            _availableProducts = productHandler.Books.Cast<Product>().Concat(productHandler.Movies.Cast<Product>()).Concat(productHandler.VideoGames.Cast<Product>());
        }

        private bool CanAbortRemove()
        {
            return true;
        }

        private void AbortRemove()
        {
            Close?.Invoke();
        }

        private bool CanRemoveProduct()
        {
            if (SelectedProduct!=null)
                return true;

            else
                return false;
        }

        private void RemoveProduct()
        {
            if (SelectedProduct.Amount > 0)
            {
                var message = MessageBox.Show("Produkten har varor i lager. Vill du fortfarande ta bort produkten?", "Ta bort produkt", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (message == MessageBoxResult.Yes)
                {
                    _prodHandler.RemoveProduct(SelectedProduct);
                    Close?.Invoke();
                }

                if (message==MessageBoxResult.No)
                {
                    Close?.Invoke();
                }
            }

            else
            {
                _prodHandler.RemoveProduct(SelectedProduct);
                Close?.Invoke();
            }
        }
    }
}
