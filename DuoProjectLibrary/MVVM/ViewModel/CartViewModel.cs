using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        ObservableCollection<BookInDaBasket> _bookInDaBasket;

        public ObservableCollection<BookInDaBasket> BookInDaBasket
        {
            get { return _bookInDaBasket; }
            set { _bookInDaBasket = value; }
        }
        
        public CartViewModel()
        {
            BookInDaBasket = CartCollection.Basket;
        }
        
       
        
    }
}

