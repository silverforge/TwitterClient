using System.Windows.Media;
using Caliburn.Micro;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.ViewModels;

namespace Silverforge.TwitterClient.Test.ViewModels
{
	[TestFixture]
	public class AdministrationViewModelTest
	{
		private readonly AdministrationViewModel administrationViewModel;

		public AdministrationViewModelTest()
		{
			var customAppearanceManager = Substitute.For<ICustomAppearanceManager>();
			customAppearanceManager.AccentColor.Returns(Colors.Brown);
			customAppearanceManager.TextColor.Returns(Colors.Black);

			var appSettings = Substitute.For<IAppSettings>();
			var navigationManager = Substitute.For<INavigationManager>();

			IoC.BuildUp = o =>
			{
				var viewModel = o as BaseViewModel;
				if (viewModel != null)
				{
					viewModel.NavigationManager = navigationManager;
					viewModel.AppSettings = appSettings;
				}
			};

			administrationViewModel = new AdministrationViewModel(customAppearanceManager, appSettings);
		}

		[Test]
		public void SelectedAccentColor_Get()
		{
			administrationViewModel.SelectedAccentColor.ShouldBeEquivalentTo(Colors.Brown);
		}

		[Test]
		public void SelectedTextColor_Get()
		{
			administrationViewModel.SelectedTextColor.ShouldBeEquivalentTo(Colors.Black);
		}

		[Test]
		public void SelectedAccentColor_Set()
		{
			administrationViewModel.SelectedAccentColor = Colors.Red;
			administrationViewModel.SelectedAccentColor.ShouldBeEquivalentTo(Colors.Red);
		}

		[Test]
		public void SelectedTextColor_Set()
		{
			administrationViewModel.SelectedTextColor = Colors.GreenYellow;
			administrationViewModel.SelectedTextColor.ShouldBeEquivalentTo(Colors.GreenYellow);
		}
	}
}