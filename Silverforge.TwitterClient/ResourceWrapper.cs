using System.ComponentModel;
using Silverforge.TwitterClient.Properties;

namespace Silverforge.TwitterClient
{
	public class ResourceWrapper
	{
		private static readonly Resources Strings = new Resources();

		public Resources LocalizedStrings
		{
			get { return Strings; }
			set { OnPropertyChanged("LocalizedStrings"); }
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}