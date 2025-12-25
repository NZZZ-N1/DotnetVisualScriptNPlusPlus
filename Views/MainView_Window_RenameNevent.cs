using Avalonia.Interactivity;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.NppControlRender;

namespace DVSNPP_Demo.Views;

public partial class MainView
{
	public void OnWindowRenameNeventDisplayed()
	{
		TextBox_RenameNevent.Text = "";
		UpdateWindowRenameNeventUI();
	}
	public void UpdateWindowRenameNeventUI()
	{
		if(!Renderer.WindowDisplayed(WindowDisplayState.RenameNevent))
			return;
		string? evName = ListBox_NppEvent.Selection.SelectedItem as string;
		if(evName == null)
			return;
		
		Label_RenameNevent.Content = "Rename event \"" + evName + "\"";
	}
	
	public void Button_WindowRenameNevent_Apply(object? sender, RoutedEventArgs arg)
	{
		string? str = TextBox_RenameNevent.Text;
		if (str == null || str.Trim() == "")
		{
			Button_WindowRenameNevent_Cancel(null, null!);
			return;
		}
		
		NeventLib.RenameNevent((string)ListBox_NppEvent.Selection.SelectedItem!, str);
		Renderer.RenderAll();
		Button_WindowRenameNevent_Cancel(null, null!);
	}
	
	public void Button_WindowRenameNevent_Cancel(object? sender, RoutedEventArgs arg)
	{
		Renderer.RemoveWindowDisplay(WindowDisplayState.RenameNevent);
	}
}