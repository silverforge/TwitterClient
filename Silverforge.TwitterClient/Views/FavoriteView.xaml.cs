using System;
using System.Reactive.Linq;
using System.Windows.Controls;

namespace Silverforge.TwitterClient.Views
{
	/// <summary>
	/// Interaction logic for FavoriteView.xaml
	/// </summary>
	public partial class FavoriteView
	{
		public FavoriteView()
		{
			InitializeComponent();

			Observable
				.FromEventPattern<ScrollChangedEventHandler, ScrollChangedEventArgs>(h => MainScroll.ScrollChanged += h,
																					 h => MainScroll.ScrollChanged -= h)
				.Subscribe(next =>
				{
					if (MainScroll.ScrollableHeight > 3 && Math.Abs(MainScroll.VerticalOffset - MainScroll.ScrollableHeight) < 2)
					{
						Caliburn.Micro.Action.Invoke(DataContext, "LoadMoreTweets");
					}
				});
		}
	}
}
