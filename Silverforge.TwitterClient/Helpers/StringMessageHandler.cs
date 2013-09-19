using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Helpers
{
	public class StringMessageHandler : IHandle<string>
	{
		private readonly INavigationManager navigationManager;
		private readonly Dictionary<string, Type> navigationRoutes = new Dictionary<string, Type>
			{
			};

		public StringMessageHandler(INavigationManager navigationManager)
		{
			this.navigationManager = navigationManager;
		}

		public void Handle(string message)
		{
			if (navigationRoutes.ContainsKey(message))
			{
				var navigationRoute = navigationRoutes[message];
				navigationManager.To(navigationRoute);
			}
		}
	}
}