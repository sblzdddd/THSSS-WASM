using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E8 RID: 488
	public class ParticleEndBullet : BaseEffect
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0006E420 File Offset: 0x0006C620
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0006E437 File Offset: 0x0006C637
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0006E440 File Offset: 0x0006C640
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0006E457 File Offset: 0x0006C657
		public int IndexX { get; set; }

		// Token: 0x06000B2D RID: 2861 RVA: 0x0006E460 File Offset: 0x0006C660
		public ParticleEndBullet(StageDataPackage StageData, PointF OriginalPosition, byte ColorType, int Size)
			: this(StageData, OriginalPosition)
		{
			base.Scale = (float)Size / 32f;
			switch (ColorType)
			{
			case 0:
				base.ColorValue = Color.White;
				break;
			case 1:
				base.ColorValue = Color.Red;
				break;
			case 2:
				base.ColorValue = Color.Red;
				break;
			case 3:
				base.ColorValue = Color.Violet;
				break;
			case 4:
				base.ColorValue = Color.Violet;
				break;
			case 5:
				base.ColorValue = Color.Blue;
				break;
			case 6:
				base.ColorValue = Color.Blue;
				break;
			case 7:
				base.ColorValue = Color.Cyan;
				break;
			case 8:
				base.ColorValue = Color.Cyan;
				break;
			case 9:
				base.ColorValue = Color.Green;
				break;
			case 10:
				base.ColorValue = Color.Green;
				break;
			case 11:
				base.ColorValue = Color.YellowGreen;
				break;
			case 12:
				base.ColorValue = Color.Yellow;
				break;
			case 13:
				base.ColorValue = Color.Yellow;
				break;
			case 14:
				base.ColorValue = Color.Orange;
				break;
			case 15:
				base.ColorValue = Color.White;
				break;
			default:
				base.ColorValue = Color.White;
				break;
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0006E5D8 File Offset: 0x0006C7D8
		public ParticleEndBullet(StageDataPackage StageData, PointF OriginalPosition, Color ColorValue, int Size)
			: this(StageData, OriginalPosition)
		{
			base.ColorValue = ColorValue;
			base.Scale = (float)Size / 32f;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0006E600 File Offset: 0x0006C800
		public ParticleEndBullet(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = OriginalPosition;
			base.Direction = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
			this.TxtureObjects = new TextureObject[]
			{
				base.TextureObjectDictionary["EndBullet00"],
				base.TextureObjectDictionary["EndBullet01"],
				base.TextureObjectDictionary["EndBullet02"],
				base.TextureObjectDictionary["EndBullet03"],
				base.TextureObjectDictionary["EndBullet04"],
				base.TextureObjectDictionary["EndBullet05"],
				base.TextureObjectDictionary["EndBullet06"],
				base.TextureObjectDictionary["EndBullet07"]
			};
			base.TxtureObject = this.TxtureObjects[0];
			base.LifeTime = 31;
			base.AngularVelocityDegree = (float)base.Ran.Next(-3, 3);
			base.ScaleVelocity = 0.05f;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0006E748 File Offset: 0x0006C948
		public override void Ctrl()
		{
			base.Ctrl();
			this.IndexX = base.Time % 32 / 4;
			base.TxtureObject = this.TxtureObjects[this.IndexX];
			if (base.Time == base.LifeTime / 2)
			{
				base.ScaleVelocity = -base.ScaleVelocity;
			}
		}
	}
}
