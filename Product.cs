using System.ComponentModel;
using System.Windows;

namespace Business_System_Laboration_4
{
    public abstract class Product : INotifyPropertyChanged
    {
        private float _price;
        private string _name;
        private string _id;
        private int _stockAmount;
        private int _cartQuantity;
        public bool CanAddToCart => Amount > 0;
        public float Price { get { return _price; } set { _price = value; OnPropertyChanged(nameof(Price)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public int Amount { get { return _stockAmount; } set { _stockAmount = value; OnPropertyChanged(nameof(Amount)); OnPropertyChanged(nameof(CanAddToCart)); } }
        public int CartQuantity { get { return _cartQuantity; } set { _cartQuantity = value; OnPropertyChanged(nameof(CartQuantity)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public Product(string id, int amount, float price, string name)
        {
            _id = id;
            _stockAmount = amount;
            _name = name;
            _price = price;
            _cartQuantity = 0;
        }

        public void DecreaseStock()
        {
            if (Amount > 0)
            {
                Amount -= 1;
            }
            //OnPropertyChanged(nameof(CanAddToCart));
        }

        public void IncreaseStock()
        {
            Amount+=1;
            //OnPropertyChanged(nameof(CanAddToCart));
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