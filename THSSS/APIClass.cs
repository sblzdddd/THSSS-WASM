using System.Runtime.InteropServices;

namespace Shooting
{
	public class APIClass
	{
#if BROWSER
		public static int GetShortPathName(string lpszLongPath, string shortFile, int cchBuffer)
		{
			return 0;
		}

		public static int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback)
		{
			return 0;
		}
#else
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(string lpszLongPath, string shortFile, int cchBuffer);

		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
#endif
	}
}
