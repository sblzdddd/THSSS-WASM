using System.Management;

namespace Shooting
{
	public class Dpi
	{
		public static void GetDpi()
		{
			using (var managementClass = new ManagementClass("Win32_DesktopMonitor"))
			{
				using (var instances = managementClass.GetInstances())
				{
					PixelsPerXLogicalInch = 0;
					PixelsPerYLogicalInch = 0;
					foreach (var managementBaseObject in instances)
					{
						var managementObject = (ManagementObject)managementBaseObject;
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
