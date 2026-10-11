using System;
using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x020000AD RID: 173
	internal class GameState_SSS03 : BaseStage, IGameState
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x00028AD9 File Offset: 0x00026CD9
		public GameState_SSS03(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "St3";
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00028B04 File Offset: 0x00026D04
		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 3360, 3800, 7300 };
			if (base.TimeMain == 1)
			{
				switch (base.StageData.Difficulty)
				{
				case DifficultLevel.Hard:
					base.MyPlane.EnchantmentCountNeeded = 5;
					break;
				case DifficultLevel.Lunatic:
					base.MyPlane.EnchantmentCountNeeded = 6;
					break;
				}
			}
			if (base.TimeMain == 2)
			{
				if (base.StageData.C_History.PracticeLevel < 3)
				{
					base.StageData.C_History.PracticeLevel = 3;
				}
				base.StageData.ChangeBGM(".\\BGM\\Stage03.wav", (this.testStartTime + 60) * 44100 / 60, 0, 255, 88200, 5606874);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "木灵们的夏夜祭", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Background3D.Envi = new EnvironmentSSS03(base.StageData);
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
						CS_Data cs_Data = base.StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = base.TimeMain - 60;
					}
				}
			}
			if (base.TimeMain == array[0])
			{
				base.StageData.RemoveBullets();
				new Boss_Koreirei01(base.StageData);
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.OrangeRed);
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
				base.Background3D.WarpColorKey = 2;
			}
			else if (base.TimeMain > array[1] && base.Boss == null && !base.RoadFlag)
			{
				base.RoadFlag = true;
				string[] array2;
				switch (base.StageData.Difficulty)
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
					CS_Data cs_Data = base.StageData.LoadCS(text);
					CSEmitterController csemitterController2 = new CSEmitterController(base.StageData, cs_Data);
					csemitterController2.OnRoad = true;
					csemitterController2.Time = base.TimeMain - 60;
				}
			}
			else if (array[2] - 130 == base.TimeMain)
			{
				BossStar bossStar = new BossStar(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 150f));
				bossStar.ColorType = 5;
			}
			else if (array[2] == base.TimeMain)
			{
				base.StageData.RemoveBullets();
				new Boss_Seiryuu01(base.StageData);
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS03_01(base.StageData);
						goto IL_058E;
					}
					if (name == "Reimu")
					{
						new Story_SSS03_01A(base.StageData);
						goto IL_058E;
					}
					if (name == "Marisa")
					{
						new Story_SSS03_01B(base.StageData);
						goto IL_058E;
					}
					if (name == "Sanae")
					{
						new Story_SSS03_01C(base.StageData);
						goto IL_058E;
					}
					if (name == "Koishi")
					{
						new Story_SSS03_01D(base.StageData);
						goto IL_058E;
					}
				}
				new Story_SSS03_01X(base.StageData);
				IL_058E:
				base.Background3D.WarpEnabled = false;
				base.Background3D.WarpColorKey = 0;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000290BA File Offset: 0x000272BA
		public override void BGM_ON()
		{
		}
	}
}
