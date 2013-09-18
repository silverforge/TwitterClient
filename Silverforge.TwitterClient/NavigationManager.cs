using System;
using System.Windows;
using Caliburn.Micro;
using FirstFloor.ModernUI.Windows.Controls;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient
{
	public class NavigationManager : INavigationManager
	{
		private readonly CaliburnContentLoader contentLoader;
		private readonly IEventAggregator eventAggregator;

		public NavigationManager(CaliburnContentLoader contentLoader, IEventAggregator eventAggregator)
		{
			this.contentLoader = contentLoader;
			this.eventAggregator = eventAggregator;

			Context = new ApplicationContext<dynamic>();
		}

		public ApplicationContext<dynamic> Context { get; private set; }

		public void To<T>() where T : IViewModel
		{
			var locateViewUri = contentLoader.LocateViewUri<T>();
			NavigateToPage(locateViewUri);
		}

		public void To(Type t)
		{
			var locateViewUri = contentLoader.LocateViewUriFromType(t);
			NavigateToPage(locateViewUri);
		}

		public string Uri<T>() where T : IViewModel
		{
			return contentLoader.LocateViewUri<T>();
		}

		public void SendMessage<T>(T message)
		{
			eventAggregator.Publish(message);
		}

		private static void NavigateToPage(string locateViewUri)
		{
			var frame = Application.Current.MainWindow.GetChildOfType<ModernFrame>();
			if (frame == null)
				return;

			frame.KeepContentAlive = false;
			frame.Source = new Uri(locateViewUri, UriKind.RelativeOrAbsolute);
		}
	}
}