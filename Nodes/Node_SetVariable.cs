using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_SetVariable : Node_GenerateAndSetVariable
	{
		public Node_SetVariable(IBranch branch) : base(branch)
		{
			
		}
		protected override Task OnExecute(ExecutionPtr ptr)
		{
			string vName = ((Variable_String)Puller.units[0].OperateAndGetNewVariable()).Value;
			object valueSet = Puller.units[1].OperateAndGetNewVariable().GetValue();
			
			if (VariableLib.IsInvolved(vName))
			{
				if (VariableLib.GetVariable(vName).GetValueType() != valueSet.GetType())
				{
					throw new ArgumentException("The variable has existed but you add another one whose type is different");
				}
				else
				{
					VariableLib.GetVariable(vName).SetValue(valueSet);
					return Task.CompletedTask;
				}
			}
			throw new MissingMemberException("The variable dose not exist");
		}
	}
}