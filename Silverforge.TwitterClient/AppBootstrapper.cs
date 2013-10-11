using System.Windows;
using System.Windows.Media;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.Helpers.View;
using Silverforge.TwitterClient.ViewModels;
using SimpleInjector;
using TweetSharp;

namespace Silverforge.TwitterClient
{
	using System;
	using System.Collections.Generic;
	using Caliburn.Micro;

	public class AppBootstrapper : Bootstrapper<IShellViewModel>
	{
		Container container;

		protected override void Configure()
		{
			container = new Container();

			IoC.GetInstance = GetInstance;
			IoC.GetAllInstances = GetAllInstances;
			IoC.BuildUp = BuildUp;

			container.RegisterSingle<IAppSettings, AppSettings>();

			var caliburnContentLoader = Application.Current.Resources["CaliburnContentLoader"] as CaliburnContentLoader;
			container.RegisterSingle(caliburnContentLoader);

			var customAppearanceManager = Application.Current.Resources["CustomAppearanceManager"] as CustomAppearanceManager;
			container.RegisterSingle<ICustomAppearanceManager>(customAppearanceManager);
			container.RegisterSingle(customAppearanceManager);

			container.RegisterSingle<IWindowManager, WindowManager>();
			container.RegisterSingle<IEventAggregator, EventAggregator>();
			container.RegisterSingle<INavigationManager, NavigationManager>();

			container.RegisterSingle<IShellViewModel, ShellViewModel>();

			container.Register<TwitterService>(() =>
				{
					var appSettings = container.GetInstance<IAppSettings>();
					var service = new TwitterService(appSettings.ConsumerKey,
					                                 appSettings.ConsumerSecret,
					                                 appSettings.AccessToken,
					                                 appSettings.AccessTokenSecret)
						{
							IncludeRetweets = true
						};

					return service;
				});

			container.RegisterSingle<StringMessageHandler>();

			container.Register<ITweetViewModel, TweetViewModel>();
			container.Register<IAdministrationViewModel, AdministrationViewModel>();
			container.Register<ITweetTimer, TweetTimer>();

			//container.Verify();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.InjectProperties(instance);
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			base.OnStartup(sender, e);
			IoC.Get<IShellViewModel>().Initialize();
			var accentColorHex = IoC.Get<IAppSettings>().AccentColorHex;
			var textColorHex = IoC.Get<IAppSettings>().TextColorHex;

			if (!string.IsNullOrEmpty(accentColorHex))
				IoC.Get<CustomAppearanceManager>().SetAccentColor((Color)ColorConverter.ConvertFromString(accentColorHex));
			if (!string.IsNullOrEmpty(textColorHex))
				IoC.Get<CustomAppearanceManager>().TextColor = (Color) ColorConverter.ConvertFromString(textColorHex);
		}
	}
}