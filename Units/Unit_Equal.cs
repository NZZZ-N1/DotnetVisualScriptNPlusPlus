using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public class Unit_Equal : UnitBase, IVariableRequire
	{
		public Unit_Equal(IVariableRequire ivr) : base(ivr)
		{
			Puller = new UnitPuller(this);
		}
		
		public override VariableBase OperateAndGetNewVariable()
		{
			VariableBase v1 = Puller.units[0].OperateAndGetNewVariable();
			VariableBase v2 = Puller.units[1].OperateAndGetNewVariable();
			Variable_Bool vr = new Variable_Bool();
			if (v1.GetValueType() != v2.GetValueType())
				throw new ArgumentException("Two variable are unable to be compared");
			vr.Value = v1.GetValue().Equals(v2.GetValue()) == TargetCondition();
			return vr;
		}
		protected virtual bool TargetCondition() => true;
		
		public override Type VariableBaseTypeReturned() => typeof(Variable_Bool);
		public Type[] RequireVariableBaseType() => new Type[] { null, null };
		
		UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		public string[] OGetParameterName() => new string[] { "value1", "value2" };
		
		public virtual string[] OGetTextIdentifier() => new string[] { null, "==", null };
	}
}