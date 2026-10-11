using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000044 RID: 68
	internal class MenuGroup_WeaponSelect : BaseMenuGroup
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000F324 File Offset: 0x0000D524
		public MenuGroup_WeaponSelect(StageDataPackage StageData, bool StageSelect, int PlaneType)
			: base(StageData)
		{
			this.StageSelect = StageSelect;
			base.MenuSelectIndex = 0;
			if (PlaneType == 1)
			{
				base.MenuItemList = new List<BaseMenuItem>();
				base.MenuItemList.Add(new CharacterMenuItem(StageData, "Weapon_PlaneA")
				{
					Scale = 0.7f,
					DestPoint1 = new PointF(270f, 150f),
					DestPoint2 = new PointF(200f, 150f),
					TransparentValueF = 0f
				});
				base.MenuItemList.Add(new CharacterMenuItem(StageData, "Weapon_PlaneB")
				{
					Scale = 0.7f,
					DestPoint1 = new PointF(270f, 150f),
					DestPoint2 = new PointF(340f, 150f),
					TransparentValueF = 0f
				});
			}
			else
			{
				base.MenuItemList = new List<BaseMenuItem>
				{
					new CharacterMenuItem(StageData, "Weapon_AyaA")
					{
						Scale = 0.7f,
						DestPoint1 = new PointF(270f, 150f),
						DestPoint2 = new PointF(200f, 150f),
						TransparentValueF = 0f
					}
				};
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.MenuTitlePos1 = new PointF(280f, 30f);
			this.MenuTitlePos2 = new PointF(280f, -100f);
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_WeaponSelect")
			{
				Position = this.MenuTitlePos2
			};
			base.TransparentValueF = 0f;
			base.MaxTransparent = 255;
			base.TransparentVelocity = 5f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F558 File Offset: 0x0000D758
		public override void ProcessKeys()
		{
			if (base.KClass.ArrowLeft && this.LastLeft == 0)
			{
				base.MenuSelectIndex++;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowRight && this.LastRight == 0)
			{
				base.MenuSelectIndex--;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 20;
				base.StageData.SoundPlay("se_ok00.wav");
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.Selected = false;
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F704 File Offset: 0x0000D904
		public override void ProcessZ()
		{
			Point point = new Point(192, 398);
			string name = base.MenuItemList[base.MenuSelectIndex].Name;
			if (name != null)
			{
				if (name == "Weapon_AyaA")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Aya(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0158;
				}
				if (name == "Weapon_AyaB")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_AyaB(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0158;
				}
				if (name == "Weapon_PlaneA")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new AutoPlane(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0158;
				}
				if (name == "Weapon_PlaneB")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_PlaneB(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0158;
				}
			}
			base.StageData.StateSwitchData = new StateSwitchDataPackage
			{
				NextState = "此功能尚未开通",
				NeedInit = true,
				SDPswitch = base.StageData
			};
			IL_0158:
			if (!this.StageSelect)
			{
				base.StageData.Rep.CreatRpy();
				base.StageData.StateSwitchData.NextState = "St1";
				base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
			}
			else
			{
				base.StageData.MenuGroupList.Add(new MenuGroup_StageSelect(base.StageData));
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public override void Show()
		{
			if (base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1] == this)
			{
				base.Show();
			}
		}

		// Token: 0x040000B8 RID: 184
		private bool StageSelect;
	}
}
