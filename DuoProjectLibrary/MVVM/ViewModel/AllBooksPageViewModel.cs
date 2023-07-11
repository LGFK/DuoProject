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
        
        BookWithAllFields? _book;
        public BookWithAllFields Book
        {
            get { return _book; }
            set { _book = value; 
             OnPropertyChanged(); }
        }
        ObservableCollection<BookWithAllFields> _books;
        public ObservableCollection<BookWithAllFields> Books
        {
            get { return _books; }
            set { _books = value; }
        }
        public AllBooksPageViewModel()
        {
            try
            {
                _books = new ObservableCollection<BookWithAllFields>();
                LoadData();
                service = ApplicationState.ModalWindowService;
            }
            catch (Exception ex)
            {
                _books = new ObservableCollection<BookWithAllFields>();
                Books.Add(new BookWithAllFields());
                Books[0].Book = new Book()
                {
                    Id = 32324,
                    Name = "Book1",
                    NameAuthor = "Author1",
                    Cost = 1,
                    Genre = "Ganre1",
                    NumberOfPages = 1,
                    PriceForSale = 1,
                    Publisher = "Publisher1",
                    TimeOfPublication = DateTime.Now
                };
                Books[0].Genre = "genre1";
                Books[0].Author = "Author1";
                Books[0].Amount = 20;
                service = ApplicationState.ModalWindowService;
            }
           
            
        }
        ICommand _openEditor;

        public ICommand OpenEditorCommand 
        {
            get => _openEditor ?? (_openEditor = new RelayCommand(OpenEditWMethod));
        }
        private void OpenEditWMethod(object param)
        {
            if(param is BookWithAllFields bookWithAllFields)
            {
                var _editVM = new EditingWindowViewModel(bookWithAllFields);
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
            if (s is BookWithAllFields _bookTmp)
            {
                foreach (var book in CartCollection.Basket)
                {

                    if (book.BookByItself.Name == _bookTmp.Book.Name)
                    {
                        isAdded = true;
                        if(book.Amount<_bookTmp.Amount)
                        book.Amount+=1;
                        break;
                    }
                   
                }
                if (isAdded == false)
                {
                    CartCollection.Basket.Add(new BookInDaBasket(1, _bookTmp.Book));
                }
                return;
            }
            foreach(var book in CartCollection.Basket)
            {
                
                if (book.BookByItself.Name == _book.Book.Name)
                {
                    isAdded = true;
                    break;
                }
                if (isAdded == false)
                {
                    CartCollection.Basket.Add(new BookInDaBasket(1, _book.Book));
                }
            }

            if(isAdded==false)
            {
                CartCollection.Basket.Add(new BookInDaBasket(1, _book.Book));
            }

        }

        private  void LoadData()
        {
            //var 0 cahe
             var res = ClientCache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());
            Books.Add(new BookWithAllFields());
            Books[0].Book = new Book()
            {
                Id = 32324,
                Name = "Book1",
                NameAuthor = "Author1",
                Cost = 1,
                Genre = "Ganre1",
                NumberOfPages = 1,
                PriceForSale = 1,
                Publisher = "Publisher1",
                TimeOfPublication = DateTime.Now
            };
            Books[0].Genre = "Genre1";
            Books[0].Author = "Author1";
            Books[0].Amount = 20;
            
            //if (res is null)
            //{

            //    return;
            //}

            //if (res.Books is null)
            //{
            //    return;
            //}

            //foreach (var book in res.Books)
            //{
            //    Books.Add(book);
            //}
            

            //var 1
             //ClientsCore clients = new();
             //var requestResult = await clients.SendRequestAsync("NONE", ComandsLib.GetAllBooks);
             //if(requestResult.IsFailure)
             //{
             //    foreach (var error in requestResult.Errors)
             //    {
             //        Books.Add(new Book() { Name = error.ErrorType });
             //    }
             //    return;

             //}

             //var result = (RequestResult<RequestResponseBase>)requestResult;

             //if (result.Value is GetBookResponse bookResponse)
             //{
             //    if(bookResponse.Command == ComandsLib.ERROR)
             //    {
             //        Books.Add(new Book() { Name = "ERROR type" });
             //        return;
             //    }

             //    foreach (var book in bookResponse.Books!)
             //    {
             //        Books.Add(book);
             //    }
             //}


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
