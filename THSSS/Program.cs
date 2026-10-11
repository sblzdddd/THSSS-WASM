using System;
using System.Threading;
using System.Windows.Forms;
using SlimDX.Windows;

namespace Shooting
{
	// Token: 0x0200009C RID: 156
	internal static class Program
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x00024C00 File Offset: 0x00022E00
		[STAThread]
		private static void Main()
		{
			bool flag;
			Mutex mutex = new Mutex(false, "AA", out flag);
			if (flag)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Game_Main game_Main = new Game_Main();
				if (game_Main.initSuccess)
				{
					MessagePump.Run(game_Main.Form_Main, new MainLoop(game_Main.MainProcess));
				}
			}
			else
			{
				MessageBox.Show("东方白丝祭(误)已启动，\r\n程序不允许双开。", "程序已启动");
			}
		}
	}
}
