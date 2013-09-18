using System.Windows;
using System.Windows.Controls;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.Views
{
	/// <summary>
	/// Interaction logic for AccountView.xaml
	/// </summary>
	public partial class AccountView
	{
		public AccountView()
		{
			InitializeComponent();
		}

		private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			var viewModel = (IAccountViewModel)DataContext;
			viewModel.Password = ((PasswordBox)sender).SecurePassword;
		}
	}
}
