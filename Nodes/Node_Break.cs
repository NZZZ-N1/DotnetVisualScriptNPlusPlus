using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_Break : NodeBase
	{
		public Node_Break(IBranch branch) : base(branch)
		{
			
		}
		
		Node_LoopBase Get()
		{
			NodeBase f(NodeBase node)
			{
				NodeBase p = node.Branch as NodeBase;
				if (p == null)
					throw new AccessViolationException("The branch belongs to is not a node");
				if (p is Node_LoopBase)
					return p;
				return f(p);
			}
			return (Node_LoopBase)f(this);
		}
		protected override Task OnExecute(ExecutionPtr ptr)
		{
			Get().SetBreak();
			return Task.CompletedTask;
		}
	}
}