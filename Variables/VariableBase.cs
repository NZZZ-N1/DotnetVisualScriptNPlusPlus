using System;

namespace DotnetVisualScriptNPlusPlus.Variables
{
    public abstract class VariableBase
    {
        public abstract object GetValue();
        public virtual Type GetValueType() => GetValue().GetType();
        public override string ToString() => GetValue().ToString();
        
        public abstract void SetValue(object value);
        
        public VariableBase()
        {
            
        }
    }
}