using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Fields;

namespace UwpPresenter.ViewModels
{
    class UwpMain
    {
        private Observable<int> _currentPage = new Observable<int>();

        public int SelectedPage { get { return _currentPage; } set { _currentPage.Value = value; } }
    }
}
