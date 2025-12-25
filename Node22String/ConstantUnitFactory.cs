using System;
using System.Collections.Generic;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Variables;
using FG = System.Func<
	DotnetVisualScriptNPlusPlus.TargetInterfaces.IVariableRequire,
	string,
	string,
	DotnetVisualScriptNPlusPlus.Units.Unit_ConstantBase>;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public static class ConstantUnitFactory
	{
		static ConstantUnitFactory()
		{
			AddGeneratorFunc(typeof(Variable_Num).FullName, (belong, variableTypeText, valueText) =>
			{
				Unit_Constant_Num u = new Unit_Constant_Num(belong);
				u.Variable = CreateNewVariable<Variable_Num>(variableTypeText);
				u.Variable.Value = double.Parse(valueText);
				return u;
			});
			AddGeneratorFunc(typeof(Variable_Bool).FullName, (belong, variableTypeText, valueText) =>
			{
				Unit_Constant_Bool u = new Unit_Constant_Bool(belong);
				u.Variable = CreateNewVariable<Variable_Bool>(variableTypeText);
				
				bool pv;
				if (valueText == "0" || valueText.ToLower() == "false")
					pv = false;
				else
					pv = true;
				u.Variable.Value = pv;
				
				return u;
			});
			AddGeneratorFunc(typeof(Variable_String).FullName, (belong, variableTypeText, valueText) =>
			{
				Unit_Constant_String u = new Unit_Constant_String(belong);
				u.Variable = CreateNewVariable<Variable_String>(variableTypeText);
				u.Variable.Value = valueText;
				return u;
			});
		}
		
		private static Dictionary<string, FG> FDic = new Dictionary<string, FG>();
		public static void AddGeneratorFunc(string variableType, FG func)
		{
			FDic.Add(variableType, func);
		}
		static TVariable CreateNewVariable<TVariable>(string typeName)
		{
			object instance = String2Node.CreateInstance(typeName, null);
			TVariable rv = (TVariable)instance;
			return rv;
		}
		
		public static Unit_ConstantBase GenerateConstantUnit(string unitTypeText, 
			IVariableRequire belong,
			string variableTypeText,
			string valueText
			)
		{
			if (!FDic.TryGetValue(unitTypeText, out var f))
				throw new MissingMemberException("There is no constant unit named " + unitTypeText);
			Unit_ConstantBase unit = f(belong, variableTypeText, valueText);
			return unit;
		}
	}
}