using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001C4 RID: 452
	public class MarisaLaser : BaseEffect
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00064CDC File Offset: 0x00062EDC
		public MarisaLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float v, double drctn)
			: base(StageData, textureName, new PointF(0f, 0f), v, drctn)
		{
			base.StageData = StageData;
			base.OriginalPosition = OriginalPosition;
			base.Damage = 4;
			base.Region = 128;
			base.MaxVelocity = 1000f;
			base.MinVelocity = -1000f;
			base.Velocity = v;
			base.LifeTime = 5;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00064D54 File Offset: 0x00062F54
		public override void Ctrl()
		{
			base.Ctrl();
			base.Velocity = 192f;
			bool flag = false;
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.EnemyPlaneList[i]))
				{
					flag = true;
					if (base.MyPlane.EnchantmentState == EnchantmentType.Red)
					{
						base.EnemyPlaneList[i].HealthPoint -= (float)base.Damage * 1.25f;
					}
					else
					{
						base.EnemyPlaneList[i].HealthPoint -= (float)base.Damage;
					}
					if (base.EnemyPlaneList[i].HealthPoint < 10000f)
					{
						base.MyPlane.Score += 10L;
					}
					if (base.EnemyPlaneList[i].HealthPoint <= 0f)
					{
						base.EnemyPlaneList[i].GiveEndEffect();
						base.EnemyPlaneList[i].GiveItems();
						base.EnemyPlaneList.RemoveAt(i);
						base.StageData.SoundPlay("se_enep00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
						break;
					}
				}
			}
			if (base.Boss != null)
			{
				if (this.HitCheck(base.Boss))
				{
					flag = true;
					if (base.Boss.Time > base.Boss.UnmatchedTime)
					{
						if (base.MyPlane.EnchantmentState == EnchantmentType.Red)
						{
							base.Boss.HealthPoint -= (float)base.Damage * 1.25f * (1f - base.Boss.Armon);
						}
						else
						{
							base.Boss.HealthPoint -= (float)base.Damage * (1f - base.Boss.Armon);
						}
					}
					base.MyPlane.Score += 10L;
					if (base.Boss.HealthPoint <= 200f)
					{
						base.StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
			if (flag)
			{
				base.Damage = 0;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00064FF0 File Offset: 0x000631F0
		public override bool HitCheck(BaseObject Target)
		{
			double distance = base.GetDistance(Target);
			double direction = base.GetDirection(Target);
			double num = distance * Math.Cos((double)((float)(base.Direction - direction)));
			double num2 = distance * Math.Sin((double)((float)(base.Direction - direction)));
			double num3 = (double)((float)base.TxtureObject.Width * base.ScaleWidth * 0.5f);
			double num4 = (double)((float)base.TxtureObject.Height * base.ScaleLength * 0.5f);
			return num2 * num2 / num4 / num4 + (num - 256.0 + 64.0 - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) * (num - 256.0 + 64.0 - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) / num3 / num3 < 1.0;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00065100 File Offset: 0x00063300
		public override void Show()
		{
			int num = base.TimeMain % 8 * 8;
			SizeF sizeF = new SizeF((float)(base.TxtureObject.Width - 64) * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, new Rectangle(base.TxtureObject.PosRect.X + 64 - num, base.TxtureObject.PosRect.Y, base.TxtureObject.PosRect.Width - 64, base.TxtureObject.PosRect.Height), sizeF, new PointF((float)((base.TxtureObject.Width - 64) / 2), (float)(base.TxtureObject.Height / 2)), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00065210 File Offset: 0x00063410
		public override void ShowRegion()
		{
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], (float)(base.TxtureObject.Width - 64) * 1f / (float)base.TextureObjectDictionary["Region"].Width * base.ScaleWidth, (float)base.TxtureObject.Height * 1f / (float)base.TextureObjectDictionary["Region"].Height * base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.White, base.Mirrored);
		}
	}
}
