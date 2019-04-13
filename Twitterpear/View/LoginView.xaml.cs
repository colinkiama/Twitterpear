using Microsoft.Toolkit.Services.Twitter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
           
        }
        

        private async Task TryLogin()
        {
            // Login to Twitter
            if (!await TwitterService.Instance.LoginAsync())
            {
                return;
            }
            else
            {
                SettingsHelper.SetUserAsLoggedIn();
                Frame.Navigate(typeof(MainView));
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await TryLogin();
        }
    }
}
