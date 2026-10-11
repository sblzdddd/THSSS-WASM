using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D7 RID: 471
	internal class Boss_Seiryuu03 : BaseBossTouhou
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00069F24 File Offset: 0x00068124
		public Boss_Seiryuu03(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossSeiryuu-00"];
			array[0, 1] = base.TextureObjectDictionary["BossSeiryuu-01"];
			array[0, 2] = base.TextureObjectDictionary["BossSeiryuu-02"];
			array[0, 3] = base.TextureObjectDictionary["BossSeiryuu-03"];
			array[1, 0] = base.TextureObjectDictionary["BossSeiryuu-10"];
			array[1, 1] = base.TextureObjectDictionary["BossSeiryuu-11"];
			array[1, 2] = base.TextureObjectDictionary["BossSeiryuu-12"];
			array[1, 3] = base.TextureObjectDictionary["BossSeiryuu-13"];
			array[2, 0] = base.TextureObjectDictionary["BossSeiryuu-20"];
			array[2, 1] = base.TextureObjectDictionary["BossSeiryuu-21"];
			array[2, 2] = base.TextureObjectDictionary["BossSeiryuu-22"];
			array[2, 3] = base.TextureObjectDictionary["BossSeiryuu-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF(30f, -50f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 150);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 3;
			base.Region = 20;
			base.Velocity = 4f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 2400;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			this.LoadArmon(".\\CS\\StEx\\道中Boss\\ctrl.csv");
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0006A138 File Offset: 0x00068338
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
			case 2:
				this.Ctrl2();
				break;
			case 3:
				this.Ctrl3();
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

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0006A448 File Offset: 0x00068648
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

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0006A6B4 File Offset: 0x000688B4
		public override void GiveEndEffect()
		{
			new EndBoss_Touhou_Small(base.StageData, base.OriginalPosition, Color.ForestGreen, Color.Green);
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			new LifeItem_Touhou(base.StageData, base.OriginalPosition);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0006A70C File Offset: 0x0006890C
		private void Ctrl3()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 180);
				}
				if (base.Time == 200)
				{
					base.OnSpell = true;
					base.SpellTime = 3000;
				}
			}
			else if (base.Time == 201)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSSEx_01(base.StageData);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0006A7CC File Offset: 0x000689CC
		private void Ctrl2()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 180);
				}
				if (base.Time == 100)
				{
					base.OnSpell = true;
					base.SpellTime = 3000;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSSEx_02(base.StageData);
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0006A888 File Offset: 0x00068A88
		private void Ctrl1()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 180);
				}
				if (base.Time == 100)
				{
					base.OnSpell = true;
					base.SpellTime = 3000;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSSEx_03(base.StageData);
			}
		}
	}
}
