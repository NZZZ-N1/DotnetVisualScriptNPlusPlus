using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.NppControlRender;
using DVSNPP_Demo.RunningNodes;
using DVSNPP_Demo.ViewModels;

namespace DVSNPP_Demo.Views;

public partial class MainView : UserControl, INotifyPropertyChanged
{
	public static MainView Instance { get; private set; } = null!;
	
	public MainView()
	{
		InitializeComponent();
		Instance = this;
		
		NodeGetterSortListBox.SelectedIndex = 0;
		Renderer.RenderNodeGetter();
		RunnerUI.UpdateUI();
	}
	
	#region INotifyPropertyChanged
	public new event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
	
	protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
	{
		base.OnApplyTemplate(e);
		
		MainViewModel.Instance.InitializeSetting();
		TipsManager.Initialize();
	}
}