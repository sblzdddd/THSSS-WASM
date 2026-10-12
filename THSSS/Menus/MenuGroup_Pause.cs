using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_Pause : MenuGroup_GameOver
	{
		public MenuGroup_Pause(StageDataPackage StageData)
			: base(StageData)
		{
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			MenuItemList.Add(new BaseMenuItem(StageData, "Menu_继续游戏"));
			if (!StageData.GlobalData.LastState.StageData.OnReplay)
			{
				if (StageData.GlobalData.LastState.StageData.ContinueTimes == 0)
				{
					MenuItemList.Add(new BaseMenuItem(StageData, "Menu_保存录像"));
				}
			}
			MenuItemList.Add(new BaseMenuItem(StageData, "Menu_返回主菜单"));
			MenuItemList.Add(new BaseMenuItem(StageData, "Menu_从头开始"));
			var num = 62;
			var num2 = 300;
			var num3 = -200;
			foreach (var baseMenuItem in MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 150f);
				num2 += 30;
				num3 -= 50;
			}
			MenuTitlePos2 = new PointF(-50f, 240f);
			MenuTitlePos1 = new PointF(44f, 240f);
			MenuTilte = new BaseMenuItem(StageData, "Menu_游戏暂停")
			{
				Position = MenuTitlePos2
			};
			MenuItemList[MenuSelectIndex].Selected = true;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_C)
			{
				for (var i = 0; i < MenuItemList.Count; i++)
				{
					MenuItemList[i].Selected = false;
					if (MenuItemList[i].Name == "Menu_从头开始")
					{
						MenuItemList[i].Selected = true;
						MenuSelectIndex = i;
						MenuItemList[MenuSelectIndex].Click();
						OnChangeMenu = TimeMain + 20;
						StageData.SoundPlay("se_ok00.wav");
					}
				}
			}
		}
	}
}
