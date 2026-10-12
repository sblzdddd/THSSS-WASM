using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class MyPlane_Koishi : BaseMyPlane_Touhou
	{
		public MyPlane_Koishi(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Koishi")
		{
			base.WeaponType = "A";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Koishi(StageData, base.Position),
				new SubPlane_Koishi(StageData, base.Position),
				new SubPlane_Koishi(StageData, base.Position),
				new SubPlane_Koishi(StageData, base.Position)
			};
			base.HighSpeed = 5f;
			base.LowSpeed = 2f;
		}

		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.OriginalPosition.X;
				float y = base.OriginalPosition.Y;
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "KoishiBullet", new PointF(x + 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou.Damage = 13;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "KoishiBullet", new PointF(x - 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou2.Damage = 13;
			}
			if (base.Time % 10 == 0)
			{
				for (int i = 0; i < base.PowerLevel; i++)
				{
					base.SubPlaneList[i].Shoot();
				}
			}
		}

		public override void SpellShoot()
		{
			if (!this.GodMode)
			{
				new Spell_Koishi(base.StageData);
				this.GodMode = true;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.TransparentVelocity = (float)(this.GodMode ? (-10) : 10);
			base.TransparentValueF += base.TransparentVelocity;
			base.SpellEnabled = !this.GodMode;
		}

		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 44f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 7f, base.OriginalPosition.Y - 36f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 7f, base.OriginalPosition.Y - 36f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 36f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 16f, base.OriginalPosition.Y - 29f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 16f, base.OriginalPosition.Y - 29f);
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 4:
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X - 7f, base.OriginalPosition.Y - 36f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 7f, base.OriginalPosition.Y - 36f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 16f, base.OriginalPosition.Y - 29f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 16f, base.OriginalPosition.Y - 29f);
					base.SubPlaneList[3].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 44f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 32f, base.OriginalPosition.Y - 32f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 32f, base.OriginalPosition.Y - 32f);
					base.SubPlaneList[0].ShootDirection = 3.9269908169872414;
					base.SubPlaneList[1].ShootDirection = 5.497787143782138;
					break;
				case 3:
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 44f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 32f, base.OriginalPosition.Y - 32f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 32f, base.OriginalPosition.Y - 32f);
					base.SubPlaneList[2].ShootDirection = 1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = 3.9269908169872414;
					base.SubPlaneList[0].ShootDirection = 5.497787143782138;
					break;
				case 4:
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X - 32f, base.OriginalPosition.Y + 32f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 32f, base.OriginalPosition.Y + 32f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 32f, base.OriginalPosition.Y - 32f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 32f, base.OriginalPosition.Y - 32f);
					base.SubPlaneList[3].ShootDirection = 2.356194490192345;
					base.SubPlaneList[2].ShootDirection = 0.7853981633974483;
					base.SubPlaneList[1].ShootDirection = 3.9269908169872414;
					base.SubPlaneList[0].ShootDirection = 5.497787143782138;
					break;
				}
			}
			for (int i = 0; i < base.PowerLevel; i++)
			{
				base.SubPlaneList[i].Enabled = true;
				base.SubPlaneList[i].DestPoint = base.SubPlanePoint[i];
				base.SubPlaneList[i].Ctrl();
			}
			for (int i = base.PowerLevel; i < base.SubPlaneList.Count; i++)
			{
				base.SubPlaneList[i].Enabled = false;
				base.SubPlaneList[i].Position = base.Position;
			}
		}

		public override void PreMiss()
		{
			if (base.Time >= base.UnmatchedTime)
			{
				if (base.Time >= base.UnmatchedTime)
				{
					if (base.Time >= base.DeadTime)
					{
						if (base.SpellList.Count == 0)
						{
							if (base.EnchantmentState == EnchantmentType.Green)
							{
								BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
								bulletRemover_Small.Region = 500;
								base.ChangeEnchantment(EnchantmentType.None);
								base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
								{
									if (x is BaseSpellCard)
									{
										((BaseSpellCard)x).Missed = true;
									}
								});
							}
							else if (this.GodMode)
							{
								this.GodMode = false;
								base.StageData.SoundPlay("ice005.wav");
								BulletRemover_Small bulletRemover_Small2 = new BulletRemover_Small(base.StageData, base.MyPlane.OriginalPosition);
								bulletRemover_Small2.Region = 150;
								base.Time = base.UnmatchedTime - 60;
								new ItemGetter(base.StageData);
								base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
								{
									if (x is BaseSpellCard)
									{
										((BaseSpellCard)x).Missed = true;
									}
								});
							}
							else
							{
								new Emitter(base.StageData, base.MyPlane.Position);
								base.StageData.SoundPlay("se_pldead00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
								base.DeadTime = base.Time + 20;
							}
						}
					}
				}
			}
		}

		public override void Refresh()
		{
			if (this.GodMode)
			{
				base.SpellExtand();
				this.GodMode = false;
			}
			base.Refresh();
		}

		public override void Show()
		{
			if (base.Time >= base.DeadTime)
			{
				if (base.Time != 0)
				{
					if (base.Time < base.UnmatchedTime && base.Time / 2 % 2 == 0)
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.DarkBlue));
					}
					else
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.White));
					}
					if (this.GodMode)
					{
						base.SpriteMain.Draw2D(base.TextureObjectDictionary["jiejie"], 1f, -(float)base.Direction * 4f, base.Position, Color.FromArgb(255 - base.TransparentValue, Color.White));
					}
					for (int i = 0; i < base.PowerLevel; i++)
					{
						base.SubPlaneList[i].Show();
					}
				}
			}
		}

		private bool GodMode;
	}
}
