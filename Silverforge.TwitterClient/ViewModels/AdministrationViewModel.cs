using System.Windows.Media;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.ViewModels
{
	public class AdministrationViewModel : BaseViewModel, IAdministrationViewModel
	{
		private readonly ICustomAppearanceManager appearanceManager;
		private readonly IAppSettings appSettings;
		private Color selectedTextColor;
		private Color selectedAccentColor;

		public AdministrationViewModel(ICustomAppearanceManager appearanceManager, IAppSettings appSettings)
		{
			this.appearanceManager = appearanceManager;
			this.appSettings = appSettings;
			selectedAccentColor = appearanceManager.AccentColor;
			selectedTextColor = appearanceManager.TextColor;
		}

		public Color SelectedTextColor
		{
			get { return selectedTextColor; }
			set
			{
				selectedTextColor = value;
				NotifyOfPropertyChange(() => SelectedTextColor);
				appearanceManager.TextColor = selectedTextColor;
				appSettings.SetTextColor(selectedTextColor.ToString());
			}
		}

		public Color SelectedAccentColor
		{
			get { return selectedAccentColor; }
			set
			{
				selectedAccentColor = value;
				NotifyOfPropertyChange(() => SelectedAccentColor);
				appearanceManager.SetAccentColor(selectedAccentColor);
				appSettings.SetAccentColor(selectedAccentColor.ToString());
			}
		}
	}
}