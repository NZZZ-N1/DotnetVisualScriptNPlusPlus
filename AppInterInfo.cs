using System;
using DotnetVisualScriptNPlusPlus;
using EasySaving;
using Path = System.IO.Path;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	const string DemoVersion = "b";
	
	public string Text_AppInterInfo => ">=DVSNPP=< [" + NppVersion.VersionNum + "-" + DemoVersion + "]";
}