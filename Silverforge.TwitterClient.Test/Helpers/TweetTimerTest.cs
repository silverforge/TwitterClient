using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient.Test.Helpers
{
	[TestFixture]
	public class TweetTimerTest
	{
		private readonly IAppSettings appSettings;
		private readonly TweetTimer tweetTimer;

		public TweetTimerTest()
		{
			appSettings = Substitute.For<IAppSettings>();
			appSettings
				.PollInterval
				.Returns(10);

			tweetTimer = new TweetTimer(appSettings);
		}

		[Test]
		public void Start_Valid()
		{
			tweetTimer.Stop();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Stopped);

			tweetTimer.Start();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Started);

			tweetTimer.Stop();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Stopped);
		}

		[Test]
		public void DoubleStart_Valid()
		{
			tweetTimer.Stop();

			tweetTimer.Start();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Started);

			tweetTimer.Start();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Started);

			tweetTimer.Stop();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Stopped);
		}

		[Test]
		public void Delay_Valid()
		{
			tweetTimer.Start();
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Started);

			var delayDate = DateTime.Now.AddSeconds(5);

			tweetTimer.Delay(delayDate);
			tweetTimer.Status.ShouldBeEquivalentTo(TweetTimerStatus.Onhold);
		}

	}
}
