using System;
using System.Drawing;

namespace Shooting
{
	public class ParticleEndBullet : BaseEffect
	{
		public TextureObject[] TxtureObjects { get; set; }

		public int IndexX { get; set; }

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

		public ParticleEndBullet(StageDataPackage StageData, PointF OriginalPosition, Color ColorValue, int Size)
			: this(StageData, OriginalPosition)
		{
			base.ColorValue = ColorValue;
			base.Scale = (float)Size / 32f;
		}

		public ParticleEndBullet(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = OriginalPosition;
			base.Direction = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
			TextureObject first = base.TextureObjectDictionary["EndBullet00"];
			if (sharedFrames == null || sharedFrames[0] != first)
			{
				sharedFrames = new TextureObject[]
				{
					first,
					base.TextureObjectDictionary["EndBullet01"],
					base.TextureObjectDictionary["EndBullet02"],
					base.TextureObjectDictionary["EndBullet03"],
					base.TextureObjectDictionary["EndBullet04"],
					base.TextureObjectDictionary["EndBullet05"],
					base.TextureObjectDictionary["EndBullet06"],
					base.TextureObjectDictionary["EndBullet07"]
				};
			}
			this.TxtureObjects = sharedFrames;
			base.TxtureObject = this.TxtureObjects[0];
			base.LifeTime = 31;
			base.AngularVelocityDegree = (float)base.Ran.Next(-3, 3);
			base.ScaleVelocity = 0.05f;
		}

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

		private static TextureObject[] sharedFrames;
	}
}
