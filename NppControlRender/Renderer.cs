using DotnetVisualScriptNPlusPlus;
using DotnetVisualScriptNPlusPlus.Libs;
using DVSNPP_Demo.RunningNodes;

namespace DVSNPP_Demo.NppControlRender;

public static partial class Renderer
{
	public static void RenderAll()
	{
		RenderWindowDisplay();
		RenderNppEventListBox();
		RenderNppNode();
		RenderNodeGetter();
		RunnerUI.UpdateUI();
	}
}