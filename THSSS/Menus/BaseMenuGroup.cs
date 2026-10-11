using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BaseMenuGroup : BaseObject
	{
		public int MenuSelectIndex { get; set; }

		public int LastSelectTime { get; set; }

		public List<BaseMenuItem> MenuItemList { get; set; }

		public BaseMenuItem MenuTilte { get; set; }

		public BaseMenuGroup()
		{
		}

		public BaseMenuGroup(StageDataPackage StageData)
		{
			base.StageData = StageData;
		}

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

		public virtual void ProcessZ()
		{
		}

		public virtual void ProcessX()
		{
			base.StageData.MenuGroupList.Remove(this);
			base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1].LastSelectTime = base.TimeMain;
		}

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

		public int LastDown = 1;

		public int LastUp = 1;

		public int LastLeft = 1;

		public int LastRight = 1;

		public int LastZ = 1;

		public int LastX = 1;

		public int OnChangeMenu = 0;

		public int OnRemoveMenu = 0;

		public PointF MenuTitlePos1 = new PointF(280f, -30f);

		public PointF MenuTitlePos2 = new PointF(280f, -150f);
	}
}
