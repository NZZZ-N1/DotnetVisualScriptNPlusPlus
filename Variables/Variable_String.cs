namespace DotnetVisualScriptNPlusPlus.Variables
{
	public class Variable_String : VariableBase
	{
		public string Value { get; set; } = "";
		public override object GetValue() => Value;
		public override void SetValue(object value) => Value = (string)value;
	}
}