using System;
using System.Runtime.InteropServices;

namespace Shooting
{
	// Token: 0x0200014D RID: 333
	public class APIClass
	{
		// Token: 0x06000898 RID: 2200
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(string lpszLongPath, string shortFile, int cchBuffer);

		// Token: 0x06000899 RID: 2201
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
	}
}
