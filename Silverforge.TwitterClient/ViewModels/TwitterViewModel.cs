using System.Linq;
using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;
using TweetSharp;

namespace Silverforge.TwitterClient.ViewModels
{
	public class TwitterViewModel : BaseViewModel, ITwitterViewModel
	{
		private readonly IAppSettings appSettings;

		public TwitterViewModel(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
			Tweets = new BindableCollection<TwitterStatus>();
		}

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			var service = new TwitterService(appSettings.ConsumerKey, appSettings.ConsumerSecret);
			service.AuthenticateWith(appSettings.AccessToken, appSettings.AccessTokenSecret);
			var listTweetsOnHomeTimeline = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
			Tweets.AddRange(listTweetsOnHomeTimeline);

			//Tweets.First().User.Name
		}

		public BindableCollection<TwitterStatus> Tweets { get; private set; }
	}
}