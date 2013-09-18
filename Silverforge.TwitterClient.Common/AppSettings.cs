using System.Configuration;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Common
{
	public class AppSettings : IAppSettings
	{
		private string accessToken;
		private string accessTokenSecret;

		public AppSettings()
		{
			ConsumerKey = ConfigurationManager.AppSettings.Get("consumerKey");
			ConsumerSecret = ConfigurationManager.AppSettings.Get("consumerSecret");
		}

		public string ConsumerKey { get; private set; }
		public string ConsumerSecret { get; private set; }

		public string AccessToken
		{
			get { return accessToken; }
			set
			{
				if (string.IsNullOrEmpty(accessToken))
					accessToken = value;
			}
		}

		public string AccessTokenSecret
		{
			get { return accessTokenSecret; }
			set
			{
				if (string.IsNullOrEmpty(accessTokenSecret))
					accessTokenSecret = value;
			}
		}
	}
}
