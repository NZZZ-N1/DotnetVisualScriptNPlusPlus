using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
	public static MainViewModel Instance { get; private set; } = null!;
	
	public MainViewModel()
	{
		Instance = this;
	}
	
	#region PropertyChanged
	public new event PropertyChangedEventHandler? PropertyChanged;
	public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}