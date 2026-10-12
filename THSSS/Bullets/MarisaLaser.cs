using System;
using System.Drawing;

namespace Shooting
{
	public class MarisaLaser : BaseEffect
	{
		public MarisaLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float v, double drctn)
			: base(StageData, textureName, new PointF(0f, 0f), v, drctn)
		{
			this.StageData = StageData;
			this.OriginalPosition = OriginalPosition;
			Damage = 4;
			Region = 128;
			MaxVelocity = 1000f;
			MinVelocity = -1000f;
			Velocity = v;
			LifeTime = 5;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Velocity = 192f;
			bool flag = false;
			for (int i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(EnemyPlaneList[i]))
				{
					flag = true;
					if (MyPlane.EnchantmentState == EnchantmentType.Red)
					{
						EnemyPlaneList[i].HealthPoint -= (float)Damage * 1.25f;
					}
					else
					{
						EnemyPlaneList[i].HealthPoint -= (float)Damage;
					}
					if (EnemyPlaneList[i].HealthPoint < 10000f)
					{
						MyPlane.Score += 10L;
					}
					if (EnemyPlaneList[i].HealthPoint <= 0f)
					{
						EnemyPlaneList[i].GiveEndEffect();
						EnemyPlaneList[i].GiveItems();
						EnemyPlaneList.RemoveAt(i);
						StageData.SoundPlay("se_enep00.wav", OriginalPosition.X / (float)BoundRect.Width);
						break;
					}
				}
			}
			if (Boss != null)
			{
				if (HitCheck(Boss))
				{
					flag = true;
					if (Boss.Time > Boss.UnmatchedTime)
					{
						if (MyPlane.EnchantmentState == EnchantmentType.Red)
						{
							Boss.HealthPoint -= (float)Damage * 1.25f * (1f - Boss.Armon);
						}
						else
						{
							Boss.HealthPoint -= (float)Damage * (1f - Boss.Armon);
						}
					}
					MyPlane.Score += 10L;
					if (Boss.HealthPoint <= 200f)
					{
						StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
			if (flag)
			{
				Damage = 0;
			}
		}

		public override bool HitCheck(BaseObject Target)
		{
			double distance = GetDistance(Target);
			double direction = GetDirection(Target);
			double num = distance * Math.Cos((double)((float)(Direction - direction)));
			double num2 = distance * Math.Sin((double)((float)(Direction - direction)));
			double num3 = (double)((float)TxtureObject.Width * ScaleWidth * 0.5f);
			double num4 = (double)((float)TxtureObject.Height * ScaleLength * 0.5f);
			return num2 * num2 / num4 / num4 + (num - 256.0 + 64.0 - (double)((float)TxtureObject.Height * ScaleLength / 2f)) * (num - 256.0 + 64.0 - (double)((float)TxtureObject.Height * ScaleLength / 2f)) / num3 / num3 < 1.0;
		}

		public override void Show()
		{
			int num = TimeMain % 8 * 8;
			SizeF sizeF = new SizeF((float)(TxtureObject.Width - 64) * ScaleWidth, (float)TxtureObject.Height * ScaleLength);
			SpriteMain.Draw2D(TxtureObject.TXTure, new Rectangle(TxtureObject.PosRect.X + 64 - num, TxtureObject.PosRect.Y, TxtureObject.PosRect.Width - 64, TxtureObject.PosRect.Height), sizeF, new PointF((float)((TxtureObject.Width - 64) / 2), (float)(TxtureObject.Height / 2)), (float)(Direction - 1.5707963267948966 + Angle), Position, Color.FromArgb(TransparentValue, ColorValue));
		}

		public override void ShowRegion()
		{
			SpriteMain.Draw2D(TextureObjectDictionary["Region"], (float)(TxtureObject.Width - 64) * 1f / (float)TextureObjectDictionary["Region"].Width * ScaleWidth, (float)TxtureObject.Height * 1f / (float)TextureObjectDictionary["Region"].Height * ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), Position, Color.White, Mirrored);
		}
	}
}
