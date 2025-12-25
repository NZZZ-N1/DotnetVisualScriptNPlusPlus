using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;

namespace DVSNPP_Demo.Extensions;

public static class IVariableRequireEx
{
	public static int? FindIndex(this IVariableRequire ivr, UnitBase target)
	{
		if (target == null)
			throw new ArgumentNullException();
		UnitBase[] arr = ivr.GetPuller().units;
		for (int i = 0; i < arr.Length; i++)
			if (arr[i] == target)
				return i;
		return null;
	}
}