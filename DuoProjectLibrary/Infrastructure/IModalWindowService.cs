using DuoProjectLibrary.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.Infrastructure
{
    public interface IModalWindowService
    {
        public void ShowModalWindow(BaseViewModel viewModel);
    }
}
