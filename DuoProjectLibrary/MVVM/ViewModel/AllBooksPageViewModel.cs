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

namespace DuoProjectLibrary.MVVM.ViewModel
{
    internal class AllBooksPageViewModel : BaseViewModel
    {
        ObservableCollection<BookInDaBasket> _booksInDaCart;
        public ObservableCollection<BookInDaBasket> BooksInDaBasket
        {
            get { return _booksInDaCart; }
            set { _booksInDaCart = value; }
        }
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
            _books = new ObservableCollection<Book>();
            LoadData();
            
        }
        public AllBooksPageViewModel(ObservableCollection<BookInDaBasket> _basket)
        {
            _books = new ObservableCollection<Book>();
            LoadData();
            BooksInDaBasket = _basket;
        }

        ICommand _addToCartCmd;

        public ICommand AddToCartCommand
        {
            get=>_addToCartCmd ?? (_addToCartCmd = new RelayCommand(AddBookInDaCart)); 
        }
        private void AddBookInDaCart(object s)
        {
            bool isAdded = false;
            if(s is Book _bookTmp)
            {
                foreach (var book in CartCollection.Basket)
                {

                    if (book.BookByItself.Name == _bookTmp.Name)
                    {
                        isAdded = true;
                        book.Amount+=1;
                        break;
                    }
                   
                }
                if (isAdded == false)
                {
                    CartCollection.Basket.Add(new BookInDaBasket(1, _bookTmp));
                }
                return;
            }
            foreach(var book in CartCollection.Basket)
            {
                
                if (book.BookByItself.Name == _book.Name)
                {
                    isAdded = true;
                    break;
                }
                if (isAdded == false)
                {
                    CartCollection.Basket.Add(new BookInDaBasket(1, _book));
                }
            }

            if(isAdded==false)
            {
                CartCollection.Basket.Add(new BookInDaBasket(1, _book));
            }

        }

        private  void LoadData()
        {
            //var 0 Cache
            var res = ClientCache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());

            if (res is null)
            {
                return;
            }

            if(res.Books is null)
            {
                return;
            }

            foreach (var book in res.Books)
            {
                Books.Add(book);
            }

            //var 1
            /*ClientsCore clients = new();
            var requestResult = await clients.SendRequestAsync("NONE", ComandsLib.GetAllBooks);
            if(requestResult.IsFailure)
            {
                foreach (var error in requestResult.Errors)
                {
                    Books.Add(new Book() { Name = error.ErrorType });
                }
                return;
                
            }

            var result = (RequestResult<RequestResponseBase>)requestResult;

            if (result.Value is GetBookResponse bookResponse)
            {
                if(bookResponse.Command == ComandsLib.ERROR)
                {
                    Books.Add(new Book() { Name = "ERROR type" });
                    return;
                }

                foreach (var book in bookResponse.Books!)
                {
                    Books.Add(book);
                }
            }*/
            

            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now }); 
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            //Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
        }
    }
}
