namespace DotnetVisualScriptNPlusPlus.Variables
{
	public class Variable_Bool : VariableBase
	{
		public bool Value { get; set; } = false;
		public override object GetValue() => Value;
		public override void SetValue(object value) => Value = (bool)value;
	}
}