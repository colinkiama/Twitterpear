using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Twitterpear.Core;
using Twitterpear.Helpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Twitterpear.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            this.InitializeComponent();
            AuthHelper.UserLoggedIn += AuthHelper_UserLoggedIn;
        }

        private void AuthHelper_UserLoggedIn(object sender, Tweetinvi.Models.IAuthenticatedUser e)
        {
            Frame.Navigate(typeof(MainView), e);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await AuthHelper.TwitterAuth();
        }
    }
}
