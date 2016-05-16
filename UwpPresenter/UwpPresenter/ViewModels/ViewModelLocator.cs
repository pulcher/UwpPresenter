using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.XAML;

namespace UwpPresenter.ViewModels
{
    class ViewModelLocator : ViewModelLocatorBase
    {
        //private UwpMainModel _uwpMain;

        public object Main
        {
            get { return ViewModel(() => new UwpMainModel()); }
        }
    }
}
