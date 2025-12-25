namespace DotnetVisualScriptNPlusPlus.Variables
{
    public class Variable_Num : VariableBase
    {
        public double Value { get; set; } = 0;
        public override object GetValue() => Value;
        public override void SetValue(object value) => Value = (double)value;
    }
}