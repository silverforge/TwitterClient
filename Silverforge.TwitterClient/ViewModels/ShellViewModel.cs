using System;
using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient.ViewModels
{
	public class ShellViewModel : Conductor<IViewModel>.Collection.OneActive, IShellViewModel
	{
		private readonly INavigationManager navigationManager;
		private string currentContentUri;

		public ShellViewModel(IEventAggregator eventAggregator, StringMessageHandler stringMessageHandler, INavigationManager navigationManager)
		{
			this.navigationManager = navigationManager;
			eventAggregator.Subscribe(stringMessageHandler);
			MenuLinkGroups = new LinkGroupCollection();

			var tweetUri = navigationManager.Uri<ITweetViewModel>();
			var favoriteUri = navigationManager.Uri<IFavoriteViewModel>();
			var administrationUri = navigationManager.Uri<IAdministrationViewModel>();

			TitleLinks = new LinkCollection
				{
					new Link {DisplayName = "tweets", Source = new Uri(tweetUri, UriKind.RelativeOrAbsolute)},
					new Link {DisplayName = "favorites", Source = new Uri(favoriteUri, UriKind.RelativeOrAbsolute)},
					new Link {DisplayName = "administration", Source = new Uri(administrationUri, UriKind.RelativeOrAbsolute)}
				};
		}

		public LinkGroupCollection MenuLinkGroups { get; private set; }
		public LinkCollection TitleLinks { get; private set; }

		public string CurrentContentUri
		{
			get { return currentContentUri; }
			set
			{
				currentContentUri = value;
				NotifyOfPropertyChange(() => CurrentContentUri);
			}
		}

		public void Initialize()
		{
			navigationManager.To<ITweetViewModel>();
		}
	}
}