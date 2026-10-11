using System;
using System.Drawing;

namespace Shooting
{
	internal class Boss_Ami02 : BaseBossTouhou
	{
		public Boss_Ami02(StageDataPackage StageData)
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
			base.Life = 2;
			base.Region = 20;
			base.Velocity = 4f;
			base.DirectionDegree = 90.0;
			base.SpellTime = 2400;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
		}

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

		public override void GiveEndEffect()
		{
			base.TransparentValueF = 255f;
			new Background2DRemover(base.StageData);
			base.Background3D.WarpEnabled = false;
			base.Region = 0;
			base.StageData.SoundPlay("se_tan01.wav");
			if (base.MyPlane.FscEnabled)
			{
				base.LifeTime = base.Time + 50;
				new EndBoss_FSC01(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
			}
			else
			{
				base.LifeTime = base.Time + 150;
				new EndBoss_Touhou01(base.StageData, base.OriginalPosition, Color.YellowGreen, Color.Green);
			}
		}

		private void Ctrl2()
		{
			if (!base.OnSpell)
			{
				base.Armon = 0.3f;
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 150);
					base.Velocity = 4f;
				}
				else if (base.Time > 50)
				{
					base.RandomMove(346, 30, 2f, new Rectangle(base.BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (base.Time == 10)
				{
					new EmitterSaveEnegy3D(base.StageData, base.OriginalPosition, Color.FromArgb(255, 255, 155));
					base.StageData.SoundPlay("se_ch02.wav");
				}
				if (base.Time == 60)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St01\\关底Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St01\\关底Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St01\\关底Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St01\\关底Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St01\\关底Boss\\1非L.mbg";
						break;
					}
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
					csemitterController.BossBinding = true;
					csemitterController.BaseOriginalPosition = base.OriginalPosition;
				}
			}
			else if (base.Time == 1)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS01_02(base.StageData);
			}
		}

		private void Ctrl1()
		{
			if (!base.OnSpell)
			{
				base.Armon = 0.32f;
				if (base.Time == 1)
				{
					new Background2DRemover(base.StageData);
					base.DestPoint = new Point(base.BoundRect.Width / 2, 100);
					base.Velocity = 4f;
				}
				else if (base.Time > 150)
				{
					base.RandomMove(346, 30, 2f, new Rectangle(base.BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (base.Time == 90)
				{
					new EmitterSaveEnegy3D(base.StageData, base.OriginalPosition, Color.FromArgb(255, 255, 155));
					base.StageData.SoundPlay("se_ch02.wav");
				}
				if (base.Time == 150)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St01\\关底Boss\\2非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St01\\关底Boss\\2非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St01\\关底Boss\\2非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St01\\关底Boss\\2非L.mbg";
						break;
					default:
						text = ".\\CS\\St01\\关底Boss\\2非L.mbg";
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
				base.Armon = 0.86f;
				if (base.Time == 1)
				{
					new BulletRemover2(base.StageData, base.OriginalPosition);
					new SpellCard_SSS01_03(base.StageData);
				}
			}
		}
	}
}
