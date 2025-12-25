using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public abstract class Node_LoopBase : NodeBase, IBranch
	{
		protected Node_LoopBase(IBranch branch) : base(branch)
		{
			
		}
		protected override async Task OnExecute(ExecutionPtr ptr)
		{
			Break = false;
			BeforeLoop();
			
			for (;;)
			{
				if(Break)
					break;
				foreach (NodeBase node in Chain.GetNodeArray())
				{
					OnceLoopStart();
					if(Break)
						break;
					ptr.Point = node;
					await node.Execute(ptr);
					OnceLoopEnd();
				}
			}
			
			AfterLoop();
			Break = false;
		}
		
		protected virtual void BeforeLoop() { }
		protected virtual void OnceLoopStart() { }
		protected virtual void OnceLoopEnd() { }
		protected virtual void AfterLoop() { }
		
		protected bool Break = false;
		public void SetBreak()
		{
			Break = true;
		}
		
		NodeChain Chain = new NodeChain();
		public NodeChain GetBranchChain() => Chain;
	}
}