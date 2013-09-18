using System.Collections.Generic;
using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient.ViewModels
{
	public class ShellViewModel : Conductor<IViewModel>.Collection.OneActive, IShellViewModel
	{
		private string currentContentUri;

		public ShellViewModel(IEnumerable<IViewModel> viewModels, IEventAggregator eventAggregator, StringMessageHandler stringMessageHandler)
		{
			eventAggregator.Subscribe(stringMessageHandler);
			MenuLinkGroups = new LinkGroupCollection();
			TitleLinks = new LinkCollection();

			foreach (var viewModel in viewModels)
			{
				Items.Add(viewModel);
			}
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

		//private void LoadMenuLinks()
		//{
		//	var applicationUri = navigationManager.Uri<IApplicationViewModel>();
		//	var administrationUri = navigationManager.Uri<IAdministrationViewModel>();

		//	var linkGroup = new LinkGroup { GroupName = "MainGroup" };
		//	linkGroup.Links.Add(new Link
		//	{
		//		DisplayName = Resources.YourProjects,
		//		Source = new Uri(applicationUri, UriKind.RelativeOrAbsolute)
		//	});
		//	linkGroup.Links.Add(new Link
		//	{
		//		DisplayName = Resources.Administration,
		//		Source = new Uri(administrationUri, UriKind.RelativeOrAbsolute)
		//	});

		//	MenuLinkGroups.Clear();
		//	MenuLinkGroups.Add(linkGroup);

		//	var accountInfo = navigationManager.Context.Get<IAccountInfo>(ContextKeys.AccountInfo);

		//	TitleLinks.Clear();
		//	TitleLinks.Add(new Link
		//	{
		//		DisplayName = Resources.Administration,
		//		Source = new Uri(administrationUri, UriKind.RelativeOrAbsolute)
		//	});
		//	TitleLinks.Add(new Link
		//	{
		//		DisplayName = accountInfo.CurrentUser.UserFullName
		//	});
		//}

	}
}