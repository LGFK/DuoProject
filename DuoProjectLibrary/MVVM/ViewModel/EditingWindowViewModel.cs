using DuoProjectLibrary.Infrastructure;
using DuoProjectLibrary.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class EditingWindowViewModel:BaseViewModel
    {
        
        BookWithAllFields book;

        public BookWithAllFields BookWithAllFields
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

        public EditingWindowViewModel(BookWithAllFields book)
        {
            this.book = book;
        }


    }
}
