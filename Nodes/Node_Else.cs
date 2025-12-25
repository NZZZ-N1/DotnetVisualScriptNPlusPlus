using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_Else : NodeBase, IBranch
	{
		public Node_Else(IBranch branch) : base(branch)
		{
			
		}
		
		protected override async Task OnExecute(ExecutionPtr ptr)
		{
			NodeBase[] ns = Branch.GetBranchChain().GetNodeArray();
			int index = -1;
			for (int i = 0; i < ns.Length; i++)
			{
				if (this == ns[i])
				{
					index = i;
					break;
				}
			}
			if (index == -1 || index == 0)
				throw new AccessViolationException("Target node not found or in the impossible position");
			if (!(ns[index - 1] is Node_If node))
				throw new AccessViolationException();
			
			if(!node.IsConditionTrue)
				await Chain.ExecuteAll(ptr);
		}
		
		private NodeChain Chain = new NodeChain();
		public NodeChain GetBranchChain() => Chain;
	}
}