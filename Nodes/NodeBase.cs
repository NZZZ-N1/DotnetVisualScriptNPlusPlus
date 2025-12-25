using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
    public abstract class NodeBase
    {
        /// <summary>
        /// The branch this node belongs to
        /// </summary>
        public IBranch Branch { get; private set; } = null;
        /// <summary>
        /// Force to set the branch to which the node belongs
        /// This operation will kill the weak balance,and it will probably cause fatal problems
        /// Only
        /// Unless you clearly know what you are doing,DO NOT USE THIS METHOD
        /// </summary>
        public void ForceSetBranch(IBranch branch) => Branch = branch;
        
        /// <summary>
        /// You can set any value you want here
        /// Highly suggest that you should create a struct singly for it
        /// </summary>
        public object Tag { get; set; } = null;

        public NodeBase(IBranch branch)
        {
            Branch = branch;
            if (Branch == null)
                throw new System.ArgumentException("Branch cannot be null");
        }

        private void CheckIsVaildBranch()
        {
            if (Branch == null)
                throw new System.ArgumentException("Branch cannot be null");
            if (!Branch.GetBranchChain().IsInvolved(this))
                throw new System.ArgumentException("Branch is not involved this node");
        }

        public async Task Execute(ExecutionPtr ptr)
        {
            if (ptr == null)
                throw new System.ArgumentException("ptr cannot be null");
            CheckIsVaildBranch();
            if (ptr.Point != this)
                Console.WriteLine("The ptr points the different object");
            NodeBase last = ptr.Point;
            ptr.Point = this;
            
            try
            {
                await OnExecute(ptr);
            }
            catch
            {
                throw;
            }
            
            ptr.Point = last ?? this;
        }

        protected abstract Task OnExecute(ExecutionPtr ptr);
    }
}