using System.ComponentModel;
using Business_System_Laboration_4.enums;

namespace Business_System_Laboration_4.Products
{
    public class Book : Product
    {
        private string _author;
        private string _language;
        private Genre _genre;
        private BookFormat _bookFormat;

        public string Author { get { return _author; } set { _author = value; OnPropertyChanged(nameof(Author)); } }
        public string Language { get { return _language; } set { _language = value; OnPropertyChanged(nameof(Language)); } }
        public Genre Genre { get { return _genre; } set { _genre = value; OnPropertyChanged(nameof(Genre)); } }
        public BookFormat BookFormat { get { return _bookFormat; } set { _bookFormat = value; OnPropertyChanged(nameof(BookFormat)); } }
        public string FormatDescription { get { return GetDescription(_bookFormat); } set { OnPropertyChanged(nameof(BookFormat)); } }

        public Book(string id, int amount, float price, string name, string author, string language, Genre genre, BookFormat bookFormat) : base(id, amount, price, name)
        {
            _author = author;
            _language = language;
            _genre = genre;
            _bookFormat = bookFormat;
        }

        public static string GetDescription(BookFormat value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

}