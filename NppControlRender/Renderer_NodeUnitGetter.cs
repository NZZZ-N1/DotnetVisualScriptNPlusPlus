using System;
using Avalonia.Controls;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo.NppControlRender;

public partial class Renderer
{
	public static void RenderNodeGetter()
	{
		MainView v = MainView.Instance;
		(ListBoxItem, StackPanel)[] arr = 
		[
			(v.NodeGetterItem_Standard, v.NodeGetterPanel_Standard),
			(v.NodeGetterItem_Variable, v.NodeGetterPanel_Variable),
			(v.UnitGetterItem_Standard, v.UnitGetterPanel_Standard),
			(v.UnitGetterItem_Compare, v.UnitGetterPanel_Compare),
			(v.UnitGetterItem_Calculate, v.UnitGetterPanel_Calculate),
		];
		
		ListBoxItem? lbi = v.NodeGetterSortListBox.SelectedItem as ListBoxItem;
		if (lbi == null)
		{
			Console.WriteLine("Null ListBoxItem for chosen node getter");
			return;
		}
		
		foreach (var (key, target) in arr)
		{
			bool b = key == lbi;
			target.IsVisible = b;
		}
	}
}