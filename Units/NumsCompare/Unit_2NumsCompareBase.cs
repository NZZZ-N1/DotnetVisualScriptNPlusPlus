using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units.NumsOperate
{
	public abstract class Unit_2NumsCompareBase : UnitBase, IVariableRequire
	{
		public Unit_2NumsCompareBase(IVariableRequire ivr) : base(ivr)
		{
			Puller = new UnitPuller(this);
		}
		
		public override VariableBase OperateAndGetNewVariable()
		{
			double f(int index) => ((Variable_Num)Puller.units[index].OperateAndGetNewVariable()).Value;
			Variable_Bool v = new  Variable_Bool();
			v.Value = CompareOperation(f(0), f(1));
			return v;
		}
		protected abstract bool CompareOperation(double num1, double num2);
		
		public override Type VariableBaseTypeReturned() => typeof(Variable_Bool);
		public Type[] RequireVariableBaseType() => new Type[] { typeof(Variable_Num), typeof(Variable_Num) };
		
		private UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		public string[] OGetParameterName() => new string[] { null, null };
		
		protected abstract string CompareText();
		public string[] OGetTextIdentifier() => new string[] { null, CompareText(), null };
	}
}