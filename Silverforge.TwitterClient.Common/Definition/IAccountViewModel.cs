using System.Security;

namespace Silverforge.TwitterClient.Common.Definition
{
	public interface IAccountViewModel : IViewModel
	{
		string EmailAddress { get; set; }
		SecureString Password { get; set; }
	}
}