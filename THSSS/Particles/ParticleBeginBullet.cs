using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D5 RID: 213
	public class ParticleBeginBullet : BaseEffect
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00034978 File Offset: 0x00032B78
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0003498F File Offset: 0x00032B8F
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00034998 File Offset: 0x00032B98
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000349AF File Offset: 0x00032BAF
		public int IndexX { get; set; }

		// Token: 0x060005E6 RID: 1510 RVA: 0x000349B8 File Offset: 0x00032BB8
		public ParticleBeginBullet(StageDataPackage StageData, PointF Position, byte ColorType)
			: this(StageData, Position)
		{
			switch (ColorType)
			{
			case 0:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_0"];
				break;
			case 1:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_1"];
				break;
			case 2:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_1"];
				break;
			case 3:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_2"];
				break;
			case 4:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_2"];
				break;
			case 5:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_3"];
				break;
			case 6:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_3"];
				break;
			case 7:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_4"];
				break;
			case 8:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_4"];
				break;
			case 9:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_5"];
				break;
			case 10:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_5"];
				break;
			case 11:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_5"];
				break;
			case 12:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_6"];
				break;
			case 13:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_6"];
				break;
			case 14:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_6"];
				break;
			case 15:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_7"];
				break;
			default:
				base.TxtureObject = base.TextureObjectDictionary["bullet25_0"];
				break;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00034BE8 File Offset: 0x00032DE8
		public ParticleBeginBullet(StageDataPackage StageData, PointF Position)
			: base(StageData, null, Position, 0f, 0.0)
		{
			base.Direction = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
			base.TxtureObject = base.TextureObjectDictionary["bullet25_0"];
			base.LifeTime = 8;
			base.Scale = 2f;
			base.Active = false;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00034C70 File Offset: 0x00032E70
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.LifeTime - 8 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent / 8);
			}
		}
	}
}
