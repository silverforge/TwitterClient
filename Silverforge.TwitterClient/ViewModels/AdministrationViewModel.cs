using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;

namespace Silverforge.TwitterClient.ViewModels
{
	public class AdministrationViewModel : BaseViewModel, IAdministrationViewModel
	{
		private readonly CustomAppearanceManager appearanceManager;
		private Color selectedTextColor;
		private Color selectedAccentColor;

		public AdministrationViewModel(CustomAppearanceManager appearanceManager)
		{
			this.appearanceManager = appearanceManager;
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
			}
		}
	}
}