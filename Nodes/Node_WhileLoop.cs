using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_WhileLoop : Node_LoopBase, IVariableRequire
	{
		public Node_WhileLoop(IBranch branch) : base(branch)
		{
			Puller = new UnitPuller(this);
		}
		public Type[] RequireVariableBaseType() =>  new Type[] { typeof(Variable_Bool) };
		
		UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		
		protected override void OnceLoopStart()
		{
			bool b = ((Variable_Bool)Puller.units[0].OperateAndGetNewVariable()).Value;
			if(!b)
				SetBreak();
		}
		public string[] OGetParameterName() => new string[] { "Condition" };
		
		public string[] OGetTextIdentifier() => new string[] { null };
	}
}