using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.ViewModels
{
	public class TwitterViewModel : BaseViewModel, ITwitterViewModel
	{
		private readonly IAppSettings appSettings;

		public TwitterViewModel(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
			Tweets = new BindableCollection<Tweet>();
		}

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			var service = new TwitterService(appSettings.ConsumerKey, appSettings.ConsumerSecret, appSettings.AccessToken, appSettings.AccessTokenSecret);
			var listTweetsOnHomeTimeline = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());

			if (listTweetsOnHomeTimeline == null)
				return;

			foreach (var ts in listTweetsOnHomeTimeline)
			{
				var textAsHtml = ts.TextAsHtml.Replace("</a>", "[/url]").Replace("\" target=\"_blank\">", "]").Replace("<a href=\"", "[url=");
				Tweets.Add(new Tweet { ImageUrl = ts.User.ProfileImageUrl, Text = textAsHtml, UserFullName = ts.User.Name });
			}
		}

		public BindableCollection<Tweet> Tweets { get; private set; }
	}
}