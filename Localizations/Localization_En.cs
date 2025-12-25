using DotnetVisualScriptNPlusPlus.Nodes;
using DVSNPP_Demo.Nodes;
using EmbededLocalization;

namespace DVSNPP_Demo.Localizations;

public sealed class Localization_En : TxtBase
{
	public override string LanguageID() => "En";
	public override (string, string)[] GetLocalizationTxts() =>
	[
		("setting", "Settings"),
		("language", "Language"),
		
		("save", "Save"),
		("load", "Load"),
		("set" + "wr", "Set Work Region"),
		
		("add_under_branch", "Add node under branch"),
		("add_event", "Add event"),
		("remove_node", "Remove node"),
		("remove_event", "Remove event"),
		("rename_event", "Rename event"),
		
		#region Node
		//node<Node_>(""),
		node<Node_Log>("Print"),
		node<Node_Call>("Invoke"),
		node<Node_If>("If"),
		node<Node_Else>("Else"),
		node<Node_Break>("Break loop"),
		node<Node_WhileLoop>("While loop"),
		node<Node_GenerateAndSetVariable>("Create and set variable"),
		node<Node_SetVariable>("Set variable"),
		#endregion
	];
}