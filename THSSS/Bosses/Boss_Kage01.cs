using System;
using System.Drawing;

namespace Shooting
{
	internal class Boss_Kage01 : BaseBossTouhou
	{
		public Boss_Kage01(StageDataPackage StageData)
			: base(StageData)
		{
			TextureObject[,] array = new TextureObject[3, 4];
			array[0, 0] = base.TextureObjectDictionary["BossKage-00"];
			array[0, 1] = base.TextureObjectDictionary["BossKage-01"];
			array[0, 2] = base.TextureObjectDictionary["BossKage-02"];
			array[0, 3] = base.TextureObjectDictionary["BossKage-03"];
			array[1, 0] = base.TextureObjectDictionary["BossKage-10"];
			array[1, 1] = base.TextureObjectDictionary["BossKage-11"];
			array[1, 2] = base.TextureObjectDictionary["BossKage-12"];
			array[1, 3] = base.TextureObjectDictionary["BossKage-13"];
			array[2, 0] = base.TextureObjectDictionary["BossKage-20"];
			array[2, 1] = base.TextureObjectDictionary["BossKage-21"];
			array[2, 2] = base.TextureObjectDictionary["BossKage-22"];
			array[2, 3] = base.TextureObjectDictionary["BossKage-23"];
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
			base.SpellTime = 2400;
			base.LifeTime = 100000;
			base.OnSpell = false;
			base.SetBossLifeLineTex();
			this.LoadArmon(".\\CS\\St04\\关底Boss\\ctrl.csv");
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
			}
			if (num != (base.Mirrored ? base.IndexY : (-base.IndexY)))
			{
				base.IndexX = 0f;
			}
			if (base.IndexX > 3f)
			{
				base.IndexX = 3f;
			}
			base.TxtureObject = base.TxtureObjects[base.IndexY, (int)base.IndexX];
		}

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
				new EndBoss_FSC04(base.StageData, base.OriginalPosition, Color.Pink, Color.Violet);
			}
			else
			{
				base.LifeTime = base.Time + 150;
				new EndBoss_Touhou04(base.StageData, base.OriginalPosition, Color.Pink, Color.Violet);
			}
		}

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
					base.RandomMove(138, 20, 2f, new Rectangle(base.BoundRect.Width / 2 - 45, 100, 90, 40));
				}
				if (base.Time == 60)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St04\\关底Boss\\1非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St04\\关底Boss\\1非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St04\\关底Boss\\1非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St04\\关底Boss\\1非L.mbg";
						break;
					default:
						text = ".\\CS\\St04\\关底Boss\\1非L.mbg";
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
				new SpellCard_SSS04_01(base.StageData);
			}
		}

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
				else if (base.Time > 50)
				{
					base.RandomMove(138, 20, 2f, new Rectangle(base.BoundRect.Width / 2 - 45, 100, 90, 40));
				}
				if (base.Time == 60)
				{
					string text;
					switch (base.Difficulty)
					{
					case DifficultLevel.Easy:
						text = ".\\CS\\St04\\关底Boss\\2非E.mbg";
						break;
					case DifficultLevel.Normal:
						text = ".\\CS\\St04\\关底Boss\\2非N.mbg";
						break;
					case DifficultLevel.Hard:
						text = ".\\CS\\St04\\关底Boss\\2非H.mbg";
						break;
					case DifficultLevel.Lunatic:
						text = ".\\CS\\St04\\关底Boss\\2非L.mbg";
						break;
					default:
						text = ".\\CS\\St04\\关底Boss\\2非L.mbg";
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
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Reimu")
					{
						new SpellCard_SSS04_02A(base.StageData);
						goto IL_01E8;
					}
					if (name == "Marisa")
					{
						new SpellCard_SSS04_02B(base.StageData);
						goto IL_01E8;
					}
					if (name == "Sanae")
					{
						new SpellCard_SSS04_02C(base.StageData);
						goto IL_01E8;
					}
					if (name == "Koishi")
					{
						new SpellCard_SSS04_02D(base.StageData);
						goto IL_01E8;
					}
				}
				new SpellCard_SSS04_02A(base.StageData);
				IL_01E8:;
			}
		}

		private void Ctrl3()
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
					base.SpellTime = 3000;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Reimu")
					{
						new SpellCard_SSS04_03A(base.StageData);
						goto IL_0130;
					}
					if (name == "Marisa")
					{
						new SpellCard_SSS04_03B(base.StageData);
						goto IL_0130;
					}
					if (name == "Sanae")
					{
						new SpellCard_SSS04_03C(base.StageData);
						goto IL_0130;
					}
					if (name == "Koishi")
					{
						new SpellCard_SSS04_03D(base.StageData);
						goto IL_0130;
					}
				}
				new SpellCard_SSS04_03A(base.StageData);
				IL_0130:;
			}
		}

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
					base.SpellTime = 2100;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Reimu")
					{
						new SpellCard_SSS04_04A(base.StageData);
						goto IL_0130;
					}
					if (name == "Marisa")
					{
						new SpellCard_SSS04_04B(base.StageData);
						goto IL_0130;
					}
					if (name == "Sanae")
					{
						new SpellCard_SSS04_04C(base.StageData);
						goto IL_0130;
					}
					if (name == "Koishi")
					{
						new SpellCard_SSS04_04D(base.StageData);
						goto IL_0130;
					}
				}
				new SpellCard_SSS04_04A(base.StageData);
				IL_0130:;
			}
		}

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
					base.SpellTime = 1680;
				}
			}
			else if (base.Time == 101)
			{
				new BulletRemover2(base.StageData, base.OriginalPosition);
				new SpellCard_SSS04_05(base.StageData);
			}
		}
	}
}
