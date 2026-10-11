using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001C6 RID: 454
	internal class MenuGroup_Main : BaseMenuGroup
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00065710 File Offset: 0x00063910
		public MenuGroup_Main(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_Start"),
				new BaseMenuItem(StageData, "Menu_Practice"),
				new BaseMenuItem(StageData, "Menu_Replay"),
				new BaseMenuItem(StageData, "Menu_PlayerData"),
				new BaseMenuItem(StageData, "Menu_MusicRoom"),
				new BaseMenuItem(StageData, "Menu_Option"),
				new BaseMenuItem(StageData, "Menu_Manual"),
				new BaseMenuItem(StageData, "Menu_Exit")
			};
			List<ClearHistory> list = StageData.PData.C_History.FindAll((ClearHistory a) => a.NoContinueClearTimes > 0);
			if (list.Count > 0)
			{
				base.MenuItemList.Insert(1, new BaseMenuItem(StageData, "Menu_ExtraStart"));
			}
			else
			{
				base.MenuItemList.Insert(1, new BaseMenuItem(StageData, "DMenu_ExtraStart"));
			}
			int num = 50;
			int num2 = 190;
			int num3 = -200;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF((float)(num + num3), 140f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				num2 += 24;
				num = num;
				num3 -= 50;
				baseMenuItem.AngleDegree = 0.0;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00065904 File Offset: 0x00063B04
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 20;
				base.StageData.SoundPlay("se_ok00.wav");
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				base.MenuSelectIndex = base.MenuItemList.Count - 1;
				this.SelectItemChanged();
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000659D0 File Offset: 0x00063BD0
		public override void ProcessZ()
		{
			string name = base.MenuItemList[base.MenuSelectIndex].Name;
			switch (name)
			{
			case "Menu_Start":
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					SDPswitch = new StageDataPackage(base.StageData.GlobalData),
					NeedInit = true
				};
				base.StageData.MenuGroupList.Add(new MenuGroup_RankSelect(base.StageData, false));
				break;
			case "Menu_ExtraStart":
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					SDPswitch = new StageDataPackage(base.StageData.GlobalData),
					NeedInit = true
				};
				base.StageData.MenuGroupList.Add(new MenuGroup_RankSelectEx(base.StageData));
				break;
			case "Menu_Replay":
				base.StageData.MenuGroupList.Add(new MenuGroup_ReplayLoader(base.StageData, new PointF(36f, 16f)));
				break;
			case "Menu_Practice":
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					SDPswitch = new StageDataPackage(base.StageData.GlobalData),
					NeedInit = true
				};
				base.StageData.MenuGroupList.Add(new MenuGroup_RankSelect(base.StageData, true));
				break;
			case "Menu_SpellPractice":
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					SDPswitch = new StageDataPackage(base.StageData.GlobalData),
					NeedInit = true
				};
				base.StageData.MenuGroupList.Add(new MenuGroup_SCPractice(base.StageData));
				break;
			case "Menu_MusicRoom":
				base.StageData.MenuGroupList.Add(new MenuGroup_MusicRoom(base.StageData));
				break;
			case "Menu_PlayerData":
				base.StageData.MenuGroupList.Add(new MenuGroup_PlayerData(base.StageData));
				break;
			case "Menu_Manual":
				base.StageData.MenuGroupList.Add(new MenuGroup_Manual(base.StageData));
				break;
			case "Menu_Option":
				base.StageData.MenuGroupList.Add(new MenuOption(base.StageData));
				break;
			case "Menu_Exit":
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "GameExit"
				};
				break;
			}
		}
	}
}
