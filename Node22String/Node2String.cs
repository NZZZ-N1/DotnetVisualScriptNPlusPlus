using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public static class Node2String
	{
		public static string GenerateNeventCode(Nevent ev, string evName)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("name:" + evName + ";");
			sb.AppendLine("Start" + ";");
			
			foreach (var node in ev.Chain.GetNodeArray())
				AddNode(ref sb, node, 0);
			return sb.ToString();
		}
		
		static void AddNode(ref StringBuilder sb, NodeBase node, int num = 0)
		{
			string str = Space(num);
			str += NCodeReflection.Type2Code(node.GetType().FullName);
			
			if (node is IVariableRequire ivr)
			{
				str += "(";
				foreach (UnitBase u in ivr.GetPuller().units)
				{
					if (u == null)
						throw new NullReferenceException(ivr.GetType().Name);
					str += Unit2Code(u);
				}
				str += ")";
			}
			str += ";";
			sb.AppendLine(str);
			
			if (node is IBranch branch)
			{
				sb.AppendLine(Space(num) + "{");
				foreach (var n in branch.GetBranchChain().GetNodeArray())
					AddNode(ref sb, n, num + 1);
				sb.AppendLine(Space(num) + "}");
			}
		}
		static string Unit2Code(UnitBase unit)
		{
			if (unit == null)
				throw new ArgumentException("unit cant be null");
			
			string v = "";
			if (unit is IVariableRequire ivr)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(NCodeReflection.Type2Code(unit.GetType().FullName));
				sb.Append("(");
				foreach (var u in ivr.GetPuller().units)
				{
					if (u == null)
						throw new NullReferenceException();
					sb.Append(Unit2Code(u));
				}
				sb.Append(")");
				v += sb.ToString();
			}
			else if (unit is Unit_ConstantBase constant)
			{
				string str = "";
				str += NCodeReflection.Type2Code(unit.VariableBaseTypeReturned().FullName);
				str += ":";
				
				//Value add
				str += unit.OperateAndGetNewVariable().ToString();
				
				LinkedList<char> list = new LinkedList<char>();
				for (int i = 0; i < str.Length; i++)
				{
					char c = str[i];
					if (c == '[' || c == ']')
						list.AddLast('@');
					list.AddLast(c);
				}
				str = "[" + new string(list.ToArray()) + "]";
				
				v += str;
			}
			else
			{
				string str = "";
				str += NCodeReflection.Type2Code(unit.GetType().FullName);
				str += "()";
				
				v += str;
			}
			return v;
		}
		static string Space(int num)
		{
			string str = "";
			for (int i = 0; i < num; i++)
				str += "    ";
			return str;
		}
	}
}