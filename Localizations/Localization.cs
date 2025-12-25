using System;
using System.Collections.Generic;

namespace EmbededLocalization
{
	public delegate void OnLocalizationReloadedAction();
	
	public static class Localization
	{
		public static TxtBase Language { get; private set; } = null!;
		private static Dictionary<string, string> TDic = new Dictionary<string, string>();
		
		public static event OnLocalizationReloadedAction? OnLocalizationReloaded;
		public static void SetLanguage(TxtBase txt)
		{
			if (txt == null)
				throw new ArgumentNullException(nameof(txt));
			Language = txt;
			InitializeTxtDic();
			if (OnLocalizationReloaded != null)
				OnLocalizationReloaded();
		}
		static void InitializeTxtDic()
		{
			TDic.Clear();
			foreach (var (key, v) in Language.GetLocalizationTxts())
				TDic.Add(key, v);
		}
		
		public static string GetTxt(string key, bool logWhenMissing = true)
		{
			if (TDic.TryGetValue(key, out var v))
				return v;
			if (logWhenMissing)
				Console.WriteLine("Missing key:" + key);
			return key;
		}
	}
	
	public static class LocalizationEx
	{
		public static string Localized(this string str, bool logWhenMissing = true) => Localization.GetTxt(str, logWhenMissing);
	}
}