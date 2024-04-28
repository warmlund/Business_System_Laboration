using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Business_System_Laboration_4.BaseClasses;
using Business_System_Laboration_4.enums;

namespace Business_System_Laboration_4.Products
{
    public class ProductHandler : NotifyPropertyChangedBase
    {
        private readonly string _fileName = "produkter.csv";
        private string _directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private string _filePath;
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books { get { return _books; } set { _books = value; OnPropertyChanged(nameof(Books)); } }

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies { get { return _movies; } set { _movies = value; OnPropertyChanged(nameof(Movies)); } }

        private ObservableCollection<VideoGame> _videoGames;
        public ObservableCollection<VideoGame> VideoGames { get { return _videoGames; } set { _videoGames = value; OnPropertyChanged(nameof(VideoGames)); } }


        public ProductHandler()
        {
            _books = new ObservableCollection<Book>();
            _movies = new ObservableCollection<Movie>();
            _videoGames = new ObservableCollection<VideoGame>();
            _filePath = Path.Combine(_directory, _fileName);
        }

        public void LoadProducts(ViewModelBase viewModelBase)
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
                                var book = AddBook(productArray);
                                book.PropertyChanged += viewModelBase.ProductPropertyChanged;
                                Books.Add(book);
                                break;

                            case "D":
                                var videoGame = AddVideoGame(productArray);
                                videoGame.PropertyChanged += viewModelBase.ProductPropertyChanged;
                                VideoGames.Add(videoGame);
                                break;

                            case "F":
                                var movie = AddMovie(productArray);
                                movie.PropertyChanged += viewModelBase.ProductPropertyChanged;
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
            }
            catch
            {
                MessageBox.Show("Fel", "Kunde inte spara ner produktfilen");
            }
        }

        internal static Book AddBook(string[] productArray)
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

        internal static Movie AddMovie(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[6], out MovieFormatType format);
            string playTime = productArray[9];


            return new Movie(id, amount, price, name, format, playTime);
        }

        internal static VideoGame AddVideoGame(string[] productArray)
        {
            string id = productArray[0];
            int.TryParse(productArray[1], out int amount);
            float.TryParse(productArray[3], out float price);
            string name = productArray[2];
            Enum.TryParse(productArray[8], out PlatformType format);

            return new VideoGame(id, amount, price, name, format);
        }

    }
}
