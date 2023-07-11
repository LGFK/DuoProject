using ClientCore.Core;
using ClientCore.Helpes;
using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;
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
    public class MainWindowViewModel:BaseViewModel
    {
        
        public BaseViewModel _observablePage;
        public BaseViewModel CurrentPage
        {
            get { return _observablePage; }
            set { 
                if(_observablePage != value)
                {
                    _observablePage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
                 }
        }
        private ICommand _allBooksButtonClick;
        public ICommand AllBooksButtonClick
        {
            get => _allBooksButtonClick ?? (_allBooksButtonClick = new RelayCommand(AllBooks));
        }
        private ICommand _cartBttnClick;

        public ICommand CartBttnClick
        {
            get => _cartBttnClick ?? (_cartBttnClick = new RelayCommand(Cart));
        }
        private void Cart(object? param)
        {
            var cartVM = new CartViewModel();
            CurrentPage = cartVM;
        }
        private void AllBooks(object? param)
        {
            var allBooksPageViewModel = new AllBooksPageViewModel();
            CurrentPage = allBooksPageViewModel;
        }

        public MainWindowViewModel()
        {
            
             _= LoadData();
            CurrentPage = new GreetingPageViewModel();
            
        }
           
        private async Task LoadData()
        {
            ClientsCore clientsCore = new ClientsCore();
            var res = await clientsCore.SendRequestAsync("TEST", ComandsLib.GetAllBooks);

            if (res.IsFailure)
            {
                return;
            }

            var resultREsponseBase = (RequestResult<RequestResponseBase>)res;

            if(resultREsponseBase.Value is GetBookResponse books)
            {
                if(books.Command == ComandsLib.ERROR)
                {
                    return;
                }

                ClientCache.Add(ComandsLib.GetAllBooks.ToString(), books);
            }
            
        }
    }
}
