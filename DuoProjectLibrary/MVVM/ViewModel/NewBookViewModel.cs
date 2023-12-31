﻿using ClientCore.Core;
using DuoProjectLibrary.Infrastructure;
using ModelsLibrary;
using System;
using System.Linq;
using System.Windows.Input;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class NewBookViewModel : BaseViewModel
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
        { get { return genre; } set { genre = value; OnPropertyChanged(); } }


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
        async void AddBttnClickMethod(object param)
        {
            try
            {
                var bookToAdd = new Book();
                bookToAdd.Name = Title;
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

                ClientsCore clientsCore = new ClientsCore();
                var net = await clientsCore.Connected();
                await clientsCore.AddBook(net.Value, bookToAdd);
                CustomMessageBoxViewModel vM = new CustomMessageBoxViewModel("Book Added");
                ApplicationState.ModalWindowService.ShowModalWindow(vM);
            }
            catch (Exception ex)
            {
                CustomMessageBoxViewModel vM = new CustomMessageBoxViewModel(ex.Message);
                ApplicationState.ModalWindowService.ShowModalWindow(vM);
            }
        }

        public NewBookViewModel()
        {

        }
        public DateTime StringToDate(string dateStr)
        {
            try
            {
                if (dateStr.Split(":").Count() < 3)
                {
                    throw new FormatException();
                }
                int day = Int32.Parse(dateStr.Split(":")[2]);
                int month = Int32.Parse(dateStr.Split(":")[1]);
                int year = Int32.Parse(dateStr.Split(":")[0]);
                Months m = (Months)month;
                string dateToParse = $"{m} {day}, {year}";

                return DateTime.Parse(dateToParse);

            }
            catch (FormatException ex)
            {
                CustomMessageBoxViewModel vM = new CustomMessageBoxViewModel("there's something wrong with your input");
                ApplicationState.ModalWindowService.ShowModalWindow(vM);
            }
            catch (Exception ex)
            {
                CustomMessageBoxViewModel vM = new CustomMessageBoxViewModel(ex.Message);
                ApplicationState.ModalWindowService.ShowModalWindow(vM);

            }
            throw new Exception("something went wrong");


        }
    }

}
