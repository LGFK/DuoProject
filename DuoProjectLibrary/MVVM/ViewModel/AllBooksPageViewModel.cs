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
            foreach(var book in CartCollection.Basket)
            {
                if(book.BookByItself.Name == _book.Name)
                {
                    isAdded = true;
                    break;
                }
            }
            if(isAdded==false)
            {
                CartCollection.Basket.Add(new BookInDaBasket(1, _book));
            }
            
            
        }

        private void LoadData()
        {
            
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now }); 
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now }); 
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
            Books.Add(new Book { Name = "Book1", NameAuthor = "Author1", Cost = 1, Genre = "Ganre1", NumberOfPages = 1, PriceForSale = 1, Publisher = "Publisher1", TimeOfPublication = DateTime.Now });
        }
    }
}
