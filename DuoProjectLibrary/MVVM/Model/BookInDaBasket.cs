using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.Model
{
    public class BookInDaBasket
    {
        Book bookByItself;
        int amount;
        public BookInDaBasket(int _am,Book _book)
        {
            bookByItself = _book;
            amount = _am;
        }
        public Book BookByItself { get { return bookByItself; } set { bookByItself = value; } }
        public int Amount { get { return amount; } set { amount = value; } }
    }
}
