using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.Exceptions;

namespace DVSNPP_Demo.RunningNodes;

public static class Runner
{
	public const string NEVENT_NAME_START = "Entrance";
	
	/// <summary>
	/// Returning null means there is no problems
	/// </summary>
	public static async Task<Exception?> ExecuteEntranceNevent()
	{
		if (NppRuntime.Pointer.Point != null)
			return new NppRuntimeIsExecutingException();
		
		Nevent? ev;
		try
		{
			ev = NeventLib.GetEvent(NEVENT_NAME_START);
			if (ev == null)
				throw new NeventNotFoundException();
		}
		catch (NeventNotFoundException)
		{
			return new NeventNotFoundException("Event\"" + NEVENT_NAME_START + "\" not found");
		}
		catch (Exception ex)
		{
			return ex;
		}
		
		RunnerUI.Add("Start running");
		NppRuntime.Pointer.Point = ev.Chain.GetNodeArray()[0];
		var (r, er) = await ev.Execute(NppRuntime.Pointer);
		return er;
	}
}