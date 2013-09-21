using System.Windows;
using System.Windows.Forms;
using FirstFloor.ModernUI.Windows.Controls;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.Helpers.View;
using Application = System.Windows.Application;

namespace Silverforge.TwitterClient.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView
	{
		public ShellView()
		{
			InitializeComponent();

			//AppearanceManager.Current.AccentColor = Colors.SaddleBrown;

			WindowStartupLocation = WindowStartupLocation.Manual;
			var workingArea = Screen.PrimaryScreen.WorkingArea;
			Top = (workingArea.Height - Height);
			Left = (workingArea.Width - Width - 20);

			Loaded += (sender, args) =>
				{
					var buttons = Application.Current.MainWindow.GetChildsOfType<ModernButton>();
					foreach (var modernButton in buttons)
					{
						modernButton.Visibility = Visibility.Collapsed;
					}
				};
		}
	}
}
