using EmbededLocalization;

namespace DVSNPP_Demo.ViewModels;
using static LocalizedTxts;

public static class LocalizedTxts
{
	public static string f(string id) => id.Localized();
}

public partial class MainViewModel
{
	//public string Txt_I => f("");
	public string Txt_Setting => f("setting");
	public string Txt_Language => f("language");
	public string Txt_Save => f("save");
	public string Txt_Load => f("load");
	public string Txt_SetWorkRegion => f("set" + "wr");
	public string Txt_RemoveNode => f("remove_node");
	public string Txt_AddEvent => f("add_event");
	public string Txt_RemoveEvent => f("remove_event");
	public string Txt_RenameEvent => f("rename_event");
	
	public void UpdateTxt()
	{ 
		OnPropertyChanged(nameof(Txt_Setting));
		OnPropertyChanged(nameof(Txt_Language));
		OnPropertyChanged(nameof(Text_WorkRegion));
		OnPropertyChanged(nameof(Txt_Save));
		OnPropertyChanged(nameof(Txt_Load));
		OnPropertyChanged(nameof(Txt_SetWorkRegion));
		OnPropertyChanged(nameof(Txt_SetWorkRegion));
		OnPropertyChanged(nameof(Txt_RemoveNode));
		OnPropertyChanged(nameof(Txt_AddEvent));
		OnPropertyChanged(nameof(Txt_RemoveEvent));
		OnPropertyChanged(nameof(Txt_RenameEvent));
		OnPropertyChanged(nameof(Text_AddNodeUnderBranch));
	}
}