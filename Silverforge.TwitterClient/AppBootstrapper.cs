using System.Windows;
using Silverforge.TwitterClient.Common;
using Silverforge.TwitterClient.Common.Definition;
using Silverforge.TwitterClient.Helpers;
using Silverforge.TwitterClient.ViewModels;
using SimpleInjector;

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

			var caliburnContentLoader = Application.Current.Resources["CaliburnContentLoader"] as CaliburnContentLoader;
			container.RegisterSingle(caliburnContentLoader);

			var customAppearanceManager = Application.Current.Resources["CustomAppearanceManager"] as CustomAppearanceManager;
			container.RegisterSingle(customAppearanceManager);

			container.RegisterSingle<IWindowManager, WindowManager>();
			container.RegisterSingle<IEventAggregator, EventAggregator>();
			container.RegisterSingle<INavigationManager, NavigationManager>();

			container.RegisterSingle<StringMessageHandler>();

			container.RegisterSingle<IAppSettings, AppSettings>();
			container.RegisterSingle<IShellViewModel, ShellViewModel>();
			container.Register<ITweetViewModel, TweetViewModel>();
			container.Register<IAdministrationViewModel, AdministrationViewModel>();

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
		}
	}
}