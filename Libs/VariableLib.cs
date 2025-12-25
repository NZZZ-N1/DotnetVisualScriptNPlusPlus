using System;
using System.Collections.Generic;
using System.Linq;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Libs
{
	public static class VariableLib
	{
		static readonly Dictionary<string, VariableBase> NDic = new Dictionary<string, VariableBase>();

        public static bool IsInvolved(VariableBase v) => NDic.ContainsValue(v);
        public static bool IsInvolved(string n) => NDic.ContainsKey(n);

        public static void RemoveInvaildVariable()
        {
            foreach (var i in NDic.Keys)
            {
                if (NDic[i] == null)
                    NDic.Remove(i);
            }
        }

        public static void AddVariable(VariableBase v, string name)
        {
            if (v == null || name == null)
                throw new ArgumentException("Value cannot be null or empty");
            if (NDic.ContainsKey(name) || NDic.ContainsValue(v))
                throw new ArgumentException("Target has been added");
            NDic.Add(name, v);
        }
        public static bool AddVariableUnsafely(VariableBase v, string name)
        {
            try
            {
                AddVariable(v, name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void RemoveVariable(string name) => NDic.Remove(name);
        public static bool RemoveVariableUnsafely(string name)
        {
            try
            {
                RemoveVariable(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void RemoveVariable(VariableBase v)
        {
            foreach (var i in NDic.Keys)
            {
                if (NDic[i] == v)
                {
                    NDic.Remove(i);
                    return;
                }
            }
        }
        public static bool RemoveVariableUnsafely(VariableBase v)
        {
            try
            {
                RemoveVariable(v);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string[] GetVariableNameArr() => NDic.Keys.ToArray();

        public static VariableBase GetVariable(string name)
        {
            try
            {
                return NDic[name];
            }
            catch
            {
                return null;
            }
        }
	}
}