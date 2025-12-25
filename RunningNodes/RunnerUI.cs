using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Threading;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Nodes;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo.RunningNodes;

public static class RunnerUI
{
	static MainView mv => MainView.Instance;
	private static TextBlock Label_StackTrack => mv.RunText_StackTrack;
	private static TextBlock Label_Output => mv.RunText_LogInfo;
	
	public const int MaxTextNum = 10;
	
	public static void UpdateUI()
	{
		Dispatcher.UIThread.Invoke(DoUpdateUI);
	}
	static void DoUpdateUI()
	{
		ExecutionPtr? ptr = NppRuntime.Pointer;
		NodeBase node = ptr.Point;
		
		//StackTrack
		{
			if (node == null)
			{
				Label_StackTrack.Text = "Empty";
			}
			else
			{//node exists
				
			}
		}
		
		//Output
		{
			StringBuilder sb = new StringBuilder();
			foreach (var i in OutputTexts)
				sb.AppendLine(i);
			Label_Output.Text = sb.ToString();
		}
	}
	
	/// <summary>
	/// Texts displayed on
	/// </summary>
	static List<string> OList = new List<string>(0);
	public static string[] OutputTexts => OList.ToArray();
	
	public static void Add(string? str)
	{
		if(str == null)
			return;
		if (OList.Count > MaxTextNum)
			OList.RemoveAt(0);
		OList.Add(str);
		UpdateUI();
	}
	public static void Clear()
	{
		OList.Clear();
		UpdateUI();
	}
}