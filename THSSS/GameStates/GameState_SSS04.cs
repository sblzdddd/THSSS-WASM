using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS04 : BaseStage, IGameState
	{
		public GameState_SSS04(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "St4";
		}

		public override void Drama()
		{
			base.Drama();
			var array = new int[] { 0, 0, 8850 };
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
				if (StageData.C_History.PracticeLevel < 4)
				{
					StageData.C_History.PracticeLevel = 4;
				}
				StageData.ChangeBGM(".\\BGM\\Stage04.ogg", (testStartTime + 60) * 44100 / 60, 0, 255, 575990, 7775976);
				var musicTitle = new MusicTitle(StageData, "万花世界的光与影", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSS04(StageData);
				var backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 20;
				backgroundTransitionIn.LifeTime = 150;
				if (testStartTime > TimeMain)
				{
					TimeMain = testStartTime;
				}
				if (TimeMain < array[2])
				{
					string[] array2;
					switch (StageData.Difficulty)
					{
					case DifficultLevel.Easy:
						array2 = Directory.GetFiles(".\\CS\\St04\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St04\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St04\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St04\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St04\\A\\L\\", "*.mbg");
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
			if (array[2] - 130 == TimeMain)
			{
				var bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 150f));
				bossStar.ColorType = 2;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Kage01(StageData);
				new Story_SSS04_01(StageData);
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
