using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Business_System_Laboration_4
{
    public class ViewModelBase : Command, INotifyPropertyChanged
    {
        private List<Product> _products;

        public ViewModelBase()
        {
            throw new System.NotImplementedException();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void LoadProducts()
        {
            throw new System.NotImplementedException();
        }
    }
}