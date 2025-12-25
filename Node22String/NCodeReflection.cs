using System;
using System.Collections.Generic;
using DotnetVisualScriptNPlusPlus.Nodes;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Units.NumsOperate;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public static class NCodeReflection
	{
		static Dictionary<string ,string> Dic_Type2Code = new Dictionary<string ,string>();
		static Dictionary<string, string> Dic_Code2Type = new Dictionary<string, string>();
		
		static NCodeReflection()
		{
			//Add(typeof(T), "t");
			
			Add(typeof(Node_GenerateAndSetVariable), "vcreate");
			Add(typeof(Node_SetVariable), "vset");
			Add(typeof(Unit_GetVariable), "vget");
			
			Add(typeof(Node_If), "if");
			Add(typeof(Node_Else), "else");
			Add(typeof(Node_Call), "call");
			Add(typeof(Node_WhileLoop), "while");
			
			Add(typeof(Variable_String), "string");
			Add(typeof(Variable_Bool), "bool");
			Add(typeof(Variable_Num), "num");
			
			Add(typeof(Unit_2NumsCompare_Greater), ">");
			Add(typeof(Unit_2NumsCompare_GreaterAndEqual), ">=");
			Add(typeof(Unit_2NumsCompare_Less), "<");
			Add(typeof(Unit_2NumsCompare_LessAndEqual), "<=");
			Add(typeof(Unit_Equal), "==");
			Add(typeof(Unit_NotEqual), "!=");
			Add(typeof(Unit_BoolInversion), "!");
			
			Add(typeof(Unit_2NumsOperate_Add), "+");
			Add(typeof(Unit_2NumsOperate_Sub), "-");
			Add(typeof(Unit_2NumsOperate_Mul), "*");
			Add(typeof(Unit_2NumsOperate_Div), "/");
		}
		
		public static void Add(Type t, string str)
		{
			if (Dic_Type2Code.ContainsKey(str) || Dic_Code2Type.ContainsKey(str))
				throw new ArgumentException(str + " has existed");
			if (t == null)
				throw new ArgumentException("Can't be null");
			if (t.FullName == null)
				throw new ArgumentException("Failed to load the FullName");
			Dic_Type2Code.Add(t.FullName, str);
			Dic_Code2Type.Add(str, t.FullName);
		}
		
		public static string Code2TypeFullName(string str)
		{
			if (Dic_Type2Code.ContainsKey(str))
				return str;
			if (Dic_Code2Type.TryGetValue(str, out var v))
				return v;
			throw new MissingMemberException("Failed to find " + str);
		}
		public static string Type2Code(string typeFullName)
		{
			if (typeFullName == null)
				throw new ArgumentException("Can't be null");
			if (Dic_Type2Code.TryGetValue(typeFullName, out var v))
				return v;
			return typeFullName;
		}
	}
}