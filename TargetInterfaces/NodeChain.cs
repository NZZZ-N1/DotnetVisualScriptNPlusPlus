using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Nodes;

namespace DotnetVisualScriptNPlusPlus.TargetInterfaces
{
    public class NodeChain
    {
        private List<NodeBase> NList = new List<NodeBase>();
        public int NodeNum => NList.Count;
        
        public bool IsInvolved(NodeBase node) => NList.Contains(node);

        private void CheckIndexAndValue(NodeBase node, bool allowContain)
        {
            if (node == null)
                throw new NullReferenceException("Node is null");
            if (!(allowContain || !NList.Contains(node)))
                throw new Exception("Node is already in the chain");
            if (node.Branch.GetBranchChain() != this)
                throw new ArgumentException("The node is not in this branch");
        }
        
        public void Add(NodeBase node, int newItemIndex)
        {
            CheckIndexAndValue(node, false);
            if (newItemIndex > NList.Count)
                throw new IndexOutOfRangeException("newItemIndex is out of range");
            NodeBase[] arr = new NodeBase[NList.Count + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < newItemIndex)
                    arr[i] = NList[i];
                if (i == newItemIndex)
                    arr[i] = node;
                if (i > newItemIndex) 
                    arr[i] = NList[i - 1];
            }
            NList = new List<NodeBase>(arr);
        }
        public void AddLast(NodeBase node) => Add(node, NodeNum);

        public void Remove(NodeBase node)
        {
            CheckIndexAndValue(node, true);
            NList.Remove(node);
        }

        public NodeBase[] GetNodeArray() => NList.ToArray();
        
        public async Task ExecuteAll(ExecutionPtr ptr)
        {
            NodeBase last = ptr.Point;
            ptr.Point = NList[0];
            foreach (var node in GetNodeArray())
                await node.Execute(ptr);
            ptr.Point = last;
        }
    }
}