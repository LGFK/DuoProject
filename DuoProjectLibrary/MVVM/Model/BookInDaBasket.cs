using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.Model
{
    public class BookInDaBasket:INotifyPropertyChanged
    {
        Book bookByItself;
        int amount;
        public BookInDaBasket(int _am,Book _book)
        {
            bookByItself = _book;
            amount = _am;
        }
        public Book BookByItself { get { return bookByItself; } set { bookByItself = value; OnPropertyChanged(); } }
        public int Amount { get { return amount; } set { amount = value;OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string? name =null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
