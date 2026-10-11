using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D6 RID: 470
	internal class Boss_Rakuki03 : BaseBossTouhou
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x00069904 File Offset: 0x00067B04
		public Boss_Rakuki03(StageDataPackage StageData)
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

		// Token: 0x06000ABD RID: 2749 RVA: 0x00069B18 File Offset: 0x00067D18
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

		// Token: 0x06000ABE RID: 2750 RVA: 0x00069E18 File Offset: 0x00068018
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			base.LifeTime = base.Time + 150;
			new EndBoss_Touhou02(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00069E94 File Offset: 0x00068094
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
					new SpellCard_SSS02_FSC(base.StageData);
				}
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00069F07 File Offset: 0x00068107
		public override void GiveItems()
		{
			base.GiveItems();
			new SpellItem_Touhou(base.StageData, base.OriginalPosition);
		}
	}
}
