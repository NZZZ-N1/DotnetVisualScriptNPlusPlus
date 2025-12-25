using DotnetVisualScriptNPlusPlus.Nodes;

namespace DotnetVisualScriptNPlusPlus
{
    public sealed class ExecutionPtr
    {
        public static ExecutionPtr instance { get; private set; } = null;
        public NodeBase Point = null;

        public ExecutionPtr()
        {
            if (instance != null)
                throw new System.ArgumentException("You can't create another ExecutionPtr instance");
            instance = this;
        }
    }
}