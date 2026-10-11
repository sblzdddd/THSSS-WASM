using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Shooting
{
	// Token: 0x02000170 RID: 368
	public class INI_RW
	{
		// Token: 0x060008FE RID: 2302
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x060008FF RID: 2303
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		// Token: 0x06000900 RID: 2304 RVA: 0x000566F8 File Offset: 0x000548F8
		public INI_RW(string INIPath)
		{
			this.inipath = INIPath;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0005670A File Offset: 0x0005490A
		public void IniWriteValue(string Section, string Key, string Value)
		{
			INI_RW.WritePrivateProfileString(Section, Key, Value, this.inipath);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0005671C File Offset: 0x0005491C
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			int privateProfileString = INI_RW.GetPrivateProfileString(Section, Key, "", stringBuilder, 500, this.inipath);
			return stringBuilder.ToString();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00056758 File Offset: 0x00054958
		public bool ExistINIFile()
		{
			return File.Exists(this.inipath);
		}

		// Token: 0x04000356 RID: 854
		public string inipath;
	}
}
