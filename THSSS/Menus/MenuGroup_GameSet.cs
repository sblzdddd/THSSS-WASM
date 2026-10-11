using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_GameSet : MenuGroup_GameOver
	{
		public MenuGroup_GameSet(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			if (!StageData.GlobalData.LastState.StageData.OnReplay)
			{
				if (StageData.GlobalData.LastState.StageData.ContinueTimes == 0)
				{
					base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_保存录像"));
				}
			}
			else
			{
				StageData.OnReplay = false;
			}
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_从头开始"));
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_返回主菜单"));
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
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}
	}
}
