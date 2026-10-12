using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Shooting
{
	internal class MenuGroup_ReplaySaver : BaseMenuGroup
	{
		public MenuGroup_ReplaySaver(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			for (var i = 0; i < 25; i++)
			{
				MenuItemList.Add(new DescriptionMenuItem(StageData, string.Format("{0:00}", i + 1)));
			}
			var num = OriginalPosition.X + 12f;
			var num2 = OriginalPosition.Y + 12f;
			foreach (var baseMenuItem in MenuItemList)
			{
				baseMenuItem.Position = new PointF(num, (float)(BoundRect.Top + 10));
				baseMenuItem.DestPoint = new PointF(num, num2);
				num2 += 17f;
				((DescriptionMenuItem)baseMenuItem).Description = SetDescroption(".\\Replay\\thSSS_" + baseMenuItem.Name + ".rpy");
			}
			MenuItemList[MenuSelectIndex].Selected = true;
		}

		public virtual string SetDescroption(string fileName)
		{
			var text = "------- -------- ------ -- --- ";
			if (File.Exists(fileName))
			{
				try
				{
					var replayInfo = Replay.ReadTitle(fileName);
					if (replayInfo.Version != "ver 1.00")
					{
						return text;
					}
					string text2 = null;
					if (replayInfo != null)
					{
						text2 += ((replayInfo.PlayerName == null) ? "        " : replayInfo.PlayerName.PadRight(8));
						text2 += ((replayInfo.Date == null) ? "       " : replayInfo.Date.PadRight(9));
						text2 += ((replayInfo.MyPlaneName == null) ? "               " : replayInfo.MyPlaneName.PadRight(7));
						switch (replayInfo.Rank)
						{
						case DifficultLevel.Easy:
							text2 += "E  ";
							break;
						case DifficultLevel.Normal:
							text2 += "N  ";
							break;
						case DifficultLevel.Hard:
							text2 += "H  ";
							break;
						case DifficultLevel.Lunatic:
							text2 += "L  ";
							break;
						case DifficultLevel.Ultra:
							text2 += "U  ";
							break;
						case DifficultLevel.Extra:
							text2 += "EX ";
							break;
						}
						text2 += ((replayInfo.LastStage == null) ? "   " : replayInfo.LastStage.PadRight(4));
					}
					return text2;
				}
				catch
				{
					return text;
				}
			}
			return text;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			ProcessKeys2();
		}

		public virtual void ProcessKeys2()
		{
			if (KClass.Key_Z && LastZ == 0)
			{
				MenuItemList[MenuSelectIndex].Click();
				OnChangeMenu = TimeMain + 1;
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				StageData.SoundPlay("se_cancel00.wav");
				if (StageData.Rep.CanRead)
				{
					var repInfo = StageData.GlobalData.LastState.StageData.RepInfo;
					repInfo.PlayerName = "autosave";
					StageData.Rep.SaveRpy(".\\Replay\\AutoSave.rpy", repInfo);
				}
				StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "MainMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(StageData.GlobalData)
				};
				StageData.Rep.CloseRpy();
			}
		}

		public override void ProcessZ()
		{
			if (StageData.Rep.CanRead)
			{
				StageData.SoundPlay("se_ok00.wav");
				StageData.GlobalData.LastState.StageData.SetReplayStageInfo();
				StageData.MenuGroupList.Add(new MenuGroup_ReplayNamer(StageData, MenuItemList[0].OriginalPosition, (DescriptionMenuItem)MenuItemList[MenuSelectIndex]));
			}
		}
	}
}
