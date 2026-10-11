using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000112 RID: 274
	internal class ParticleWater : BaseEffect
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00043B5C File Offset: 0x00041D5C
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00043B73 File Offset: 0x00041D73
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x06000773 RID: 1907 RVA: 0x00043B7C File Offset: 0x00041D7C
		public ParticleWater(StageDataPackage StageData, PointF p, float v, double drctn)
			: base(StageData, null, p, v, drctn)
		{
			this.TxtureObjects = new TextureObject[]
			{
				base.TextureObjectDictionary["588-1"],
				base.TextureObjectDictionary["589-1"],
				base.TextureObjectDictionary["590-1"],
				base.TextureObjectDictionary["591-1"],
				base.TextureObjectDictionary["592-1"],
				base.TextureObjectDictionary["593-1"]
			};
			base.LifeTime = 17;
			base.Ay = 0.6f;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00043C2E File Offset: 0x00041E2E
		public override void Ctrl()
		{
			base.Ctrl();
			base.TxtureObject = this.TxtureObjects[base.Time % 18 / 3];
		}
	}
}
