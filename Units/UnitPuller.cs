using System;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public sealed class UnitPuller
	{
		public readonly IVariableRequire RUnit = null;
		public UnitBase[] units { get; set; } = null;
		
		public UnitPuller(IVariableRequire iv)
		{
			RUnit = iv ?? throw new ArgumentException();
			units = new UnitBase[iv.RequireVariableBaseType().Length];
			iv.CheckRequireVariableType(this, true);
		}
	}
}