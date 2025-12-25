using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using DotnetVisualScriptNPlusPlus.TargetInterfaces;
using DotnetVisualScriptNPlusPlus.Units;
using DotnetVisualScriptNPlusPlus.Units.NumsOperate;
using DVSNPP_Demo.Extensions;
using DVSNPP_Demo.ViewModels;

namespace DVSNPP_Demo.NppControlRender;

public static partial class Renderer
{
	static StackPanel RON_AddPanel(StackPanel vp, UnitBase? unit, bool ic)
	{
		StackPanel panel = new StackPanel()
		{
			Tag = unit,
			Orientation = Orientation.Horizontal,
			MinHeight = 35,
			Background = !ic ? Brushes.Gray : Brushes.DimGray,
		};
		vp.Children.Add(panel);
		
		if (unit != null)
		{
			Button btn = new Button()
			{
				Content = "x",
				FontSize = 5,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Left,
				Foreground = Brushes.White,
				Background = Brushes.Gray,
			};
			btn.Click += (sender, args) =>
			{//移除指定Unit
				IVariableRequire ivr = unit.RUnit;
				int index = ivr.FindIndex(unit) ?? throw new AggregateException("Out of IVR");
				ivr.GetPuller().units[index] = null;
				RenderNppNode();
			};
			panel.Children.Add(btn);
		}
		
		return panel;
	}
	
	static void RON_RenderOneUnit(StackPanel vp,IVariableRequire ivrParent, UnitBase? unit, int unitIndex, bool inverseColor = false)
	{
		const string kSpace = " ";
		vp.Children.Add(new Label() { Content = kSpace });
		
		if (unit == null)
		{
			bool setSelected()
			{
				(IVariableRequire?, int)? info = MainViewModel.SelectedUnitIndex;
				if (info == null)
					return false;
				IVariableRequire? ivrSelected = info.Value.Item1;
				int indexSelected = info.Value.Item2!;
				if (ivrSelected == null)
					return false;
				return ivrParent == ivrSelected && unitIndex == indexSelected;
			}
			bool selected = setSelected();
			
			Button btn = new Button()
			{
				Content = selected ? "Selected" : "Select",
				Foreground = Brushes.White,
				Background = Brushes.Gray, 
				FontSize = 10,
			};
			btn.Click += (sender, args) => MainViewModel.Click_SelectTargetUnit(ivrParent ,unitIndex);
			vp.Children.Add(btn);
		}
		else
		{//unit != null
			string? text = null;
			
			if (unit is Unit_ConstantBase || unit is Unit_2NumsCompareBase)
				text = null;
			else
				text = unit.GetType().Name;
			
			if(!string.IsNullOrWhiteSpace(text))
				vp.Children.Add(new Label()
				{
					Content = text,
					Foreground = Brushes.White,
					FontSize = 12,
				});
			
			if (unit is IVariableRequire ivr)
			{
				string?[] texts = ivr.GetTextIdentifier();
				UnitBase?[] units = ivr.GetPuller().units;
				int uIndex = 0;
				foreach (var it in texts)
				{
					if (it != null)
					{
						Label label = new Label()
						{
							Content = it,
							VerticalAlignment = VerticalAlignment.Center, 
							HorizontalAlignment = HorizontalAlignment.Left,
							FontSize = 17,
						};
						vp.Children.Add(label);
					}
					else
					{
						UnitBase? u = units[uIndex];
						uIndex += 1;
						StackPanel panel = RON_AddPanel(vp, u, inverseColor);
						RON_RenderOneUnit(panel, ivr, u, uIndex, !inverseColor);
						if (uIndex < units.Length - 1) 
							vp.Children.Add(new Label() { Content = "  " });
					}
				}
			}
			
			if (unit is Unit_ConstantBase constant)
			{
				vp.Children.Add(new Label()
				{
					Content = constant.GetDefaultValue().GetType().Name,
					Foreground = Brushes.White,
					FontSize = 8
				});
				
				if (constant is Unit_Constant_Bool ub)
				{//bool
					ToggleSwitch toggle = new ToggleSwitch()
					{
						VerticalAlignment = VerticalAlignment.Center,
						HorizontalAlignment = HorizontalAlignment.Left,
					};
					toggle.IsChecked = ub.Variable.Value;
					vp.Children.Add(toggle);
					
					toggle.IsCheckedChanged += (sender, args) =>
					{
						bool b = toggle.IsChecked ?? false;
						ub.Variable.Value = b;
					};
				}
				else
				{//else
					TextBox tb = new TextBox() { };
					vp.Children.Add(tb);
					tb.Text = constant.GetValue().ToString();
					
					tb.TextChanged += (sender, args) =>
					{//常量输入
						string? t = tb.Text;
						if (string.IsNullOrEmpty(t))
							return;
						Console.WriteLine("Constant value text changed:" + t);
						try
						{
							constant.SetValueByString(t);
						}
						catch (Exception ex)
						{
							string txt = constant.SetDefaultValue().ToString() ?? "";
							tb.Text = txt;
							Console.WriteLine("Unit TextBox Cast Error:" + ex.GetType().Name);
						}
					};
				}
			}

		}
		
		vp.Children.Add(new Label() { Content = kSpace });
	}
}