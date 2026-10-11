using System;
using System.Management;

namespace Shooting
{
	// Token: 0x020001BC RID: 444
	public class Dpi
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x000636B0 File Offset: 0x000618B0
		public static void GetDpi()
		{
			using (ManagementClass managementClass = new ManagementClass("Win32_DesktopMonitor"))
			{
				using (ManagementObjectCollection instances = managementClass.GetInstances())
				{
					Dpi.PixelsPerXLogicalInch = 0;
					Dpi.PixelsPerYLogicalInch = 0;
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						Dpi.PixelsPerXLogicalInch = int.Parse(managementObject.Properties["PixelsPerXLogicalInch"].Value.ToString());
						Dpi.PixelsPerYLogicalInch = int.Parse(managementObject.Properties["PixelsPerYLogicalInch"].Value.ToString());
					}
				}
			}
		}

		// Token: 0x040003DD RID: 989
		public static int PixelsPerXLogicalInch;

		// Token: 0x040003DE RID: 990
		public static int PixelsPerYLogicalInch;
	}
}
