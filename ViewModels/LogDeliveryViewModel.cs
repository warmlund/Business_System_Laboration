using System.Collections.ObjectModel;
using System.Windows;

namespace Business_System_Laboration_4
{
    public class LogDeliveryViewModel : NotifyPropertyChangedBase, ICloseWindow
    {
        private ProductHandler _originalProdHandler;
        private ObservableCollection<Book> _editedBooks = new ObservableCollection<Book>();
        private ObservableCollection<Movie> _editedMovies = new ObservableCollection<Movie>();
        private ObservableCollection<VideoGame> _editedVideoGames = new ObservableCollection<VideoGame>();

        public ObservableCollection<Book> EditedBooks { get { return _editedBooks; } set { if (_editedBooks != value) { _editedBooks = value; OnPropertyChanged(nameof(EditedBooks)); } } }
        public ObservableCollection<Movie> EditedMovies { get { return _editedMovies; } set { if (_editedMovies != value) { _editedMovies = value; OnPropertyChanged(nameof(EditedMovies)); } } }
        public ObservableCollection<VideoGame> EditedVideoGames { get { return _editedVideoGames; } set { if (_editedVideoGames != value) { _editedVideoGames = value; OnPropertyChanged(nameof(EditedVideoGames)); } } }

        public Action Close { get; set; }
        public Command ConfirmUpdateProduct { get; private set; }
        public Command AbortUpdateProduct { get; private set; }

        public LogDeliveryViewModel(ProductHandler productHandler)
        {
            ConfirmUpdateProduct = new Command(UpdateProduct, CanUpdateProduct);
            AbortUpdateProduct = new Command(AbortUpdate, CanAbortUpdate);
            _originalProdHandler = productHandler;
            CopyInventory();
        }

        private bool CanAbortUpdate()
        {
            return true;
        }

        private void AbortUpdate()
        {
            _editedBooks.Clear();
            _editedMovies.Clear();
            _editedVideoGames.Clear();
            Close?.Invoke();
        }

        private bool CanUpdateProduct()
        {
            return true;
        }

        private void UpdateProduct()
        {
            _originalProdHandler.Books.Clear();
            foreach (Book book in EditedBooks)
            {
                _originalProdHandler.Books.Add(book);
            }

            _originalProdHandler.Movies.Clear();
            foreach (Movie movie in EditedMovies)
            {
                _originalProdHandler.Movies.Add(movie);
            }

            _originalProdHandler.VideoGames.Clear();
            foreach (VideoGame game in EditedVideoGames)
            {
                _originalProdHandler.VideoGames.Add(game);
            }

            Close?.Invoke();
        }

        private void CopyInventory()
        {
            MessageBox.Show("hej");
            foreach (var book in _originalProdHandler.Books)
            {
                EditedBooks.Add(new Book(book.Id, book.Amount, book.Price, book.Name, book.Author, book.Language, book.Genre, book.BookFormat));
            }
            foreach (var movie in _originalProdHandler.Movies)
            {
                EditedMovies.Add(new Movie(movie.Id, movie.Amount, movie.Price, movie.Name, movie.VideoFormat, movie.PlayTime));
            }
            foreach (var game in _originalProdHandler.VideoGames)
            {
                EditedVideoGames.Add(new VideoGame(game.Id, game.Amount, game.Price, game.Name, game.Platform));
            }
        }
    }
}
