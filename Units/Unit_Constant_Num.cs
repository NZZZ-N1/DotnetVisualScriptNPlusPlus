using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public sealed class Unit_Constant_Num : Unit_ConstantBase
	{
		public Unit_Constant_Num(IVariableRequire belong) : base(belong)
		{
			
		}
		
		public Variable_Num Variable { get; set; } = new Variable_Num();
		
		public override VariableBase OperateAndGetNewVariable() => Variable;
		public override Type VariableBaseTypeReturned() => typeof(Variable_Num);
		
		public override void SetValue(object v) => Variable.SetValue(v);
		public override void SetValueByString(string v) => Variable.SetValue(double.Parse(v));
		public override object GetDefaultValue() => 0.0;
		
		public override object GetValue() => Variable.GetValue();
	}
}