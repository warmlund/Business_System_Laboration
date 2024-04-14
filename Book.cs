using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_System_Laboration_4
{
    public class Book : Product
    {
        private string _author;
        private string _language;
        private Genre _genre;
        private BookFormat _bookFormat;
        private int _bookCount = 0;

        public string Author { get { return _author; } set { _author = value; } }
        public string Language { get { return _language; } set { _language = value; } }
        public Genre Genre { get { return _genre; } set { _genre = value; } }
        public BookFormat BookFormat { get { return _bookFormat; } set { _bookFormat = value; } }

        public Book(string id, int amount, float price, string name,string author, string language, Genre genre, BookFormat bookFormat): base(id,amount, price,name)
        {
            _author=author;
            _language=language;
            _genre=genre;
            _bookFormat=bookFormat;
        }
    }

}