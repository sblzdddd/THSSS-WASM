using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000115 RID: 277
	internal class MenuGroup_RankSelectEx : MenuGroup_RankSelect
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00043E38 File Offset: 0x00042038
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

		// Token: 0x06000779 RID: 1913 RVA: 0x00043F74 File Offset: 0x00042174
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

		// Token: 0x0600077A RID: 1914 RVA: 0x00044020 File Offset: 0x00042220
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
