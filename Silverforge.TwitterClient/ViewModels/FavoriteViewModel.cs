using System.Linq;
using System.Net;
using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.ViewModels
{
	public class FavoriteViewModel : BaseViewModel, IFavoriteViewModel
	{
		private readonly TwitterService service;
		private bool isLoading;

		public FavoriteViewModel(TwitterService service)
		{
			this.service = service;
			Tweets = new BindableCollection<Tweet>();
		}

		public BindableCollection<Tweet> Tweets { get; private set; }

		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;
				NotifyOfPropertyChange(() => IsLoading);
			}
		}

		protected override void OnViewLoaded(object view)
		{
			IsLoading = true;
			base.OnViewLoaded(view);

			var favoriteTweets = service.ListFavoriteTweets(new ListFavoriteTweetsOptions {Count = 40});

			foreach (var tweet in favoriteTweets.Select(TweetTransformer.TweetMapper))
			{
				Tweets.Add(tweet);
			}
			IsLoading = false;
		}

		public void Favorite(Tweet tweet)
		{
			if (tweet.IsFavorited)
				service.UnfavoriteTweet(new UnfavoriteTweetOptions { Id = tweet.OriginalId });
			else
				service.FavoriteTweet(new FavoriteTweetOptions { Id = tweet.OriginalId });

			var twitterResponse = service.Response;
			if (twitterResponse.StatusCode == HttpStatusCode.OK)
				tweet.IsFavorited = !tweet.IsFavorited;
		}

		public void LoadMoreTweets()
		{
			IsLoading = true;
			var historicalTweets =
				service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = 40, MaxId = Tweets.Min(t => t.OriginalId) });

			if (historicalTweets == null)
			{
				IsLoading = false;
				return;
			}

			var twitterStatuses = historicalTweets.ToArray();

			foreach (var ts in twitterStatuses.Where(ts => Tweets.FirstOrDefault(t => t.OriginalId == ts.Id) == null))
			{
				TweetTransformer.TweetMapper(ts);
			}

			IsLoading = false;
		}
	}
}
