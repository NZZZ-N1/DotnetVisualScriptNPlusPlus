using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units.NumsOperate
{
	public abstract class Unit_2NumsOperateBase : UnitBase, IVariableRequire
	{
		public Unit_2NumsOperateBase(IVariableRequire ivr) : base(ivr)
		{
			Puller = new UnitPuller(this);
		}
		
		public override Type VariableBaseTypeReturned() => typeof(Variable_Num);
		
		public Type[] RequireVariableBaseType() => new Type[] { typeof(Variable_Num), typeof(Variable_Num) };
		
		public readonly UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		
		public override VariableBase OperateAndGetNewVariable()
		{
			Variable_Num v = new Variable_Num();
			CheckVaildValue();
			double v1 = ((Variable_Num)Puller.units[0].OperateAndGetNewVariable()).Value;
			double v2 = ((Variable_Num)Puller.units[1].OperateAndGetNewVariable()).Value;
			v.Value = DoOperate(v1, v2);
			return v;
		}
		protected abstract double DoOperate(double v1, double v2);
		public string[] OGetParameterName() => new string[] { null, null };
		
		protected abstract string OperateText();
		public string[] OGetTextIdentifier() => new string[] { null, OperateText(), null };
	}
}