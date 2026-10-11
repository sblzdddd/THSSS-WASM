using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Shooting
{
	// Token: 0x02000042 RID: 66
	internal class MenuGroup_ReplaySaver : BaseMenuGroup
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000EA7C File Offset: 0x0000CC7C
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

		// Token: 0x06000255 RID: 597 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
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

		// Token: 0x06000256 RID: 598 RVA: 0x0000ED58 File Offset: 0x0000CF58
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			this.ProcessKeys2();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000ED6C File Offset: 0x0000CF6C
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

		// Token: 0x06000258 RID: 600 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
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
