using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Twitterpear.Base;
using Twitterpear.Commands;

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

        private string _tweetContent;

        public string TweetContent
        {
            get { return _tweetContent; }
            set {
                _tweetContent = value;
                NotifyPropertyChanged();
            }
        }


        public RelayCommand TweetCommand;

        public MainViewModel()
        {
            TweetCommand = new RelayCommand(() => CreateTweet());
        }

        internal void LoadUser(Tweetinvi.Models.IAuthenticatedUser user)
        {
            User = user;
            
        }

        private void CreateTweet()
        {
            Tweet.PublishTweet(_tweetContent);
        }
    }
}
