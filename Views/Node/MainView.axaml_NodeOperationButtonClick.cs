using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.Nodes;
using DVSNPP_Demo.NppControlRender;
using DVSNPP_Demo.RunningNodes;
using DVSNPP_Demo.ViewModels;

namespace DVSNPP_Demo.Views;

public delegate void OnTreeNodeSelectionChanged(NodeBase? nowChosenNode);

public partial class MainView
{
	public bool AddNodeUnderBranch { get; set; } = false;
	
	#region Node
	
	public void Button_RefreshNodes_Click(object? sender, RoutedEventArgs e)
	{
		if (ListBox_NppEvent.SelectedItem == null)
			return;
		Renderer.RenderNppNode();
		//Console.WriteLine(ListBox_NppEvent.SelectedItem!.ToString());
	}
	
	public void Button_RemoveNode_Click(object? sender, RoutedEventArgs e)
	{
		Nevent ev;
		{
			string? str = ListBox_NppEvent.Selection.SelectedItem as string;
			if (str == null)
				return;
			ev = NeventLib.GetEvent(str);
			if (ev == null)
				return;
		}
		
		NodeBase? chosenNode = GetChosenNode();
		if (chosenNode == null)
			return;
		ev.Chain.Remove(chosenNode);
		Renderer.RenderNppNode();
	}
	
	public void Button_ChangeAddUnderBranch_Click(object? sender, RoutedEventArgs e)
	{
		AddNodeUnderBranch = !AddNodeUnderBranch;
		MainViewModel.Instance.OnPropertyChanged(nameof(MainViewModel.Text_AddNodeUnderBranch));
	}
	
	#endregion
	
	#region Nevent
	
	public void Button_RefreshNevent_Click(object? sender, RoutedEventArgs e)
	{
		if(NeventLib.GetEventNameArr().Length <= 0)
			NeventLib.AddEvent(new Nevent(), "Default Event");
		Renderer.RenderNppEventListBox();
	}
	
	public void Button_AddNewNevent_Click(object? sender, RoutedEventArgs e)
	{
		Nevent ev = new Nevent();
		
		string? name = null;
		if (!NeventLib.IsInvolved("Entrance"))
			name = Runner.NEVENT_NAME_START;
		
		int num = 1;
		while (!NeventLib.AddEventUnsafely(ev, name ?? "New Event" + num))
			num += 1;
		
		Renderer.RenderAll();
	}
	
	public void Button_RenameNevent_Click(object? sender, RoutedEventArgs e)
	{
		if (ListBox_NppEvent.Selection.SelectedItem == null)
			return;
		Renderer.SetWindowDisplay(WindowDisplayState.RenameNevent);
		OnWindowRenameNeventDisplayed();
	}
	
	public void Button_RemoveNevent_Click(object? sender, RoutedEventArgs e)
	{
		string? str = ListBox_NppEvent.Selection.SelectedItem as string;
		if (str == null)
			return;
		NeventLib.RemoveEvent(str);
		Renderer.RenderAll();
	}
	
	#endregion
	
	public void OnNppEventSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		Renderer.RenderNppNode();
		UpdateWindowRenameNeventUI();
	}
	
	public void Clicked_LanguageSelectedChanged(object? sender, SelectionChangedEventArgs e) => MainViewModel.Instance.Clicked_LanguageSelectedChanged(sender, e);
	
	public static event OnTreeNodeSelectionChanged? OnNodeSelectionChanged = null;
	public void OnNodeTreeSelectionChanged(object? sender, SelectionChangedEventArgs arg)
	{
		OnNodeSelectionChanged?.Invoke(GetChosenNode());
	}
}