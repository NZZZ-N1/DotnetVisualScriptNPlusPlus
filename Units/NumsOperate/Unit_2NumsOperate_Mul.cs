using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Units.NumsOperate
{
	public class Unit_2NumsOperate_Mul : Unit_2NumsOperateBase
	{
		public Unit_2NumsOperate_Mul(IVariableRequire ivr) : base(ivr)
		{
			
		}
		
		protected override double DoOperate(double v1, double v2) => v1 * v2;
		protected override string OperateText() => "×";
	}
}