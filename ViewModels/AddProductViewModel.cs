using System.Collections.ObjectModel;

namespace Business_System_Laboration_4
{
    public class AddProductViewModel : NotifyPropertyChangedBase, ICloseWindow
    {
        private string _name;
        private string _price;
        private string _amount;
        private string _informationTitleOne;
        private string _informationTitleTwo;
        private string _informationTitleThree;
        private string _informationTitleFour;
        private bool _showInformationOne = false;
        private bool _showInformationTwo = false;
        private bool _showInformationComboBoxTwo = false;
        private bool _showInformationTextBoxTwo = false;
        private bool _showInformationThree = false;
        private bool _showInformationFour = false;
        private bool _isProductTypeSelected = false;
        private bool _isQuitting = false;
        private string _selectedProductType;
        private string _selectedFormat;
        private string _selectedGenre;
        private string _informationOne;
        private string _informationTwo;
        private string _informationThree;
        private string _informationFour;
        private ObservableCollection<string> _genres = new ObservableCollection<string>();
        private ObservableCollection<string> _productTypes = new ObservableCollection<string> { "Bok", "Film", "Datorspel" };
        private ObservableCollection<string> _informationOneList = new ObservableCollection<string>();
        private ProductHandler _prodHandler;
        public string Name { get { return _name; } set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string Price { get { return _price; } set { if (_price != value) { _price = value; OnPropertyChanged(nameof(Price)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string Amount { get { return _amount; } set { if (_amount != value) { _amount = value; OnPropertyChanged(nameof(Amount)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string InformationOne { get { return _informationOne; } set { if (_informationOne != value) { _informationOne = value; OnPropertyChanged(nameof(InformationOne)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string InformationTwo { get { return _informationTwo; } set { if (_informationTwo != value) { _informationTwo = value; OnPropertyChanged(nameof(InformationTwo)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string InformationThree { get { return _informationThree; } set { if (_informationThree != value) { _informationThree = value; OnPropertyChanged(nameof(InformationThree)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string InformationFour { get { return _informationFour; } set { if (_informationFour != value) { _informationFour = value; OnPropertyChanged(nameof(InformationFour)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string InformationTitleOne { get { return _informationTitleOne; } set { if (_informationTitleOne != value) { _informationTitleOne = value; OnPropertyChanged(nameof(InformationTitleOne)); } } }
        public string InformationTitleTwo { get { return _informationTitleTwo; } set { if (_informationTitleTwo != value) { _informationTitleTwo = value; OnPropertyChanged(nameof(InformationTitleTwo)); } } }
        public string InformationTitleThree { get { return _informationTitleThree; } set { if (_informationTitleThree != value) { _informationTitleThree = value; OnPropertyChanged(nameof(InformationTitleThree)); } } }
        public string InformationTitleFour { get { return _informationTitleFour; } set { if (_informationTitleFour != value) { _informationTitleFour = value; OnPropertyChanged(nameof(InformationTitleFour)); } } }
        public bool ShowInformationOne { get { return _showInformationOne; } set { if (_showInformationOne != value) { _showInformationOne = value; OnPropertyChanged(nameof(ShowInformationOne)); } } }
        public bool ShowInformationTwo { get { return _showInformationTwo; } set { if (_showInformationTwo != value) { _showInformationTwo = value; OnPropertyChanged(nameof(ShowInformationTwo)); } } }
        public bool ShowInformationComboBoxTwo { get { return _showInformationComboBoxTwo; } set { if (_showInformationComboBoxTwo != value) { _showInformationComboBoxTwo = value; OnPropertyChanged(nameof(ShowInformationComboBoxTwo)); } } }
        public bool ShowInformationTextBoxTwo { get { return _showInformationTextBoxTwo; } set { if (_showInformationTextBoxTwo != value) { _showInformationTextBoxTwo = value; OnPropertyChanged(nameof(ShowInformationTextBoxTwo)); } } }
        public bool ShowInformationThree { get { return _showInformationThree; } set { if (_showInformationThree != value) { _showInformationThree = value; OnPropertyChanged(nameof(ShowInformationThree)); } } }
        public bool ShowInformationFour { get { return _showInformationFour; } set { if (_showInformationFour != value) { _showInformationFour = value; OnPropertyChanged(nameof(ShowInformationFour)); } } }
        public bool IsProductTypeSelected { get { return _isProductTypeSelected; } set { if (_isProductTypeSelected != value) { _isProductTypeSelected = value; OnPropertyChanged(nameof(IsProductTypeSelected)); } } }
        public bool IsQuitting { get { return _isQuitting; } set { if (_isQuitting != value) { _isQuitting = value; OnPropertyChanged(nameof(IsQuitting)); } } }
        public string SelectedProductType { get { return _selectedProductType; } set { if (_selectedProductType != value) { _selectedProductType = value; OnPropertyChanged(nameof(SelectedProductType)); CheckSelectedProductType(); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string SelectedFormat { get { return _selectedFormat; } set { if (_selectedFormat != value) { _selectedFormat = value; OnPropertyChanged(nameof(SelectedFormat)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public string SelectedGenre { get { return _selectedGenre; } set { if (_selectedGenre != value) { _selectedGenre = value; OnPropertyChanged(nameof(SelectedGenre)); ConfirmAddProduct.RaiseCanExecuteChanged(); } } }
        public IEnumerable<string> ProductTypes { get { return _productTypes; } }
        public IEnumerable<string> Genres { get { return _genres; } }
        public IEnumerable<string> InformationOneList { get { return _informationOneList; } }
        public Command ConfirmAddProduct { get; private set; }
        public Command AbortAddProduct { get; private set; }
        public Action Close { get; set; }

        public AddProductViewModel(ProductHandler productHandler)
        {
            ConfirmAddProduct = new Command(ConfirmNewProduct, CanConfirmNewProduct);
            AbortAddProduct = new Command(AbortNewProduct, CanAbortProduct);
            _prodHandler = productHandler;
        }

        private void ConfirmNewProduct()
        {
            if (SelectedProductType != null)
            {
                int.TryParse(_amount, out int amount);
                float.TryParse(_price, out float price);

                if (SelectedProductType == _productTypes[0])
                {
                    BookFormat format = BookFormat.Pocket;
                    Genre genre = Genre.Historia;

                    foreach (BookFormat b in Enum.GetValues(typeof(BookFormat)))
                    {
                        if (_selectedFormat == EnumDescriptionExtractor<BookFormat>.GetDescription(b))
                            format = b;
                    }

                    foreach (Genre g in Enum.GetValues(typeof(Genre)))
                    {
                        if (_selectedGenre == EnumDescriptionExtractor<Genre>.GetDescription(g))
                            genre = g;

                    }
                    _prodHandler.AddBook(amount, price, _name, _informationThree, _informationFour, genre, format);
                }

                else if (SelectedProductType == _productTypes[1])
                {
                    MovieFormatType format = MovieFormatType.DVD;

                    foreach (MovieFormatType m in Enum.GetValues(typeof(MovieFormatType)))
                    {
                        if (_selectedFormat == EnumDescriptionExtractor<MovieFormatType>.GetDescription(m))
                            format = m;
                    }

                    _prodHandler.AddMovie(amount, price, _name, format, _informationTwo);
                }

                else if (SelectedProductType == _productTypes[2])
                {
                    PlatformType platform = PlatformType.PC;

                    foreach (PlatformType p in Enum.GetValues(typeof(PlatformType)))
                    {
                        if (_selectedFormat == EnumDescriptionExtractor<PlatformType>.GetDescription(p))
                            platform = p; break;
                    }

                    _prodHandler.AddVideoGame(amount, price, _name, platform);
                }
            }
            ClearUserInput();
            Close?.Invoke();
        }

        private bool CanConfirmNewProduct()
        {
            if (SelectedProductType != null)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Price) || string.IsNullOrWhiteSpace(Amount))
                    return false;

                if (SelectedProductType == _productTypes[0])
                {
                    return !string.IsNullOrWhiteSpace(SelectedFormat) &&
                           !string.IsNullOrWhiteSpace(SelectedGenre) &&
                           !string.IsNullOrWhiteSpace(InformationThree) &&
                           !string.IsNullOrWhiteSpace(InformationFour);
                }

                else if (SelectedProductType == _productTypes[1])
                {
                    return !string.IsNullOrWhiteSpace(SelectedFormat) &&
                           !string.IsNullOrWhiteSpace(InformationTwo) && PlaytimeIsOnlyNumbers();
                }

                else if (SelectedProductType == _productTypes[2])
                {
                    return !string.IsNullOrWhiteSpace(SelectedFormat);
                }
            }
            else
            {
                return false;
            }

            return true;
        }


        private bool CanAbortProduct()
        {
            return true;
        }

        private void AbortNewProduct()
        {
            ClearUserInput();
            Close?.Invoke();
        }

        public void CheckSelectedProductType()
        {

            if (SelectedProductType == _productTypes[0])
            {
                IsProductTypeSelected = true;
                InformationTitleOne = "Bokformat";
                InformationTitleTwo = "Genre";
                InformationTitleThree = "Författare";
                InformationTitleFour = "Språk";

                ShowInformationOne = true;
                ShowInformationTwo = true;
                ShowInformationComboBoxTwo = true;
                ShowInformationTextBoxTwo = false;
                ShowInformationThree = true;
                ShowInformationFour = true;

                _informationOneList.Clear();

                foreach (BookFormat b in Enum.GetValues(typeof(BookFormat)))
                {
                    _informationOneList.Add(EnumDescriptionExtractor<BookFormat>.GetDescription(b));
                }

                foreach (Genre g in Enum.GetValues(typeof(Genre)))
                {
                    _genres.Add(EnumDescriptionExtractor<Genre>.GetDescription(g));
                }
            }

            else if (SelectedProductType == _productTypes[1])
            {
                IsProductTypeSelected = true;
                InformationTitleOne = "Format";
                InformationTitleTwo = "Speltid";
                InformationTitleThree = "";
                InformationTitleFour = "";

                ShowInformationOne = true;
                ShowInformationTwo = true;
                ShowInformationComboBoxTwo = false;
                ShowInformationTextBoxTwo = true;
                ShowInformationThree = false;
                ShowInformationFour = false;

                _informationOneList.Clear();

                foreach (MovieFormatType m in Enum.GetValues(typeof(MovieFormatType)))
                {
                    _informationOneList.Add(EnumDescriptionExtractor<MovieFormatType>.GetDescription(m));
                }
            }

            else if (SelectedProductType == _productTypes[2])
            {
                IsProductTypeSelected = true;
                InformationTitleOne = "Plattform";
                InformationTitleTwo = "";
                InformationTitleThree = "";
                InformationTitleFour = "";

                ShowInformationOne = true;
                ShowInformationTwo = false;
                ShowInformationComboBoxTwo = false;
                ShowInformationTextBoxTwo = false;
                ShowInformationThree = false;
                ShowInformationFour = false;

                _informationOneList.Clear();

                foreach (PlatformType p in Enum.GetValues(typeof(PlatformType)))
                {
                    _informationOneList.Add(EnumDescriptionExtractor<PlatformType>.GetDescription(p));
                }
            }

        }

        private void ClearUserInput()
        {
            Name = string.Empty;
            Price = string.Empty;
            Amount = string.Empty;
            InformationOne = string.Empty;
            InformationTwo = string.Empty;
            InformationThree = string.Empty;
            InformationFour = string.Empty;
        }

        private bool PlaytimeIsOnlyNumbers()
        {
            if (SelectedProductType == _productTypes[1])
            {
                if (int.TryParse(InformationTwo, out int number) && number > 0)
                    return true;

                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
