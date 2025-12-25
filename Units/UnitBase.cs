using System;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Variables;

namespace DotnetVisualScriptNPlusPlus.Units
{
	public abstract class UnitBase
	{
        public UnitBase(IVariableRequire belong)
        {
            RUnit = belong;
            CheckVaildValue(true);
        }
        
		public IVariableRequire RUnit { get; private set; } = null;//Belong
        /// <summary>
        /// force to set the value of RUnit
        /// DO NOT use it unless you clearly know what you are doing
        /// This operation may cause serious problem
        /// </summary>
        public void ForceSetRUnit(IVariableRequire value)
        {
            RUnit = value;
        }
        
		public abstract VariableBase OperateAndGetNewVariable();
		public abstract Type VariableBaseTypeReturned();
		
		public void CheckVaildValue(bool allowEmptyPuller = false)
		{
			if (RUnit == null)
				throw new ArgumentException("The RUnit cannot be null");
			if (!allowEmptyPuller && RUnit.GetPuller() == null)
				throw new Exception();
			Type t = VariableBaseTypeReturned();
			try
			{
				if (t != null)
				{
					object v = Activator.CreateInstance(t);
					if (v == null || !(v is VariableBase))
						throw new Exception();
				}
			}
			catch (Exception e)
			{
				throw new ArgumentException("The type returned is not legal\n" + e.ToString());
			}
		}
	}
}