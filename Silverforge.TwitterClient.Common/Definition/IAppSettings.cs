namespace Silverforge.TwitterClient.Common.Definition
{
	public interface IAppSettings
	{
		string ConsumerKey { get;  }
		string ConsumerSecret { get;  }

		string AccessToken { get; set; }
		string AccessTokenSecret { get; set; }
	}
}