using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class EditingWindowViewModel:BaseViewModel
    {
        
        Book book;

        public Book Book
        {
            get => book;
            set
            {
                if(book != value) 
                {
                    book = new Book();
                    book.Name = value.Name;
                    book.Cost = value.Cost;
                    book.Author = value.Author;
                    book.AuthorId = value.AuthorId;
                    book.NumberOfPages = value.NumberOfPages;
                    book.Publisher = value.Publisher;
                    book.PublisherId = value.PublisherId;
                    book.Genre = value.Genre;
                    book.GenreId = value.GenreId;
                    book.Id = value.Id;
                    book.CountBooks = value.CountBooks;
                    book.PriceForSale = value.PriceForSale;
                    book.TimeOfPublication = value.TimeOfPublication;
                    OnPropertyChanged();
                }
            }
        }

        public EditingWindowViewModel(Book book)
        {
            this.Book = book;
        }

        ICommand applyChanges;

        public ICommand ApplyChangesCommand
        {
            get => applyChanges ??(applyChanges=new RelayCommand(ApplyChangesMethod)); 
        }
        private void ApplyChangesMethod(object p)
        {
            //client.EditBook(book)
            MessageBox.Show("Test");
        }


    }
}
