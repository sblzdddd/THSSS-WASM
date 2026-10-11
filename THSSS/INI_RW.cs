using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Shooting
{
	public class INI_RW
	{
#if !BROWSER
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
#endif

		public INI_RW(string INIPath)
		{
			this.inipath = INIPath;
		}

		public void IniWriteValue(string Section, string Key, string Value)
		{
#if BROWSER
			INI_RW.WriteManaged(this.inipath, Section, Key, Value);
#else
			INI_RW.WritePrivateProfileString(Section, Key, Value, this.inipath);
#endif
		}

		public string IniReadValue(string Section, string Key)
		{
#if BROWSER
			return INI_RW.ReadManaged(this.inipath, Section, Key);
#else
			StringBuilder stringBuilder = new StringBuilder(500);
			int privateProfileString = INI_RW.GetPrivateProfileString(Section, Key, "", stringBuilder, 500, this.inipath);
			return stringBuilder.ToString();
#endif
		}

		public bool ExistINIFile()
		{
			return File.Exists(this.inipath);
		}

		public string inipath;

#if BROWSER
		private static void WriteManaged(string path, string section, string key, string value)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (File.Exists(path))
			{
				list.AddRange(File.ReadAllLines(path));
			}
			string text = "[" + section + "]";
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < list.Count; i++)
			{
				string text2 = list[i].Trim();
				if (text2.StartsWith("[") && text2.EndsWith("]"))
				{
					if (num >= 0 && num2 < 0)
					{
						break;
					}
					num = string.Equals(text2, text, StringComparison.OrdinalIgnoreCase) ? i : -1;
				}
				else if (num >= 0 && text2.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase))
				{
					num2 = i;
					break;
				}
			}
			string text3 = key + "=" + value;
			if (num2 >= 0)
			{
				list[num2] = text3;
			}
			else if (num >= 0)
			{
				list.Insert(num + 1, text3);
			}
			else
			{
				if (list.Count > 0)
				{
					list.Add("");
				}
				list.Add(text);
				list.Add(text3);
			}
			string text4 = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(text4))
			{
				Directory.CreateDirectory(text4);
			}
			File.WriteAllLines(path, list.ToArray());
		}

		private static string ReadManaged(string path, string section, string key)
		{
			if (!File.Exists(path))
			{
				return "";
			}
			string[] array = File.ReadAllLines(path);
			string text = "[" + section + "]";
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim();
				if (text2.StartsWith("[") && text2.EndsWith("]"))
				{
					flag = string.Equals(text2, text, StringComparison.OrdinalIgnoreCase);
				}
				else if (flag)
				{
					int num = text2.IndexOf('=');
					if (num > 0 && string.Equals(text2.Substring(0, num).Trim(), key, StringComparison.OrdinalIgnoreCase))
					{
						return text2.Substring(num + 1).Trim();
					}
				}
			}
			return "";
		}
#endif
	}
}
