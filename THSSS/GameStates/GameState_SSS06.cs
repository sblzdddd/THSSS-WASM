using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS06 : BaseStage, IGameState
	{
		public GameState_SSS06(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "St6";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 2550, 3000, 8550 };
			if (TimeMain == 1)
			{
				switch (StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					MyPlane.EnchantmentCountNeeded = 6;
					break;
				case DifficultLevel.Lunatic:
					MyPlane.EnchantmentCountNeeded = 7;
					break;
				}
			}
			if (TimeMain == 2)
			{
				if (StageData.C_History.PracticeLevel < 6)
				{
					StageData.C_History.PracticeLevel = 6;
				}
				StageData.ChangeBGM(".\\BGM\\Stage06.ogg", (testStartTime + 60) * 44100 / 60, 0, 255, 0, 6220746);
				MusicTitle musicTitle = new MusicTitle(StageData, "银河的彼方", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSS06(StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 30;
				backgroundTransitionIn.LifeTime = 250;
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
						array2 = Directory.GetFiles(".\\CS\\St06\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St06\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St06\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St06\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St06\\A\\L\\", "*.mbg");
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
			if (array[2] - 130 == TimeMain)
			{
				BossStar bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 150f));
				bossStar.ColorType = 4;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Tensei01(StageData);
				new Story_SSS06_01(StageData);
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 0;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
