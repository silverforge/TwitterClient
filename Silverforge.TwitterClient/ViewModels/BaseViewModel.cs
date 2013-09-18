using Caliburn.Micro;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.ViewModels
{
	public class BaseViewModel : Screen, IViewModel
	{
		public BaseViewModel()
		{
			IoC.BuildUp(this);
		}

		public INavigationManager NavigationManager { get; set; }
		public IAppSettings AppSettings { get; set; }
	}
}