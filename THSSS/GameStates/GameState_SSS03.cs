using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS03 : BaseStage, IGameState
	{
		public GameState_SSS03(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "St3";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 3360, 3800, 7300 };
			if (TimeMain == 1)
			{
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					MyPlane.EnchantmentCountNeeded = 5;
					break;
				case DifficultLevel.Lunatic:
					MyPlane.EnchantmentCountNeeded = 6;
					break;
				}
			}
			if (TimeMain == 2)
			{
				if (StageData.C_History.PracticeLevel < 3)
				{
					StageData.C_History.PracticeLevel = 3;
				}
				StageData.ChangeBGM(".\\BGM\\Stage03.ogg", (testStartTime + 60) * 44100 / 60, 0, 255, 88200, 5606874);
				MusicTitle musicTitle = new MusicTitle(StageData, "木灵们的夏夜祭", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSS03(StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 200;
				backgroundTransitionIn.LifeTime = 320;
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
						array2 = Directory.GetFiles(".\\CS\\St03\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St03\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St03\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St03\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St03\\A\\L\\", "*.mbg");
						break;
					}
					foreach (string text in array2)
					{
						CS_Data cs_Data = StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = TimeMain - 60;
					}
				}
			}
			if (TimeMain == array[0])
			{
				StageData.RemoveBullets();
				new Boss_Koreirei01(StageData);
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.OrangeRed);
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
				Background3D.WarpColorKey = 2;
			}
			else if (TimeMain > array[1] && Boss == null && !RoadFlag)
			{
				RoadFlag = true;
				string[] array2;
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Easy:
					array2 = Directory.GetFiles(".\\CS\\St03\\B\\E\\", "*.mbg");
					break;
				case DifficultLevel.Normal:
					array2 = Directory.GetFiles(".\\CS\\St03\\B\\N\\", "*.mbg");
					break;
				case DifficultLevel.Hard:
					array2 = Directory.GetFiles(".\\CS\\St03\\B\\H\\", "*.mbg");
					break;
				case DifficultLevel.Lunatic:
					array2 = Directory.GetFiles(".\\CS\\St03\\B\\L\\", "*.mbg");
					break;
				default:
					array2 = Directory.GetFiles(".\\CS\\St03\\B\\L\\", "*.mbg");
					break;
				}
				foreach (string text in array2)
				{
					CS_Data cs_Data = StageData.LoadCS(text);
					CSEmitterController csemitterController2 = new CSEmitterController(StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = TimeMain - 60;
				}
			}
			else if (array[2] - 130 == TimeMain)
			{
				BossStar bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 150f));
				bossStar.ColorType = 5;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Seiryuu01(StageData);
				string name = MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS03_01(StageData);
						goto IL_058E;
					}
					if (name == "Reimu")
					{
						new Story_SSS03_01A(StageData);
						goto IL_058E;
					}
					if (name == "Marisa")
					{
						new Story_SSS03_01B(StageData);
						goto IL_058E;
					}
					if (name == "Sanae")
					{
						new Story_SSS03_01C(StageData);
						goto IL_058E;
					}
					if (name == "Koishi")
					{
						new Story_SSS03_01D(StageData);
						goto IL_058E;
					}
				}
				new Story_SSS03_01X(StageData);
				IL_058E:
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
