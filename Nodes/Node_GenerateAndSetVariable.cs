using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus.Libs;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Nodes
{
	public class Node_GenerateAndSetVariable : NodeBase, IVariableRequire
	{
		public Node_GenerateAndSetVariable(IBranch branch) : base(branch)
		{
			Puller = new UnitPuller(this);
		}
		
		protected override Task OnExecute(ExecutionPtr ptr)
		{
			string vName = ((Variable_String)Puller.units[0].OperateAndGetNewVariable()).Value;
			object valueSet = Puller.units[1].OperateAndGetNewVariable().GetValue();
			
			//变量存在
			if (VariableLib.IsInvolved(vName))
			{
				if (VariableLib.GetVariable(vName).GetValueType() != valueSet.GetType())
				{
					throw new ArgumentException("The variable has existed but you add another one whose type is different");
				}
				else
				{
					VariableLib.GetVariable(vName).SetValue(valueSet);
					return Task.CompletedTask;;
				}
			}
			
			//不存在，需要创建变量
			VariableBase variable;
			Type t = Puller.units[1].OperateAndGetNewVariable().GetType();
			if (t == typeof(Variable_Num))
				variable = new Variable_Num();
			else if (t == typeof(Variable_String))
				variable = new Variable_String();
			else if (t == typeof(Variable_Bool))
				variable = new Variable_Bool();
			else
				throw new Exception("Unknown variable type");
			variable.SetValue(Puller.units[1].OperateAndGetNewVariable().GetValue());
			VariableLib.AddVariable(variable, vName);
			
			return Task.CompletedTask;
		}
		
		public Type[] RequireVariableBaseType() => new Type[] { typeof(Variable_String), null };
		
		protected UnitPuller Puller;
		public UnitPuller GetPuller() => Puller;
		public string[] OGetParameterName() => new string[] { "variable name", "initial value" };
		
		public string[] OGetTextIdentifier() => new string[] { null, null };
	}
}