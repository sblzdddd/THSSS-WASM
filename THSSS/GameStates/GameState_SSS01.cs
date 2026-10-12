using System;
using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS01 : BaseStage, IGameState
	{
		public GameState_SSS01(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "St1";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 3180, 4000, 6440 };
			if (base.TimeMain == 1)
			{
				switch (base.StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					base.MyPlane.EnchantmentCountNeeded = 3;
					break;
				case DifficultLevel.Lunatic:
					base.MyPlane.EnchantmentCountNeeded = 4;
					break;
				}
				new TransitionIn(base.StageData);
			}
			if (base.TimeMain == 2)
			{
				if (base.StageData.C_History.PracticeLevel < 1)
				{
					base.StageData.C_History.PracticeLevel = 1;
				}
				base.StageData.ChangeBGM(".\\BGM\\Stage01.ogg", (this.testStartTime + 60) * 44100 / 60, 0, 0, 0, 0);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "宁静夏夜的微风", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Background3D.Envi = new EnvironmentSSS01(base.StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(base.StageData);
				backgroundTransitionIn.Delay = 200;
				backgroundTransitionIn.LifeTime = 320;
				new BackgroundGroupSSS01(base.StageData);
				if (this.testStartTime > base.TimeMain)
				{
					base.TimeMain = this.testStartTime;
				}
				if (base.TimeMain < array[0])
				{
					string[] array2;
					switch (base.StageData.Difficulty)
					{
					case DifficultLevel.Easy:
						array2 = Directory.GetFiles(".\\CS\\St01\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St01\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St01\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St01\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St01\\A\\L\\", "*.mbg");
						break;
					}
					foreach (string text in array2)
					{
						CS_Data cs_Data = base.StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = base.TimeMain + 60;
					}
				}
				TextTwinkle textTwinkle = new TextTwinkle(base.StageData, base.StageData.Difficulty.ToString(), new PointF((float)(base.BoundRect.X + base.BoundRect.Width / 2), (float)(base.BoundRect.Y + 16)), 0f, 1.5707963267948966);
				textTwinkle.TwinkleColor = Color.Gray;
				textTwinkle.Circle = 10;
				textTwinkle.LifeTime = 150;
			}
			else if (base.TimeMain == 30)
			{
				TextTwinkle textTwinkle2 = new TextTwinkle(base.StageData, "ItemGetBorderLine", new PointF((float)(base.BoundRect.X + base.BoundRect.Width / 2), (float)(base.BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle2.TwinkleColor = Color.OrangeRed;
				TextTwinkle textTwinkle3 = new TextTwinkle(base.StageData, "ItemGetBorderLine01", new PointF((float)(base.BoundRect.X + base.BoundRect.Width / 2 - 168), (float)(base.BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle3.TwinkleColor = Color.OrangeRed;
				TextTwinkle textTwinkle4 = new TextTwinkle(base.StageData, "ItemGetBorderLine01", new PointF((float)(base.BoundRect.X + base.BoundRect.Width / 2 + 158), (float)(base.BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle4.TwinkleColor = Color.OrangeRed;
			}
			if (base.TimeMain == array[0])
			{
				base.StageData.RemoveBullets();
				new Boss_Ami01(base.StageData);
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(40, 255, 20));
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
				base.Background3D.WarpColorKey = 0;
			}
			else if (base.TimeMain > array[1] && base.Boss == null && !base.RoadFlag)
			{
				base.RoadFlag = true;
				string[] array2;
				switch (base.StageData.Difficulty)
				{
				case DifficultLevel.Easy:
					array2 = Directory.GetFiles(".\\CS\\St01\\B\\E\\", "*.mbg");
					break;
				case DifficultLevel.Normal:
					array2 = Directory.GetFiles(".\\CS\\St01\\B\\N\\", "*.mbg");
					break;
				case DifficultLevel.Hard:
					array2 = Directory.GetFiles(".\\CS\\St01\\B\\H\\", "*.mbg");
					break;
				case DifficultLevel.Lunatic:
					array2 = Directory.GetFiles(".\\CS\\St01\\B\\L\\", "*.mbg");
					break;
				default:
					array2 = Directory.GetFiles(".\\CS\\St01\\B\\L\\", "*.mbg");
					break;
				}
				foreach (string text in array2)
				{
					CS_Data cs_Data = new CS_Data(text);
					cs_Data.String2Data(base.StageData);
					CSEmitterController csemitterController2 = new CSEmitterController(base.StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = base.TimeMain + 60;
				}
			}
			else if (array[2] - 130 == base.TimeMain)
			{
				BossStar bossStar = new BossStar(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 150f));
				bossStar.ColorType = 6;
			}
			else if (array[2] == base.TimeMain)
			{
				base.StageData.RemoveBullets();
				new Boss_Ami02(base.StageData);
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS01_01(base.StageData);
						goto IL_07AE;
					}
					if (name == "Reimu")
					{
						new Story_SSS01_01A(base.StageData);
						goto IL_07AE;
					}
					if (name == "Marisa")
					{
						new Story_SSS01_01B(base.StageData);
						goto IL_07AE;
					}
					if (name == "Sanae")
					{
						new Story_SSS01_01C(base.StageData);
						goto IL_07AE;
					}
					if (name == "Koishi")
					{
						new Story_SSS01_01D(base.StageData);
						goto IL_07AE;
					}
				}
				new Story_SSS01_01X(base.StageData);
				IL_07AE:
				base.Background3D.WarpEnabled = false;
				base.Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
