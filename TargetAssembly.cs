using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotnetVisualScriptNPlusPlus
{
	public static class TargetAssembly
	{
		private static LinkedList<Assembly> Assemblies = new LinkedList<Assembly>(new []{ Assembly.LoadFrom("DotnetVisualScriptNPlusPlus.dll"),  });
		
		public static void Add(Assembly assembly) =>  Assemblies.AddLast(assembly);
		public static void Remove(Assembly assembly) => Assemblies.Remove(assembly);
		public static Assembly[] GetAssemblies() => Assemblies.ToArray();
	}
}