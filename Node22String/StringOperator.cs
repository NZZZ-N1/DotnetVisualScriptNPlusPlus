using System;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public static class StringOperator
	{
		public static string ContentBefore(string str, string target)
		{
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(target))
				return str;
			
			int index = str.IndexOf(target, StringComparison.Ordinal);
			if (index == -1)
				return str;
			
			return str.Substring(0, index);
		}
		
		public static string ContentAfter(string str, string target)
		{
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(target))
				return str;
			
			int index = str.IndexOf(target, StringComparison.Ordinal);
			if (index == -1)
				return str;
			
			return str.Substring(index + target.Length);
		}
		
		public static string[] Separate(string str, string sep)
		{
			if (string.IsNullOrEmpty(str))
				return new string[0];
			if (string.IsNullOrEmpty(sep))
				return new string[] { str };
			return str.Split(new string[] { sep }, StringSplitOptions.None);
		}
	}
}