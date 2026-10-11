using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Config
{
	// Token: 0x02000004 RID: 4
	public class INI_RW
	{
		// Token: 0x0600000F RID: 15
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		// Token: 0x06000011 RID: 17 RVA: 0x00004B11 File Offset: 0x00002D11
		public INI_RW(string INIPath)
		{
			this.inipath = INIPath;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00004B20 File Offset: 0x00002D20
		public void IniWriteValue(string Section, string Key, string Value)
		{
			INI_RW.WritePrivateProfileString(Section, Key, Value, this.inipath);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00004B34 File Offset: 0x00002D34
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			INI_RW.GetPrivateProfileString(Section, Key, "", stringBuilder, 500, this.inipath);
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00004B6B File Offset: 0x00002D6B
		public bool ExistINIFile()
		{
			return File.Exists(this.inipath);
		}

		// Token: 0x04000038 RID: 56
		public string inipath;
	}
}
