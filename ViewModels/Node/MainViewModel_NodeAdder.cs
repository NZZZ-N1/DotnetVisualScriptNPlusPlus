using System;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DVSNPP_Demo.Nodes;
using DVSNPP_Demo.NppControlRender;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	
	#region Ahead
	
	static void AddNode(NodeBase node/*已被初始化的*/)
	{
		Nevent? ev = GetNowChooseNeventForNewNode();
		if (node.Branch == null || ev == null || ev != node.Branch)
		{
			Console.WriteLine("Invaild branch");
			return;
		}
		
		NodeBase? chosen = MainView.Instance.GetChosenNode();
		NEXT: ;
		if (chosen == null)
		{
			ev.Chain.AddLast(node);
		}
		else
		{
			bool addInBranch = MainView.Instance.AddNodeUnderBranch;
			if (!addInBranch || chosen is not IBranch br)
			{
				int index = -1;
				NodeBase[] arr = ev.Chain.GetNodeArray();
				for (int i = 0; i < arr.Length; i++)
					if (arr[i] == chosen)
						index = i;
				if (index == -1)
				{
					chosen = null;
					goto NEXT;
				}
				index += 1;
				ev.Chain.Add(node, index);
			}
			else
			{
				node.ForceSetBranch(br);
				br.GetBranchChain().AddLast(node);
			}
		}
		
		Renderer.RenderNppNode();
	}
	static Nevent? GetNowChooseNeventForNewNode()
	{
		try
		{
			string str = (string)MainView.Instance.ListBox_NppEvent.Selection.SelectedItem!;
			Nevent ev = NeventLib.GetEvent(str);
			if (ev == null)
				throw new Exception();
			return ev;
		}
		catch
		{
			return new Nevent();
		}
	}
	
	#endregion
	
	/*public void NodeAddButtonClick_@()
	 => AddNode(new Node(GetNowChooseNeventForNewNode()));
	*/
	
	public void NodeAddButtonClick_Empty()
		=> AddNode(new EmptyNode(GetNowChooseNeventForNewNode()));
	
	#region Standard
	
	public void NodeAddButtonClick_CallEvent()
		=> AddNode(new Node_Call(GetNowChooseNeventForNewNode()));
	
	public void NodeAddButtonClick_If()
		=> AddNode(new Node_If(GetNowChooseNeventForNewNode()));
	
	public void NodeAddButtonClick_Log()
		=> AddNode(new Node_Log(GetNowChooseNeventForNewNode()!));
	
	public void NodeAddButtonClick_Else()
		=> AddNode(new Node_Else(GetNowChooseNeventForNewNode()));
	
	#endregion
	#region Variable
	
	public void NodeAddButtonClick_CreateVariable()
		=> AddNode(new Node_GenerateAndSetVariable(GetNowChooseNeventForNewNode()));
	
	public void NodeAddButtonClick_SetVariable()
		=> AddNode(new Node_SetVariable(GetNowChooseNeventForNewNode()));
	
	#endregion
}