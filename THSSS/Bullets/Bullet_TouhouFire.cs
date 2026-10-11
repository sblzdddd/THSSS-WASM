using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000050 RID: 80
	internal class Bullet_TouhouFire : Bullet_Touhou_01
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00010F88 File Offset: 0x0000F188
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00010F9F File Offset: 0x0000F19F
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x06000298 RID: 664 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public Bullet_TouhouFire(StageDataPackage StageData, int type, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, null, OriginalPosition, Velocity, Direction, 0)
		{
			switch (type)
			{
			case 0:
				base.ColorType = 1;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet30-0"],
					base.TextureObjectDictionary["bullet30-1"],
					base.TextureObjectDictionary["bullet30-2"],
					base.TextureObjectDictionary["bullet30-3"]
				};
				base.Active = false;
				break;
			case 1:
				base.ColorType = 1;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet17-0"],
					base.TextureObjectDictionary["bullet17-1"],
					base.TextureObjectDictionary["bullet17-2"],
					base.TextureObjectDictionary["bullet17-3"]
				};
				base.Angle = 3.1415927410125732;
				base.Active = true;
				break;
			case 2:
				base.ColorType = 7;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet18-0"],
					base.TextureObjectDictionary["bullet18-1"],
					base.TextureObjectDictionary["bullet18-2"],
					base.TextureObjectDictionary["bullet18-3"]
				};
				base.Angle = 3.1415927410125732;
				base.Active = true;
				break;
			case 3:
				base.ColorType = 4;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet42_0"],
					base.TextureObjectDictionary["bullet42_1"],
					base.TextureObjectDictionary["bullet42_2"],
					base.TextureObjectDictionary["bullet42_3"]
				};
				base.Angle = 3.1415927410125732;
				base.Active = true;
				break;
			case 4:
				base.ColorType = 14;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet43_0"],
					base.TextureObjectDictionary["bullet43_1"],
					base.TextureObjectDictionary["bullet43_2"],
					base.TextureObjectDictionary["bullet43_3"]
				};
				base.Angle = 3.1415927410125732;
				base.Active = true;
				break;
			case 5:
				base.ColorType = 14;
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet44_0"],
					base.TextureObjectDictionary["bullet44_1"],
					base.TextureObjectDictionary["bullet44_2"],
					base.TextureObjectDictionary["bullet44_3"]
				};
				base.Angle = 3.1415927410125732;
				base.Active = true;
				break;
			default:
				this.TxtureObjects = new TextureObject[]
				{
					base.TextureObjectDictionary["bullet30-0"],
					base.TextureObjectDictionary["bullet30-1"],
					base.TextureObjectDictionary["bullet30-2"],
					base.TextureObjectDictionary["bullet30-3"]
				};
				base.Active = true;
				break;
			}
			this.timeOffset = base.Ran.Next(16);
			base.TxtureObject = this.TxtureObjects[(base.Time + this.timeOffset) % 16 / 4];
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00011372 File Offset: 0x0000F572
		public override void Ctrl()
		{
			base.Ctrl();
			base.TxtureObject = this.TxtureObjects[(base.Time + this.timeOffset) % 16 / 4];
		}

		// Token: 0x040000C4 RID: 196
		private int timeOffset;
	}
}
