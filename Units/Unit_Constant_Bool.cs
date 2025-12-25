using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public sealed class Unit_Constant_Bool : Unit_ConstantBase
	{
		public Unit_Constant_Bool(IVariableRequire belong) : base(belong)
		{
			
		}
		
		public Variable_Bool Variable { get; set; } = new Variable_Bool();
		
		public override VariableBase OperateAndGetNewVariable() => Variable;
		public override Type VariableBaseTypeReturned() => typeof(Variable_Bool);
		
		public override void SetValue(object v) => Variable.SetValue(v);
		public override void SetValueByString(string v) => Variable.SetValue(bool.Parse(v));
		public override object GetDefaultValue() => false;
		
		public override object GetValue() => Variable.GetValue();
	}
}