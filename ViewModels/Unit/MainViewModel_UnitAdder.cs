using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Units.NumsOperate;
using DVSNPP_Demo.Extensions;
using DVSNPP_Demo.NppControlRender;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	static void AddUnit(Func<UnitBase> func)
	{
		if (func == null) 
			throw new ArgumentNullException();
		var info = SelectedUnitIndex;
		if(info == null)
			return;
		
		IVariableRequire? selectedIvr = GetSelectedIvr();
		if(selectedIvr == null)
			return;
		if (GetSelectedUnit() != null)
			return;
		UnitBase unit = func();
		int index = (int)info?.Item2!;
		selectedIvr.GetPuller().units[index] = unit;
		Renderer.RenderNppNode();
	}
	
	//public void UnitAdd_@() => AddUnit(() => new Unit_(GetSelectedIvr()));
	
	#region Standard
	
	public void UnitAdd_GetVariable() => AddUnit(() => new Unit_GetVariable(GetSelectedIvr()));
	
	public void UnitAdd_Constant_Num() => AddUnit(() => new Unit_Constant_Num(GetSelectedIvr()));
	
	public void UnitAdd_Constant_Bool() => AddUnit(() => new Unit_Constant_Bool(GetSelectedIvr()));
	
	public void UnitAdd_Constant_String() => AddUnit(() => new Unit_Constant_String(GetSelectedIvr()));
	
	#endregion
	#region Compare
	
	public void UnitAdd_Equal() => AddUnit(() => new Unit_Equal(GetSelectedIvr()));
	
	public void UnitAdd_NotEqual() => AddUnit(() => new Unit_NotEqual(GetSelectedIvr()));
	
	public void UnitAdd_Greater() => AddUnit(() => new Unit_2NumsCompare_Greater(GetSelectedIvr()));
	
	public void UnitAdd_GreaterEqual() => AddUnit(() => new Unit_2NumsCompare_GreaterAndEqual(GetSelectedIvr()));
	
	public void UnitAdd_Less() => AddUnit(() => new Unit_2NumsCompare_Less(GetSelectedIvr()));
	
	public void UnitAdd_LessEqual() => AddUnit(() => new Unit_2NumsCompare_LessAndEqual(GetSelectedIvr()));
	
	#endregion
	#region Calculate
	
	#endregion
}