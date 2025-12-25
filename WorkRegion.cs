using System;
using System.IO;

namespace DVSNPP_Demo;

public static class WorkRegion
{
	public static string? WorkRegionPath { get; private set; } = null;
	public static void SetWorkRegionPath(string? path)
	{
		WorkRegionPath = path;
		if (!IsPathValid && WorkRegionPath != null)
			Console.WriteLine("An invalid path has been set");
	}
	public static bool IsPathValid => WorkRegionPath != null && Directory.Exists(WorkRegionPath);
}