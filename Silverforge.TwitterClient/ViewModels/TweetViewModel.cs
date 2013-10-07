using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.ViewModels
{
	public class TweetViewModel : BaseViewModel, ITweetViewModel
	{
		private enum ListPositionType : byte
		{
			Top,
			Bottom
		}

		private readonly ITweetTimer tweetTimer;
		private readonly TwitterService service;
		private string rateRatio;
		private string resetTime;
		private bool isDelayed;
		private bool isLoading;

		public TweetViewModel(ITweetTimer tweetTimer, TwitterService service)
		{
			this.tweetTimer = tweetTimer;
			this.service = service;

			Tweets = new BindableCollection<Tweet>();
		}

		public BindableCollection<Tweet> Tweets { get; private set; }

		public string RateRatio
		{
			get { return rateRatio; }
			set
			{
				rateRatio = value;
				NotifyOfPropertyChange(() => RateRatio);
			}
		}

		public string ResetTime
		{
			get { return resetTime; }
			set
			{
				resetTime = value;
				NotifyOfPropertyChange(() => ResetTime);
			}
		}

		public bool IsDelayed
		{
			get { return isDelayed; }
			set
			{
				isDelayed = value;
				NotifyOfPropertyChange(() => IsDelayed);
			}
		}

		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;
				NotifyOfPropertyChange(() => IsLoading);
			}
		}

		public void ReadTweet(Tweet tweet)
		{
			tweet.IsNew = false;
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

		public void LoadMoreTweets()
		{
			if (isLoading)
				return;

			IsLoading = true;

			var historicalTweets =
				service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = 40, MaxId = Tweets.Min(t => t.OriginalId) });

			AddNewTweets(historicalTweets, ListPositionType.Bottom);

			IsLoading = false;
		}

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			tweetTimer
				.Subject
				.Subscribe(next => DownloadNewTweets(),
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

		private void DownloadNewTweets()
		{
			IsLoading = true;

			long? sinceId = null;
			if (Tweets.Count > 0)
				sinceId = Tweets.Max(t => t.OriginalId);

			var downloadedTweets =
				service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = 40, SinceId = sinceId });

			AddNewTweets(downloadedTweets);

			IsLoading = false;
		}

		private void AddNewTweets(IEnumerable<TwitterStatus> downloadedTweets, ListPositionType listPositionType = ListPositionType.Top)
		{
			var twitterRateLimitStatus = service.Response.RateLimitStatus;
			RateRatio = String.Format("{0} / {1}", twitterRateLimitStatus.RemainingHits, twitterRateLimitStatus.HourlyLimit);
			ResetTime = String.Format("{0:yyyy-MM-dd HH:mm}", twitterRateLimitStatus.ResetTime);
			IsDelayed = false;

			if ((int)service.Response.StatusCode == 429) // NOTE [MGJ] : Rate limit exceeded
			{
				service.GetRateLimitStatus(new GetRateLimitStatusOptions());
				tweetTimer.Delay(service.Response.RateLimitStatus.ResetTime);
				twitterRateLimitStatus = service.Response.RateLimitStatus;
				RateRatio = String.Format("{0} / {1}", twitterRateLimitStatus.RemainingHits, twitterRateLimitStatus.HourlyLimit);
				ResetTime = String.Format("{0:yyyy-MM-dd HH:mm}", twitterRateLimitStatus.ResetTime);
				IsDelayed = true;
			}

			if (downloadedTweets == null)
				return;

			var tweets = downloadedTweets.ToArray();

			if (listPositionType == ListPositionType.Top)
			{
				for (var i = tweets.Length - 1; i >= 0; i--)
				{
					Tweets.Insert(0, TweetTransformer.TweetMapper(tweets[i]));
				}
			}
			else
			{
				foreach (var ts in tweets.Where(ts => Tweets.FirstOrDefault(t => t.OriginalId == ts.Id) == null))
				{
					Tweets.Add(TweetTransformer.TweetMapper(ts));
				}
			}
		}
	}
}