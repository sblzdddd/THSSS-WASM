using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D9 RID: 473
	internal class Boss_Freya07 : BaseBossTouhou
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0006AA6C File Offset: 0x00068C6C
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0006AA83 File Offset: 0x00068C83
		private bool ShowWings { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0006AA8C File Offset: 0x00068C8C
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0006AAA3 File Offset: 0x00068CA3
		private TextureObject TxtureObject2 { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0006AAAC File Offset: 0x00068CAC
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0006AAC3 File Offset: 0x00068CC3
		private TextureObject[] TxtureObjects2 { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0006AACC File Offset: 0x00068CCC
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0006AAE3 File Offset: 0x00068CE3
		private int IndexX2 { get; set; }

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0006AAEC File Offset: 0x00068CEC
		public Boss_Freya07(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossFreya-00"];
			array[0, 1] = base.TextureObjectDictionary["BossFreya-01"];
			array[0, 2] = base.TextureObjectDictionary["BossFreya-02"];
			array[0, 3] = base.TextureObjectDictionary["BossFreya-03"];
			array[1, 0] = base.TextureObjectDictionary["BossFreya-10"];
			array[1, 1] = base.TextureObjectDictionary["BossFreya-11"];
			array[1, 2] = base.TextureObjectDictionary["BossFreya-12"];
			array[1, 3] = base.TextureObjectDictionary["BossFreya-13"];
			array[2, 0] = base.TextureObjectDictionary["BossFreya-00"];
			array[2, 1] = base.TextureObjectDictionary["BossFreya-01"];
			array[2, 2] = base.TextureObjectDictionary["BossFreya-02"];
			array[2, 3] = base.TextureObjectDictionary["BossFreya-03"];
			base.TxtureObjects = array;
			this.TxtureObjects2 = new TextureObject[]
			{
				base.TextureObjectDictionary["BossFreyaWing-00"],
				base.TextureObjectDictionary["BossFreyaWing-01"],
				base.TextureObjectDictionary["BossFreyaWing-02"],
				base.TextureObjectDictionary["BossFreyaWing-03"],
				base.TextureObjectDictionary["BossFreyaWing-04"],
				base.TextureObjectDictionary["BossFreyaWing-05"],
				base.TextureObjectDictionary["BossFreyaWing-06"],
				base.TextureObjectDictionary["BossFreyaWing-07"]
			};
			base.TxtureObject = base.TxtureObjects[0, 0];
			this.TxtureObject2 = this.TxtureObjects2[0];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 150f);
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
			this.ShowWings = true;
			base.SetBossLifeLineTex();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0006ADBC File Offset: 0x00068FBC
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

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0006B0CC File Offset: 0x000692CC
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
				this.IndexX2 = base.TimeMain % 32 / 8;
			}
			if (num != (base.Mirrored ? base.IndexY : (-base.IndexY)))
			{
				base.IndexX = 0f;
				this.IndexX2 = 0;
			}
			if (base.IndexX > 3f)
			{
				base.IndexX = 3f;
			}
			if (this.IndexX2 > 7)
			{
				this.IndexX2 = 7;
			}
			base.TxtureObject = base.TxtureObjects[base.IndexY, (int)base.IndexX];
			this.TxtureObject2 = this.TxtureObjects2[this.IndexX2];
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0006B380 File Offset: 0x00069580
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.LifeTime = base.Time + 150;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			new EndBoss_Touhou01(base.StageData, base.OriginalPosition, Color.LightPink, Color.Pink);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0006B3FC File Offset: 0x000695FC
		private void Ctrl3()
		{
			if (!base.OnSpell)
			{
				base.Armon = 0f;
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 160);
				}
				else if (base.Time > 50)
				{
					base.RandomMove(346, 30, 2f, new Rectangle(base.BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (base.Time == 60)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St02\\关底Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St02\\关底Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St02\\关底Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St02\\关底Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St02\\关底Boss\\1非L.mbg";
						break;
					}
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (base.Time == 1)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS02_02(base.StageData);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0006B568 File Offset: 0x00069768
		private void Ctrl2()
		{
			if (!base.OnSpell)
			{
				this.ShowWings = false;
				if (base.Time == 100)
				{
					base.StageData.SoundPlay("se_boon01.wav");
					new EmitterGiveOutEnegy3D(base.StageData, base.Boss.OriginalPosition, Color.SkyBlue);
					new BackgroundWing(base.StageData, "Wing", base.Boss.OriginalPosition, 0f, 0.0, 0);
					BackgroundWing backgroundWing = new BackgroundWing(base.StageData, "Wing", base.Boss.OriginalPosition, 0f, 3.141592653589793, 0);
					backgroundWing.Mirrored = true;
					base.StageData.VibrateStart(50);
				}
				base.Armon = 0.3f;
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 160);
				}
				else if (base.Time > 150)
				{
					base.RandomMove(346, 30, 2f, new Rectangle(base.BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (base.Time == 160)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St02\\关底Boss\\2非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St02\\关底Boss\\2非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St02\\关底Boss\\2非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St02\\关底Boss\\2非L.mbg";
						break;
					default:
						text = ".\\CS\\St02\\关底Boss\\2非L.mbg";
						break;
					}
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (base.Time == 1)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS02_03(base.StageData);
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0006B79C File Offset: 0x0006999C
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
				if (base.Time == 150)
				{
					base.OnSpell = true;
					base.SpellTime = 3000;
				}
			}
			else if (base.Time == 151)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS02_04(base.StageData);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0006B874 File Offset: 0x00069A74
		public override void GiveItems()
		{
			for (int i = 0; i < 15; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-49, 50), base.OriginalPosition.Y + (float)base.Ran.Next(-49, 50));
				new PowerItem_Touhou(base.StageData, pointF);
			}
			for (int i = 0; i < 20; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-49, 50), base.OriginalPosition.Y + (float)base.Ran.Next(-49, 50));
				new ScoreItem_Touhou(base.StageData, pointF);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0006B950 File Offset: 0x00069B50
		public override void Show()
		{
			if (this.ShowWings && this.TxtureObject2 != null)
			{
				base.SpriteMain.Draw2D(this.TxtureObject2, base.ScaleWidth, base.ScaleLength, (float)(base.Angle - 1.5707963267948966), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
			}
			base.Show();
		}
	}
}
