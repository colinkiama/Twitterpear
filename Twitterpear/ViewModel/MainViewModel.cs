using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Twitterpear.Base;

namespace Twitterpear.ViewModel
{
    class MainViewModel : Notifier
    {
        private IAuthenticatedUser _user;

        public IAuthenticatedUser User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged();
            }
        }

        internal void LoadUser(Tweetinvi.Models.IAuthenticatedUser user)
        {
            User = user;
        }
    }
}
