using System.ComponentModel;
using Business_System_Laboration_4.BaseClasses;

namespace Business_System_Laboration_4.Products
{
    public abstract class Product : NotifyPropertyChangedBase
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

        public Product(string id, int amount, float price, string name)
        {
            _id = id;
            _stockAmount = amount;
            _name = name;
            _price = price;
            _cartQuantity = 0;
        }

        public void DecreaseStock(int amount)
        {
            if (Amount > 0)
            {
                Amount -= amount;
            }
        }

        public void IncreaseStock(int amount)
        {
            Amount += amount;
        }

    }
}