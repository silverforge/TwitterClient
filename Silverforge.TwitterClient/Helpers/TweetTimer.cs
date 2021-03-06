﻿using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Helpers
{
	public class TweetTimer : ITweetTimer
	{
		private readonly IAppSettings appSettings;
		private readonly ISubject<long> timer = new Subject<long>();
		private IDisposable internalTimer;

		public TweetTimer(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
			Status = TweetTimerStatus.Stopped;
		}

		public ISubject<long> Subject
		{
			get { return timer; }
		}

		public TweetTimerStatus Status { get; private set; }

		public void Start()
		{
			if (Status == TweetTimerStatus.Started) 
				return;

			internalTimer = Observable
				.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(appSettings.PollInterval))
				.Subscribe(
					l => timer.OnNext(l),
					err => timer.OnError(err),
					() => timer.OnCompleted());
			Status = TweetTimerStatus.Started;
			Debug.WriteLine("TweetService started.");
		}

		public void Stop()
		{
			if (Status != TweetTimerStatus.Started) 
				return;

			internalTimer.Dispose();
			Status = TweetTimerStatus.Stopped;
			Debug.WriteLine("TweetService stopped.");
		}

		public void Delay(DateTime date)
		{
			if (Status != TweetTimerStatus.Started) 
				return;

			internalTimer.Dispose();
			Status = TweetTimerStatus.Onhold;

			var currentDate = DateTime.Now;
			var due = date.Subtract(currentDate);
			Observable
				.Timer(due)
				.Subscribe(n => Start());

			var format = String.Format("TweetService is delayed to {0} m {1} sec", due.Minutes, due.Seconds);
			Debug.WriteLine(format);
		}
	}
}