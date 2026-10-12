using System.Runtime.InteropServices;

namespace Shooting
{
	public class MouseCtrl
	{
#if !BROWSER
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int ShowCursor(bool bShow);
#endif

		public static void DrawCursor(bool cursorShow)
		{
#if !BROWSER
			if (cursorShow)
			{
				while (MouseCtrl.ShowCursor(true) < 0)
				{
				}
			}
			else
			{
				while (MouseCtrl.ShowCursor(false) >= 0)
				{
				}
			}
#endif
		}
	}
}
