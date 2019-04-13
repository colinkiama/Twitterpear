using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Twitterpear.Helpers
{
    public class SettingsHelper
    {
        private const string loggedInSettingsKey = "loggedIn";
        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        internal static void SetUserAsLoggedIn()
        {
            localSettings.Values[loggedInSettingsKey] = true;
        }

        internal static void SetUserAsLoggedOut()
        {
            localSettings.Values[loggedInSettingsKey] = false;
        }

        internal static bool CheckIfUserHasLoggedIn()
        {
            bool hasUserLoggedIn = false;
            if (localSettings.Values[loggedInSettingsKey] != null)
            {
                hasUserLoggedIn = (bool)localSettings.Values[loggedInSettingsKey];
            }

            return hasUserLoggedIn;
        }
    }
}
