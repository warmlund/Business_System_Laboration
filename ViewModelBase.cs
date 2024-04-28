using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Web;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private ShoppingCart _cart;
        private ProductHandler _prodHandler;
        private readonly string fileName = "produkter.csv";
        private string directory = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private string filePath;


        public Command ConfirmPurchase { get; private set; }
        public Command<Product> AddItemToCart { get; private set; }
        public Command<Product> RemoveItemFromCart { get; private set; }
        public Command<Product> IncreaseItemInCart { get; private set; }
        public Command<Product> DecreaseItemInCart { get; private set; }

        public ShoppingCart Cart { get { return _cart; } set { if (_cart != value) { _cart = value; OnPropertyChanged(nameof(Cart)); } } }
        public ProductHandler ProdHandler { get { return _prodHandler; } set { if (_prodHandler != value) { _prodHandler = value; OnPropertyChanged(nameof(ProdHandler)); } } }

        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBase()
        {
            ConfirmPurchase = new Command(Checkout, CanPurchase);
            AddItemToCart = new Command<Product>(AddToCart, CanAddToCart);
            RemoveItemFromCart = new Command<Product>(RemoveFromCart, CanRemoveFromCart);

            _cart = new ShoppingCart();
            _cart.PropertyChanged += ProductPropertyChanged;
            _prodHandler = new ProductHandler();
            filePath = Path.Combine(directory, fileName);
        }

        public void LoadProducts()
        {

            using (StreamReader reader = new(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var productArray = reader.ReadLine().Split(",");

                    switch (productArray.Last())
                    {
                        case "B":
                            var book = ProdHandler.AddBook(productArray);
                            book.PropertyChanged += ProductPropertyChanged;
                            ProdHandler.Books.Add(book);
                            break;

                        case "D":
                            var videoGame = ProdHandler.AddVideoGame(productArray);
                            videoGame.PropertyChanged += ProductPropertyChanged;
                            ProdHandler.VideoGames.Add(videoGame);
                            break;

                        case "F":
                            var movie = ProdHandler.AddMovie(productArray);
                            movie.PropertyChanged += ProductPropertyChanged;
                            ProdHandler.Movies.Add(movie);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void SaveProducts()
        {
            var writer = new StreamWriter(filePath);

            foreach (var book in ProdHandler.Books)
            {
                string line = $"{book.Id},{book.Amount},{book.Name},{book.Price},{book.Author},{book.Genre},{book.BookFormat},{book.Language},,,B";
                writer.WriteLine(line);
            }

            foreach (var videoGame in ProdHandler.VideoGames)
            {
                string line = $"{videoGame.Id},{videoGame.Amount},{videoGame.Name},{videoGame.Price},,,,,{videoGame.Platform},,D";
                writer.WriteLine(line);
            }

            foreach (var movie in ProdHandler.Movies)
            {
                string line = $"{movie.Id},{movie.Amount},{movie.Name},{movie.Price},,,{movie.VideoFormat},,,{movie.PlayTime},F";
                writer.WriteLine(line);
            }
            writer.Close();
        }


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


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ProductPropertyChanged(object sender, PropertyChangedEventArgs e)
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