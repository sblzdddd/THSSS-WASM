using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_RankSelectEx : MenuGroup_RankSelect
	{
		public MenuGroup_RankSelectEx(StageDataPackage StageData)
			: base(StageData, false)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_Extra")
			};
			int num = 200 + base.MenuSelectIndex * 150;
			int num2 = 200 - base.MenuSelectIndex * 100;
			int num3 = -200;
			int num4 = 0;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF((float)(num + num3), 48f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Scale = 0.8f;
				baseMenuItem.Vibrateable = false;
				baseMenuItem.MaxVelocity = 30f;
				num2 += 100;
				num -= 150;
				num3 -= 100;
				num4++;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}

		public override void SelectItemChanged()
		{
			if (base.MenuSelectIndex < 0)
			{
				base.MenuSelectIndex = 0;
			}
			else if (base.MenuSelectIndex > base.MenuItemList.Count - 1)
			{
				base.MenuSelectIndex = base.MenuItemList.Count - 1;
			}
			base.LastSelectTime = base.TimeMain;
			base.MenuItemList.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			base.MenuItemList[base.MenuSelectIndex].Select();
		}

		public override void ProcessZ()
		{
			base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Extra;
			base.StageData.MenuGroupList.Add(new MenuGroup_PlayerSelectEx(base.StageData)
			{
				OriginalPosition = base.OriginalPosition
			});
		}
	}
}
