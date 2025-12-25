using System;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public class Unit_GetVariable : UnitBase, IVariableRequire
	{
		public Unit_GetVariable(IVariableRequire ivr) : base(ivr)
		{
			Puller = new UnitPuller(this);
		}
		
		public Type[] RequireVariableBaseType() => new[] { typeof(Variable_String) };
		public override Type VariableBaseTypeReturned()
		{
			if (Puller == null || Puller.units == null)
				return null;
			if (Puller.units.Length <= 0)
				return null;
			string vName = ((Variable_String)Puller.units[0].OperateAndGetNewVariable()).Value;
			if (!VariableLib.IsInvolved(vName))
				return null;
			VariableBase vGet = VariableLib.GetVariable(vName);
			if (vGet == null)
				return null;
			return vGet.GetType();
		}
		
		protected UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		
		public override VariableBase OperateAndGetNewVariable()
		{
			string vName = ((Variable_String)Puller.units[0].OperateAndGetNewVariable()).Value;
			if (!VariableLib.IsInvolved(vName))
				throw new MissingMemberException("The variable does not exist");
			VariableBase vGet = VariableLib.GetVariable(vName);
			
			VariableBase variable;
			Type t = vGet.GetType();
			if (t == typeof(Variable_Num))
				variable = new Variable_Num();
			else if (t == typeof(Variable_String))
				variable = new Variable_String();
			else if (t == typeof(Variable_Bool))
				variable = new Variable_Bool();
			else
				throw new Exception("Unknown variable type");
			variable.SetValue(vGet.GetValue());
			return variable;
		}
		
		public string[] OGetParameterName() => new string[] { "variable name" };
		
		public string[] OGetTextIdentifier() => new string[] { null };
	}
}