using DuoProjectLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary;
using DuoProjectLibrary.MVVM.Model;
using System.Windows.Input;
using ClientCore.Core;
using ClientCore.Rusults;
using CommunicationLibrary;
using ComandLibrary;
using ClientCore.Helpes;
using System.Threading;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    internal class AllBooksPageViewModel : BaseViewModel
    {
        readonly private IModalWindowService service;
        
        Book? _book;
        public Book Book
        {
            get { return _book; }
            set { _book = value; 
             OnPropertyChanged(); }
        }
        ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }
        public AllBooksPageViewModel()
        {
            try
            {
                _books = new ObservableCollection<Book>();
                LoadData();
                service = ApplicationState.ModalWindowService;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
           
            
        }
        ICommand _openEditor;

        public ICommand OpenEditorCommand 
        {
            get => _openEditor ?? (_openEditor = new RelayCommand(OpenEditWMethod));
        }
        private void OpenEditWMethod(object param)
        {
            if(param is Book book)
            {
                var _editVM = new EditingWindowViewModel(book);
                service.ShowModalWindow(_editVM);
            }
           
        }

        ICommand _addToCartCmd;

        public ICommand AddToCartCommand
        {
            get=>_addToCartCmd ?? (_addToCartCmd = new RelayCommand(AddBookInDaCart)); 
        }
        private void AddBookInDaCart(object s)
        {
            bool isAdded = false;
            if (s is Book _bookTmp)
            {
                foreach (var book in CartCollection.Basket)
                {

                    if (book.BookByItself.Name == _bookTmp.Name)
                    {
                        isAdded = true;
                        if(book.Amount<_bookTmp.CountBooks.Count)
                        {
                            book.Amount += 1;
                        }    
                        break;
                    }
                   
                }
                if (isAdded == false)
                {
                    CartCollection.Basket.Add(new BookInDaBasket(1, _bookTmp));
                }
                return;
            }
           

        }

        private  void LoadData()
        {
            var res = ClientCache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());

            if (res is null)
            {

                return;
            }

            if (res.Books is null)
            {
                return;
            }

            foreach (var book in res.Books)
            {
                Books.Add(book);
            }

        }
    }
}
