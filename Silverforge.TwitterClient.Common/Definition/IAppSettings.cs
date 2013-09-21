namespace Silverforge.TwitterClient.Common.Definition
{
	public interface IAppSettings
	{
		string ConsumerKey { get;  }
		string ConsumerSecret { get;  }
		string AccessToken { get; }
		string AccessTokenSecret { get; }

		int PollInterval { get; }

		string AccentColorHex { get; }
		string TextColorHex { get; }

		void SetAccentColor(string color);
		void SetTextColor(string color);
	}
}