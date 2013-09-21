using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Silverforge.TwitterClient.Helpers.View
{
	public class CustomAppearanceManager : PropertyChangedBase
	{
		private Color accentColor;
		private Color textColor;

		public CustomAppearanceManager()
		{
			accentColor = ((Color) Application.Current.Resources["AccentColor"]);
			textColor = Colors.Black;
		}

		public Color AccentColor
		{
			get { return accentColor; }
			set
			{
				accentColor = value;
				NotifyOfPropertyChange(() => AccentColor);
			}
		}

		public Color TextColor
		{
			get { return textColor; }
			set
			{
				textColor = value;
				NotifyOfPropertyChange(() => TextColor);
			}
		}

		public void SetAccentColor(Color color)
		{
			AccentColor = color;
			AppearanceManager.Current.AccentColor = color;
			((Rectangle)((ModernWindow)Application.Current.MainWindow).BackgroundContent).Fill = new SolidColorBrush(color) { Opacity = 0.2 };
		}
	}
}