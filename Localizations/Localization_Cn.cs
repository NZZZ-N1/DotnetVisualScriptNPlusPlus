using DotnetVisualScriptNPlusPlus.Nodes;
using DVSNPP_Demo.Nodes;
using EmbededLocalization;

namespace DVSNPP_Demo.Localizations;

public sealed class Localization_Cn : TxtBase
{
	public override string LanguageID() => "Cn";
	public override (string, string)[] GetLocalizationTxts() =>
	[
		("setting", "设置"),
		("language", "偏好语言"),
		
		("save", "保存"),
		("load", "加载"),
		("set" + "wr", "设置工作路径"),
		
		("add_under_branch", "将节点添加到分支下"),
		("add_event", "添加事件"),
		("remove_node", "移除节点"),
		("remove_event", "移除事件"),
		("rename_event", "重命名事件"),
		
		#region Node
		//node<Node_>(""),
		node<Node_Log>("输出"),
		node<Node_Call>("调用"),
		node<Node_If>("如果"),
		node<Node_Else>("否则"),
		node<Node_Break>("跳出循环"),
		node<Node_WhileLoop>("While循环"),
		node<Node_GenerateAndSetVariable>("创建并设置变量"),
		node<Node_SetVariable>("设置变量"),
		#endregion
	];
}