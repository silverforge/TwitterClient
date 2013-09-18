using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.ViewModels
{
	public class AccountViewModel : BaseViewModel, IAccountViewModel
	{
		private readonly IAppSettings appSettings;
		private string accessToken;
		private string accessTokenSecret;

		public AccountViewModel(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

		public string AccessToken
		{
			get { return accessToken; }
			set
			{
				accessToken = value;
				NotifyOfPropertyChange(() => AccessToken);
				NotifyOfPropertyChange(() => CanLogin);
			}
		}

		public string AccessTokenSecret
		{
			get { return accessTokenSecret; }
			set
			{
				accessTokenSecret = value;
				NotifyOfPropertyChange(() => AccessTokenSecret);
				NotifyOfPropertyChange(() => CanLogin);
			}
		}

		public bool CanLogin
		{
			get { return !string.IsNullOrEmpty(AccessToken) && !string.IsNullOrEmpty(AccessTokenSecret); }
		}

		public void Login()
		{
			appSettings.AccessToken = AccessToken;
			appSettings.AccessTokenSecret = AccessTokenSecret;

			NavigationManager.SendMessage(MessageKeys.Authenticated);
		}
	}
}