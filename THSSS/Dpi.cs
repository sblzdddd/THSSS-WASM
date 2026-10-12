using System.Management;

namespace Shooting
{
	public class Dpi
	{
		public static void GetDpi()
		{
			using (ManagementClass managementClass = new ManagementClass("Win32_DesktopMonitor"))
			{
				using (ManagementObjectCollection instances = managementClass.GetInstances())
				{
					PixelsPerXLogicalInch = 0;
					PixelsPerYLogicalInch = 0;
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						PixelsPerXLogicalInch = int.Parse(managementObject.Properties["PixelsPerXLogicalInch"].Value.ToString());
						PixelsPerYLogicalInch = int.Parse(managementObject.Properties["PixelsPerYLogicalInch"].Value.ToString());
					}
				}
			}
		}

		public static int PixelsPerXLogicalInch;

		public static int PixelsPerYLogicalInch;
	}
}
