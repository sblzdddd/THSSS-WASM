using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000029 RID: 41
	public class BaseEnemyPlane : BaseObject_CS
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000AF5C File Offset: 0x0000915C
		public BaseEnemyPlane()
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000AF67 File Offset: 0x00009167
		public BaseEnemyPlane(StageDataPackage StageData, string textureName, PointF p, float v, double drctn)
			: base(StageData, textureName, p, v, drctn)
		{
			base.Region = 4;
			base.EnemyPlaneList.Add(this);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AF8E File Offset: 0x0000918E
		public override void Shoot()
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000AF94 File Offset: 0x00009194
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.ShootEnabled)
			{
				this.Shoot();
			}
			if (this.OutBoundary())
			{
				base.EnemyPlaneList.Remove(this);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AFDC File Offset: 0x000091DC
		public override void HitCheckAll()
		{
			for (int i = base.MyBulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.MyBulletList[i]))
				{
					if (base.MyPlane.EnchantmentState == EnchantmentType.Red)
					{
						base.HealthPoint -= (float)base.MyBulletList[i].Damage * 1.25f;
					}
					else
					{
						base.HealthPoint -= (float)base.MyBulletList[i].Damage;
					}
					base.MyBulletList[i].GiveEndEffect();
					base.MyBulletList.RemoveAt(i);
					base.MyPlane.Score += 10L;
					if (base.HealthPoint <= 0f)
					{
						this.GiveEndEffect();
						this.GiveItems();
						base.EnemyPlaneList.Remove(this);
						base.StageData.SoundPlay("se_enep00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
						break;
					}
					base.StageData.SoundPlay("se_damage01.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				}
			}
			if (base.MyPlane.HitEnabled)
			{
				if (this.HitCheck(base.MyPlane))
				{
					base.MyPlane.PreMiss();
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B189 File Offset: 0x00009389
		public override void GiveEndEffect()
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B18C File Offset: 0x0000938C
		public override void GiveItems()
		{
		}
	}
}
