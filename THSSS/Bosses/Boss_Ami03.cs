using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000033 RID: 51
	internal class Boss_Ami03 : BaseBossTouhou
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000D170 File Offset: 0x0000B370
		public Boss_Ami03(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossAmi-00"];
			array[0, 1] = base.TextureObjectDictionary["BossAmi-01"];
			array[0, 2] = base.TextureObjectDictionary["BossAmi-02"];
			array[0, 3] = base.TextureObjectDictionary["BossAmi-03"];
			array[1, 0] = base.TextureObjectDictionary["BossAmi-10"];
			array[1, 1] = base.TextureObjectDictionary["BossAmi-11"];
			array[1, 2] = base.TextureObjectDictionary["BossAmi-12"];
			array[1, 3] = base.TextureObjectDictionary["BossAmi-13"];
			array[2, 0] = base.TextureObjectDictionary["BossAmi-20"];
			array[2, 1] = base.TextureObjectDictionary["BossAmi-21"];
			array[2, 2] = base.TextureObjectDictionary["BossAmi-22"];
			array[2, 3] = base.TextureObjectDictionary["BossAmi-23"];
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
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D384 File Offset: 0x0000B584
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

		// Token: 0x06000223 RID: 547 RVA: 0x0000D684 File Offset: 0x0000B884
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			base.LifeTime = base.Time + 150;
			new EndBoss_Touhou01(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D700 File Offset: 0x0000B900
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
					new SpellCard_SSS01_FSC(base.StageData);
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D773 File Offset: 0x0000B973
		public override void GiveItems()
		{
			base.GiveItems();
			new SpellItem_Touhou(base.StageData, base.OriginalPosition);
		}
	}
}
