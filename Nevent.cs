using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus
{
    public sealed class Nevent : IBranch
    {
        public readonly NodeChain Chain = new NodeChain();
        public NodeChain GetBranchChain() => Chain;

        public async Task<(NeventExecutionResult, Exception)> Execute(ExecutionPtr ptr)
        {
            NodeBase last = ptr.Point;
            (NeventExecutionResult, Exception) v = await this.ExecuteFromBranch();
            ptr.Point = last;
            
            return v;
        }
    }
}