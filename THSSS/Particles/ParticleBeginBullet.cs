using System.Drawing;

namespace Shooting
{
	public class ParticleBeginBullet : BaseEffect
	{
		public TextureObject[] TxtureObjects { get; set; }

		public int IndexX { get; set; }

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

		public ParticleBeginBullet(StageDataPackage StageData, PointF Position)
			: base(StageData, null, Position, 0f, 0.0)
		{
			base.Direction = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
			base.TxtureObject = base.TextureObjectDictionary["bullet25_0"];
			base.LifeTime = 8;
			base.Scale = 2f;
			base.Active = false;
		}

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
