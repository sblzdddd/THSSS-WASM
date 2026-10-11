using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001B2 RID: 434
	internal class Boss_Kage02 : BaseBossTouhou
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x00061420 File Offset: 0x0005F620
		public Boss_Kage02(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossKage-00"];
			array[0, 1] = base.TextureObjectDictionary["BossKage-01"];
			array[0, 2] = base.TextureObjectDictionary["BossKage-02"];
			array[0, 3] = base.TextureObjectDictionary["BossKage-03"];
			array[1, 0] = base.TextureObjectDictionary["BossKage-10"];
			array[1, 1] = base.TextureObjectDictionary["BossKage-11"];
			array[1, 2] = base.TextureObjectDictionary["BossKage-12"];
			array[1, 3] = base.TextureObjectDictionary["BossKage-13"];
			array[2, 0] = base.TextureObjectDictionary["BossKage-20"];
			array[2, 1] = base.TextureObjectDictionary["BossKage-21"];
			array[2, 2] = base.TextureObjectDictionary["BossKage-22"];
			array[2, 3] = base.TextureObjectDictionary["BossKage-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 150f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 150);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 1;
			base.Region = 20;
			base.Velocity = 4f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 2400;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			this.LoadArmon(".\\CS\\St04\\关底Boss\\ctrl.csv");
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00061640 File Offset: 0x0005F840
		public override void Ctrl()
		{
			base.Ctrl();
			if (!base.OnSpell)
			{
				base.Armon = 0f;
			}
			switch (base.Life)
			{
			case 0:
				base.Velocity = 0f;
				base.DestPoint = new Point(base.Ran.Next(100, base.BoundRect.Width - 100), base.Ran.Next(70, 200));
				break;
			case 1:
				this.Ctrl1();
				break;
			}
			base.MoveToPoint(base.DestPoint);
			if (!base.OnSpell)
			{
				if (base.HealthPoint < (float)base.SpellcardHP || base.Time > base.SpellTime)
				{
					base.HealthPoint = (float)base.SpellcardHP;
					base.Time = 0;
					base.OnSpell = true;
					new ShootingStarShard(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 0f));
				}
			}
			else if (base.HealthPoint <= 0f || base.Time > base.SpellTime)
			{
				base.Life--;
				if (base.HealthPoint <= 0f && base.Life >= -1)
				{
					this.GiveItems();
					new ShootingStarShard(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 0f));
				}
				if (base.Life <= 0)
				{
					if (base.Life == 0)
					{
						base.DestPoint = new Point(base.Ran.Next((int)base.OriginalPosition.X - 30, (int)base.OriginalPosition.X + 30), base.Ran.Next((int)base.OriginalPosition.Y + 30, (int)base.OriginalPosition.Y + 50));
						base.Velocity = 0.5f;
						new BulletRemover2(base.StageData, base.OriginalPosition);
						base.HealthPoint = 0f;
						this.GiveEndEffect();
						base.Life = -1;
					}
				}
				else
				{
					new BulletRemover2(base.StageData, base.OriginalPosition);
					base.StageData.SoundPlay("se_tan00.wav");
					base.HealthPoint = (float)base.MaxHP;
					base.Time = 0;
					base.OnSpell = false;
					base.DestPoint = new Point(base.BoundRect.Width / 2, 120);
					base.Velocity = 4f;
				}
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00061938 File Offset: 0x0005FB38
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			base.LifeTime = base.Time + 150;
			new EndBoss_Touhou04(base.StageData, base.OriginalPosition, Color.Pink, Color.Violet);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000619B4 File Offset: 0x0005FBB4
		public override void TextureCtrl()
		{
			int num = (base.Mirrored ? base.IndexY : (-base.IndexY));
			if (base.OnAction > 0)
			{
				base.IndexY = 2;
				base.IndexX += 0.125f;
				base.OnAction--;
			}
			else if (base.Vx < -0.05f)
			{
				base.Mirrored = false;
				base.IndexY = 1;
				if (base.Vx < -1f)
				{
					if (base.IndexX < 2f)
					{
						base.IndexX += 0.125f;
					}
					else
					{
						base.IndexX = (float)(2 + base.Time % 16 / 8);
					}
				}
				else if (base.Vx < -0.5f)
				{
					base.IndexX = 1f;
				}
				else
				{
					base.IndexX = 0f;
				}
			}
			else if (base.Vx > 0.05f)
			{
				base.Mirrored = true;
				base.IndexY = 1;
				if (base.Vx > 1f)
				{
					if (base.IndexX < 2f)
					{
						base.IndexX += 0.125f;
					}
					else
					{
						base.IndexX = (float)(2 + base.Time % 16 / 8);
					}
				}
				else if (base.Vx > 0.5f)
				{
					base.IndexX = 1f;
				}
				else
				{
					base.IndexX = 0f;
				}
			}
			else
			{
				base.Mirrored = false;
				base.IndexY = 0;
				base.IndexX = (float)(base.TimeMain % 32 / 8);
			}
			if (num != (base.Mirrored ? base.IndexY : (-base.IndexY)))
			{
				base.IndexX = 0f;
			}
			if (base.IndexX > 3f)
			{
				base.IndexX = 3f;
			}
			base.TxtureObject = base.TxtureObjects[base.IndexY, (int)base.IndexX];
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00061C20 File Offset: 0x0005FE20
		private void Ctrl1()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					base.OnSpell = true;
					base.SpellTime = 3000;
				}
			}
			else
			{
				base.Boss.Armon = 0.4f;
				if (base.Time == 2)
				{
					new SpellCard_SSS04_FSC(base.StageData);
				}
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00061C93 File Offset: 0x0005FE93
		public override void GiveItems()
		{
			base.GiveItems();
			new SpellItem_Touhou(base.StageData, base.OriginalPosition);
		}
	}
}
