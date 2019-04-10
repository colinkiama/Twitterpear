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
            set
            {
                _tweetContent = value;
                NotifyPropertyChanged();
            }
        }

        private string _publishedTweetURL;

        public string PublishedTweetURL
        {
            get { return _publishedTweetURL; }
            set
            {
                _publishedTweetURL = value;
                NotifyPropertyChanged();
            }
        }

        private bool _tweetPublishAttempted;

        public bool TweetPublishAttempted
        {
            get { return _tweetPublishAttempted; }
            set
            {
                _tweetPublishAttempted = value;
                NotifyPropertyChanged();
            }
        }




        public RelayCommand TweetCommand;

        public MainViewModel()
        {
            TweetCommand = new RelayCommand(async () => await CreateTweetAsync().AsAsyncAction());
        }

        internal void LoadUser(Tweetinvi.Models.IAuthenticatedUser user)
        {
            User = user;
        }

        private async Task CreateTweetAsync()
        {
            var publishedTweetDetails = await TweetAsync.PublishTweet(_tweetContent);
            TweetPublishAttempted = true;
            if (publishedTweetDetails.IsTweetPublished)
            {
                PublishedTweetURL = publishedTweetDetails.Url;
            }
        }
    }
}
