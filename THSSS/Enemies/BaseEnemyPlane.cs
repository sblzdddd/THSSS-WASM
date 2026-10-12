using System.Drawing;

namespace Shooting
{
	public class BaseEnemyPlane : BaseObject_CS
	{
		public BaseEnemyPlane()
		{
		}

		public BaseEnemyPlane(StageDataPackage StageData, string textureName, PointF p, float v, double drctn)
			: base(StageData, textureName, p, v, drctn)
		{
			base.Region = 4;
			base.EnemyPlaneList.Add(this);
		}

		public override void Shoot()
		{
		}

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

		public override void GiveEndEffect()
		{
		}

		public override void GiveItems()
		{
		}
	}
}
