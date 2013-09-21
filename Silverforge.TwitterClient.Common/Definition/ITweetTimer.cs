using System;
using System.Reactive.Subjects;

namespace Silverforge.TwitterClient.Common.Definition
{
	public interface ITweetTimer
	{
		void Start();
		void Stop();
		void Delay(DateTime date);
		ISubject<long> Subject { get; }
		TweetTimerStatus Status { get; }
	}
}