using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS05 : BaseStage, IGameState
	{
		public GameState_SSS05(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "St5";
		}

		public override void Drama()
		{
			base.Drama();
			var array = new int[] { 2500, 3000, 8750 };
			if (TimeMain == 1)
			{
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					MyPlane.EnchantmentCountNeeded = 8;
					break;
				case DifficultLevel.Lunatic:
					MyPlane.EnchantmentCountNeeded = 9;
					break;
				}
			}
			if (TimeMain == 2)
			{
				if (StageData.C_History.PracticeLevel < 5)
				{
					StageData.C_History.PracticeLevel = 5;
				}
				StageData.ChangeBGM(".\\BGM\\Stage05.ogg", (testStartTime + 60) * 44100 / 60, 0, 255, 66150, 0);
				var musicTitle = new MusicTitle(StageData, "记忆中遥远的星星", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSS05(StageData);
				var backgroundTransitionIn = new BackgroundTransitionIn(StageData);
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
						array2 = Directory.GetFiles(".\\CS\\St05\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St05\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St05\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St05\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St05\\A\\L\\", "*.mbg");
						break;
					}
					foreach (var text in array2)
					{
						var cs_Data = StageData.LoadCS(text);
						var csemitterController = new CSEmitterController(StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = TimeMain - 60;
					}
				}
			}
			if (TimeMain == array[0])
			{
				new Boss_Rakuki04(StageData);
				StageData.RemoveBullets();
			}
			else if (TimeMain == array[0] + 90)
			{
				new Story_SSS05_01(StageData);
			}
			else if (TimeMain > array[1] && Boss == null && !RoadFlag)
			{
				RoadFlag = true;
				string[] array2;
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Easy:
					array2 = Directory.GetFiles(".\\CS\\St05\\B\\E\\", "*.mbg");
					break;
				case DifficultLevel.Normal:
					array2 = Directory.GetFiles(".\\CS\\St05\\B\\N\\", "*.mbg");
					break;
				case DifficultLevel.Hard:
					array2 = Directory.GetFiles(".\\CS\\St05\\B\\H\\", "*.mbg");
					break;
				case DifficultLevel.Lunatic:
					array2 = Directory.GetFiles(".\\CS\\St05\\B\\L\\", "*.mbg");
					break;
				default:
					array2 = Directory.GetFiles(".\\CS\\St05\\B\\L\\", "*.mbg");
					break;
				}
				foreach (var text in array2)
				{
					var cs_Data = StageData.LoadCS(text);
					var csemitterController2 = new CSEmitterController(StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = TimeMain - 60;
				}
			}
			if (array[2] - 130 == TimeMain)
			{
				var bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 150f));
				bossStar.ColorType = 1;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Rakukun01(StageData);
				new Story_SSS05_02(StageData);
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
