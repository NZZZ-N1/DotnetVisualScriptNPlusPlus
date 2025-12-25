using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DVSNPP_Demo.Localizations;
using DVSNPP_Demo.ViewModels;
using DVSNPP_Demo.Views;
using EasySaving;
using EmbededLocalization;

namespace DVSNPP_Demo;

public partial class App : Application
{
	public override void Initialize()
	{
		new SavingInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Savings"), "sav", true);
		Localization.SetLanguage(new Localization_En());
		AvaloniaXamlLoader.Load(this);
	}
	
	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow { DataContext = new MainViewModel() };
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			singleViewPlatform.MainView = new MainView { DataContext = new MainViewModel() };
		}
		
		base.OnFrameworkInitializationCompleted();
	}
}