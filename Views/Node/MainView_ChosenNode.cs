using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DVSNPP_Demo.Views;

public partial class MainView
{
	public NodeBase? GetChosenNode()
	{
		TreeViewItem? selectedI = TreeView_Nodes.SelectedItem as TreeViewItem;
		if (selectedI == null)
		{
			Console.WriteLine("selected is null");
			return null;
		}
		
		return selectedI.Tag as NodeBase;
	}
}