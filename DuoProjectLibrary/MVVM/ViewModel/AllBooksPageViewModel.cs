using ClientCore.Core;
using ClientCore.Helpes;
using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;
using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using ModelsLibrary;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel;

internal class AllBooksPageViewModel : BaseViewModel
{
    readonly private IModalWindowService service;

    Book? _book;
    public Book Book
    {
        get { return _book; }
        set
        {
            _book = value;
            OnPropertyChanged();
        }
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
        if (param is Book book)
        {
            var _editVM = new EditingWindowViewModel(book);
            service.ShowModalWindow(_editVM);
        }

    }

    ICommand _addToCartCmd;

    public ICommand AddToCartCommand
    {
        get => _addToCartCmd ?? (_addToCartCmd = new RelayCommand(AddBookInDaCart));
    }
    private void AddBookInDaCart(object s)
    {
        bool isAdded = false;

        try
        {
            if (s is Book _bookTmp)

            {
                if (_bookTmp.CountBooks.Count > 0)
                {
                    foreach (var book in CartCollection.Basket)
                    {

                        if (book.BookByItself.Name == _bookTmp.Name)
                        {
                            isAdded = true;
                            if (book.Amount < _bookTmp.CountBooks.Count)
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
                else
                {
                    System.Windows.MessageBox.Show("Out Of Stock");
                }

            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message);
        }
    }

    private async void LoadData()
    {
        ClientsCore clientsCore = new ClientsCore();
        var res = await clientsCore.SendRequestAsync(ComandsLib.GetAllBooks);

        if (res.IsFailure)
        {
            System.Windows.MessageBox.Show($"{res.Errors}");
            return;
        }

        var resultResponseBase = (RequestResult<RequestResponseBase>)res;

        if (resultResponseBase.Value is GetBookResponse books)
        {
            if (books.Command == ComandsLib.ERROR)
            {
                System.Windows.MessageBox.Show($"Error:{res.Errors}");
                return;
            }

            foreach (var book in books.Books)
            {
                Books.Add(book);
            }
        }
    }
}

/* var res = ClientCache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());
 //
 ClientsCore core = new ClientsCore();
 var net = await core.Connected();
 var books = await core.SendResultAsync(ComandsLib.GetAllBooks,net.Value);
 var com = (GetBookResponse)books.Value;
 //if()
 try
 {
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
 catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }*/