
using System.Windows.Media;

namespace Silverforge.TwitterClient.Common.Definition
{
	public interface ICustomAppearanceManager
	{
		Color AccentColor { get; set; }
		Color TextColor { get; set; }
		void SetAccentColor(Color color);
	}
}