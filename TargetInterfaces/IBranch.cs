using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Nodes;

namespace DotnetVisualScriptNPlusPlus.TargetInterfaces
{
    public interface IBranch
    {
        NodeChain GetBranchChain();
    }

    public static class IBranchEx
    {
        public static async Task<(NeventExecutionResult, Exception)> ExecuteFromBranch(this IBranch branch)
        {
            NodeChain chain = branch.GetBranchChain();
            if (chain == null)
                throw new NullReferenceException("It got a null chain on the branch");
            NodeBase last = NppRuntime.Pointer.Point;
            foreach (var node in chain.GetNodeArray())
            {
                NppRuntime.Pointer.Point = node;
                try
                {
                    await node.Execute(NppRuntime.Pointer);
                }
                catch (Exception e)
                {
                    return (NeventExecutionResult.Unexpected, e);
                }
            }
            NppRuntime.Pointer.Point = last;
            return (NeventExecutionResult.Safe, null);
        }

        public static bool IsInvolved(this IBranch branch, NodeBase node) => false;
    }
}