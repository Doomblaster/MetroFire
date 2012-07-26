﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.SampleNotificationSettingsViewModel
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class SampleNotificationSettingsViewModel { }
#else

	public class SampleNotificationSettingsViewModel : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public SampleNotificationSettingsViewModel()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/MetroFire;component/SampleData/SampleNotificationSettingsViewModel/SampleNotificationSettingsViewModel.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private Notifications _Notifications = new Notifications();

		public Notifications Notifications
		{
			get
			{
				return this._Notifications;
			}
		}
	}

	public class Notifications : System.Collections.ObjectModel.ObservableCollection<NotificationsItem>
	{ 
	}

	public class NotificationsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private Triggers _Triggers = new Triggers();

		public Triggers Triggers
		{
			get
			{
				return this._Triggers;
			}
		}

		private Actions _Actions = new Actions();

		public Actions Actions
		{
			get
			{
				return this._Actions;
			}
		}
	}

	public class Triggers : System.Collections.ObjectModel.ObservableCollection<TriggersItem>
	{ 
	}

	public class TriggersItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private TriggerTypes _TriggerTypes = new TriggerTypes();

		public TriggerTypes TriggerTypes
		{
			get
			{
				return this._TriggerTypes;
			}
		}

		private string _DisplayText = string.Empty;

		public string DisplayText
		{
			get
			{
				return this._DisplayText;
			}

			set
			{
				if (this._DisplayText != value)
				{
					this._DisplayText = value;
					this.OnPropertyChanged("DisplayText");
				}
			}
		}
	}

	public class TriggerTypes : System.Collections.ObjectModel.ObservableCollection<TriggerTypesItem>
	{ 
	}

	public class TriggerTypesItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Text = string.Empty;

		public string Text
		{
			get
			{
				return this._Text;
			}

			set
			{
				if (this._Text != value)
				{
					this._Text = value;
					this.OnPropertyChanged("Text");
				}
			}
		}
	}

	public class Actions : System.Collections.ObjectModel.ObservableCollection<ActionsItem>
	{ 
	}

	public class ActionsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _DisplayText = string.Empty;

		public string DisplayText
		{
			get
			{
				return this._DisplayText;
			}

			set
			{
				if (this._DisplayText != value)
				{
					this._DisplayText = value;
					this.OnPropertyChanged("DisplayText");
				}
			}
		}
	}
#endif
}
