using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Shooting.Properties
{
	// Token: 0x0200019A RID: 410
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0005CA84 File Offset: 0x0005AC84
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x040003AF RID: 943
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
