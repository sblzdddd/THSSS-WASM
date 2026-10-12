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
			base.OriginalPosition = OriginalPosition;
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			for (int i = 0; i < 25; i++)
			{
				base.MenuItemList.Add(new DescriptionMenuItem(StageData, string.Format("{0:00}", i + 1)));
			}
			float num = OriginalPosition.X + 12f;
			float num2 = OriginalPosition.Y + 12f;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF(num, (float)(base.BoundRect.Top + 10));
				baseMenuItem.DestPoint = new PointF(num, num2);
				num2 += 17f;
				((DescriptionMenuItem)baseMenuItem).Description = this.SetDescroption(".\\Replay\\thSSS_" + baseMenuItem.Name + ".rpy");
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}

		public virtual string SetDescroption(string fileName)
		{
			string text = "------- -------- ------ -- --- ";
			if (File.Exists(fileName))
			{
				try
				{
					ReplayInfo replayInfo = Replay.ReadTitle(fileName);
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
			this.ProcessKeys2();
		}

		public virtual void ProcessKeys2()
		{
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 1;
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				base.StageData.SoundPlay("se_cancel00.wav");
				if (base.StageData.Rep.CanRead)
				{
					ReplayInfo repInfo = base.StageData.GlobalData.LastState.StageData.RepInfo;
					repInfo.PlayerName = "autosave";
					base.StageData.Rep.SaveRpy(".\\Replay\\AutoSave.rpy", repInfo);
				}
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "MainMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.StageData.GlobalData)
				};
				base.StageData.Rep.CloseRpy();
			}
		}

		public override void ProcessZ()
		{
			if (base.StageData.Rep.CanRead)
			{
				base.StageData.SoundPlay("se_ok00.wav");
				base.StageData.GlobalData.LastState.StageData.SetReplayStageInfo();
				base.StageData.MenuGroupList.Add(new MenuGroup_ReplayNamer(base.StageData, base.MenuItemList[0].OriginalPosition, (DescriptionMenuItem)base.MenuItemList[base.MenuSelectIndex]));
			}
		}
	}
}
