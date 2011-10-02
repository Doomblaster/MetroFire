﻿using System.Linq;
using Castle.Facilities.Startable;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ReactiveUI;
using System;
using Rogue.MetroFire.CampfireClient;
using Rogue.MetroFire.UI.ViewModels;
using Rogue.MetroFire.UI.Views;
using Castle.Facilities.TypedFactory;

namespace Rogue.MetroFire.UI
{
	public class Bootstrapper
	{
		private readonly WindsorContainer _container;

		public Bootstrapper()
		{
			_container = new WindsorContainer();
		}

		public IShellWindow Bootstrap()
		{
			_container.Kernel.ComponentModelBuilder.RemoveContributor(
				_container.Kernel.ComponentModelBuilder.Contributors.OfType<PropertiesDependenciesModelInspector>().Single());

			_container.AddFacility<StartableFacility>();
			_container.AddFacility<TypedFactoryFacility>();

			_container.Install(FromAssembly.This());
			_container.Install(FromAssembly.Containing<RequestLoginMessage>());


			_container.Register(Component.For<IInlineUploadView>().LifestyleTransient().ImplementedBy<InlineUploadView>());
			_container.Register(
				AllTypes.FromThisAssembly().Where(t => t.Namespace == typeof (RoomModuleViewModel).Namespace).
					WithServiceAllInterfaces().LifestyleTransient());

			_container.Register(Component.For<IChatDocument>().ImplementedBy<ChatDocument>().LifestyleTransient());
			_container.Register(Component.For<IMessageBus>().ImplementedBy<MessageBus>());
			_container.Register(Component.For<IRoomModuleViewModelFactory>().AsFactory());
			_container.Register(Component.For<IInlineUploadViewFactory>().AsFactory());
			_container.Register(Component.For<IImageView>().LifestyleTransient().ImplementedBy<ImageView>());

			_container.Register(AllTypes.FromThisAssembly().Pick().WithServiceAllInterfaces());

			_container.Register(Component.For<IWindsorContainer>().Instance(_container));


			var messageBus = _container.Resolve<IMessageBus>();


			messageBus.Listen<ApplicationLoadedMessage>().Subscribe(
				msg => messageBus.SendMessage(new ActivateMainModuleMessage(ModuleNames.Login)));

			return _container.Resolve<IShellWindow>();
		}
	}
}
