using System;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using Caliburn.Micro;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.ViewModels
{
	public class TweetViewModel : BaseViewModel, ITweetViewModel
	{
		private readonly IAppSettings appSettings;
		private TwitterService service;

		public TweetViewModel(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
			Tweets = new BindableCollection<Tweet>();
		}

		public BindableCollection<Tweet> Tweets { get; private set; }

		public void ReadTweet(Tweet tweet)
		{
			tweet.IsNew = false;
		}

		public void Favorite(Tweet tweet)
		{
			if (tweet.IsFavorited)
				service.UnfavoriteTweet(new UnfavoriteTweetOptions { Id = tweet.Id });
			else
				service.FavoriteTweet(new FavoriteTweetOptions { Id = tweet.Id });

			var twitterResponse = service.Response;
			if (twitterResponse.StatusCode == HttpStatusCode.OK)
				tweet.IsFavorited = !tweet.IsFavorited;
		}

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			service = new TwitterService(appSettings.ConsumerKey,
			                             appSettings.ConsumerSecret,
			                             appSettings.AccessToken,
			                             appSettings.AccessTokenSecret)
				{
					IncludeRetweets = true
				};


			Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(60)).Subscribe(next => AddNewTweets());
		}

		private void AddNewTweets()
		{
			long? maxId = null;
			if (Tweets.Count > 0)
				maxId = Tweets.Max(t => t.Id);

			var downloadedTweets =
				service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions {Count = 40, SinceId = maxId})
				.ToArray();

			if (!downloadedTweets.Any())
				return;

			for (var i = downloadedTweets.Length - 1; i >= 0; i--)
			{
				var ts = downloadedTweets[i];
				Tweets.Insert(0, new Tweet
				{
					Id = ts.Id,
					ImageUrl = ts.User.ProfileImageUrl,
					Text = FormatHelper.HtmlToBbCodeText(ts.TextAsHtml),
					UserFullName = ts.User.Name,
					Created = FormatHelper.UniDate(ts.CreatedDate),
					IsNew = true,
					IsFavorited = ts.IsFavorited
				});
			}
		}

		private void AddNewTweetsStatic()
		{
			for (var i = 0; i < 4; i++)
			{
				Tweets.Insert(0, new Tweet
					{
						Id = 1,
						ImageUrl = "http://a0.twimg.com/profile_images/195275920/square-logo-no-text-2_normal.png",
						Text =
							FormatHelper.HtmlToBbCodeText(
								"RT @andyjohnw: Today we start adding a sharding @neo4j adapter to @phalconphp with option of async writes via #redis  #geekexcitement"),
						UserFullName = "neo4j",
						Created = FormatHelper.UniDate(DateTime.Now),
						IsNew = true,
						IsFavorited = true
					});
			}
		}
	}
}