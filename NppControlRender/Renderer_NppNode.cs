using System;
using System.Collections.Generic;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DVSNPP_Demo.Extensions;
using DVSNPP_Demo.Nodes;
using DVSNPP_Demo.ViewModels;
using DVSNPP_Demo.Views;
using EmbededLocalization;

namespace DVSNPP_Demo.NppControlRender;

public static partial class Renderer
{
	public static void RenderNppNode()
	{
		SetNodeTag();
		string str = (string)MainView.Instance.ListBox_NppEvent.Selection.SelectedItem!;
		Nevent ev = NeventLib.GetEvent(str);
		TreeView tree = MainView.Instance.TreeView_Nodes;
		SetTreeViewItem(tree, ev, str);
		
		SetTreeViewItemExpanded();
		
		TreeView tv = MainView.Instance.TreeView_Nodes;
		tv.SelectionMode = SelectionMode.Single;
	}
	
	static void ForeachTreeItem(Action<TreeViewItem> act)
	{
		TreeView tv  = MainView.Instance.TreeView_Nodes;
		
		void f(TreeViewItem item)
		{
			act(item);
			foreach (object? o in item.Items!)
			{
				if (o is TreeViewItem i)
					f(i);
			}
		}
		
		foreach (TreeViewItem i in tv.Items!)
			f(i!);
	}
	
	public static void SetNodeTag()
	{
		ForeachTreeItem(item =>
		{
			if(item.Tag is not NodeBase node)
				return;
			NodeTags tag = new NodeTags();
			tag.UnfoldTreeItem = item.IsExpanded;
			node.Tag = tag;
		});
	}
	public static void SetTreeViewItemExpanded()
	{
		ForeachTreeItem((item) =>
		{
			if (item.Tag is not NodeBase node)
				return;
			if (node.Tag == null)
				return;
			NodeTags? tag = node.Tag as NodeTags?;
			item.IsExpanded = tag?.UnfoldTreeItem ?? true;
		});
	}
	
	
	static void SetTreeViewItem(TreeView tree, Nevent? ev, string evName)
	{
		tree.Items.Clear();
		
		void f(NodeBase node, TreeViewItem parent)
		{
			TreeViewItem item = new TreeViewItem();
			item.Tag = node;
			//item.Header = node.GetType().Name;
			RenderOneNode(item, node);
			
			parent.Items.Add(item);
			if (node is IBranch branch)
				foreach (var i in branch.GetBranchChain().GetNodeArray())
					f(i, item);
		}
		
		TreeViewItem item = new TreeViewItem();
		tree.Items.Add(item);
		item.Header = evName;
		item.IsExpanded = true;
		
		if(ev != null)
			foreach (var n in ev.Chain.GetNodeArray())
				f(n, item);
	}
	
	static void RenderOneNode(TreeViewItem vp, NodeBase node)
	{
		StackPanel panel = new StackPanel()
		{
			Orientation = Orientation.Horizontal,
			HorizontalAlignment = HorizontalAlignment.Left,
			VerticalAlignment = VerticalAlignment.Center,
		};
		vp.Header = panel;
		
		panel.Children.Add(new Label
		{
			Content = node.GetType().Name.Localized(),
			FontSize = 18,
			Foreground = Brushes.White,
			VerticalAlignment = VerticalAlignment.Center,
		});
		
		if (node is not IVariableRequire ivr)
			return;
		int i = 0;
		foreach (var u in ivr.GetPuller().units)
		{
			StackPanel p = RON_AddPanel(panel, u, true);
			p.Tag = node;
			RON_RenderOneUnit(p, ivr, u, i);
			i += 1;
		}
	}
}