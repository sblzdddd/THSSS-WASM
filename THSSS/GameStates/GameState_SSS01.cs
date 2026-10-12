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
			StageName = "St1";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 3180, 4000, 6440 };
			if (TimeMain == 1)
			{
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					MyPlane.EnchantmentCountNeeded = 3;
					break;
				case DifficultLevel.Lunatic:
					MyPlane.EnchantmentCountNeeded = 4;
					break;
				}
				new TransitionIn(StageData);
			}
			if (TimeMain == 2)
			{
				if (StageData.C_History.PracticeLevel < 1)
				{
					StageData.C_History.PracticeLevel = 1;
				}
				StageData.ChangeBGM(".\\BGM\\Stage01.ogg", (testStartTime + 60) * 44100 / 60, 0, 0, 0, 0);
				MusicTitle musicTitle = new MusicTitle(StageData, "宁静夏夜的微风", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSS01(StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 200;
				backgroundTransitionIn.LifeTime = 320;
				new BackgroundGroupSSS01(StageData);
				if (testStartTime > TimeMain)
				{
					TimeMain = testStartTime;
				}
				if (TimeMain < array[0])
				{
					string[] array2;
					switch (StageData.Difficulty)
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
						CS_Data cs_Data = StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = TimeMain + 60;
					}
				}
				TextTwinkle textTwinkle = new TextTwinkle(StageData, StageData.Difficulty.ToString(), new PointF((float)(BoundRect.X + BoundRect.Width / 2), (float)(BoundRect.Y + 16)), 0f, 1.5707963267948966);
				textTwinkle.TwinkleColor = Color.Gray;
				textTwinkle.Circle = 10;
				textTwinkle.LifeTime = 150;
			}
			else if (TimeMain == 30)
			{
				TextTwinkle textTwinkle2 = new TextTwinkle(StageData, "ItemGetBorderLine", new PointF((float)(BoundRect.X + BoundRect.Width / 2), (float)(BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle2.TwinkleColor = Color.OrangeRed;
				TextTwinkle textTwinkle3 = new TextTwinkle(StageData, "ItemGetBorderLine01", new PointF((float)(BoundRect.X + BoundRect.Width / 2 - 168), (float)(BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle3.TwinkleColor = Color.OrangeRed;
				TextTwinkle textTwinkle4 = new TextTwinkle(StageData, "ItemGetBorderLine01", new PointF((float)(BoundRect.X + BoundRect.Width / 2 + 158), (float)(BoundRect.Y + 130)), 0f, 1.5707963267948966);
				textTwinkle4.TwinkleColor = Color.OrangeRed;
			}
			if (TimeMain == array[0])
			{
				StageData.RemoveBullets();
				new Boss_Ami01(StageData);
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(40, 255, 20));
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
				Background3D.WarpColorKey = 0;
			}
			else if (TimeMain > array[1] && Boss == null && !RoadFlag)
			{
				RoadFlag = true;
				string[] array2;
				switch (StageData.Difficulty)
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
					cs_Data.String2Data(StageData);
					CSEmitterController csemitterController2 = new CSEmitterController(StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = TimeMain + 60;
				}
			}
			else if (array[2] - 130 == TimeMain)
			{
				BossStar bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 150f));
				bossStar.ColorType = 6;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Ami02(StageData);
				string name = MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS01_01(StageData);
						goto IL_07AE;
					}
					if (name == "Reimu")
					{
						new Story_SSS01_01A(StageData);
						goto IL_07AE;
					}
					if (name == "Marisa")
					{
						new Story_SSS01_01B(StageData);
						goto IL_07AE;
					}
					if (name == "Sanae")
					{
						new Story_SSS01_01C(StageData);
						goto IL_07AE;
					}
					if (name == "Koishi")
					{
						new Story_SSS01_01D(StageData);
						goto IL_07AE;
					}
				}
				new Story_SSS01_01X(StageData);
				IL_07AE:
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
