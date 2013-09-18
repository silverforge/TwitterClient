using System;
using System.Windows;
using Caliburn.Micro;
using FirstFloor.ModernUI.Windows;
using Silverforge.TwitterClient.Common.Definition;

namespace Silverforge.TwitterClient
{
	public class CaliburnContentLoader : DefaultContentLoader
	{
		protected override object LoadContent(Uri uri)
		{
			var content = base.LoadContent(uri);
			if (content == null)
				return null;

			var vm = ViewModelLocator.LocateForView(content);
			if (vm == null)
				return content;

			if (content is DependencyObject)
				ViewModelBinder.Bind(vm, content as DependencyObject, null);

			return content;
		}

		public string LocateViewUri(IViewModel viewModel)
		{
			var view = ViewLocator.LocateForModel(viewModel, null, null);
			var uriFromType = ViewLocator.DeterminePackUriFromType(viewModel.GetType(), view.GetType());
			return uriFromType;
		}

		public string LocateViewUriFromType(Type viewType)
		{
			return GetUriByType(viewType);
		}

		public string LocateViewUri<T>() where T : IViewModel
		{
			var viewType = typeof (T);
			return GetUriByType(viewType);
		}

		private static string GetUriByType(Type viewType)
		{
			string uri;
			if (viewType.IsInterface)
				uri = String.Format(@"\Views\{0}.xaml", viewType.Name.Substring(1).Replace("ViewModel", "View"));
			else
				uri = String.Format(@"\Views\{0}.xaml", viewType.Name.Replace("ViewModel", "View"));

			return uri;
		}
	}
}
