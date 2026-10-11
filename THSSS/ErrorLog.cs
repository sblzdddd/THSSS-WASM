using System;
using System.IO;

namespace Shooting
{
	// Token: 0x02000035 RID: 53
	internal class ErrorLog
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public static void SaveErrorLog(string Message, string Description)
		{
			string text = null;
			text += DateTime.Now.ToString("yyyy'/'MM'/'dd HH:mm");
			text += "\r\n";
			text += Message;
			text += "\r\n";
			text += Description;
			text += "\r\n";
			string text2 = "Log.txt";
			StreamWriter streamWriter;
			if (!File.Exists(text2))
			{
				streamWriter = File.CreateText(text2);
			}
			else
			{
				streamWriter = new StreamWriter(text2, true);
			}
			streamWriter.Write(text);
			streamWriter.Close();
		}
	}
}
