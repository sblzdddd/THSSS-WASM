using System.Drawing;

namespace Shooting
{
	internal class Boss_Seiryuu03 : BaseBossTouhou
	{
		public Boss_Seiryuu03(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = TextureObjectDictionary["BossSeiryuu-00"];
			array[0, 1] = TextureObjectDictionary["BossSeiryuu-01"];
			array[0, 2] = TextureObjectDictionary["BossSeiryuu-02"];
			array[0, 3] = TextureObjectDictionary["BossSeiryuu-03"];
			array[1, 0] = TextureObjectDictionary["BossSeiryuu-10"];
			array[1, 1] = TextureObjectDictionary["BossSeiryuu-11"];
			array[1, 2] = TextureObjectDictionary["BossSeiryuu-12"];
			array[1, 3] = TextureObjectDictionary["BossSeiryuu-13"];
			array[2, 0] = TextureObjectDictionary["BossSeiryuu-20"];
			array[2, 1] = TextureObjectDictionary["BossSeiryuu-21"];
			array[2, 2] = TextureObjectDictionary["BossSeiryuu-22"];
			array[2, 3] = TextureObjectDictionary["BossSeiryuu-23"];
			TxtureObjects = array;
			TxtureObject = TxtureObjects[0, 0];
			OriginalPosition = new PointF(30f, -50f);
			DestPoint = new Point(BoundRect.Width / 2, 150);
			MaxHP = 10000;
			HealthPoint = (float)MaxHP;
			Life = 3;
			Region = 20;
			Velocity = 4f;
			DirectionDegree = 90.0;
			SpellTime = 2400;
			LifeTime = 100000;
			OnSpell = false;
			SetBossLifeLineTex();
			LoadArmon(".\\CS\\StEx\\道中Boss\\ctrl.csv");
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

		public override void TextureCtrl()
		{
			int num = (Mirrored ? IndexY : (-IndexY));
			if (OnAction > 0)
			{
				IndexY = 2;
				IndexX += 0.125f;
				OnAction--;
			}
			else if (Vx < -0.05f)
			{
				Mirrored = false;
				IndexY = 1;
				if (Vx < -1f)
				{
					if (IndexX < 2f)
					{
						IndexX += 0.125f;
					}
					else
					{
						IndexX = (float)(2 + Time % 16 / 8);
					}
				}
				else if (Vx < -0.5f)
				{
					IndexX = 1f;
				}
				else
				{
					IndexX = 0f;
				}
			}
			else if (Vx > 0.05f)
			{
				Mirrored = true;
				IndexY = 1;
				if (Vx > 1f)
				{
					if (IndexX < 2f)
					{
						IndexX += 0.125f;
					}
					else
					{
						IndexX = (float)(2 + Time % 16 / 8);
					}
				}
				else if (Vx > 0.5f)
				{
					IndexX = 1f;
				}
				else
				{
					IndexX = 0f;
				}
			}
			else
			{
				Mirrored = false;
				IndexY = 0;
				IndexX = (float)(TimeMain % 32 / 8);
			}
			if (num != (Mirrored ? IndexY : (-IndexY)))
			{
				IndexX = 0f;
			}
			if (IndexX > 3f)
			{
				IndexX = 3f;
			}
			TxtureObject = TxtureObjects[IndexY, (int)IndexX];
		}

		public override void GiveEndEffect()
		{
			new EndBoss_Touhou_Small(StageData, OriginalPosition, Color.ForestGreen, Color.Green);
			new Background2DRemover(StageData);
			Background3D.WarpEnabled = false;
			new LifeItem_Touhou(StageData, OriginalPosition);
		}

		private void Ctrl3()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 180);
				}
				if (Time == 200)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else if (Time == 201)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSSEx_01(StageData);
			}
		}

		private void Ctrl2()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 180);
				}
				if (Time == 100)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else if (Time == 101)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSSEx_02(StageData);
			}
		}

		private void Ctrl1()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 180);
				}
				if (Time == 100)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else if (Time == 101)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSSEx_03(StageData);
			}
		}
	}
}
