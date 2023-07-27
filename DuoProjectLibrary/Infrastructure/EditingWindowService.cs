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
        public void ShowModalWindow(BaseViewModel viewModel)
        {
            if (viewModel is EditingWindowViewModel eWVM)
            {
                var window = new EditingWindow()
                {
                    DataContext = eWVM
                };
                window.ShowDialog();
            }
            else if(viewModel is CustomMessageBoxViewModel cMBVM)
            {
                var window = new CustomMessageBoxView()
                {
                    DataContext = cMBVM
                };
                window.ShowDialog();
            }
            
            
        }
    }
}
