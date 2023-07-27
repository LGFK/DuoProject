using DuoProjectLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.MVVM.ViewModel
{
    public class CustomMessageBoxViewModel:BaseViewModel
    {
        string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        public CustomMessageBoxViewModel(string msgText) 
        {
            message = msgText;
        }
    }
}
