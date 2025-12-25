using DVSNPP_Demo.Views;
using EmbededLocalization;

namespace DVSNPP_Demo.ViewModels;

public partial class MainViewModel
{
	public string Text_AddNodeUnderBranch =>  "add_under_branch".Localized() + ":" + (MainView.Instance.AddNodeUnderBranch ? "True" : "False");
}