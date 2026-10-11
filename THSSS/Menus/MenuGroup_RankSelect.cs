using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_RankSelect : BaseMenuGroup
	{
		public MenuGroup_RankSelect(StageDataPackage StageData, bool StageSelect)
			: base(StageData)
		{
			this.StageSelect = StageSelect;
			base.MenuSelectIndex = 1;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_Easy"),
				new BaseMenuItem(StageData, "Menu_Normal"),
				new BaseMenuItem(StageData, "Menu_Hard"),
				new BaseMenuItem(StageData, "Menu_Lunatic")
			};
			int num = 220 + base.MenuSelectIndex * 150;
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
			this.MenuTitlePos1 = new PointF(0f, 0f);
			this.MenuTitlePos2 = new PointF(0f, -150f);
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_RankSelect")
			{
				Position = this.MenuTitlePos2
			};
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.Scale = 0.625f;
			base.MaxScale = 0.75f;
			base.ScaleVelocity = 0.005f;
			base.DestPoint = new PointF((float)(base.BoundRect.Width / 2 + 30 * (base.MenuSelectIndex - 1)), (float)(base.BoundRect.Height / 2 - 20 * (base.MenuSelectIndex - 1)));
			base.Velocity = 4f;
			base.AngleWithDirection = false;
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			int num = 220 + base.MenuSelectIndex * 150;
			int num2 = 200 - base.MenuSelectIndex * 100;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				num2 += 100;
				num -= 150;
			}
			base.DestPoint = new PointF((float)(base.BoundRect.Width / 2 + 30 * (base.MenuSelectIndex - 1)), (float)(base.BoundRect.Height / 2 - 20 * (base.MenuSelectIndex - 1)));
			base.Velocity = 4f;
		}

		public override void ProcessKeys()
		{
			if ((base.KClass.ArrowDown && (this.LastDown == 0 || this.LastDown > 30)) || (base.KClass.ArrowLeft && (this.LastLeft == 0 || this.LastLeft > 30)))
			{
				base.MenuSelectIndex++;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if ((base.KClass.ArrowUp && (this.LastUp == 0 || this.LastUp > 30)) || (base.KClass.ArrowRight && (this.LastRight == 0 || this.LastRight > 30)))
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
					baseMenuItem.DestPoint = new PointF(0f, 0f);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void ProcessZ()
		{
			string name = base.MenuItemList[base.MenuSelectIndex].Name;
			if (name != null)
			{
				if (name == "Menu_Easy")
				{
					base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Easy;
					goto IL_010E;
				}
				if (name == "Menu_Normal")
				{
					base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Normal;
					goto IL_010E;
				}
				if (name == "Menu_Hard")
				{
					base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Hard;
					goto IL_010E;
				}
				if (name == "Menu_Lunatic")
				{
					base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Lunatic;
					goto IL_010E;
				}
				if (name == "Ultra")
				{
					base.StageData.StateSwitchData.SDPswitch.Difficulty = DifficultLevel.Ultra;
					goto IL_010E;
				}
			}
			base.StageData.StateSwitchData = new StateSwitchDataPackage
			{
				NextState = "此功能尚未开通",
				NeedInit = true,
				SDPswitch = base.StageData
			};
			IL_010E:
			base.StageData.MenuGroupList.Add(new MenuGroup_PlayerSelect(base.StageData, this.StageSelect));
		}

		public override void Show()
		{
			base.Show();
			base.MenuItemList[base.MenuSelectIndex].Show();
		}

		private bool StageSelect;
	}
}
