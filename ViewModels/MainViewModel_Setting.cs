using System;
using Avalonia.Controls;
using DVSNPP_Demo.Localizations;
using DVSNPP_Demo.NppControlRender;
using DVSNPP_Demo.Views;
using DynamicData;
using EasySaving;
using EmbededLocalization;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	private const string kSettingFile_Language = "language";
	private const string kSettingKey_Language = "lgg";
	
	public void InitializeSetting()
	{
		Initialize_Language();
	}
	
	#region SettingPanelDisplay
	
	private bool _SettingPanelDisplay = false;
	public bool SettingPanelDisplay
	{
		get => _SettingPanelDisplay;
		set
		{
			_SettingPanelDisplay = value;
			OnPropertyChanged();
		}
	}
	
	public void Command_CloseSetting()
	{
		SettingPanelDisplay = false;
	}
	
	#endregion
	#region Language
	
	void Initialize_Language()
	{
		ComboBox cb = MainView.Instance.ComboBox_Language;
		
		void f(Type t)
		{
			ComboBoxItem item = new ComboBoxItem() { Content = t.Name, Tag = t};
			cb.Items.Add(item);
		}
		
		f(typeof(Localization_En));
		f(typeof(Localization_Cn));
		
		SavingPack p = DataSaving.Load(kSettingFile_Language, true);
		if (p != null)
		{
			string id = p.TryGetValue(kSettingKey_Language, "En");
			if(id == "En")
				cb.SelectedIndex = 0;
			else if (id == "Cn")
				cb.SelectedIndex = 1;
			else
				throw new MissingKeyException("Localization key missed");
		}
		else
		{
			cb.SelectedIndex = 0;
		}
	}
	
	public void Clicked_LanguageSelectedChanged(object? sender, SelectionChangedEventArgs e)
	{
		Type? type = (MainView.Instance.ComboBox_Language.SelectedItem as ComboBoxItem)?.Tag as Type;
		if (type == null)
			return;
		TxtBase txt = (Activator.CreateInstance(type) as TxtBase)!;
		Localization.SetLanguage(txt);
		UpdateTxt();
		Renderer.RenderAll();
		
		SavingPack p = new SavingPack();
		p.Add(kSettingKey_Language, txt.LanguageID());
		DataSaving.Save(p, kSettingFile_Language);
	}
	
	#endregion
}