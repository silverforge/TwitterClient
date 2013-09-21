using System;
using System.Configuration;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Common
{
	public class AppSettings : IAppSettings
	{
		private const string AccentColorKey = "accentColor";
		private const string TextColorKey = "textColor";

		public AppSettings()
		{
			ConsumerKey = ConfigurationManager.AppSettings.Get("consumerKey");
			ConsumerSecret = ConfigurationManager.AppSettings.Get("consumerSecret");
			AccessToken = ConfigurationManager.AppSettings.Get("accessToken");
			AccessTokenSecret = ConfigurationManager.AppSettings.Get("accessTokenSecret");

			AccentColorHex = ConfigurationManager.AppSettings.Get(AccentColorKey);
			TextColorHex = ConfigurationManager.AppSettings.Get(TextColorKey);

			PollInterval = SafeToInt(ConfigurationManager.AppSettings.Get("pollInterval"));
		}

		public string ConsumerKey { get; private set; }
		public string ConsumerSecret { get; private set; }

		public string AccessToken { get; private set; }
		public string AccessTokenSecret { get; private set; }

		public int PollInterval { get; private set; }

		public string AccentColorHex { get; private set; }
		public string TextColorHex { get; private set; }

		public void SetAccentColor(string color)
		{
			AccentColorHex = color;
			SaveColor(AccentColorKey, color);
		}

		public void SetTextColor(string color)
		{
			TextColorHex = color;
			SaveColor(TextColorKey, color);
		}

		private static void SaveColor(string key, string value)
		{
			var openExeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			openExeConfiguration.AppSettings.Settings[key].Value = value;
			openExeConfiguration.Save();
		}

		private static int SafeToInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return int.MaxValue;

			return Convert.ToInt32(value);
		}
	}
}
