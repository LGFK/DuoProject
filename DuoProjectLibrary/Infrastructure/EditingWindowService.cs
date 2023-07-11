using DuoProjectLibrary.MVVM.View;
using DuoProjectLibrary.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.Infrastructure
{
    public class EditingWindowService:IModalWindowService
    {
        public void ShowModalWindow(EditingWindowViewModel viewModel)
        {
            var window = new EditingWindow()
            {
                DataContext = viewModel
            };
            window.ShowDialog();
        }
    }
}
