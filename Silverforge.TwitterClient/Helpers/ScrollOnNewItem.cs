// http://stackoverflow.com/questions/12255055/how-to-get-itemscontrol-scrollbar-position-programmatically

using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Silverforge.TwitterClient.Helpers
{
	public class ScrollOnNewItem : Behavior<ItemsControl>
	{
		private ScrollViewer scrollViewer;

		protected override void OnAttached()
		{
			AssociatedObject.Loaded += OnLoaded;
			AssociatedObject.Unloaded += OnUnLoaded;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Loaded -= OnLoaded;
			AssociatedObject.Unloaded -= OnUnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			var incc = AssociatedObject.ItemsSource as INotifyCollectionChanged;
			if (incc == null) return;

			incc.CollectionChanged += OnCollectionChanged;

			scrollViewer = AssociatedObject.GetSelfAndAncestors().FirstOrDefault(a => a.GetType() == typeof(ScrollViewer)) as ScrollViewer;
			if (scrollViewer == null)
				scrollViewer = AssociatedObject.GetChildOfType<ScrollViewer>();

			scrollViewer.ScrollToBottom();
		}

		private void OnUnLoaded(object sender, RoutedEventArgs e)
		{
			var incc = AssociatedObject.ItemsSource as INotifyCollectionChanged;
			if (incc == null) return;

			incc.CollectionChanged -= OnCollectionChanged;
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (scrollViewer != null && e.Action == NotifyCollectionChangedAction.Add)
				scrollViewer.ScrollToBottom();
		}
	}
}