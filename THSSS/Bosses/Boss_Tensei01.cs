using System.Drawing;

namespace Shooting
{
	internal class Boss_Tensei01 : BaseBossTouhou
	{
		public Boss_Tensei01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = TextureObjectDictionary["BossTensei-00"];
			array[0, 1] = TextureObjectDictionary["BossTensei-01"];
			array[0, 2] = TextureObjectDictionary["BossTensei-02"];
			array[0, 3] = TextureObjectDictionary["BossTensei-03"];
			array[1, 0] = TextureObjectDictionary["BossTensei-10"];
			array[1, 1] = TextureObjectDictionary["BossTensei-11"];
			array[1, 2] = TextureObjectDictionary["BossTensei-12"];
			array[1, 3] = TextureObjectDictionary["BossTensei-13"];
			array[2, 0] = TextureObjectDictionary["BossTensei-20"];
			array[2, 1] = TextureObjectDictionary["BossTensei-21"];
			array[2, 2] = TextureObjectDictionary["BossTensei-22"];
			array[2, 3] = TextureObjectDictionary["BossTensei-23"];
			TxtureObjects = array;
			TxtureObject = TxtureObjects[0, 0];
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), 150f);
			DestPoint = new Point(BoundRect.Width / 2, 150);
			MaxHP = 10000;
			HealthPoint = (float)MaxHP;
			Life = 6;
			Region = 20;
			Velocity = 4f;
			DirectionDegree = 90.0;
			SpellTime = 3600;
			LifeTime = 100000;
			OnSpell = false;
			SetBossLifeLineTex();
			LoadArmon(".\\CS\\St06\\关底Boss\\ctrl.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (!OnSpell)
			{
				Armon = 0f;
			}
			switch (Life)
			{
			case 0:
				Velocity = 0f;
				DestPoint = new Point(Ran.Next(100, BoundRect.Width - 100), Ran.Next(70, 200));
				break;
			case 1:
				Ctrl1();
				break;
			case 2:
				Ctrl2();
				break;
			case 3:
				Ctrl3();
				break;
			case 4:
				Ctrl4();
				break;
			case 5:
				Ctrl5();
				break;
			case 6:
				Ctrl6();
				break;
			}
			MoveToPoint(DestPoint);
			if (!OnSpell)
			{
				if (HealthPoint < (float)SpellcardHP || Time > SpellTime)
				{
					HealthPoint = (float)SpellcardHP;
					Time = 0;
					OnSpell = true;
					new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
				}
			}
			else if (HealthPoint <= 0f || Time > SpellTime)
			{
				Life--;
				if (HealthPoint <= 0f && Life >= -1)
				{
					GiveItems();
					new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
				}
				if (Life <= 0)
				{
					if (Life == 0)
					{
						DestPoint = new Point(Ran.Next((int)OriginalPosition.X - 30, (int)OriginalPosition.X + 30), Ran.Next((int)OriginalPosition.Y + 30, (int)OriginalPosition.Y + 50));
						Velocity = 0.5f;
						new BulletRemover2(StageData, OriginalPosition);
						HealthPoint = 0f;
						GiveEndEffect();
						Life = -1;
					}
				}
				else
				{
					new BulletRemover2(StageData, OriginalPosition);
					StageData.SoundPlay("se_tan00.wav");
					HealthPoint = (float)MaxHP;
					Time = 0;
					OnSpell = false;
					DestPoint = new Point(BoundRect.Width / 2, 120);
					Velocity = 4f;
				}
			}
		}

		public override void GiveEndEffect()
		{
			TransparentValueF = 255f;
			new Background2DRemover(StageData);
			Background3D.WarpEnabled = false;
			LifeTime = Time + 150;
			Region = 0;
			StageData.SoundPlay("se_tan01.wav");
			if (MyPlane.FscEnabled)
			{
				LifeTime = Time + 50;
				new EndBoss_FSC06(StageData, OriginalPosition, Color.White, Color.Blue);
			}
			else
			{
				LifeTime = Time + 150;
				new EndBoss_Touhou06(StageData, OriginalPosition, Color.White, Color.Blue);
			}
		}

		private void Ctrl6()
		{
			if (!OnSpell)
			{
				Armon = ArmonArray[0];
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				else if (Time > 50)
				{
					RandomMove(346, 30, 2f, new Rectangle(BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (Time == 60)
				{
					string text;
					switch (Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St06\\关底Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St06\\关底Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St06\\关底Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St06\\关底Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St06\\关底Boss\\1非L.mbg";
						break;
					}
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_01(StageData);
			}
		}

		private void Ctrl5()
		{
			if (!OnSpell)
			{
				Armon = ArmonArray[2];
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				else if (Time > 50)
				{
					RandomMove(346, 30, 2f, new Rectangle(BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (Time == 60)
				{
					string text;
					switch (Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St06\\关底Boss\\2非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St06\\关底Boss\\2非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St06\\关底Boss\\2非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St06\\关底Boss\\2非L.mbg";
						break;
					default:
						text = ".\\CS\\St06\\关底Boss\\2非L.mbg";
						break;
					}
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_02(StageData);
			}
		}

		private void Ctrl4()
		{
			if (!OnSpell)
			{
				Armon = ArmonArray[4];
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				else if (Time > 150)
				{
					RandomMove(346, 30, 2f, new Rectangle(BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (Time == 160)
				{
					string text;
					switch (Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St06\\关底Boss\\3非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St06\\关底Boss\\3非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St06\\关底Boss\\3非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St06\\关底Boss\\3非L.mbg";
						break;
					default:
						text = ".\\CS\\St06\\关底Boss\\3非L.mbg";
						break;
					}
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_03(StageData);
			}
		}

		private void Ctrl3()
		{
			if (!OnSpell)
			{
				Armon = ArmonArray[6];
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				else if (Time > 50)
				{
					RandomMove(346, 30, 2f, new Rectangle(BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (Time == 60)
				{
					new TenseiWing(StageData);
					StageData.VibrateStart(90);
				}
				if (Time == 150)
				{
					string text;
					switch (Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St06\\关底Boss\\4非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St06\\关底Boss\\4非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St06\\关底Boss\\4非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St06\\关底Boss\\4非L.mbg";
						break;
					default:
						text = ".\\CS\\St06\\关底Boss\\4非L.mbg";
						break;
					}
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_04(StageData);
			}
		}

		private void Ctrl2()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				if (Time == 100)
				{
					OnSpell = true;
				}
			}
			else if (Time == 101)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_05(StageData);
			}
		}

		private void Ctrl1()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 150);
				}
				if (Time == 100)
				{
					OnSpell = true;
					SpellTime = 7200;
				}
			}
			else if (Time == 101)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS06_06(StageData);
			}
		}
	}
}
