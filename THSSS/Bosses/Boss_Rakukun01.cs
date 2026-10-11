using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200007C RID: 124
	internal class Boss_Rakukun01 : BaseBossTouhou
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001D68C File Offset: 0x0001B88C
		public Boss_Rakukun01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossRakukun-00"];
			array[0, 1] = base.TextureObjectDictionary["BossRakukun-01"];
			array[0, 2] = base.TextureObjectDictionary["BossRakukun-02"];
			array[0, 3] = base.TextureObjectDictionary["BossRakukun-03"];
			array[1, 0] = base.TextureObjectDictionary["BossRakukun-10"];
			array[1, 1] = base.TextureObjectDictionary["BossRakukun-11"];
			array[1, 2] = base.TextureObjectDictionary["BossRakukun-12"];
			array[1, 3] = base.TextureObjectDictionary["BossRakuki-13"];
			array[2, 0] = base.TextureObjectDictionary["BossRakukun-20"];
			array[2, 1] = base.TextureObjectDictionary["BossRakukun-21"];
			array[2, 2] = base.TextureObjectDictionary["BossRakukun-22"];
			array[2, 3] = base.TextureObjectDictionary["BossRakukun-23"];
			base.TxtureObjects = array;
			base.TxtureObject = base.TxtureObjects[0, 0];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 150f);
			base.DestPoint = new Point(base.BoundRect.Width / 2, 150);
			base.MaxHP = 10000;
			base.HealthPoint = (float)base.MaxHP;
			base.Life = 5;
			base.Region = 20;
			base.Velocity = 4f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 3600;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			this.LoadArmon(".\\CS\\St05\\关底Boss\\ctrl.csv");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001D8AC File Offset: 0x0001BAAC
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
			case 4:
				this.Ctrl4();
				break;
			case 5:
				this.Ctrl5();
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

		// Token: 0x0600042B RID: 1067 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.LifeTime = base.Time + 150;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			if (base.MyPlane.FscEnabled)
			{
				base.LifeTime = base.Time + 50;
				new EndBoss_FSC05(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
			}
			else
			{
				base.LifeTime = base.Time + 150;
				new EndBoss_Touhou05(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		private void Ctrl5()
		{
			if (!base.OnSpell)
			{
				base.Armon = base.ArmonArray[0];
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
						text = ".\\CS\\St05\\关底Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St05\\关底Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St05\\关底Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St05\\关底Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St05\\关底Boss\\1非L.mbg";
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
				new SpellCard_SSS05_01(base.StageData);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001DE18 File Offset: 0x0001C018
		private void Ctrl4()
		{
			if (!base.OnSpell)
			{
				base.Armon = base.ArmonArray[2];
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
						text = ".\\CS\\St05\\关底Boss\\2非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St05\\关底Boss\\2非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St05\\关底Boss\\2非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St05\\关底Boss\\2非L.mbg";
						break;
					default:
						text = ".\\CS\\St05\\关底Boss\\2非L.mbg";
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
				new SpellCard_SSS05_02(base.StageData);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001DF8C File Offset: 0x0001C18C
		private void Ctrl3()
		{
			if (!base.OnSpell)
			{
				base.Armon = base.ArmonArray[4];
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
						text = ".\\CS\\St05\\关底Boss\\3非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St05\\关底Boss\\3非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St05\\关底Boss\\3非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St05\\关底Boss\\3非L.mbg";
						break;
					default:
						text = ".\\CS\\St05\\关底Boss\\3非L.mbg";
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
				new SpellCard_SSS05_03(base.StageData);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
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
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS05_04(base.StageData);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
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
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS05_05(base.StageData);
			}
		}
	}
}
