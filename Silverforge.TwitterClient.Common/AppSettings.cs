using System;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Common
{
	public class AppSettings : IAppSettings
	{
		public AppSettings()
		{
		}

		private static int SafeConvertToInt(string value)
		{
		    return string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
		}
	}
}