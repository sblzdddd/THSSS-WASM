using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_Pause : MenuGroup_GameOver
	{
		public MenuGroup_Pause(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_继续游戏"));
			if (!StageData.GlobalData.LastState.StageData.OnReplay)
			{
				if (StageData.GlobalData.LastState.StageData.ContinueTimes == 0)
				{
					base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_保存录像"));
				}
			}
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_返回主菜单"));
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_从头开始"));
			int num = 62;
			int num2 = 300;
			int num3 = -200;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 150f);
				num2 += 30;
				num3 -= 50;
			}
			this.MenuTitlePos2 = new PointF(-50f, 240f);
			this.MenuTitlePos1 = new PointF(44f, 240f);
			base.MenuTilte = new BaseMenuItem(StageData, "Menu_游戏暂停")
			{
				Position = this.MenuTitlePos2
			};
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_C)
			{
				for (int i = 0; i < base.MenuItemList.Count; i++)
				{
					base.MenuItemList[i].Selected = false;
					if (base.MenuItemList[i].Name == "Menu_从头开始")
					{
						base.MenuItemList[i].Selected = true;
						base.MenuSelectIndex = i;
						base.MenuItemList[base.MenuSelectIndex].Click();
						this.OnChangeMenu = base.TimeMain + 20;
						base.StageData.SoundPlay("se_ok00.wav");
					}
				}
			}
		}
	}
}
