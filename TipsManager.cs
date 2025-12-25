using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using DVSNPP_Demo.Views;

namespace DVSNPP_Demo;

public static class TipsManager
{
	private static readonly Lock _lock = new Lock();
	public static void Initialize()
	{
		_ = Task.Run(async () => await UpdateTipsAsync());
	}
	
	static List<(string, float)> SList = new();
	
	static async Task UpdateTipsAsync()
	{
		for (;;)
		{
			UpdateTips();
			await RenderTipAsync();
			await Task.Delay(100);
		}
	}
	static void UpdateTips()
	{
		lock (_lock)
		{
			for (int i = SList.Count - 1; i >= 0; i--)
			{
				var (_text, _time) = SList[i];
				float time = _time;
				
				time -= 0.1f;
				if (time <= 0)
				{
					SList.RemoveAt(i);
					continue;
				}
				SList[i] = (_text, time);
			}
		}
	}
	static async Task RenderTipAsync()
	{
		await Dispatcher.UIThread.InvokeAsync(() =>
		{
			lock (_lock)
			{
				RenderTip();
			}
		});
	}
	static void RenderTip()
	{
		StackPanel parent = MainView.Instance.Panel_Tips;
		parent.Children.Clear();
		List<(string text, float time)> list = new(SList);
		
		foreach (var (text, time) in list)
		{
			StackPanel p = new StackPanel();
			Label label = new Label();
			label.Content = text;
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.HorizontalContentAlignment = HorizontalAlignment.Center;
			label.VerticalAlignment = VerticalAlignment.Center;
			label.VerticalContentAlignment = VerticalAlignment.Center;
			p.Background = Brushes.DimGray;
			p.Opacity = 0.4;
			p.MaxWidth = 400;
			p.Margin = new Thickness(0, 0, 0, 10);
			
			p.Children.Add(label);
			parent.Children.Add(p);
		}
	}
	
	public static void AddTip(string text, float time = 3)
	{
		lock (_lock)
		{
			SList.Add((text, time));
			RenderTip();
		}
	}
}