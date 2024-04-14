using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        private readonly string filePath = "produkter.csv";

        public CheckoutViewModel CheckoutViewModel { get; }
        public StockViewModel StockViewModel { get; }

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books { get { return _books; } set { _books = value; OnPropertyChanged(nameof(Books)); } }

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies { get { return _movies; } set { _movies = value; OnPropertyChanged(nameof(Movies)); } }

        private ObservableCollection<VideoGame> _videoGames;
        public ObservableCollection<VideoGame> VideoGames { get { return _videoGames; } set { _videoGames = value; OnPropertyChanged(nameof(VideoGames)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBase()
        {
            CheckoutViewModel = new CheckoutViewModel();
            StockViewModel = new StockViewModel();
            _books = new ObservableCollection<Book>();
            _movies = new ObservableCollection<Movie>();
            _videoGames=new ObservableCollection<VideoGame>();
        }

        public void LoadProducts()
        {
            using (StreamReader reader = new(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var productArray = reader.ReadLine().Split(",");

                    switch (productArray[0].First())
                    {
                        case 'B':
                            Books.Add(AddBook(productArray));
                            break;

                        case 'D':
                            VideoGames.Add(AddVideoGame(productArray));
                            break;

                        case 'F':
                            Movies.Add(AddMovie(productArray));
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void SaveProducts()
        {

        }

        private Book AddBook(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            string author = productArray[4];
            string language = productArray[7];
            Enum.TryParse(productArray[5], out Genre genre);
            Enum.TryParse(productArray[6], out BookFormat format);

            return new Book(id, amount, price, name, author, language, genre, format);
        }

        private Movie AddMovie(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[6], out MovieFormatType format);
            int.TryParse(productArray[9], out int playTime);

            return new Movie(id, amount, price, name, format, playTime);
        }

        private VideoGame AddVideoGame(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[8], out PlatformType format);

            return new VideoGame(id, amount, price, name, format);
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