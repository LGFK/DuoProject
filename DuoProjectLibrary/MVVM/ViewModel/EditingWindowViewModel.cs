using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class EditingWindowViewModel:BaseViewModel
    {
        
        Book book;

        public Book BookWithAllFields
        {
            get => book;
            set
            {
                if(book != value) 
                {
                    book = value;
                    OnPropertyChanged();
                }
            }
        }

        public EditingWindowViewModel(Book book)
        {
            this.book = book;
        }


    }
}
