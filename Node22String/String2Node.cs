using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;
using SO = DotnetVisualScriptNPlusPlus.Node22String.StringOperator;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public  static class String2Node
	{
		//public const string ASSEMBLY = "DotnetVisualScriptNPlusPlus.dll";
		internal static object CreateInstance(string typeName, object[] args)
		{
			foreach (var assembly in TargetAssembly.GetAssemblies())
			{
				try
				{
					object instance;
					if (args != null && args.Length > 0)
						instance = assembly.CreateInstance(typeName, false, BindingFlags.Default, null, args, null, null);
					else
						instance = assembly.CreateInstance(typeName);
					if (instance != null)
						return instance;
				}
				catch (TargetInvocationException ex)
				{
					Exception inner = ex.InnerException;
					if (inner != null)
					{
						Console.WriteLine(inner.ToString());
						throw inner;
					}
				}
				catch
				{
					continue;
				}
			}
			throw new Exception("Failed to create the instance:" + typeName);
		}
		
		public static Nevent GenerateNevent(string txt, out string ev_Name)
		{
			txt = GN_Format(txt);
			GN_NeventInfo(txt, out var v_name);
			txt = SO.ContentAfter(txt, "Start;");
			string[] strs = GN_Separate(txt);
			
			NI[] ns = GN_NIs(strs);
			Nevent ev = new Nevent();
			ev_Name = v_name;
			NodeBase[] nodes = GN_Nodes(ns, ev);
			foreach (NodeBase node in nodes)
				ev.Chain.AddLast(node);
			return ev;
		}
		public static NI[] GetCodeNI(string txt)
		{
			txt = GN_Format(txt);
			GN_NeventInfo(txt, out var v_name);
			txt = SO.ContentAfter(txt, "Start;");
			string[] strs = GN_Separate(txt);
			
			NI[] ns = GN_NIs(strs);
			return ns;
		}
		
		#region O1_Format
		static string GN_Format(string txt)
		{
			LinkedList<char> list = new LinkedList<char>();
			bool b = false;//是否在括号中
			
			for (int i = 0; i < txt.Length; i++)
			{
				char c = txt[i];
				if (c == '[')
				{
					if (txt[i - 1] != '@')
						b = true;
				}
				else if (c == ']')
				{
					if (txt[i - 1] != '@')
						b = false;
				}
				if(!b && char.IsWhiteSpace(c))
					continue;
				list.AddLast(c);
			}
			return new string(list.ToArray());
		}
		#endregion
		#region O2_NeventInfo
		static void GN_NeventInfo(string str, out string evInfo_Name)
		{
			str = SO.ContentBefore(str, "Start;");
			string[] us = SO.Separate(str, ";");
			
			evInfo_Name = null;
			
			foreach (var u in us)
			{
				if(string.IsNullOrEmpty(u) || string.IsNullOrWhiteSpace(u))
					continue;
				
				string head = SO.ContentBefore(u, ":");
				string content = SO.ContentAfter(u, ":");
				
				if (head == "name")
					if (evInfo_Name != null)
						throw new NeventArgHasBeenInitializedException();
					else
						evInfo_Name = content;
				else
					throw new ArgumentException("Keyword " + head + " did not be found");
			}
		}
		#endregion
		#region O3_Separate
		static string[] GN_Separate(string str)
		{
			if (string.IsNullOrEmpty(str))
				return new string[0];
			
			List<string> result = new List<string>();
			StringBuilder current = new StringBuilder();
			
			foreach (char c in str)
			{
				if (c == '{' || c == '}')
				{
					if (current.Length > 0)
					{
						result.Add(current.ToString());
						current.Clear();
					}
					result.Add(c.ToString());
				}
				else if (c == ';')
				{
					if (current.Length > 0)
					{
						result.Add(current.ToString());
						current.Clear();
					}
				}
				else
				{
					current.Append(c);
				}
			}
			if (current.Length > 0)
			{
				result.Add(current.ToString());
			}
			
			return result.ToArray();
		}
		#endregion
		#region O4_GenerateNI
		static NI[] GN_NIs(string[] strs)
		{
			NI[] f(int index, NI parent, out int endIndex)
			{
				LinkedList<NI> list = new LinkedList<NI>();
				NI last = null;
				int i = index;
				for (; i < strs.Length; i++)
				{
					string str = strs[i];
					if (str == "}")
						break;
					if (str == "{")
					{
						NI[] childs = f(i + 1, last, out var ei);
						if (last == null)
							throw new Exception("There is no node but exist \"{\"");
						last.Childs = childs;
						i = ei;
						continue;
					}
					NI ni = GN_NI_GetNI(str, parent);
					last = ni;
					list.AddLast(ni);
				}
				endIndex = i;
				return list.ToArray();
			}
			return f(0, null, out _);
		}
		#endregion
		#region O5_NiPara
		static NI GN_NI_GetNI(string str, NI parent)
		{
			int idx = 0;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c == '(')
				{
					idx = i + 1;
					goto PARA;
				}
				sb.Append(c);
			}
			NI ni = new NI(parent, false);
			ni.ThisName = sb.ToString();
			return ni;
			
			PARA:
			//有参
			NI[] f(int index, NI p, out int endIndex)
			{
				LinkedList<NI> list = new LinkedList<NI>();
				LinkedList<Char> clist = new LinkedList<char>();
				int i = index;
				for (; i < str.Length; i++)
				{
					char c = str[i];
					if (c == '(')
					{
						string s = new string(clist.ToArray());
						NI n = new NI(parent, true);
						n.ThisName = s;
						NI[] childs = f(i + 1, n, out var ei);
						n.Paras = childs;
						i = ei;
						clist.Clear();
						list.AddLast(n);
						continue;
					}
					if (c == ')')
					{
						if (clist.Count > 0)
						{
							NI n2 = new NI(parent, true);
							n2.ThisName = new string(clist.ToArray());
							list.AddLast(n2);
						}
						break;
					}
					if (c == ']')
					{
						if (str[i - 1] != '@')
						{
							clist.AddLast(c);
							string s = new string(clist.ToArray());
							NI n = new NI(parent, true);
							n.ThisName = s;
							list.AddLast(n);
							clist.Clear();
							continue;
						}
						else
						{//last == '@'
							clist.RemoveLast();
						}
					}
					if (c == '[')
					{
						if (str[i - 1] == '@')
							clist.RemoveLast();
					}
					clist.AddLast(c);
				}
				endIndex = i;
				return list.ToArray();
			}
			NI rn = new NI(parent, false);
			rn.ThisName = sb.ToString();
			rn.Paras = f(idx, rn, out _);
			return rn;
		}
		#endregion
		#region O6_Node
		static NodeBase[] GN_Nodes(NI[] nis, IBranch branch)
		{
			NodeBase[] arr = new NodeBase[nis.Length];
			for (int i = 0; i < arr.Length; i++)
				arr[i] = NI2Node(nis[i], branch);
			return arr;
		}
		
		static NodeBase NI2Node(NI ni, IBranch parent)
		{
			if (ni.IsPara)
				throw new ArgumentException("ni cant be a parameter");
			NodeBase node;
			{
				object[] args = { parent };
				string rtf = ni.ReflectedTypeFullName;
				object instance = CreateInstance(rtf, args);
				node = (NodeBase)instance;
			}
			
			if (node is IVariableRequire ivr)
			{
				UnitBase[] units = new UnitBase[ivr.RequireVariableBaseType().Length];
				for (int i = 0; i < units.Length; i++)
					units[i] = NI2Unit(ni.Paras[i], ivr);
				ivr.GetPuller().units = units;
			}
			if (node is IBranch br)
			{
				NodeBase[] childs = new NodeBase[0];
				if (ni.Childs != null)
					childs = new NodeBase[ni.Childs.Length];
				int index = 0;
				foreach (var i in ni.Childs ?? new NI[0])
				{
					childs[index] = NI2Node(i, br);
					index += 1;
				}
				foreach (var i in childs)
					br.GetBranchChain().AddLast(i);
			}
			return node;
		}
		
		static UnitBase NI2Unit(NI ni, IVariableRequire ivrp)
		{
			if (!ni.IsPara)
				throw new ArgumentException("ni must be a parameter");
			UnitBase unit;
			{
				string rtf = ni.ReflectedTypeFullName;
				if (rtf[0] == '[')
					unit = GetConstantByCode(rtf, ivrp);
				else
					unit = (UnitBase)CreateInstance(rtf, new object[] { ivrp });
			}
			
			if (unit is IVariableRequire ivr)
			{
				UnitBase[] childs = new UnitBase[ivr.RequireVariableBaseType().Length];
				for (int i = 0; i < childs.Length; i++)
				{
					NI para = ni.Paras[i];
					childs[i] = NI2Unit(para, ivr);
				}
				ivr.GetPuller().units = childs;
			}
			else if (unit is Unit_ConstantBase)
			{
				string rtf = ni.ReflectedTypeFullName;
				unit = GetConstantByCode(rtf, ivrp);
			}
			else
			{
				string rtf = ni.ReflectedTypeFullName;
				unit = (UnitBase)CreateInstance(rtf, new object[1] { ivrp });
			}
			
			return unit;
		}
		
		static UnitBase GetConstantByCode(string str, IVariableRequire belong, bool allowTryExpand = true)
		{
			if (str[0] != '[' || str[str.Length - 1] != ']')
				throw new ArgumentException("value is not a valid constant unit:" + str);
			
			List<char> lc = new List<char>(str);
			lc.RemoveAt(0);
			lc.RemoveAt(lc.Count - 1);
			str = new string(lc.ToArray());
			
			string t = SO.ContentBefore(str, ":");
			string v = SO.ContentAfter(str, ":");
			
			Unit_ConstantBase unit = ConstantUnitFactory.GenerateConstantUnit(t, belong, t, v);
			if (unit != null)
				return unit;
			
			if (allowTryExpand)
			{
				try
				{
					NI ni = new NI(null, true);
					ni.ThisName = str;
					string vv = ni.ThisName;
					UnitBase uv = GetConstantByCode(vv, belong, false);
					return uv;
				}
				catch
				{
					//
				}
			}
			
			throw new ArgumentException(str + " is not a valid constant unit");
		}
		#endregion
	}
}