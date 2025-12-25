using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetVisualScriptNPlusPlus.Libs
{
    public static class NeventLib
    {
        static readonly Dictionary<string, Nevent> NDic = new Dictionary<string, Nevent>();

        public static bool IsInvolved(Nevent v) => NDic.ContainsValue(v);
        public static bool IsInvolved(string n) => NDic.ContainsKey(n);

        public static void RemoveInvaildEvent()
        {
            foreach (var i in NDic.Keys)
            {
                if (NDic[i] == null)
                    NDic.Remove(i);
            }
        }

        public static void AddEvent(Nevent v, string name)
        {
            if (v == null || name == null)
                throw new ArgumentException("Value cannot be null or empty");
            if (NDic.ContainsKey(name) || NDic.ContainsValue(v))
                throw new ArgumentException("Target has been added");
            NDic.Add(name, v);
        }
        public static bool AddEventUnsafely(Nevent v, string name)
        {
            try
            {
                AddEvent(v, name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void RemoveEvent(string name) => NDic.Remove(name);
        public static bool RemoveEventUnsafely(string name)
        {
            try
            {
                RemoveEvent(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void RemoveEvent(Nevent v)
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
        public static bool RemoveEventUnsafely(Nevent v)
        {
            try
            {
                RemoveEvent(v);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string[] GetEventNameArr() => NDic.Keys.ToArray();

        public static Nevent GetEvent(string name)
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
        
        public static void RenameNevent(string originalName, string targetName)
        {
            if (!NDic.ContainsKey(originalName))
                throw new MissingMemberException("There is no nevent named " + originalName);
            if (NDic.ContainsKey(targetName))
                throw new ArgumentException("Target name has existed");
            Nevent ev = NDic[originalName];
            RemoveEvent(ev);
            AddEvent(ev, targetName);
        }
    }
}