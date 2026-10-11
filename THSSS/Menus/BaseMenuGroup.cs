using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000012 RID: 18
	public class BaseMenuGroup : BaseObject
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000063F0 File Offset: 0x000045F0
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00006407 File Offset: 0x00004607
		public int MenuSelectIndex { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006410 File Offset: 0x00004610
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00006427 File Offset: 0x00004627
		public int LastSelectTime { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006430 File Offset: 0x00004630
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00006447 File Offset: 0x00004647
		public List<BaseMenuItem> MenuItemList { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006450 File Offset: 0x00004650
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00006467 File Offset: 0x00004667
		public BaseMenuItem MenuTilte { get; set; }

		// Token: 0x0600011C RID: 284 RVA: 0x00006470 File Offset: 0x00004670
		public BaseMenuGroup()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000064E8 File Offset: 0x000046E8
		public BaseMenuGroup(StageDataPackage StageData)
		{
			base.StageData = StageData;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006574 File Offset: 0x00004774
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.MenuItemList.Count > 0)
			{
				this.MenuItemList.ForEach(delegate(BaseMenuItem x)
				{
					x.Ctrl();
				});
				if (this.MenuTilte != null)
				{
					if (base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1] == this && base.TimeMain > this.OnRemoveMenu)
					{
						this.MenuTilte.DestPoint = this.MenuTitlePos1;
					}
					else
					{
						this.MenuTilte.DestPoint = this.MenuTitlePos2;
					}
					this.MenuTilte.Ctrl();
					this.MenuTilte.ColorValue = Color.White;
					this.MenuTilte.TransparentValueF = 255f;
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006674 File Offset: 0x00004874
		public virtual void MenuSelect()
		{
			if (base.TimeMain - this.LastSelectTime >= 7)
			{
				if (base.TimeMain >= this.OnChangeMenu)
				{
					if (base.TimeMain == this.OnChangeMenu)
					{
						this.ProcessZ();
					}
					else if (base.TimeMain >= this.OnRemoveMenu)
					{
						if (base.TimeMain == this.OnRemoveMenu)
						{
							this.ProcessX();
						}
						else
						{
							this.ProcessKeys();
							this.LastDown = (base.KClass.ArrowDown ? (this.LastDown + 1) : 0);
							this.LastUp = (base.KClass.ArrowUp ? (this.LastUp + 1) : 0);
							this.LastLeft = (base.KClass.ArrowLeft ? (this.LastLeft + 1) : 0);
							this.LastRight = (base.KClass.ArrowRight ? (this.LastRight + 1) : 0);
							this.LastZ = (base.KClass.Key_Z ? (this.LastZ + 1) : 0);
							this.LastX = ((base.KClass.Key_X || base.KClass.Key_ESC) ? (this.LastX + 1) : 0);
						}
					}
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000067E4 File Offset: 0x000049E4
		public virtual void ProcessKeys()
		{
			if (base.KClass.ArrowDown && (this.LastDown == 0 || this.LastDown > 30))
			{
				this.MenuSelectIndex++;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowUp && (this.LastUp == 0 || this.LastUp > 30))
			{
				this.MenuSelectIndex--;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000689C File Offset: 0x00004A9C
		public virtual void ProcessZ()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000068A0 File Offset: 0x00004AA0
		public virtual void ProcessX()
		{
			base.StageData.MenuGroupList.Remove(this);
			base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1].LastSelectTime = base.TimeMain;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000068F8 File Offset: 0x00004AF8
		public virtual void SelectItemChanged()
		{
			if (this.MenuSelectIndex < 0)
			{
				this.MenuSelectIndex += this.MenuItemList.Count;
			}
			else if (this.MenuSelectIndex > this.MenuItemList.Count - 1)
			{
				this.MenuSelectIndex -= this.MenuItemList.Count;
			}
			this.LastSelectTime = base.TimeMain;
			this.MenuItemList.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			this.MenuItemList[this.MenuSelectIndex].Select();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000069C4 File Offset: 0x00004BC4
		public override void Show()
		{
			base.Show();
			if (this.MenuItemList.Count > 0)
			{
				this.MenuItemList.ForEach(delegate(BaseMenuItem x)
				{
					x.Show();
				});
				if (this.MenuTilte != null)
				{
					this.MenuTilte.Show();
				}
			}
		}

		// Token: 0x04000051 RID: 81
		public int LastDown = 1;

		// Token: 0x04000052 RID: 82
		public int LastUp = 1;

		// Token: 0x04000053 RID: 83
		public int LastLeft = 1;

		// Token: 0x04000054 RID: 84
		public int LastRight = 1;

		// Token: 0x04000055 RID: 85
		public int LastZ = 1;

		// Token: 0x04000056 RID: 86
		public int LastX = 1;

		// Token: 0x04000057 RID: 87
		public int OnChangeMenu = 0;

		// Token: 0x04000058 RID: 88
		public int OnRemoveMenu = 0;

		// Token: 0x04000059 RID: 89
		public PointF MenuTitlePos1 = new PointF(280f, -30f);

		// Token: 0x0400005A RID: 90
		public PointF MenuTitlePos2 = new PointF(280f, -150f);
	}
}
