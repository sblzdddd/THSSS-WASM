using System.Drawing;

namespace Shooting
{
	internal class Boss_Rakuki01 : BaseBossTouhou
	{
		public Boss_Rakuki01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = TextureObjectDictionary["BossRakuki-00"];
			array[0, 1] = TextureObjectDictionary["BossRakuki-01"];
			array[0, 2] = TextureObjectDictionary["BossRakuki-02"];
			array[0, 3] = TextureObjectDictionary["BossRakuki-03"];
			array[1, 0] = TextureObjectDictionary["BossRakuki-10"];
			array[1, 1] = TextureObjectDictionary["BossRakuki-11"];
			array[1, 2] = TextureObjectDictionary["BossRakuki-12"];
			array[1, 3] = TextureObjectDictionary["BossRakuki-13"];
			array[2, 0] = TextureObjectDictionary["BossRakuki-20"];
			array[2, 1] = TextureObjectDictionary["BossRakuki-21"];
			array[2, 2] = TextureObjectDictionary["BossRakuki-22"];
			array[2, 3] = TextureObjectDictionary["BossRakuki-23"];
			TxtureObjects = array;
			TxtureObject = TxtureObjects[0, 0];
			OriginalPosition = new PointF(30f, -50f);
			DestPoint = new Point(BoundRect.Width / 2, 150);
			MaxHP = 10000;
			HealthPoint = (float)MaxHP;
			Life = 1;
			Region = 20;
			Velocity = 6f;
			DirectionDegree = 90.0;
			SpellTime = 1200;
			LifeTime = 100000;
			OnSpell = false;
			SetBossLifeLineTex();
			new ItemGetter(StageData);
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
				OnAction = 0;
				if (HealthPoint <= 0f && Life >= -1)
				{
					GiveItems();
					new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
				}
				if (Life <= 0)
				{
					if (Life == 0)
					{
						Velocity = 0f;
						new BulletRemover2(StageData, OriginalPosition);
						HealthPoint = 0f;
						GiveEndEffect();
						Life = -1;
					}
					else if (Time == LifeTime - 100)
					{
						DestPoint = new Point(Ran.Next((int)OriginalPosition.X - 30, (int)OriginalPosition.X + 30), -200);
						Velocity = 5f;
						Background3D.WarpEnabled = false;
					}
				}
				else
				{
					new BulletRemover2(StageData, OriginalPosition);
					StageData.SoundPlay("se_tan00.wav");
					HealthPoint = (float)MaxHP;
					Time = 0;
					OnSpell = false;
					DestPoint = new Point(BoundRect.Width / 2, 100);
					Velocity = 4f;
				}
			}
		}

		public override void GiveEndEffect()
		{
			new Background2DRemover(StageData);
			Background3D.WarpEnabled = false;
			LifeTime = Time + 150;
			Region = 0;
			StageData.SoundPlay("se_tan01.wav");
			new SpellItem_Touhou(StageData, OriginalPosition);
		}

		private void Ctrl1()
		{
			if (!OnSpell)
			{
				Armon = 0f;
				if (Time == 1)
				{
					DestPoint = new Point(BoundRect.Width / 2, 120);
				}
				else if (Time > 100)
				{
					RandomMove(300, 30, 2f, new Rectangle(BoundRect.Width / 2 - 60, 100, 120, 40));
				}
				if (Time == 60)
				{
					string text;
					switch (Difficulty)
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
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
					csemitterController.BaseOriginalPosition = OriginalPosition;
				}
			}
			else
			{
				Armon = 0.8f;
				if (Time == 1)
				{
					new BulletRemover2(StageData, OriginalPosition);
					Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
					Boss.Velocity = 4f;
					new SpellCard_SSS02_01(StageData);
				}
			}
		}
	}
}
