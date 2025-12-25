using System;
using System.Collections.Generic;

namespace EmbededLocalization
{
	public abstract class TxtBase
	{
		public abstract string LanguageID();
		
		public abstract (string, string)[] GetLocalizationTxts();
		
		protected (string, string) node<T>(string str) => (typeof(T).Name, str);
	}
}