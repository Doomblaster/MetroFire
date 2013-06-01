﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using ReactiveUI;
using Rogue.MetroFire.UI.Behaviors;

namespace Rogue.MetroFire.UI.Views
{
	public partial class ShellView : IShellWindow
	{
		private readonly IMessageBus _bus;
		private readonly ISettings _settings;

		protected ShellView()
		{
			InitializeComponent();

			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var behaviors = Interaction.GetBehaviors(this);
			if (!_settings.General.UseStandardWindowsChrome)
			{
				behaviors.Add(new BorderlessWindowBehavior());
			}

			_bus.SendMessage<ApplicationLoadedMessage>(null);
		}

		public ShellView(IShellViewModel vm, IMessageBus bus,  ISettings settings) : this()
		{
			_bus = bus;
			_settings = settings;
			DataContext = vm;

			

			_bus.Listen<SettingsChangedMessage>().Subscribe(OnSettingsChanged);

		}

		private void OnSettingsChanged(SettingsChangedMessage settingsChangedMessage)
		{
			var behaviors = Interaction.GetBehaviors(this);
			if (_settings.General.UseStandardWindowsChrome && behaviors.OfType<BorderlessWindowBehavior>().Any())
			{
				behaviors.Remove(behaviors.OfType<BorderlessWindowBehavior>().First());
			}
			else if (!_settings.General.UseStandardWindowsChrome && !behaviors.OfType<BorderlessWindowBehavior>().Any())
			{
				behaviors.Add(new BorderlessWindowBehavior());
			}
		}

		
		public Window Window
		{
			get { return this; }
		}

		private void TopOnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				DragMove();
			}
		}


		private void CloseOnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Close();
		}
	}
}
