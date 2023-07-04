using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.Model
{
    public class CartCollection
    {
        public static ObservableCollection<BookInDaBasket> _basket;
        public static ObservableCollection<BookInDaBasket> Basket
        {
            get
            {
                if (_basket == null)
                    _basket = new ObservableCollection<BookInDaBasket>();
                return _basket;
            }
            set { _basket = value; }
        }

    }
}
