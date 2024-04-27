//using System.Collections.ObjectModel;
//using System.ComponentModel;

//namespace Business_System_Laboration_4
//{
//    public class CheckoutViewModel : INotifyPropertyChanged
//    {
//        private float _totalPrice;
//        private ObservableCollection<Product> _cartItems;

//        public ObservableCollection<Product> CartItems { get { return _cartItems; } set { _cartItems = value; OnPropertyChanged(nameof(CartItems)); } }
//        public float TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
//        public Command ConfirmPurchase { get; private set; }
//        public Command<Product> AddItemToCart { get; private set; }
//        public Command<Product> RemoveItemFromCart { get; private set; }
//        public Command<Product> IncreaseItemInCart { get; private set; }
//        public Command<Product> DecreaseItemInCart { get; private set; }

//        public event PropertyChangedEventHandler PropertyChanged;

//        public CheckoutViewModel()
//        {
//            _totalPrice = 0;
//            ConfirmPurchase = new Command(Checkout, CanPurchase);
//            AddItemToCart = new Command<Product>(AddToCart, CanAddToCart);
//            RemoveItemFromCart = new Command<Product>(RemoveFromCart, CanRemoveFromCart);
//            IncreaseItemInCart = new Command<Product>(IncreaseItem, CanIncrease);
//            DecreaseItemInCart = new Command<Product>(DecreaseItem, CanDecrease);
//            _cartItems = new ObservableCollection<Product>();
//        }

//        public void Checkout()
//        {
//            return;  
//        }

//        public void RemoveFromCart(Product product)
//        {
//            return;
//        }

//        public void AddToCart(Product product)
//        {
//           CartItems.Add(product);
//            TotalPrice+= product.Price; 
//        }

//        public void IncreaseItem(Product product)
//        {
//            return;
//        }

//        public void DecreaseItem(Product product)
//        {
//            return;
//        }

//        public bool CanPurchase()
//        {
//            return true;
//        }

//        public bool CanAddToCart(Product product)
//        {
//            var id = product.Id;
//            int stockQuantity=CheckStock(id);
//            int cartQuantity=CheckProductCountInCart(id);
//            bool hasAllbeenAddedToCart = (stockQuantity - cartQuantity)==0;
//            if (CheckStock(id) > 0 && !hasAllbeenAddedToCart)
//                return true;

//            else
//                return false;

//        }
//        public bool CanRemoveFromCart(Product product)
//        {
//            return true;
//        }

//        public bool CanIncrease(Product product)
//        {
//            return true;
//        }

//        private bool CanDecrease(Product product)
//        {
//            return true;
//        }
 

//        public int CheckProductCountInCart(string id)
//        {
//            int countOfSameItem=CartItems.Count(x=>x.Id==id);

//            return countOfSameItem;
//        }

//        public int CheckStock(string id)
//        {
//            switch (id[0])
//            {
//                case 'B':
//                    return GetBookStock(id);

//                case 'D':
//                    return GetVideoGameStock(id);

//                case 'F':
//                    return GetMovieStock(id);

//                default:
//                    return -1;
//            }
//        }

//        public int GetBookStock(string id) 
//        {
//            var book=_viewModelBase.Books.FirstOrDefault(x=>x.Id==id);
//            int amount = book.Amount;
//            return amount;
//        }

//        public int GetVideoGameStock(string id)
//        {
//            var videoGame = _viewModelBase.VideoGames.FirstOrDefault(x => x.Id == id);
//            int amount = videoGame.Amount;
//            return amount;
//        }

//        public int GetMovieStock(string id)
//        {
//            var movie= _viewModelBase.Movies.FirstOrDefault(x => x.Id == id);
//            int amount = movie.Amount;
//            return amount;
//        }

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//            }
//        }
//    }
//}