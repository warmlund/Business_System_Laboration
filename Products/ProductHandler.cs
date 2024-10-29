using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Xml;

namespace Business_System_Laboration_4
{
    public class ProductHandler : NotifyPropertyChangedBase
    {
        private readonly string _fileName = "Data/produkter.csv";
        private readonly string _directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private string _filePath;

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books { get { return _books; } set { if (_books != value) { _books = value; OnPropertyChanged(nameof(Books)); } } }

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies { get { return _movies; } set { if (_movies != value) { _movies = value; OnPropertyChanged(nameof(Movies)); } } }

        private ObservableCollection<VideoGame> _videoGames;
        public ObservableCollection<VideoGame> VideoGames { get { return _videoGames; } set { if (_videoGames != value) { _videoGames = value; OnPropertyChanged(nameof(VideoGames)); } } }

        public ViewModelBase ModelBase { get; set; }


        public ProductHandler()
        {
            _books = new ObservableCollection<Book>();
            _movies = new ObservableCollection<Movie>();
            _videoGames = new ObservableCollection<VideoGame>();
            _filePath = Path.Combine(_directory, _fileName);

            Books.CollectionChanged += OnCollectionChanged;
            Movies.CollectionChanged += OnCollectionChanged;
            VideoGames.CollectionChanged += OnCollectionChanged;
        }

        public void LoadProducts()
        {
            try
            {
                using (StreamReader reader = new(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var productArray = reader.ReadLine().Split(",");

                        switch (productArray.Last())
                        {
                            case "B":
                                var book = AddBookCSV(productArray);
                                book.PropertyChanged += ModelBase.ProductPropertyChanged;
                                Books.Add(book);
                                break;

                            case "D":
                                var videoGame = AddVideoGameFromCSV(productArray);
                                videoGame.PropertyChanged += ModelBase.ProductPropertyChanged;
                                VideoGames.Add(videoGame);
                                break;

                            case "F":
                                var movie = AddMovieFromCSV(productArray);
                                movie.PropertyChanged += ModelBase.ProductPropertyChanged;
                                Movies.Add(movie);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel", "Kunde inte ladda in produktfilen");
            }
        }

        public void SaveProducts()
        {
            try
            {
                var writer = new StreamWriter(_filePath);

                foreach (var book in Books)
                {
                    string line = $"{book.Id},{book.Amount},{book.Name},{book.Price},{book.Author},{book.Genre},{book.BookFormat},{book.Language},,,B";
                    writer.WriteLine(line);
                }

                foreach (var videoGame in VideoGames)
                {
                    string line = $"{videoGame.Id},{videoGame.Amount},{videoGame.Name},{videoGame.Price},,,,,{videoGame.Platform},,D";
                    writer.WriteLine(line);
                }

                foreach (var movie in Movies)
                {
                    string line = $"{movie.Id},{movie.Amount},{movie.Name},{movie.Price},,,{movie.VideoFormat},,,{movie.PlayTime},F";
                    writer.WriteLine(line);
                }
                writer.Close();

                Books.Clear();
                Movies.Clear();
                VideoGames.Clear();
            }
            catch
            {
                MessageBox.Show("Fel", "Kunde inte spara ner produktfilen");
            }
        }

        internal static Book AddBookCSV(string[] productArray)
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
        internal static Movie AddMovieFromCSV(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[6], out MovieFormatType format);
            string playTime = productArray[9];

            return new Movie(id, amount, price, name, format, playTime);
        }
        internal static VideoGame AddVideoGameFromCSV(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[8], out PlatformType format);

            return new VideoGame(id, amount, price, name, format);
        }

        public void AddBook(int amount, float price, string name, string author, string language, Genre genre, BookFormat format)
        {
            if (Books.Any(book => book.Name == name && book.Author == author))
            {
                MessageBox.Show("Den här boken finns redan.");
                return;
            }

            Book newBook = new Book(GenerateId(), amount, price, name, author, language, genre, format);
            newBook.PropertyChanged += ModelBase.ProductPropertyChanged;
            Books.Add(newBook);
        }

        public void AddMovie(int amount, float price, string name, MovieFormatType format, string playtime)
        {
            if (Movies.Any(movie => movie.Name == name))
            {
                MessageBox.Show("Den här filmen finns redan.");
                return;
            }

            Movie newMovie = new Movie(GenerateId(), amount, price, name, format, playtime);
            newMovie.PropertyChanged += ModelBase.ProductPropertyChanged;
            Movies.Add(newMovie);
        }

        public void AddVideoGame(int amount, float price, string name, PlatformType platform)
        {
            if (VideoGames.Any(game => game.Name == name))
            {
                MessageBox.Show("Det här spelet finns redan.");
                return;
            }

            VideoGame newGame = new VideoGame(GenerateId(), amount, price, name, platform);
            newGame.PropertyChanged += ModelBase.ProductPropertyChanged;
            VideoGames.Add(newGame);

        }

        private string GenerateId()
        {
            var collections = new List<IEnumerable<Product>> { _books, _movies, _videoGames };
            int largestId = 0;

            foreach (var collection in collections)
            {
                foreach (var item in collection)
                {
                    if (int.TryParse(item.Id, out int id))
                    {
                        if (id > largestId)
                        {
                            largestId = id;
                        }
                    }
                }
            }
            largestId++;

            return largestId.ToString("D3");
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Books));
            OnPropertyChanged(nameof(Movies));
            OnPropertyChanged(nameof(VideoGames));
        }

        internal void RemoveProduct(Product selectedProduct)
        {
            try
            {
                if (selectedProduct is Book book)
                {
                    Books.Remove(book);
                }

                else if (selectedProduct is Movie movie)
                {
                    Movies.Remove(movie);
                }

                else if (selectedProduct is VideoGame game)
                {
                    VideoGames.Remove(game);
                }
            }

            catch
            {
                MessageBox.Show("Fel", "Fel vid borttagande av produkt");
            }

        }

        public void SyncProductDataWithCentral(string xmlData)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlData);
                XmlNode productsNode = doc.SelectSingleNode("/response/products");
                foreach (XmlNode node in productsNode.ChildNodes)
                {

                    int id = int.Parse(node.SelectSingleNode("id").InnerText);
                    float price = float.Parse(node.SelectSingleNode("price").InnerText);
                    int amount = int.Parse(node.SelectSingleNode("stock").InnerText);

                    if (node.Name == "book")
                    {
                        Book matchingBook = Books.FirstOrDefault(x => int.Parse(x.Id) == id);

                        if (matchingBook != null)
                        {
                            matchingBook.Price = price;
                            matchingBook.Amount = amount;

                            if (matchingBook.CartQuantity > 0)
                                matchingBook.Amount -= matchingBook.CartQuantity;
                        }

                    }

                    else if (node.Name == "game")
                    {
                        VideoGame matchingGame = VideoGames.FirstOrDefault(x => int.Parse(x.Id) == id);
                        if (matchingGame != null)
                        {
                            matchingGame.Price = price;
                            matchingGame.Amount = amount;

                            if (matchingGame.CartQuantity > 0)
                                matchingGame.Amount -= matchingGame.CartQuantity;
                        }
                    }

                    else if (node.Name == "movie")
                    {
                        Movie matchingMovie = Movies.FirstOrDefault(x => int.Parse(x.Id) == id);
                        if (matchingMovie != null)
                        {
                            matchingMovie.Price = price;
                            matchingMovie.Amount = amount;

                            if (matchingMovie.CartQuantity > 0)
                                matchingMovie.Amount -= matchingMovie.CartQuantity;
                        }
                    }
                }

                MessageBox.Show("Synkning slutförd");
            }

            catch
            {
                MessageBox.Show("Synkning misslyckades");
            }
        }
    }
}
