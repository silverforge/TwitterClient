using System;
using System.Globalization;
using System.Windows.Data;

namespace Silverforge.TwitterClient.Converters
{
	public class BoolToOpacityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value)
				return 0.5d;

			return 0.1d;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (((double) value).Equals(0.5))
				return true;

			return false;
		}
	}
}