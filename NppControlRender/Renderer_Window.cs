using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo.NppControlRender;

public enum WindowDisplayState : byte
{
	None = 0,
	RenameNevent,
	LoadFileOnDisk,
}

public static class WindowRenderInfo
{
	public static (Grid, WindowDisplayState)[] GetWindowDisplayTargets() =>
	[
		(MainView.Instance.Window_RenameNevent, WindowDisplayState.RenameNevent),
		(MainView.Instance.Window_AccessDisk, WindowDisplayState.LoadFileOnDisk),
	];
}

public partial class Renderer
{
	static HashSet<WindowDisplayState> WindowStatesSet = new HashSet<WindowDisplayState>();
	public static WindowDisplayState[] GetWindowDisplayStates() => WindowStatesSet.ToArray();
	public static bool WindowDisplayed(WindowDisplayState state) => (state == WindowDisplayState.None) || WindowStatesSet.Contains(state);
	
	public static bool AddWindowDisplay(WindowDisplayState state)
	{ 
		bool b = WindowStatesSet.Add(state);
		RenderWindowDisplay();
		return b;
	}
	public static bool RemoveWindowDisplay(WindowDisplayState state)
	{
		bool b = WindowStatesSet.Remove(state);
		RenderWindowDisplay();
		return b;
	}
	public static bool SetWindowDisplay(WindowDisplayState state)
	{
		WindowStatesSet.Clear();
		AddWindowDisplay(state);
		return true;
	}
	
	public static void RenderWindowDisplay()
	{
		foreach (var (target, targetState) in WindowRenderInfo.GetWindowDisplayTargets())
		{
			target.IsVisible = WindowDisplayed(targetState);
		}
	}
}