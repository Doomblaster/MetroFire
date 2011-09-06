﻿using System;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace Rogue.MetroFire.UI.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : IShellWindow
	{
		private readonly IMessageBus _bus;
		private readonly IModuleResolver _resolver;

		protected ShellView()
		{
			InitializeComponent();

			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_bus.SendMessage<ApplicationLoadedMessage>(null);
		}

		public ShellView(IShellViewModel vm, IMessageBus bus, IModuleResolver resolver) : this()
		{
			_bus = bus;
			_resolver = resolver;
			DataContext = vm;

			_bus.Listen<ActivateMainModuleMessage>().Subscribe(msg => ActivateMainModule(msg.ModuleName));
		}

		private void ActivateMainModule(string moduleName)
		{
			_mainContent.Content = _resolver.ResolveModule(moduleName);
		}

		public Window Window
		{
			get { return this; }
		}

		private void TopOnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

	}
}
