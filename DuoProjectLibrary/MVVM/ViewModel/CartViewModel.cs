using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        ObservableCollection<BookInDaBasket> _bookInDaBasket;

        BookInDaBasket _bookSelected;
        public BookInDaBasket BookSelected
        {
            get { return _bookSelected; }
            set { if(value!=_bookSelected)
                {
                    _bookSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        ICommand _removeFromCart;
        public ICommand RemoveFromCartCommand
        {
            get => _removeFromCart ?? (_removeFromCart = new RelayCommand(RemoveFromCartMehod));
        }

        void RemoveFromCartMehod(object? param)
        {
            if(param is BookInDaBasket bidb)
            {
                BookInDaBasket.Remove(bidb);
                CartCollection.Basket = BookInDaBasket;
            }
        }
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

