using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DuoProjectLibrary.MVVM.Model
{
    public class BookWithAllFields
    {
        Book bookByItself;
        public Book Book { get { return bookByItself; } set { bookByItself = value; } }

        int amount;

        public int Amount { get { return amount; } set {  amount = value; } }

        string author;

        public string Author { get { return author; } set { author = value; } }

        string genre;

        public string Genre { get {  return genre; } set {  genre = value; } }
    }
}
