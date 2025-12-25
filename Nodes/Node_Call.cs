using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public sealed class Node_Call : NodeBase, IVariableRequire
	{
		public Node_Call(IBranch branch) : base(branch)
		{
			Puller = new UnitPuller(this);
		}
		
		protected override async Task OnExecute(ExecutionPtr ptr)
		{
			UnitBase unit = Puller.units[0];
			Variable_String v = (Variable_String)unit.OperateAndGetNewVariable();
			//bool b = NeventLib.ExecuteEventUnsafely(v.Value);
			
			Nevent ev = NeventLib.GetEvent(v.Value);
			if (ev == null)
				throw new MissingMemberException("There is no Nevent named " + v);
			await ev.Execute(ptr);
		}
		
		public Type[] RequireVariableBaseType() => new[] { typeof(Variable_String) };
		
		private UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		
		public string[] OGetParameterName() => new string[] { "Event name" };
		
		public string[] OGetTextIdentifier() => new string[] { null };
	}
}