using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
    public sealed class EmptyNode : NodeBase
    {
        public EmptyNode(IBranch branch) : base(branch)
        {
            
        }

        protected override Task OnExecute(ExecutionPtr ptr)
        {
            return Task.CompletedTask;
        }
    }
}