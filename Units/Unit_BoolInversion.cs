using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public class Unit_BoolInversion : UnitBase, IVariableRequire
	{
		public Unit_BoolInversion(IVariableRequire ivr) : base(ivr)
		{
			Puller = new UnitPuller(this);
		}
		
		public override VariableBase OperateAndGetNewVariable()
		{
			Variable_Bool va = new Variable_Bool();
			Variable_Bool v = (Variable_Bool)Puller.units[0].OperateAndGetNewVariable();
			va.Value = !v.Value;
			return va;
		}
		
		public override Type VariableBaseTypeReturned() => typeof(Variable_Bool);
		public Type[] RequireVariableBaseType() => new[] { typeof(Variable_Bool) };
		
		private UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		public string[] OGetParameterName() => new string[] { null };
		
		public string[] OGetTextIdentifier() => new string[] { "Inverse", null };
	}
}