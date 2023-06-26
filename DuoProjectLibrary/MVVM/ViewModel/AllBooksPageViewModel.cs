using DuoProjectLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    internal class AllBooksPageViewModel:BaseViewModel
    {
        string? _book;
        public string Book
        {
            get { return _book; }
            set { _book = value; OnPropertyChanged();}
        }
        ObservableCollection<string> _books;
        public ObservableCollection<string> Books
        {
            get { return _books; }
            set { _books = value;}
        }
        public AllBooksPageViewModel()
        {
            _books = new ObservableCollection<string>();
            LoadData();
        }

        private void LoadData()
        {
            for (int i = 0; i < 10; i++)
            {
                _books.Add(i.ToString());
            }
        }
    }
}
