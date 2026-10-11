using System;
using System.Windows.Forms;

namespace Config
{
	// Token: 0x02000006 RID: 6
	internal static class Program
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00004B9D File Offset: 0x00002D9D
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form_Config());
		}
	}
}
