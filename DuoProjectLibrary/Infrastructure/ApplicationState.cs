using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoProjectLibrary.Infrastructure
{
    public class ApplicationState
    {
        private static IModalWindowService _modalWindowService;
        public static IModalWindowService ModalWindowService
        {
            get
            {
                return _modalWindowService ?? (_modalWindowService = new EditingWindowService());
            }
        }
    }
}
