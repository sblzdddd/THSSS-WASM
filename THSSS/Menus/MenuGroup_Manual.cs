using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001FB RID: 507
	internal class MenuGroup_Manual : BaseMenuGroup
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00071948 File Offset: 0x0006FB48
		public MenuGroup_Manual(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_Manual01"),
				new BaseMenuItem(StageData, "Menu_Manual02"),
				new BaseMenuItem(StageData, "Menu_Manual03"),
				new BaseMenuItem(StageData, "Menu_Manual04"),
				new BaseMenuItem(StageData, "Menu_Manual05"),
				new BaseMenuItem(StageData, "Menu_Manual06")
			};
			int num = 20;
			int num2 = 85;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF((float)num, 90f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				num2 += 30;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.MenuItemListManual = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Manual01"),
				new BaseMenuItem(StageData, "Manual02"),
				new BaseMenuItem(StageData, "Manual03"),
				new BaseMenuItem(StageData, "Manual04"),
				new BaseMenuItem(StageData, "Manual05"),
				new BaseMenuItem(StageData, "Manual06")
			};
			foreach (BaseMenuItem baseMenuItem in this.MenuItemListManual)
			{
				baseMenuItem.Position = new PointF(0f, 0f);
				baseMenuItem.DestPoint = new PointF(0f, 0f);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
				baseMenuItem.Twinkleable = false;
				baseMenuItem.TransparentValueF = 0f;
			}
			this.MenuItemListManual[base.MenuSelectIndex].Selected = true;
			base.TransparentValueF = 0f;
			base.MaxTransparent = 255;
			base.TransparentVelocity = 5f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.FromArgb(30, 110, 150);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00071C3C File Offset: 0x0006FE3C
		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			this.MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			this.MenuItemListManual[base.MenuSelectIndex].Select();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00071C9C File Offset: 0x0006FE9C
		public override void Ctrl()
		{
			base.Ctrl();
			this.MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00071CD4 File Offset: 0x0006FED4
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in this.MenuItemListManual)
				{
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00071E0D File Offset: 0x0007000D
		public override void Show()
		{
			base.Show();
			this.MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
		}

		// Token: 0x04000434 RID: 1076
		private List<BaseMenuItem> MenuItemListManual;
	}
}
