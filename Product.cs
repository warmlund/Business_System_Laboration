using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Business_System_Laboration_4
{
    public class Product
    {
        private float _price;
        private string _name;
        private string _id;
        private int _amount;

        public float Price { get { return _price; } set { _price = value; }}
        public string Name { get { return _name; } set { _name = value; } }
        public string Id { get { return _id; } set { _id = value; } }

        public int Amount { get { return _amount; } set { _amount = value; } }

        public Product(string id, int amount, float price, string name)
        {
            _id = id;
            _amount = amount;
            _name = name;
            _price = price;
        }
    }
}