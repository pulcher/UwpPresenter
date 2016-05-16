using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpPresenter.ViewModels;

namespace UwpPresenter
{
    class Container
    {
        private readonly UwpMainModel _uwpMain;

        public Container()
        {
            _uwpMain = new UwpMainModel();
        }

        public UwpMainModel UwpMain => _uwpMain;
    }
}
