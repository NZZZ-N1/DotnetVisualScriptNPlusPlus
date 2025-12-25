using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public sealed class Unit_Constant_String : Unit_ConstantBase
	{
		public Unit_Constant_String(IVariableRequire belong) : base(belong)
		{
			
		}
		
		public Variable_String Variable { get; set; } = new Variable_String();
		
		public override VariableBase OperateAndGetNewVariable() => Variable;
		public override Type VariableBaseTypeReturned() => typeof(Variable_String);
		
		public override void SetValue(object v) => Variable.SetValue(v);
		public override void SetValueByString(string v) => Variable.SetValue(v);
		public override object GetDefaultValue() => "";
		
		public override object GetValue() => Variable.GetValue();
	}
}