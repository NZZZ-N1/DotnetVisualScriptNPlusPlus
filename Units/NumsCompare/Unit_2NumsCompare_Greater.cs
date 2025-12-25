using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Units.NumsOperate
{
	public class Unit_2NumsCompare_Greater :  Unit_2NumsCompareBase
	{
		public Unit_2NumsCompare_Greater(IVariableRequire ivr) : base(ivr)
		{
			
		}
		
		protected override bool CompareOperation(double num1, double num2) => num1 > num2;
		protected override string CompareText() => ">";
	}
}