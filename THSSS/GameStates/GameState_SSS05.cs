using System;
using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x02000084 RID: 132
	internal class GameState_SSS05 : BaseStage, IGameState
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0001F933 File Offset: 0x0001DB33
		public GameState_SSS05(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "St5";
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 2500, 3000, 8750 };
			if (base.TimeMain == 1)
			{
				switch (base.StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					base.MyPlane.EnchantmentCountNeeded = 8;
					break;
				case DifficultLevel.Lunatic:
					base.MyPlane.EnchantmentCountNeeded = 9;
					break;
				}
			}
			if (base.TimeMain == 2)
			{
				if (base.StageData.C_History.PracticeLevel < 5)
				{
					base.StageData.C_History.PracticeLevel = 5;
				}
				base.StageData.ChangeBGM(".\\BGM\\Stage05.wav", (this.testStartTime + 60) * 44100 / 60, 0, 255, 66150, 0);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "记忆中遥远的星星", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Background3D.Envi = new EnvironmentSSS05(base.StageData);
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(base.StageData);
				backgroundTransitionIn.Delay = 200;
				backgroundTransitionIn.LifeTime = 320;
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
					foreach (string text in array2)
					{
						CS_Data cs_Data = base.StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = base.TimeMain - 60;
					}
				}
			}
			if (base.TimeMain == array[0])
			{
				new Boss_Rakuki04(base.StageData);
				base.StageData.RemoveBullets();
			}
			else if (base.TimeMain == array[0] + 90)
			{
				new Story_SSS05_01(base.StageData);
			}
			else if (base.TimeMain > array[1] && base.Boss == null && !base.RoadFlag)
			{
				base.RoadFlag = true;
				string[] array2;
				switch (base.StageData.Difficulty)
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
				foreach (string text in array2)
				{
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController2 = new CSEmitterController(base.StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = base.TimeMain - 60;
				}
			}
			if (array[2] - 130 == base.TimeMain)
			{
				BossStar bossStar = new BossStar(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 150f));
				bossStar.ColorType = 1;
			}
			else if (array[2] == base.TimeMain)
			{
				base.StageData.RemoveBullets();
				new Boss_Rakukun01(base.StageData);
				new Story_SSS05_02(base.StageData);
				base.Background3D.WarpEnabled = false;
				base.Background3D.WarpColorKey = 0;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001FE32 File Offset: 0x0001E032
		public override void BGM_ON()
		{
		}
	}
}
