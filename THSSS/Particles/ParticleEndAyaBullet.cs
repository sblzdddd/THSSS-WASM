using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000153 RID: 339
	public class ParticleEndAyaBullet : BaseEffect
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00050CFC File Offset: 0x0004EEFC
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00050D13 File Offset: 0x0004EF13
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00050D1C File Offset: 0x0004EF1C
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00050D33 File Offset: 0x0004EF33
		public int IndexX { get; set; }

		// Token: 0x060008B6 RID: 2230 RVA: 0x00050D3C File Offset: 0x0004EF3C
		public ParticleEndAyaBullet(StageDataPackage StageData, string Name, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, null, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			this.TxtureObjects = new TextureObject[]
			{
				base.TextureObjectDictionary[Name + "00"],
				base.TextureObjectDictionary[Name + "01"],
				base.TextureObjectDictionary[Name + "02"],
				base.TextureObjectDictionary[Name + "03"]
			};
			base.TxtureObject = this.TxtureObjects[0];
			base.LifeTime = 15;
			base.Angle = 1.5707963705062866;
			base.MaxTransparent = 80;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00050E08 File Offset: 0x0004F008
		public override void Ctrl()
		{
			base.Ctrl();
			this.IndexX = base.Time % 16 / 4;
			base.TxtureObject = this.TxtureObjects[this.IndexX];
			base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			base.Scale += 0.1f;
		}
	}
}
