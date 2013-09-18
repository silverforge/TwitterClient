using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient.ViewModels
{
	public class ShellViewModel : Conductor<IViewModel>.Collection.OneActive, IShellViewModel
	{
		private string currentContentUri;

		public ShellViewModel(IEventAggregator eventAggregator, StringMessageHandler stringMessageHandler)
		{
			eventAggregator.Subscribe(stringMessageHandler);
			MenuLinkGroups = new LinkGroupCollection();
			TitleLinks = new LinkCollection();
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

		public void Initialize(INavigationManager navigationManager)
		{
			navigationManager.To<IAccountViewModel>();
		}
	}
}