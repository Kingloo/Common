using System;
using System.ComponentModel;

namespace .Common
{
	public abstract class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		[System.Diagnostics.DebuggerStepThrough]
		protected bool SetProperty<T>(ref T storage, T value, string propertyName)
		{
			if (String.IsNullOrWhiteSpace(propertyName))
			{
				throw new ArgumentNullException(nameof(propertyName));
			}

			if (Equals(storage, value))
			{
				return false;
			}

			storage = value;

			RaisePropertyChanged(propertyName);

			return true;
		}

		[System.Diagnostics.DebuggerStepThrough]
#pragma warning disable CA1030
		protected virtual void RaisePropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#pragma warning restore CA1030
	}
}
