using System.Drawing;

namespace Shooting
{
	public class BaseBullet_Touhou : BaseObject_CS
	{
		public int Layer { get; set; }

		public byte ColorType { get; set; }

		public int SizeValue { get; set; }

		public byte Reflect { get; set; }

		public bool Grazed { get; set; }

		public BaseBullet_Touhou(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public BaseBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: this(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			this.ColorType = ColorType;
			BeginningEffect = true;
		}

		public BaseBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			BulletList.Add(this);
			this.OriginalPosition = OriginalPosition;
			AngleDegree = 180.0;
			SizeValue = 32;
			SetRegion(textureName);
		}

		public void SetRegion(string textureName)
		{
			if (textureName != null)
			{
				var array = new char[] { '_' };
				var array2 = textureName.Split(array);
				var text = array2[0];
				switch (text)
				{
				case "bullet0":
					Region = 1;
					break;
				case "bullet1":
					Region = 1;
					break;
				case "bullet2":
					Region = 2;
					break;
				case "bullet3":
					Region = 2;
					break;
				case "bullet4":
					Region = 1;
					break;
				case "bullet5":
					Region = 1;
					break;
				case "bullet6":
					Region = 1;
					break;
				case "bullet7":
					Region = 1;
					break;
				case "bullet8":
					Region = 1;
					break;
				case "bullet9":
					Region = 1;
					break;
				case "bullet10":
					Region = 2;
					break;
				case "bullet11":
					Region = 2;
					break;
				case "bullet12":
					Region = 1;
					break;
				case "bullet13":
					Region = 3;
					break;
				case "bullet15":
					Region = 1;
					break;
				case "bullet20":
					Region = 6;
					break;
				case "bullet21":
					Region = 6;
					break;
				case "bullet22":
					Region = 3;
					break;
				case "bullet23":
					Region = 3;
					break;
				case "bullet24":
					Region = 3;
					break;
				case "bullet26":
					Region = 16;
					AngularVelocityDegree = 10f;
					Active = true;
					SizeValue = 80;
					OutsideRegion = 32;
					break;
				case "bullet50":
					Region = 12;
					Active = true;
					SizeValue = 64;
					OutsideRegion = 64;
					break;
				case "bullet40":
					Region = 6;
					SizeValue = 32;
					OutsideRegion = 32;
					break;
				case "bullet46":
					Region = 1;
					break;
				case "bullet61":
					Region = 2;
					break;
				case "bullet62":
					Region = 3;
					break;
				case "bullet64":
					Region = 3;
					break;
				case "bullet65":
					Region = 3;
					break;
				case "bullet66":
					Region = 3;
					break;
				case "bullet67":
					Region = 3;
					break;
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1 && BeginningEffect)
			{
				var particleBeginBullet = new ParticleBeginBullet(StageData, Position, ColorType);
				particleBeginBullet.Active = Active;
			}
			if (Time == LifeTime && EndingEffect)
			{
				GiveEndEffect();
			}
			if (OutBoundary())
			{
				BulletList.Remove(this);
			}
		}

		public override bool OutBoundary()
		{
			bool flag;
			if (LifeTime != 0 && Time > LifeTime)
			{
				flag = true;
			}
			else if (Reflect <= 0)
			{
				flag = base.OutBoundary();
			}
			else
			{
				if (OriginalPosition.X < 0f)
				{
					Reflect -= 1;
					Direction -= 2.0 * (Direction - 1.5707963267948966);
					OriginalPosition = new PointF(-OriginalPosition.X, OriginalPosition.Y);
				}
				else if (OriginalPosition.X >= (float)BoundRect.Width)
				{
					Reflect -= 1;
					Direction -= 2.0 * (Direction - 1.5707963267948966);
					OriginalPosition = new PointF((float)(2 * BoundRect.Width) - OriginalPosition.X, OriginalPosition.Y);
				}
				if (OriginalPosition.Y < 20f)
				{
					if (Reflect > 0)
					{
						Reflect -= 1;
						Direction = -Direction;
						OriginalPosition = new PointF(OriginalPosition.X, 20f - OriginalPosition.Y);
					}
				}
				flag = false;
			}
			return flag;
		}

		public override void HitCheckAll()
		{
			if (MyPlane.HitEnabled)
			{
				if (HitCheck(MyPlane))
				{
					MyPlane.PreMiss();
					if (!UnRemoveable)
					{
						GiveEndEffect();
						BulletList.Remove(this);
					}
				}
				else if (!Grazed && HitCheck(MyPlane, 24f / ScaleLength + (float)Region))
				{
					Grazed = true;
					MyPlane.Graze++;
					if (MyPlane.Graze % 10 == 0)
					{
						MyPlane.HighItemScore += 10;
					}
					StageData.SoundPlay("se_graze.wav");
					var baseEffect = new BaseEffect(StageData, "GrazeG", MyPlane.Position, 5f, Ran.NextDouble() * 3.141592653589793 * 2.0);
					baseEffect.Scale = 1f;
					baseEffect.TransparentVelocity = -20f;
					baseEffect.ScaleVelocity = -0.05f;
					baseEffect.AngleWithDirection = false;
				}
			}
		}

		public override void GiveEndEffect()
		{
			new ParticleEndBullet(StageData, OriginalPosition, ColorType, SizeValue);
		}

		public override void GiveItems()
		{
			new StarItem_Touhou(StageData, OriginalPosition);
		}
	}
}
