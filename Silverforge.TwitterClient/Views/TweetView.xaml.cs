using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Caliburn.Micro;

namespace Silverforge.TwitterClient.Views
{
	/// <summary>
	/// Interaction logic for TweetView.xaml
	/// </summary>
	public partial class TweetView
	{
		public TweetView()
		{
			InitializeComponent();

			var mouseDown = Observable
				.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(
					h => Tweets.MouseDown += h, 
					h => Tweets.MouseDown -= h);

			mouseDown
				.Throttle(TimeSpan.FromSeconds(2))
				.Subscribe(n => Execute.OnUIThread(() => Caliburn.Micro.Action.Invoke(DataContext, "SetAllRead")));
		}
	}
}
