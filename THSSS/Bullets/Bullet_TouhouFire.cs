using System;
using System.Drawing;

namespace Shooting
{
	internal class Bullet_TouhouFire : Bullet_Touhou_01
	{
		public TextureObject[] TxtureObjects { get; set; }

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

		public override void Ctrl()
		{
			base.Ctrl();
			base.TxtureObject = this.TxtureObjects[(base.Time + this.timeOffset) % 16 / 4];
		}

		private int timeOffset;
	}
}
