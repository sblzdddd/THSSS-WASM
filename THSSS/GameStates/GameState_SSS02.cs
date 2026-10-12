using System;
using System.Drawing;
using System.IO;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class GameState_SSS02 : BaseStage, IGameState
	{
		public GameState_SSS02(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "St2";
		}

		public override void Drama()
		{
			base.Drama();
			int[] array = new int[] { 4680, 5000, 7890 };
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
				if (base.StageData.C_History.PracticeLevel < 2)
				{
					base.StageData.C_History.PracticeLevel = 2;
				}
				base.StageData.ChangeBGM(".\\BGM\\Stage02.ogg", (this.testStartTime + 60) * 44100 / 60, 0, 255, 1194228, 6866370);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "灯火竹林", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Background3D.Envi = new EnvironmentSSS02(base.StageData);
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
						array2 = Directory.GetFiles(".\\CS\\St02\\A\\E\\", "*.mbg");
						break;
					case DifficultLevel.Normal:
						array2 = Directory.GetFiles(".\\CS\\St02\\A\\N\\", "*.mbg");
						break;
					case DifficultLevel.Hard:
						array2 = Directory.GetFiles(".\\CS\\St02\\A\\H\\", "*.mbg");
						break;
					case DifficultLevel.Lunatic:
						array2 = Directory.GetFiles(".\\CS\\St02\\A\\L\\", "*.mbg");
						break;
					default:
						array2 = Directory.GetFiles(".\\CS\\St02\\A\\L\\", "*.mbg");
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
				new Boss_Rakuki01(base.StageData);
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
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
					array2 = Directory.GetFiles(".\\CS\\St02\\B\\E\\", "*.mbg");
					break;
				case DifficultLevel.Normal:
					array2 = Directory.GetFiles(".\\CS\\St02\\B\\N\\", "*.mbg");
					break;
				case DifficultLevel.Hard:
					array2 = Directory.GetFiles(".\\CS\\St02\\B\\H\\", "*.mbg");
					break;
				case DifficultLevel.Lunatic:
					array2 = Directory.GetFiles(".\\CS\\St02\\B\\L\\", "*.mbg");
					break;
				default:
					array2 = Directory.GetFiles(".\\CS\\St02\\B\\L\\", "*.mbg");
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
				BossStar bossStar = new BossStar(base.StageData, new PointF((float)(base.BoundRect.Width / 2), 120f));
				bossStar.ColorType = 5;
			}
			else if (array[2] == base.TimeMain)
			{
				base.StageData.RemoveBullets();
				new Boss_Rakuki02(base.StageData);
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS02_01(base.StageData);
						goto IL_059D;
					}
					if (name == "Reimu")
					{
						new Story_SSS02_01A(base.StageData);
						goto IL_059D;
					}
					if (name == "Marisa")
					{
						new Story_SSS02_01B(base.StageData);
						goto IL_059D;
					}
					if (name == "Sanae")
					{
						new Story_SSS02_01C(base.StageData);
						goto IL_059D;
					}
					if (name == "Koishi")
					{
						new Story_SSS02_01D(base.StageData);
						goto IL_059D;
					}
				}
				new Story_SSS02_01X(base.StageData);
				IL_059D:
				base.Background3D.WarpEnabled = false;
				base.Background3D.WarpColorKey = 2;
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
