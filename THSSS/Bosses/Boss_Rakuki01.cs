using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000208 RID: 520
	internal class Boss_Rakuki01 : BaseBossTouhou
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x00072D08 File Offset: 0x00070F08
		public Boss_Rakuki01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossRakuki-00"];
			array[0, 1] = base.TextureObjectDictionary["BossRakuki-01"];
			array[0, 2] = base.TextureObjectDictionary["BossRakuki-02"];
			array[0, 3] = base.TextureObjectDictionary["BossRakuki-03"];
			array[1, 0] = base.TextureObjectDictionary["BossRakuki-10"];
			array[1, 1] = base.TextureObjectDictionary["BossRakuki-11"];
			array[1, 2] = base.TextureObjectDictionary["BossRakuki-12"];
			array[1, 3] = base.TextureObjectDictionary["BossRakuki-13"];
			array[2, 0] = base.TextureObjectDictionary["BossRakuki-20"];
			array[2, 1] = base.TextureObjectDictionary["BossRakuki-21"];
			array[2, 2] = base.TextureObjectDictionary["BossRakuki-22"];
			array[2, 3] = base.TextureObjectDictionary["BossRakuki-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF(30f, -50f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 150);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 1;
			base.Region = 20;
			base.Velocity = 6f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 1200;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			new ItemGetter(StageData);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00072F14 File Offset: 0x00071114
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

		// Token: 0x06000B97 RID: 2967 RVA: 0x0007321C File Offset: 0x0007141C
		public override void GiveEndEffect()
		{
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.LifeTime = base.Time + 150;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			new SpellItem_Touhou(base.StageData, base.OriginalPosition);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00073284 File Offset: 0x00071484
		private void Ctrl1()
		{
			if (!base.OnSpell)
			{
				base.Armon = 0f;
				if (base.Time == 1)
				{
					base.DestPoint = new Point(base.BoundRect.Width / 2, 120);
				}
				else if (base.Time > 100)
				{
					base.RandomMove(300, 30, 2f, new Rectangle(base.BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (base.Time == 60)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St02\\道中Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St02\\道中Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St02\\道中Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St02\\道中Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St02\\道中Boss\\1非L.mbg";
						break;
					}
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
					csemitterController.BossBinding = true;
					csemitterController.BaseOriginalPosition = base.OriginalPosition;
				}
			}
			else
			{
				base.Armon = 0.8f;
				if (base.Time == 1)
				{
					new BulletRemover2(base.StageData, base.OriginalPosition);
					base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
					base.Boss.Velocity = 4f;
					new SpellCard_SSS02_01(base.StageData);
				}
			}
		}
	}
}
