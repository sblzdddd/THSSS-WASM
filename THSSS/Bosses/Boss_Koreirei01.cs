using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000032 RID: 50
	internal class Boss_Koreirei01 : BaseBossTouhou
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public Boss_Koreirei01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossKoreirei-00"];
			array[0, 1] = base.TextureObjectDictionary["BossKoreirei-01"];
			array[0, 2] = base.TextureObjectDictionary["BossKoreirei-02"];
			array[0, 3] = base.TextureObjectDictionary["BossKoreirei-03"];
			array[1, 0] = base.TextureObjectDictionary["BossKoreirei-10"];
			array[1, 1] = base.TextureObjectDictionary["BossKoreirei-11"];
			array[1, 2] = base.TextureObjectDictionary["BossKoreirei-12"];
			array[1, 3] = base.TextureObjectDictionary["BossKoreirei-13"];
			array[2, 0] = base.TextureObjectDictionary["BossKoreirei-20"];
			array[2, 1] = base.TextureObjectDictionary["BossKoreirei-21"];
			array[2, 2] = base.TextureObjectDictionary["BossKoreirei-22"];
			array[2, 3] = base.TextureObjectDictionary["BossKoreirei-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF(30f, -50f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 2;
			base.Region = 20;
			base.Velocity = 6f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 1200;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SpellcardHP = 0;
			base.SetBossLifeLineTex();
			new ItemGetter(StageData);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000CC54 File Offset: 0x0000AE54
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
				base.OnAction = 0;
				if (base.HealthPoint <= 0f && base.Life >= -1)
				{
					this.GiveItems();
					new ShootingStarShard(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 0f));
				}
				if (base.Life <= 0)
				{
					if (base.Life == 0)
					{
						base.Velocity = 0f;
						new BulletRemover2(base.StageData, base.OriginalPosition);
						base.HealthPoint = 0f;
						this.GiveEndEffect();
						base.Life = -1;
					}
					else if (base.Time == base.LifeTime - 100)
					{
						base.DestPoint = new Point(base.Ran.Next((int)base.OriginalPosition.X - 30, (int)base.OriginalPosition.X + 30), -200);
						base.Velocity = 5f;
						base.Background3D.WarpEnabled = false;
					}
				}
				else
				{
					new BulletRemover2(base.StageData, base.OriginalPosition);
					base.StageData.SoundPlay("se_tan00.wav");
					base.HealthPoint = (float)base.MaxHP;
					base.Time = 0;
					base.OnSpell = false;
					base.DestPoint = new Point(base.BoundRect.Width / 2, 100);
					base.Velocity = 4f;
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CF68 File Offset: 0x0000B168
		public override void GiveEndEffect()
		{
			new EndBoss_Touhou_Small(base.StageData, base.OriginalPosition, Color.Yellow, Color.OrangeRed);
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			new LifeItem_Touhou(base.StageData, base.OriginalPosition);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		private void Ctrl2()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 140);
				}
				else
				{
					base.RandomMove1(150, 4f);
				}
				if (base.Time == 90)
				{
					base.OnSpell = true;
				}
			}
			else if (base.Time == 91)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				base.Armon = 0.4f;
				new SpellCard_SSS03_01(base.StageData);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000D090 File Offset: 0x0000B290
		private void Ctrl1()
		{
			if (!base.OnSpell)
			{
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 180);
				}
				else
				{
					base.RandomMove1(150, 4f);
				}
				if (base.Time == 100)
				{
					base.OnSpell = true;
					base.SpellTime = 2400;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				base.Armon = 0.4f;
				new SpellCard_SSS03_02(base.StageData);
			}
		}
	}
}
