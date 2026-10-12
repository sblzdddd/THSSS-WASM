using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSSEx : BaseStage, IGameState
	{
		public GameState_SSSEx(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "StEx";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 4920, 5060, 10260 };
			if (TimeMain == 1)
			{
				MyPlane.EnchantmentCountNeeded = 10;
			}
			if (TimeMain == 62)
			{
				if (StageData.C_History.PracticeLevel < 8)
				{
					StageData.C_History.PracticeLevel = 8;
				}
				StageData.ChangeBGM(".\\BGM\\StageEx.ogg", (testStartTime + 60) * 44100 / 60, 0, 255, 1341963, 7621362);
				MusicTitle musicTitle = new MusicTitle(StageData, "来自仙界的新风", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Background3D.Envi = new EnvironmentSSSEx(StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 0;
				backgroundTransitionIn.LifeTime = 320;
				if (testStartTime > TimeMain)
				{
					TimeMain = testStartTime;
				}
				if (TimeMain < array[0])
				{
					string[] array2 = Directory.GetFiles(".\\CS\\StEx\\A\\", "*.mbg");
					foreach (string text in array2)
					{
						CS_Data cs_Data = StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = TimeMain - 60;
					}
				}
				TextTwinkle textTwinkle = new TextTwinkle(StageData, StageData.Difficulty.ToString(), new PointF((float)(BoundRect.X + BoundRect.Width / 2), (float)(BoundRect.Y + 16)), 0f, 1.5707963267948966);
				textTwinkle.TwinkleColor = Color.Gray;
				textTwinkle.Circle = 10;
				textTwinkle.LifeTime = 150;
			}
			if (TimeMain == array[0])
			{
				StageData.RemoveBullets();
				new Boss_Seiryuu03(StageData);
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(40, 255, 20));
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
				Background3D.WarpColorKey = 2;
			}
			else if (TimeMain > array[1] && Boss == null && !RoadFlag)
			{
				RoadFlag = true;
				string[] array2 = Directory.GetFiles(".\\CS\\StEx\\B\\", "*.mbg");
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
				BossStar bossStar = new BossStar(StageData, new PointF((float)(BoundRect.Width / 2), 120f));
				bossStar.ColorType = 5;
			}
			else if (array[2] == TimeMain)
			{
				StageData.RemoveBullets();
				new Boss_Rika01(StageData);
				new Story_SSSEx_01(StageData);
				Background3D.WarpEnabled = false;
				Background3D.WarpColorKey = 2;
			}
		}

		public override void BGM_ON()
		{
		}

		private new int testStartTime = 0;
	}
}
