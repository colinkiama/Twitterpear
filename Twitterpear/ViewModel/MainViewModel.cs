using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterpear.Base;

namespace Twitterpear.ViewModel
{
    class MainViewModel : Notifier
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        internal void LoadUser()
        {
            
        }
    }
}
