using System;
using Avalonia.Controls;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DVSNPP_Demo.Extensions;
using DVSNPP_Demo.NppControlRender;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	public static (IVariableRequire?, int)? SelectedUnitIndex { get; set; } = null;
	public static void LogSelectedUnitAndIvrInfo()
	{
		string f()
		{
			if (SelectedUnitIndex == null)
				return "No selection";
			(IVariableRequire?, int) info = SelectedUnitIndex.Value;
			IVariableRequire? ivr = info.Item1;
			int index = info.Item2;
			
			string str = "";
			str += ivr != null ? ivr.GetType().Name : "No target";
			str += "->" + index;
			return str;
		}
		Console.WriteLine("Unit Selection:" + f());
	}
	public static UnitBase? GetSelectedUnit()
	{
		if (SelectedUnitIndex == null)
			return null;
		var (ivr, index) = ((IVariableRequire?, int))SelectedUnitIndex;
		if (ivr == null)
			return null;
		UnitBase? unit = ivr.GetPuller().units[index];
		return unit;
	}
	public static IVariableRequire? GetSelectedIvr()
	{
		if (SelectedUnitIndex == null)
			return null;
		var (ivr, index) = ((IVariableRequire?, int))SelectedUnitIndex;
		if (ivr == null)
			return null;
		return ivr;
	}
	
	public static void Click_SelectTargetUnit(IVariableRequire ivrp, int unitIndex)
	{
		Console.WriteLine(ivrp.GetType().Name + ":" + unitIndex);
		SelectedUnitIndex = (ivrp, unitIndex);
		Renderer.RenderNppNode();
		//LogSelectedUnitAndIvrInfo();
	}
}