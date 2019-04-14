using Microsoft.Toolkit.Services.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterpear.Base;
using Twitterpear.Commands;
using Twitterpear.Helpers;
using Twitterpear.View;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Twitterpear.ViewModel
{
    class MainViewModel : Notifier
    {
        private const string TwitterUrlString = "https://www.twitter.com/";
        private TwitterUser _user;

        public TwitterUser User
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

        internal async Task LoadUser()
        {
            try
            {
                User = await TwitterService.Instance.GetUserAsync();
            }
            catch
            {
                await ShowNetworkError();
                GoToLoginView();
            }
        }

        private async Task ShowNetworkError()
        {
            var tweetErrorDialog = new ContentDialog
            {
                Title = "Network Error",
                Content = "Can't send the tweet without internet access",
                CloseButtonText = "Ok"
            };
            await tweetErrorDialog.ShowAsync();
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
        public RelayCommand LogoutCommand;

        public MainViewModel()
        {
            TweetCommand = new RelayCommand(async () => await CreateTweetAsync().AsAsyncAction());
            LogoutCommand = new RelayCommand(() => Logout());
        }

        private void Logout()
        {
            TwitterService.Instance.Logout();
            SettingsHelper.SetUserAsLoggedOut();

            GoToLoginView();
           
        }

        private void GoToLoginView()
        {
            var currentFrame = Window.Current.Content as Frame;
            if (currentFrame != null)
            {
                currentFrame.Navigate(typeof(LoginView));
                currentFrame.BackStack.Clear();
            }
        }

        private async Task CreateTweetAsync()
        {
            try
            {
                bool wasTweetPublished = await TwitterService.Instance.TweetStatusAsync(_tweetContent);
                if (wasTweetPublished)
                {
                    TweetPublishAttempted = true;
                    PublishedTweetURL = $"{TwitterUrlString}{User.ScreenName}";
                }
            }
            catch
            {
                await ShowNetworkError();
            }
            

        }
    }
}
