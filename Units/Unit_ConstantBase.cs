using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public abstract class Unit_ConstantBase : UnitBase
	{
		protected Unit_ConstantBase(IVariableRequire belong) : base(belong)
		{
			
		}
		
		public abstract object GetValue();
		
		public abstract void SetValue(object v);
		public bool SetValueUnsafely(object v)
		{
			try
			{
				SetValue(v);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		public abstract void SetValueByString(string v);
		public bool SetValueByStringUnsafely(string v)
		{
			try
			{
				SetValueByString(v);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		public abstract object GetDefaultValue();
		public object SetDefaultValue()
		{
			object v = GetDefaultValue();
			SetValue(v);
			return v;
		}
	}
}