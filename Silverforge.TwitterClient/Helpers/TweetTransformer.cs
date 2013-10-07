using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.Helpers
{
	public static class TweetTransformer
	{
		public static Tweet TweetMapper(TwitterStatus originalTweet)
		{
			var tweet = new Tweet();
			TwitterStatus twitterStatus;
			if (originalTweet.RetweetedStatus != null)
			{
				tweet.OriginalId = originalTweet.Id;
				tweet.IsRetweeted = true;
				tweet.RetweetedBy = originalTweet.User.Name;
				twitterStatus = originalTweet.RetweetedStatus;
			}
			else
			{
				twitterStatus = originalTweet;
				tweet.OriginalId = twitterStatus.Id;
			}

			tweet.Id = twitterStatus.Id;
			tweet.ImageUrl = twitterStatus.User.ProfileImageUrl;
			tweet.Text = FormatHelper.HtmlToBbCodeText(twitterStatus.TextAsHtml);
			tweet.UserFullName = twitterStatus.User.Name;
			tweet.Created = FormatHelper.UniDate(twitterStatus.CreatedDate);
			tweet.IsNew = true;
			tweet.IsFavorited = twitterStatus.IsFavorited;
			tweet.IsExpanded = true;

			return tweet;
		}
	}
}