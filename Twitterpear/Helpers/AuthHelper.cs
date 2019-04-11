using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Twitterpear.Core;
using Twitterpear.Enums;
using Windows.Foundation;
using Windows.Storage;
using Windows.System;

namespace Twitterpear.Helpers
{
    // Keeping this static because authentication info will be needed across 
    //the whole lifetime of the app.
    internal static class AuthHelper
    {
        
        private static IAuthenticationContext _authenticationContext;
        public static event EventHandler<IAuthenticatedUser> UserLoggedIn;
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

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

        internal static void ValidateTwitterAuth(Uri calledbackUri)
        {
            const string verifierQueryName = "oauth_verifier";

            // Get verifier code from the URI
            WwwFormUrlDecoder urlDecoder = new WwwFormUrlDecoder(calledbackUri.AbsoluteUri);
            string verifierCode = urlDecoder.GetFirstValueByName(verifierQueryName);

            // Create the user credentials
            var userCreds = AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, _authenticationContext);
            var user = Tweetinvi.User.GetAuthenticatedUser(userCreds);

            // Gives class credentials to use in future operations
            Auth.SetUserCredentials(userCreds.ConsumerKey, userCreds.ConsumerSecret, userCreds.AccessToken, userCreds.AccessTokenSecret);

            // Return User through UserLoggedIn event
            UserLoggedIn?.Invoke(null, user);

            StoreToken(userCreds);
        }

        private static void StoreToken(ITwitterCredentials userCreds)
        {
            // Save Token details so app can automatically login to user.
            var token = _authenticationContext.Token;
            SaveTokenSetting(TokenValueType.AuthorizationKey, token.AuthorizationKey);
            SaveTokenSetting(TokenValueType.AuthorizationSecret, token.AuthorizationSecret);
            SaveTokenSetting(TokenValueType.AccessToken, userCreds.AccessToken);
            SaveTokenSetting(TokenValueType.AccessTokenSecret, userCreds.AccessTokenSecret);
        }

        private static void SaveTokenSetting(TokenValueType tokenValueType, string tokenValue)
        {
            localSettings.Values[nameof(tokenValueType)] = tokenValue;
            Debug.WriteLine("Token Stored!");
        }

        internal static bool TryRestoreToken()
        {
            string authKey = LoadTokenSetting(TokenValueType.AuthorizationKey);
            if (authKey == string.Empty)
            {
                return false;
            }
            string authSecret = LoadTokenSetting(TokenValueType.AuthorizationSecret);
            string token = LoadTokenSetting(TokenValueType.AccessToken);
            string tokenSecret = LoadTokenSetting(TokenValueType.AccessTokenSecret);

            Auth.SetUserCredentials(authKey, authSecret, token, tokenSecret);
            
            return true;
        }

        private static string LoadTokenSetting(TokenValueType tokenValueType)
        {
            string valueToReturn = "";
            string tokenValueTypeString = nameof(tokenValueType);
            if (localSettings.Values[tokenValueTypeString] != null)
            {
                valueToReturn = (string)localSettings.Values[tokenValueTypeString];
            }
            return valueToReturn;
        }
    }
}
