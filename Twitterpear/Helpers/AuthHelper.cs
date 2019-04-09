using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Twitterpear.Core;
using Windows.System;

namespace Twitterpear.Helpers
{
    internal static class AuthHelper
    {
        private static IAuthenticationContext _authenticationContext;

        // Step 1 : Redirect user to go on Twitter.com to authenticate
        internal async static Task TwitterAuth()
        {
            var appCreds = new ConsumerCredentials(APIKeys.APIKey, APIKeys.SecretKey);

            // Specify the url you want the user to be redirected to
            var redirectURL = "twitterpear://callback";
            _authenticationContext = AuthFlow.InitAuthentication(appCreds, redirectURL);
            var authURI = new Uri(_authenticationContext.AuthorizationURL);
            // The browser is with the URI!
            await Launcher.LaunchUriAsync(authURI);
        }
    }
}
