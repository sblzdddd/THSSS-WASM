using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B2 RID: 178
	public class Spell_AyaA : BaseSpell
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x000295A0 File Offset: 0x000277A0
		public Spell_AyaA(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.OriginalPosition = base.MyPlane.OriginalPosition;
			base.Damage = 10;
			base.Region = 30;
			base.LifeTime = 300;
			base.Direction = 1.5707963267948966;
			base.SpellList.Add(this);
			new BackgroundBlack(StageData, base.Position);
			BackgroundMySC backgroundMySC = new BackgroundMySC(StageData, "76-4");
			backgroundMySC.LifeTime = 350;
			backgroundMySC.MaxTransparent = 180;
			backgroundMySC.TransparentValueF = 0f;
			backgroundMySC.TransparentVelocity = 4f;
			backgroundMySC.ColorValue = Color.Green;
			BackgroundMySC backgroundMySC2 = new BackgroundMySC(StageData, "76-4");
			backgroundMySC2.LifeTime = 350;
			backgroundMySC2.MaxTransparent = 150;
			backgroundMySC2.TransparentValueF = 0f;
			backgroundMySC2.TransparentVelocity = 4f;
			backgroundMySC2.ColorValue = Color.Green;
			backgroundMySC2.AngularVelocityDegree = -0.5f;
			backgroundMySC2.Active = true;
			StageData.VibrateStart(base.LifeTime);
			base.MyPlane.HighSpeed = 2f;
			base.MyPlane.LowSpeed = 1f;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 4f;
			base.ScaleLength = 0.2f;
			base.ScaleVelocity = 0.05f;
			base.MaxScale = 2f;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002972B File Offset: 0x0002792B
		public override void Move()
		{
			base.OriginalPosition = base.MyPlane.OriginalPosition;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00029740 File Offset: 0x00027940
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time < 16)
			{
				base.TxtureObject = base.TextureObjectDictionary["wind" + string.Format("{0:00}", base.Time % 16 / 2)];
			}
			else if (base.Time < 284)
			{
				base.TxtureObject = base.TextureObjectDictionary["wind" + string.Format("{0:00}", 7 + (base.Time - 16) % 36 / 2)];
			}
			else
			{
				base.TxtureObject = base.TextureObjectDictionary["wind" + string.Format("{0:00}", 25 + (base.Time - 300 + 16) % 16 / 2)];
			}
			base.TxtureObject.OffsetY = 100;
			if (base.Time == 240)
			{
				base.TransparentVelocity = -4f;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002986C File Offset: 0x00027A6C
		public override bool OutBoundary()
		{
			bool flag = base.OutBoundary();
			if (flag)
			{
				base.MyPlane.HighSpeed = 5f;
				base.MyPlane.LowSpeed = 2f;
			}
			return flag;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000298B4 File Offset: 0x00027AB4
		public override bool HitCheck(BaseObject Target)
		{
			return Target.OriginalPosition.Y - base.OriginalPosition.Y < 2.8f * (Target.OriginalPosition.X - base.OriginalPosition.X) + 136f && Target.OriginalPosition.Y - base.OriginalPosition.Y < -2.8f * (Target.OriginalPosition.X - base.OriginalPosition.X) + 136f;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00029968 File Offset: 0x00027B68
		public override void HitCheckAll()
		{
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.BulletList[i]))
				{
					if (!base.BulletList[i].UnRemoveable)
					{
						base.BulletList[i].GiveEndEffect();
						base.BulletList[i].GiveItems();
						base.BulletList.RemoveAt(i);
					}
				}
			}
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.EnemyPlaneList[i]))
				{
					int num = base.Damage + (int)((400.0 - base.EnemyPlaneList[i].GetDistance(base.MyPlane)) / 50.0);
					if (num < base.Damage)
					{
						num = base.Damage;
					}
					else if (num > 19)
					{
						num = 19;
					}
					base.EnemyPlaneList[i].HealthPoint -= (float)num;
					if (base.EnemyPlaneList[i].HealthPoint <= 0f)
					{
						base.EnemyPlaneList[i].GiveEndEffect();
						base.EnemyPlaneList[i].GiveItems();
						base.EnemyPlaneList.RemoveAt(i);
						base.StageData.SoundPlay("se_enep00.wav");
						break;
					}
				}
			}
			if (base.Boss != null)
			{
				if (base.Boss.Time > base.Boss.UnmatchedTime && this.HitCheck(base.Boss))
				{
					int num = base.Damage + (int)((400.0 - base.Boss.GetDistance(base.MyPlane)) / 40.0);
					if (num < base.Damage)
					{
						num = base.Damage;
					}
					else if (num > 19)
					{
						num = 19;
					}
					base.Boss.HealthPoint -= (float)((int)((float)num * (1f - base.Boss.Armon)));
					if (base.Boss.HealthPoint <= 200f)
					{
						base.StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
		}
	}
}
