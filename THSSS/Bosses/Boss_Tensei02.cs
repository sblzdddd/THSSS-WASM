using System.Drawing;

namespace Shooting
{
	internal class Boss_Tensei02 : BaseBossTouhou
	{
		public Boss_Tensei02(StageDataPackage StageData)
			: base(StageData)
		{
			var array = new TextureObject[3, 4];
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
			Life = 3;
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
					if (Life > 0)
					{
						GiveItems();
						new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
					}
				}
				if (Life <= 0)
				{
					if (Life == 0)
					{
						DestPoint = new Point(Ran.Next((int)OriginalPosition.X - 30, (int)OriginalPosition.X + 30), Ran.Next((int)OriginalPosition.Y + 30, (int)OriginalPosition.Y + 50));
						Velocity = 0.5f;
						new BulletRemover2(StageData, OriginalPosition);
						GiveEndEffect();
						HealthPoint = 0f;
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
			Region = 0;
			StageData.SoundPlay("se_tan01.wav");
			LifeTime = Time + 150;
			new EndBoss_Touhou06(StageData, OriginalPosition, Color.White, Color.Blue);
		}

		private void Ctrl3()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
				}
				if (Time == 1)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else
			{
				Boss.Armon = ArmonArray[10];
				if (Time == 2)
				{
					new SpellCard_SSS06_FSC01(StageData);
				}
			}
		}

		private void Ctrl2()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
				}
				if (Time == 1)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else
			{
				Boss.Armon = ArmonArray[11];
				if (Time == 2)
				{
					new SpellCard_SSS06_FSC02(StageData);
				}
			}
		}

		private void Ctrl1()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
				}
				if (Time == 1)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else
			{
				Boss.Armon = ArmonArray[12];
				if (Time == 2)
				{
					new SpellCard_SSS06_FSC03(StageData);
				}
			}
		}
	}
}
