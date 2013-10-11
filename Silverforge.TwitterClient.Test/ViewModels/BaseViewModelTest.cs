using Caliburn.Micro;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.ViewModels;

namespace Silverforge.TwitterClient.Test.ViewModels
{
	[TestFixture]
	public class BaseViewModelTest
	{
		private readonly BaseViewModel baseViewModel;

		public BaseViewModelTest()
		{
			var navigationManager = Substitute.For<INavigationManager>();
			var appSettings = Substitute.For<IAppSettings>();
			IoC.BuildUp = o =>
				{
					var viewModel = o as BaseViewModel;
					if (viewModel != null)
					{
						viewModel.NavigationManager = navigationManager;
						viewModel.AppSettings = appSettings;
					}
				};

			baseViewModel = new BaseViewModel();
		}

		[Test]
		public void GetNavigationManager_Test()
		{
			baseViewModel.NavigationManager.Should().NotBeNull();
		}

		[Test]
		public void GetAppSettings_Test()
		{
			baseViewModel.AppSettings.Should().NotBeNull();
		}
	}
}