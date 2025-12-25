using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_If : NodeBase, IVariableRequire, IBranch
	{
		public Node_If(IBranch branch) : base(branch)
		{
			Puller = new UnitPuller(this);
		}
		
		protected override async Task OnExecute(ExecutionPtr ptr)
		{
			UnitBase unit = Puller.units[0];
			bool b = ((Variable_Bool)unit.OperateAndGetNewVariable()).Value;
			if (b)
				await Chain.ExecuteAll(ptr);
			IsConditionTrue = b;
		}
		
		public bool IsConditionTrue { get; protected set; } = false;
		
		public Type[] RequireVariableBaseType() => new Type[] { typeof(Variable_Bool) };
		
		private UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		
		private NodeChain Chain = new NodeChain();
		public NodeChain GetBranchChain() => Chain;
		public string[] OGetParameterName() => new string[] { "Condition" };
		
		public string[] OGetTextIdentifier() => new string[] { null };
	}
}