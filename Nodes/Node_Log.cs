using System;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;
using DVSNPP_Demo.RunningNodes;

namespace DVSNPP_Demo.Nodes;

public sealed class Node_Log : NodeBase, IVariableRequire
{
	public Node_Log(IBranch branch) : base(branch)
	{
		Puller = new UnitPuller(this);
	}
	
	public Type[] RequireVariableBaseType() => [typeof(Variable_String)];
	public UnitPuller Puller { get; private set; }
	public UnitPuller GetPuller() => Puller;
	public string[] OGetParameterName() => new[] { "LogText" };
	public string?[] OGetTextIdentifier() => [null];
	protected override Task OnExecute(ExecutionPtr ptr)
	{
		RunnerUI.Add(Puller.units[0].OperateAndGetNewVariable().GetValue().ToString());
		return Task.CompletedTask;
	}
}