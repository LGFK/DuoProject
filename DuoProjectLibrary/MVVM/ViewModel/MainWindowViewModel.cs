using DuoProjectLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class MainWindowViewModel:BaseViewModel
    {
        public Uri _observaPage;
        public Uri CurrentPage
        {
            get { return _observaPage; }
            set { _observaPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }
        ICommand _allBooksButtonClick;
        public ICommand AllBooksButtonClick
        {
            get => _allBooksButtonClick ?? (_allBooksButtonClick = new RelayCommand(AllBooks));
        }

        void AllBooks(object? param)
        {
            CurrentPage = new Uri("AllBooksPage.xaml", UriKind.Relative);
        }

        public MainWindowViewModel()
        {
            CurrentPage = new Uri("GreatingPage.xaml", UriKind.Relative);
        }
            
    }
}
