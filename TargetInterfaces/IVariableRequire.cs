using System;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.TargetInterfaces
{
	public interface IVariableRequire
	{
		Type[] RequireVariableBaseType();
		UnitPuller GetPuller();
		
		/// <summary>
		/// Don't invoke it directly, please use its extensive invokes
		/// Only overwrite it through Object-Oriented
		/// </summary>
		string[] OGetParameterName();
		
		/// <summary>
		/// Get the text's pack for visualizing units texts
		/// null value means that it is used to display a unit's value
		/// DO NOT invoke it directly unless you know you are getting an array without tackling
		/// </summary>
		string[] OGetTextIdentifier();
	}
	
	public static class IVariableRequireExtensions
	{
		public static void CheckRequireVariableType(this IVariableRequire i, UnitPuller puller = null, bool allowNullPullerVariable = false)
		{
			Type[] ts = i.RequireVariableBaseType(); 
			UnitPuller p = i.GetPuller();
			
			if (p == null)
			{
				if (puller != null && puller.RUnit == i)
					p = puller;
				else
					throw new ArgumentException();
			}
			if (ts == null || ts.Length != p.units.Length)
				throw new ArgumentException("The type of variables requested is missing");
			if (p.RUnit != i)
				throw new ArgumentException("The RUnit and the IV is not the same instance");
			
			foreach (Type t in ts)
			{
				try
				{
					if (t == null)
						continue;
					object o = (VariableBase)Activator.CreateInstance(t);
					if (o == null || !(o is VariableBase))
						throw new Exception();
				}
				catch (Exception e)
				{
					throw new ArgumentException("The type required is not legal\n" + e.Message);
				}
			}
			
			for (int index = 0; index < p.units.Length; index++)
			{
				UnitBase unit = p.units[index];
				
				if (unit == null)
				{
					if (!allowNullPullerVariable)
						throw new ArgumentException("The unit is null but you did not allow it");
				}
				else
				{
					if (ts[index] != null && unit.GetType() != ts[index])
						throw new ArgumentException("The type required is not the same to what the puller has");
				}
			}
		}
		public static string[] GetParameterName(this IVariableRequire ivr)
		{
			string[] r =  new string[ivr.RequireVariableBaseType().Length];
			string[] v = ivr.OGetParameterName();
			
			for (int i = 0; i < r.Length; i++)
			{
				if (i >= v.Length)
					continue;
				string str = v[i];
				r[i] = str ?? ("para" + i + 1);
			}
			
			return r;
		}
		public static string[] GetTextIdentifier(this IVariableRequire ivr)
		{
			string[] strs = ivr.OGetTextIdentifier() ?? throw new ArgumentException("The Text Identifier has been set as null");
			//检查null值数量
			{
				int nullNum = 0;
				for(int i = 0; i < strs.Length; i++)
					if (strs[i] == null)
						nullNum += 1;
				if (nullNum != ivr.RequireVariableBaseType().Length)
					throw new ArgumentException("The num of null is not equal to the num of variables returned");
			}
			return strs;
		}
	}
}