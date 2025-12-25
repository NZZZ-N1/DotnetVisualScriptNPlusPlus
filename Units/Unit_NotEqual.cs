using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public class Unit_NotEqual : Unit_Equal
	{
		public Unit_NotEqual(IVariableRequire ivr) : base(ivr)
		{
			
		}
		
		protected override bool TargetCondition() => false;
		public override string[] OGetTextIdentifier() => new[] { null, "!=", null };
	}
}