namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	public string Text_WorkRegion
	{
		get
		{
			string? str = WorkRegion.WorkRegionPath;
			if (str == null)
				return "No work region path";
			if (!WorkRegion.IsPathValid)
				str += " (invalid)";
			return str;
		}
	}
	
	public void ButtonClick_SetWorkRegionPath()
	{
		TipsManager.AddTip("Yes!");
	}
	public void ButtonClick_SaveProject()
	{
		
	}
	public void ButtonClick_LoadProject()
	{
		
	}
	
	public void Command_ChangeSettingPanel() => SettingPanelDisplay = !SettingPanelDisplay;
}