using System;
using System.IO;

namespace Shooting
{
	internal class ErrorLog
	{
		public static void SaveErrorLog(string Message, string Description)
		{
			string text = null;
			text += DateTime.Now.ToString("yyyy'/'MM'/'dd HH:mm");
			text += "\r\n";
			text += Message;
			text += "\r\n";
			text += Description;
			text += "\r\n";
			var text2 = "Log.txt";
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
