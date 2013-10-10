using System;
using FluentAssertions;
using NUnit.Framework;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.Model;
using TweetSharp;

namespace Silverforge.TwitterClient.Test.Helpers
{
	[TestFixture]
    public class TweetTransformerTest
    {
		[Test]
		public void TweetMapper_Valid()
		{
			var date = DateTime.Now;
			var twitterStatus = new TwitterStatus
				{
					Id = 23,
					User = new TwitterUser
						{
							ProfileImageUrl = "http://mycustom.image.com/myface.jpg",
							Name = "My Name"
						},
					TextAsHtml = "Watching some fish jump @ Issaquah Salmon Days <a href=\"http://instagram.com/p/fJGGVDCndT/\" target=\"_blank\">http://t.co/rPhK2JPvBP</a>",
					CreatedDate = date,
					IsFavorited = true
				};

			var expected = new Tweet
				{
					Id = twitterStatus.Id,
					Created = FormatHelper.UniDate(twitterStatus.CreatedDate),
					ImageUrl = twitterStatus.User.ProfileImageUrl,
					IsExpanded = true,
					IsFavorited = twitterStatus.IsFavorited,
					IsNew = true,
					IsRetweeted = false,
					OriginalId = twitterStatus.Id,
					RetweetedBy = null,
					Text = FormatHelper.HtmlToBbCodeText(twitterStatus.TextAsHtml),
					UserFullName = twitterStatus.User.Name
				};

			var tweetMapper = TweetTransformer.TweetMapper(twitterStatus);

			tweetMapper.ShouldBeEquivalentTo(expected);
		}

		[Test]
		public void TweetMapper_ReTweet_Valid()
		{
			var date = DateTime.Now;
			var twitterStatus = new TwitterStatus
				{
					Id = 23,
					User = new TwitterUser
						{
							ProfileImageUrl = "http://mycustom.image.com/myface.jpg",
							Name = "My Name"
						},
					RetweetedStatus = new TwitterStatus
						{
							Id = 45,
							User = new TwitterUser
							{
								ProfileImageUrl = "http://mycustom.image.com/myfancyface.jpg",
								Name = "My Fancy Name"
							},
							TextAsHtml = "Awesome! RT <a href=\"https://twitter.com/jhalbrecht\" target=\"_blank\">@jhalbrecht</a> Ah-Ha moments coming Fast and Furious <a href=\"https://twitter.com/ShawnWildermuth\" target=\"_blank\">@ShawnWildermuth</a>'s \"Building a Site with Bootstrap, AngularJS, ASP[.]NET...\"",
							CreatedDate = date.AddDays(-2),
							IsFavorited = false
						},
					TextAsHtml = "Watching some fish jump @ Issaquah Salmon Days <a href=\"http://instagram.com/p/fJGGVDCndT/\" target=\"_blank\">http://t.co/rPhK2JPvBP</a>",
					CreatedDate = date,
					IsFavorited = true
				};

			var expected = new Tweet
				{
					Id = twitterStatus.RetweetedStatus.Id,
					Created = FormatHelper.UniDate(twitterStatus.RetweetedStatus.CreatedDate),
					ImageUrl = twitterStatus.RetweetedStatus.User.ProfileImageUrl,
					IsExpanded = true,
					IsFavorited = twitterStatus.RetweetedStatus.IsFavorited,
					IsNew = true,
					IsRetweeted = true,
					OriginalId = twitterStatus.Id,
					RetweetedBy = twitterStatus.User.Name,
					Text = FormatHelper.HtmlToBbCodeText(twitterStatus.RetweetedStatus.TextAsHtml),
					UserFullName = twitterStatus.RetweetedStatus.User.Name
				};

			var tweetMapper = TweetTransformer.TweetMapper(twitterStatus);

			tweetMapper.ShouldBeEquivalentTo(expected);
		}


		[Test]
		public void TweetMapper_Null_IsValid()
		{
			
			var tweetMapper = TweetTransformer.TweetMapper(null);
			tweetMapper.ShouldBeEquivalentTo(new Tweet());
		}
    }
}
