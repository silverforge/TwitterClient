using System.Security;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient.ViewModels
{
	public class AccountViewModel : BaseViewModel, IAccountViewModel
	{
		private SecureString password;
		private string emailAddress;
		private bool isEpamEmployee;
		private bool isRememberMe;

		public string EmailAddress
		{
			get { return emailAddress; }
			set
			{
				emailAddress = value;
				NotifyOfPropertyChange();
				NotifyOfPropertyChange(() => CanLogin);
			}
		}

		public SecureString Password
		{
			get { return password; }
			set
			{
				password = value;
				NotifyOfPropertyChange();
				NotifyOfPropertyChange(() => CanLogin);
			}
		}

		public bool IsEpamEmployee
		{
			get { return isEpamEmployee; }
			set
			{
				isEpamEmployee = value;
				NotifyOfPropertyChange();
			}
		}

		public bool IsRememberMe
		{
			get { return isRememberMe; }
			set
			{
				isRememberMe = value;
				NotifyOfPropertyChange();
			}
		}

		public bool CanLogin
		{
			get { return !string.IsNullOrEmpty(EmailAddress) && (Password != null && Password.Length > 0); }
		}

		public void Login()
		{
			// TODO [MGJ] : Do not forget to remove it after you got it from service
			//var accountInfo = new AccountInfo
			//{
			//	CurrentUser = new User
			//	{
			//		Email = "Teszt_Elek@real.com",
			//		FirstName = "Teszt",
			//		LastName = "Elek",
			//		Id = 6,
			//		NickName = "Teszt",
			//		TitleName = "Tester",
			//		UserFullName = "TESZT, Elek",
			//		UserName = "teszt_elek",
			//		UserType = "E"
			//	},
			//	IsAuthenticated = true
			//};

			//navigationManager.Context.Set(ContextKeys.AccountInfo, accountInfo);
			NavigationManager.SendMessage(MessageKeys.Authenticated);
		}

		public void PasswordChanged(object current)
		{
			
		}
	}
}