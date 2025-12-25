using Avalonia.Controls;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo.NppControlRender;

public static partial class Renderer
{
	public static void RenderNppEventListBox()
	{
		ListBox lb = MainView.Instance.ListBox_NppEvent;
		lb.Items.Clear();
		foreach (var id in NeventLib.GetEventNameArr())
		{
			Nevent e = NeventLib.GetEvent(id);
			lb.Items.Add(id);
		}
		if (lb.SelectedItem == null)
			lb.SelectedIndex = 0;
		Renderer.RenderNppNode();
	}
}