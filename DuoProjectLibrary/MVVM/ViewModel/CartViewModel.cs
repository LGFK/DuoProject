using ClientCore.Core;
using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        ObservableCollection<BookInDaBasket> _bookInDaBasket;
        int totalPrice;

        public int TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(); }
        }

        BookInDaBasket _bookSelected;
        public BookInDaBasket BookSelected
        {
            get { return _bookSelected; }
            set
            {
                if (value != _bookSelected)
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
            if (param is BookInDaBasket bidb)
            {
                BookInDaBasket.Remove(bidb);
                CartCollection.Basket.Remove(bidb);
                TotalPrice -= bidb.BookByItself.Cost * bidb.Amount;
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
            totalPrice = BookInDaBasket.Sum(x => x.BookByItself.Cost * x.Amount);
        }

        ICommand _sell;
        public ICommand SellCommand
        {
            get => _sell ?? (_sell = new RelayCommand(SellMethod));
        }

        async void SellMethod(object? param)
        {
            ClientsCore clientsCore = new ClientsCore();
            var net = await clientsCore.Connected();
            try
            {
                if (CartCollection.Basket.Count > 0)
                {
                    for (int i = 0; i < CartCollection.Basket.Count; i++)
                    {
                        CartCollection.Basket[i].BookByItself.CountBooks.Count -= CartCollection.Basket[i].Amount;

                        _ = clientsCore.EditBook(net.Value, CartCollection.Basket[i].BookByItself);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

    }
}

