using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C8 RID: 200
	public interface IModel
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600058A RID: 1418
		// (set) Token: 0x0600058B RID: 1419
		Color ColorValue { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600058C RID: 1420
		// (set) Token: 0x0600058D RID: 1421
		Device DeviceMain { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600058E RID: 1422
		// (set) Token: 0x0600058F RID: 1423
		TextureObject TxtureObject { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000590 RID: 1424
		// (set) Token: 0x06000591 RID: 1425
		int TransparentValue { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000592 RID: 1426
		// (set) Token: 0x06000593 RID: 1427
		string Name { get; set; }

		// Token: 0x06000594 RID: 1428
		void Draw();

		// Token: 0x06000595 RID: 1429
		void SetModel();

		// Token: 0x06000596 RID: 1430
		void SetupMaterial();

		// Token: 0x06000597 RID: 1431
		void SetupVertex();

		// Token: 0x06000598 RID: 1432
		void SetVertexFormat();

		// Token: 0x06000599 RID: 1433
		void Dispose();
	}
}
