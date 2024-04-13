using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_System_Laboration_4
{
    public class Product
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public ProductType Type { get; set; }
    }
}