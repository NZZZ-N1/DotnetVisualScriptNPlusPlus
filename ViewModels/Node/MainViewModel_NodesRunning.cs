using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.Exceptions;
using DVSNPP_Demo.RunningNodes;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	private static Lock Locker_Running = new Lock();
	private static bool IsRunning = false;
	
	public void ButtonClick_RunNodes()
	{
		NppRuntime.Pointer.Point = null;
		lock (Locker_Running)
		{
			if (IsRunning)
			{
				RunnerUI.Add(new NppRuntimeIsExecutingException().Message);
				return;
			}
		}
		Task.Run(StartExecuting);
	}
	
	async Task StartExecuting()
	{
		lock (Locker_Running)
		{
			IsRunning = true;
		}
		
		Exception? ex = await Runner.ExecuteEntranceNevent();
		if (ex != null)
		{
			RunnerUI.Add("ERROR:" + ex.Message);
			
			if (ex is NppRuntimeIsExecutingException) ;
			else if (ex is NeventNotFoundException) ;
			else
				throw ex;
		}
		
		lock (Locker_Running)
		{
			IsRunning = false;
		}
		NppRuntime.Pointer.Point = null;
		RunnerUI.Add("Execution ended");
	}
}