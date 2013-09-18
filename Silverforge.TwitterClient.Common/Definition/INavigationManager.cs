using System;

namespace Silverforge.TwitterClient.Common.Definition
{
	public interface INavigationManager
	{
		ApplicationContext<dynamic> Context { get; }
		void To<T>() where T : IViewModel;
		void To(Type t);
		string Uri<T>() where T : IViewModel;
		void SendMessage<T>(T message);
	}
}