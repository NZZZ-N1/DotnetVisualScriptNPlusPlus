using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetVisualScriptNPlusPlus.Node22String
{
	public sealed class NI
	{
		public string ThisName = "";
		public string ReflectedTypeFullName
		{
			get
			{
				if (ThisName[0] == '[')
				{
					string str = ThisName;
					List<char> list = new List<char>(str);
					list.RemoveAt(0);
					list.RemoveAt(list.Count - 1);
					str = new string(list.ToArray());
					string be = StringOperator.ContentAfter(str, ":");
					str = StringOperator.ContentBefore(str, ":");
					str = NCodeReflection.Code2TypeFullName(str);
					str = "[" + str + ":" + be + "]";
					return str;
				}
				else
				{
					return NCodeReflection.Code2TypeFullName(ThisName);
				}
			}
		}
		
		[Obsolete("DO NOT use this method,this is for expand the short word but there is not case user need to use it")]
		public void ExpandIfIsConstantPara()
		{
			if (!IsPara)
				throw new ArgumentException("It is not a parameter");
			if (ThisName[0] != '[' || ThisName[ThisName.Length - 1] != ']')
				throw new ArgumentException("It is not a legal parameter");
			
			string content;
			{
				LinkedList<char> l = new LinkedList<char>(ThisName);
				l.RemoveLast();
				l.RemoveFirst();
				content = l.ToString();
			}
			
			bool is_originallyOK = new LinkedList<char>(content.ToCharArray()).Contains(':');
			bool is_string = content[0] == '"' && content[content.Length - 1] == '"';
			
			if (!is_string && is_originallyOK)//已经为完整表达式，无需扩展
				return;
			
			//string
			{
				if (content[0] == '"' && content[content.Length - 1] == '"')
				{
					LinkedList<char> l = new LinkedList<char>(content);
					l.RemoveLast();
					l.RemoveLast();
					ThisName = "[string:" + new string(l.ToArray()) + "]";
					return;
				}
			}
			
			//num
			{
				double? vn = null;
				try
				{
					vn = double.Parse(content);
					ThisName = "[num:" + vn.Value + ']';
					return;
				}
				catch
				{
					// ignored
				}
			}
			
			//bool
			{
				bool? vb = null;
				if (content.ToLower() == "false")
					vb = false;
				else if (content.ToLower() == "true")
					vb = true;
				if (vb is bool b)
				{
					ThisName = "[bool:" + b.ToString() + ']';
					return;
				}
			}
		}
		
		public NI[] Childs;
		public NI[] Paras;
		public NI Parent;
		
		public bool IsPara = false;
		
		public NI(NI parent, bool isPara)
		{
			Parent = parent;
			IsPara = isPara;
		}
		public NI Clone()
		{
			NI ni = new NI(this.Parent, this.IsPara);
			ni.Childs = Childs.Clone() as NI[];
			ni.Paras = Paras.Clone() as NI[];
			return ni;
		}
	}
}