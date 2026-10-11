using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000053 RID: 83
	internal class Boss_Rika02 : BaseBossTouhou
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0001158C File Offset: 0x0000F78C
		public Boss_Rika02(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossRika-00"];
			array[0, 1] = base.TextureObjectDictionary["BossRika-01"];
			array[0, 2] = base.TextureObjectDictionary["BossRika-02"];
			array[0, 3] = base.TextureObjectDictionary["BossRika-03"];
			array[1, 0] = base.TextureObjectDictionary["BossRika-10"];
			array[1, 1] = base.TextureObjectDictionary["BossRika-11"];
			array[1, 2] = base.TextureObjectDictionary["BossRika-12"];
			array[1, 3] = base.TextureObjectDictionary["BossRika-13"];
			array[2, 0] = base.TextureObjectDictionary["BossRika-20"];
			array[2, 1] = base.TextureObjectDictionary["BossRika-21"];
			array[2, 2] = base.TextureObjectDictionary["BossRika-22"];
			array[2, 3] = base.TextureObjectDictionary["BossRika-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 120f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 1;
			base.Region = 20;
			base.Velocity = 4f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 3600;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			this.LoadArmon(".\\CS\\StEx\\关底Boss\\ctrl.csv");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000117A8 File Offset: 0x0000F9A8
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

		// Token: 0x060002A0 RID: 672 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.LifeTime = base.Time + 150;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			new EndBoss_TouhouEx(base.StageData, base.OriginalPosition, Color.White, Color.Blue);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011B1C File Offset: 0x0000FD1C
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
			else if (base.Time == 2)
			{
				new SpellCard_SSSEx_FSC(base.StageData);
			}
		}
	}
}
