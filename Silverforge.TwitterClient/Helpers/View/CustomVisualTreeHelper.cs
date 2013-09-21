using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Silverforge.TwitterClient.Helpers.View
{
	public static class CustomVisualTreeHelper
	{
		public static T GetChildOfType<T>(this DependencyObject depObj)
			where T : DependencyObject
		{
			if (depObj == null) return null;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);

				var result = (child as T) ?? GetChildOfType<T>(child);
				if (result != null) return result;
			}
			return null;
		}

		public static IList<T> GetChildsOfType<T>(this DependencyObject depObj)
			where T : DependencyObject
		{
			var retValues = new List<T>();

			if (depObj == null)
				return retValues;

			CollectChildren(retValues, depObj);

			return retValues;
		}

		private static void CollectChildren<T>(ICollection<T> retValues, DependencyObject depObj) 
			where T : DependencyObject
		{
			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);

				var result = (child as T);
				if (result != null)
					retValues.Add(result);
				else
					CollectChildren(retValues, child);
			}
		}
	}
}