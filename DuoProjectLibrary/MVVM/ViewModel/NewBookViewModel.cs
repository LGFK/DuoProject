using DuoProjectLibrary.Infrastructure;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class NewBookViewModel:BaseViewModel
    {
        string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        int price;
        public int Price
        {
            get => price; set
            {
                price = value; OnPropertyChanged();
            }
        }
        int primeCost;
        public int PrimeCost
        {
            get => primeCost;
            set
            {
                primeCost = value; OnPropertyChanged();
            }
        }
        string author;
        public string Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged(); }

        }

        string genre;
        public string Genre
        { get { return genre; } set {  genre = value; OnPropertyChanged(); } }


        string publisher;
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; OnPropertyChanged(); }

        }

        string publicationDate;
        public string PublicationDate
        {
            get { return publicationDate; }
            set { publicationDate = value; OnPropertyChanged(); }
        }
        int numberOfPages;

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set { numberOfPages = value; OnPropertyChanged(); }
        }

        ICommand _addBttn;
        public ICommand AddBttn
        {
            get { return _addBttn ?? (_addBttn = new RelayCommand(AddBttnClickMethod)); }
        }
        void AddBttnClickMethod(object param)
        {
            var bookToAdd = new Book();
            bookToAdd.Cost = Price;
            bookToAdd.Author = new Author();
            bookToAdd.Author.Name = this.Author;
            bookToAdd.NumberOfPages = NumberOfPages;
            bookToAdd.Publisher = new ModelsLibrary.Publisher();
            bookToAdd.Publisher.Name = this.Publisher;
            bookToAdd.Genre = new ModelsLibrary.Genre();
            bookToAdd.Genre.Name = this.Genre;
            bookToAdd.PriceForSale = this.PrimeCost;
            bookToAdd.TimeOfPublication = StringToDate(publicationDate);

            //тут должен быть метод добавления этой книги в бд.
        }

        public NewBookViewModel()
        {
            
        }
        public DateTime StringToDate(string dateStr)
        {
            try
            {
                int day = Int32.Parse(dateStr.Split(":")[2]);
                int month = Int32.Parse(dateStr.Split(":")[1]);
                int year = Int32.Parse(dateStr.Split(":")[0]);
                Months m = (Months)month;
                string dateToParse = $"{m} {day}, {year}";
                return DateTime.Parse(dateToParse);
            }
            catch(FormatException ex)
            {
                System.Windows.MessageBox.Show("there's something wrong with your input");
            }
            catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message);}
            throw new Exception("something went wrong");


        }
    }

}
