using System;
using System.Configuration;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Common
{
	public class AppSettings : IAppSettings
	{
		public AppSettings()
		{
			ConsumerKey = ConfigurationManager.AppSettings.Get("consumerKey");
			ConsumerSecret = ConfigurationManager.AppSettings.Get("consumerSecret");
			AccessToken = ConfigurationManager.AppSettings.Get("accessToken");
			AccessTokenSecret = ConfigurationManager.AppSettings.Get("accessTokenSecret");

			PollInterval = SafeToInt(ConfigurationManager.AppSettings.Get("pollInterval"));
		}

		public string ConsumerKey { get; private set; }
		public string ConsumerSecret { get; private set; }

		public string AccessToken { get; private set; }
		public string AccessTokenSecret { get; private set; }

		public int PollInterval { get; set; }

		private int SafeToInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return int.MaxValue;

			return Convert.ToInt32(value);
		}
	}
}
