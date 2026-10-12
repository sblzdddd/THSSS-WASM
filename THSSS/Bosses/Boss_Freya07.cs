using System.Drawing;

namespace Shooting
{
	internal class Boss_Freya07 : BaseBossTouhou
	{
		private bool ShowWings { get; set; }

		private TextureObject TxtureObject2 { get; set; }

		private TextureObject[] TxtureObjects2 { get; set; }

		private int IndexX2 { get; set; }

		public Boss_Freya07(StageDataPackage StageData)
			: base(StageData)
		{
			var array = new TextureObject[3, 4];
			array[0, 0] = TextureObjectDictionary["BossFreya-00"];
			array[0, 1] = TextureObjectDictionary["BossFreya-01"];
			array[0, 2] = TextureObjectDictionary["BossFreya-02"];
			array[0, 3] = TextureObjectDictionary["BossFreya-03"];
			array[1, 0] = TextureObjectDictionary["BossFreya-10"];
			array[1, 1] = TextureObjectDictionary["BossFreya-11"];
			array[1, 2] = TextureObjectDictionary["BossFreya-12"];
			array[1, 3] = TextureObjectDictionary["BossFreya-13"];
			array[2, 0] = TextureObjectDictionary["BossFreya-00"];
			array[2, 1] = TextureObjectDictionary["BossFreya-01"];
			array[2, 2] = TextureObjectDictionary["BossFreya-02"];
			array[2, 3] = TextureObjectDictionary["BossFreya-03"];
			TxtureObjects = array;
			TxtureObjects2 = new TextureObject[]
			{
				TextureObjectDictionary["BossFreyaWing-00"],
				TextureObjectDictionary["BossFreyaWing-01"],
				TextureObjectDictionary["BossFreyaWing-02"],
				TextureObjectDictionary["BossFreyaWing-03"],
				TextureObjectDictionary["BossFreyaWing-04"],
				TextureObjectDictionary["BossFreyaWing-05"],
				TextureObjectDictionary["BossFreyaWing-06"],
				TextureObjectDictionary["BossFreyaWing-07"]
			};
			TxtureObject = TxtureObjects[0, 0];
			TxtureObject2 = TxtureObjects2[0];
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), 150f);
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
			ShowWings = true;
			SetBossLifeLineTex();
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
			var num = (Mirrored ? IndexY : (-IndexY));
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
				IndexX2 = TimeMain % 32 / 8;
			}
			if (num != (Mirrored ? IndexY : (-IndexY)))
			{
				IndexX = 0f;
				IndexX2 = 0;
			}
			if (IndexX > 3f)
			{
				IndexX = 3f;
			}
			if (IndexX2 > 7)
			{
				IndexX2 = 7;
			}
			TxtureObject = TxtureObjects[IndexY, (int)IndexX];
			TxtureObject2 = TxtureObjects2[IndexX2];
		}

		public override void GiveEndEffect()
		{
			TransparentValueF = 255f;
			new Background2DRemover(StageData);
			Background3D.WarpEnabled = false;
			LifeTime = Time + 150;
			Region = 0;
			StageData.SoundPlay("se_tan01.wav");
			new EndBoss_Touhou01(StageData, OriginalPosition, Color.LightPink, Color.Pink);
		}

		private void Ctrl3()
		{
			if (!OnSpell)
			{
				Armon = 0f;
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 160);
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
					var cs_Data = StageData.LoadCS(text);
					var csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS02_02(StageData);
			}
		}

		private void Ctrl2()
		{
			if (!OnSpell)
			{
				ShowWings = false;
				if (Time == 100)
				{
					StageData.SoundPlay("se_boon01.wav");
					new EmitterGiveOutEnegy3D(StageData, Boss.OriginalPosition, Color.SkyBlue);
					new BackgroundWing(StageData, "Wing", Boss.OriginalPosition, 0f, 0.0, 0);
					var backgroundWing = new BackgroundWing(StageData, "Wing", Boss.OriginalPosition, 0f, 3.141592653589793, 0);
					backgroundWing.Mirrored = true;
					StageData.VibrateStart(50);
				}
				Armon = 0.3f;
				if (Time == 1)
				{
					new Background2DRemover(StageData);
					DestPoint = new Point(BoundRect.Width / 2, 160);
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
					var cs_Data = StageData.LoadCS(text);
					var csemitterController = new CSEmitterController(StageData, cs_Data);
					csemitterController.BossBinding = true;
				}
			}
			else if (Time == 1)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS02_03(StageData);
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
				else
				{
					RandomMove1(150, 4f);
				}
				if (Time == 150)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else if (Time == 151)
			{
				new BulletRemover2(StageData, OriginalPosition);
				new SpellCard_SSS02_04(StageData);
			}
		}

		public override void GiveItems()
		{
			for (var i = 0; i < 15; i++)
			{
				var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-49, 50), OriginalPosition.Y + (float)Ran.Next(-49, 50));
				new PowerItem_Touhou(StageData, pointF);
			}
			for (var i = 0; i < 20; i++)
			{
				var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-49, 50), OriginalPosition.Y + (float)Ran.Next(-49, 50));
				new ScoreItem_Touhou(StageData, pointF);
			}
		}

		public override void Show()
		{
			if (ShowWings && TxtureObject2 != null)
			{
				SpriteMain.Draw2D(TxtureObject2, ScaleWidth, ScaleLength, (float)(Angle - 1.5707963267948966), Position, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
			}
			base.Show();
		}
	}
}
