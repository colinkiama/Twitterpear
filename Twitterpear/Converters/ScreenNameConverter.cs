using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Twitterpear.Converters
{
    class ScreenNameConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string valueToReturn = "";
            if (value is string screenName)
            {
                valueToReturn = '@' + screenName;
            }
            return valueToReturn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
