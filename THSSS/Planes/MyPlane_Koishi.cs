using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class MyPlane_Koishi : BaseMyPlane_Touhou
	{
		public MyPlane_Koishi(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Koishi")
		{
			WeaponType = "A";
			SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Koishi(StageData, Position),
				new SubPlane_Koishi(StageData, Position),
				new SubPlane_Koishi(StageData, Position),
				new SubPlane_Koishi(StageData, Position)
			};
			HighSpeed = 5f;
			LowSpeed = 2f;
		}

		public override void Shoot()
		{
			if (Time % 3 == 0)
			{
				StageData.SoundPlay("se_plst00.wav", OriginalPosition.X / (float)BoundRect.Width);
				float x = OriginalPosition.X;
				float y = OriginalPosition.Y;
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "KoishiBullet", new PointF(x + 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou.Damage = 13;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "KoishiBullet", new PointF(x - 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou2.Damage = 13;
			}
			if (Time % 10 == 0)
			{
				for (int i = 0; i < PowerLevel; i++)
				{
					SubPlaneList[i].Shoot();
				}
			}
		}

		public override void SpellShoot()
		{
			if (!GodMode)
			{
				new Spell_Koishi(StageData);
				GodMode = true;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			TransparentVelocity = (float)(GodMode ? (-10) : 10);
			TransparentValueF += TransparentVelocity;
			SpellEnabled = !GodMode;
		}

		public override void SubPlaneCtrl()
		{
			if (KClass.Key_Shift)
			{
				switch (PowerLevel)
				{
				case 1:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 44f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 7f, OriginalPosition.Y - 36f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 7f, OriginalPosition.Y - 36f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					SubPlanePoint[2] = new PointF(OriginalPosition.X, OriginalPosition.Y - 36f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 16f, OriginalPosition.Y - 29f);
					SubPlanePoint[0] = new PointF(OriginalPosition.X + 16f, OriginalPosition.Y - 29f);
					SubPlaneList[2].ShootDirection = -1.5707963267948966;
					SubPlaneList[1].ShootDirection = -1.5707963267948966;
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 4:
					SubPlanePoint[3] = new PointF(OriginalPosition.X - 7f, OriginalPosition.Y - 36f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X + 7f, OriginalPosition.Y - 36f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 16f, OriginalPosition.Y - 29f);
					SubPlanePoint[0] = new PointF(OriginalPosition.X + 16f, OriginalPosition.Y - 29f);
					SubPlaneList[3].ShootDirection = -1.5707963267948966;
					SubPlaneList[2].ShootDirection = -1.5707963267948966;
					SubPlaneList[1].ShootDirection = -1.5707963267948966;
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				}
			}
			else
			{
				switch (PowerLevel)
				{
				case 1:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 44f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 32f, OriginalPosition.Y - 32f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 32f, OriginalPosition.Y - 32f);
					SubPlaneList[0].ShootDirection = 3.9269908169872414;
					SubPlaneList[1].ShootDirection = 5.497787143782138;
					break;
				case 3:
					SubPlanePoint[2] = new PointF(OriginalPosition.X, OriginalPosition.Y + 44f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 32f, OriginalPosition.Y - 32f);
					SubPlanePoint[0] = new PointF(OriginalPosition.X + 32f, OriginalPosition.Y - 32f);
					SubPlaneList[2].ShootDirection = 1.5707963267948966;
					SubPlaneList[1].ShootDirection = 3.9269908169872414;
					SubPlaneList[0].ShootDirection = 5.497787143782138;
					break;
				case 4:
					SubPlanePoint[3] = new PointF(OriginalPosition.X - 32f, OriginalPosition.Y + 32f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X + 32f, OriginalPosition.Y + 32f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 32f, OriginalPosition.Y - 32f);
					SubPlanePoint[0] = new PointF(OriginalPosition.X + 32f, OriginalPosition.Y - 32f);
					SubPlaneList[3].ShootDirection = 2.356194490192345;
					SubPlaneList[2].ShootDirection = 0.7853981633974483;
					SubPlaneList[1].ShootDirection = 3.9269908169872414;
					SubPlaneList[0].ShootDirection = 5.497787143782138;
					break;
				}
			}
			for (int i = 0; i < PowerLevel; i++)
			{
				SubPlaneList[i].Enabled = true;
				SubPlaneList[i].DestPoint = SubPlanePoint[i];
				SubPlaneList[i].Ctrl();
			}
			for (int i = PowerLevel; i < SubPlaneList.Count; i++)
			{
				SubPlaneList[i].Enabled = false;
				SubPlaneList[i].Position = Position;
			}
		}

		public override void PreMiss()
		{
			if (Time >= UnmatchedTime)
			{
				if (Time >= UnmatchedTime)
				{
					if (Time >= DeadTime)
					{
						if (SpellList.Count == 0)
						{
							if (EnchantmentState == EnchantmentType.Green)
							{
								BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(StageData, OriginalPosition);
								bulletRemover_Small.Region = 500;
								ChangeEnchantment(EnchantmentType.None);
								EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
								{
									if (x is BaseSpellCard)
									{
										((BaseSpellCard)x).Missed = true;
									}
								});
							}
							else if (GodMode)
							{
								GodMode = false;
								StageData.SoundPlay("ice005.wav");
								BulletRemover_Small bulletRemover_Small2 = new BulletRemover_Small(StageData, MyPlane.OriginalPosition);
								bulletRemover_Small2.Region = 150;
								Time = UnmatchedTime - 60;
								new ItemGetter(StageData);
								EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
								{
									if (x is BaseSpellCard)
									{
										((BaseSpellCard)x).Missed = true;
									}
								});
							}
							else
							{
								new Emitter(StageData, MyPlane.Position);
								StageData.SoundPlay("se_pldead00.wav", OriginalPosition.X / (float)BoundRect.Width);
								DeadTime = Time + 20;
							}
						}
					}
				}
			}
		}

		public override void Refresh()
		{
			if (GodMode)
			{
				SpellExtand();
				GodMode = false;
			}
			base.Refresh();
		}

		public override void Show()
		{
			if (Time >= DeadTime)
			{
				if (Time != 0)
				{
					if (Time < UnmatchedTime && Time / 2 % 2 == 0)
					{
						SpriteMain.Draw2D(TxtureObject, 1f, 0f, Position, Color.FromArgb(TransparentValue, Color.DarkBlue));
					}
					else
					{
						SpriteMain.Draw2D(TxtureObject, 1f, 0f, Position, Color.FromArgb(TransparentValue, Color.White));
					}
					if (GodMode)
					{
						SpriteMain.Draw2D(TextureObjectDictionary["jiejie"], 1f, -(float)Direction * 4f, Position, Color.FromArgb(255 - TransparentValue, Color.White));
					}
					for (int i = 0; i < PowerLevel; i++)
					{
						SubPlaneList[i].Show();
					}
				}
			}
		}

		private bool GodMode;
	}
}
