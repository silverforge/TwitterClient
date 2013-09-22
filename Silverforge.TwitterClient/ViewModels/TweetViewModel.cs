using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
		private readonly ITweetTimer tweetTimer;
		private TwitterService service;

		public TweetViewModel(IAppSettings appSettings, ITweetTimer tweetTimer)
		{
			this.appSettings = appSettings;
			this.tweetTimer = tweetTimer;
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

		public void SetAllRead()
		{
			foreach (var tweet in Tweets)
			{
				tweet.IsNew = false;
			}
		}

		public void CollapseAll()
		{
			foreach (var tweet in Tweets)
			{
				tweet.IsExpanded = false;
			}
		}

		public void ExpandAll()
		{
			foreach (var tweet in Tweets)
			{
				tweet.IsExpanded = true;
			}
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

			tweetTimer
				.Subject
				.Subscribe(next => AddNewTweets(),
				           err =>
					           {
						           Debug.WriteLine("Error");
					           },
				           () =>
					           {
						           Debug.WriteLine("Completed");
					           });
			tweetTimer.Start();
		}

		private void AddNewTweets()
		{
			long? sinceId = null;
			if (Tweets.Count > 0)
				sinceId = Tweets.Max(t => t.Id);

			var downloadedTweets =
				service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = 40, SinceId = sinceId });

			if ((int)service.Response.StatusCode == 429) // NOTE [MGJ] : Rate limit exceeded
			{
				service.GetRateLimitStatus(new GetRateLimitStatusOptions());
				tweetTimer.Delay(service.Response.RateLimitStatus.ResetTime);
			}

			if (downloadedTweets == null)
				return;

			var tweets = downloadedTweets.ToArray();

			for (var i = tweets.Length - 1; i >= 0; i--)
			{
				var ts = tweets[i];
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
						IsFavorited = true,
						IsExpanded = true
					});
			}
		}
	}
}