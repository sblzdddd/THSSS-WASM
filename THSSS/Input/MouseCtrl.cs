using System;
using System.Runtime.InteropServices;

namespace Shooting
{
	// Token: 0x0200018D RID: 397
	public class MouseCtrl
	{
		// Token: 0x06000986 RID: 2438
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int ShowCursor(bool bShow);

		// Token: 0x06000987 RID: 2439 RVA: 0x0005B698 File Offset: 0x00059898
		public static void DrawCursor(bool cursorShow)
		{
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
		}
	}
}
