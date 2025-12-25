using System;
using Avalonia.Controls;
using DVSNPP_Demo.NppControlRender;

namespace DVSNPP_Demo.Views;

public partial class MainView
{
	public void OnNodeGetterSortChanged(object? sender, SelectionChangedEventArgs e)
	{
		Renderer.RenderNodeGetter();
	}
}