using System.Collections.Concurrent;

namespace Silverforge.TwitterClient.Common
{
	public class ApplicationContext<T> : ConcurrentDictionary<string, T>
	{
		public void Set(string key, T value)
		{
			AddOrUpdate(key, value, (k, oldValue) => value);
		}

		public TT Get<TT>(string key)
		{
			return (TT) ((object)this[key]);
		}
	}
}