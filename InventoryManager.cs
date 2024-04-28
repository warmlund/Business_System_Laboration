using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Business_System_Laboration_4.BaseClasses;
using Business_System_Laboration_4.Products;

namespace Business_System_Laboration_4
{
    public class InventoryManager: NotifyPropertyChangedBase
    {


        public InventoryManager()
        {
           
        }

        public void AddProduct()
        {
            
        }

        public void RemoveProduct(Product product)
        {
           if(product != null)
            {
                if(product is Book)
                {
                     
                }

                else if(product is Movie)
                {

                }

                else if(product is VideoGame)
                {

                }
            }
        }

        public void AddWholeSaleDelivery()
        {
           
        }


    }
}