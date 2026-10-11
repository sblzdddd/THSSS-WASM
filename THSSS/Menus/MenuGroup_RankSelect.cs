using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000013 RID: 19
	internal class MenuGroup_RankSelect : BaseMenuGroup
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00006A34 File Offset: 0x00004C34
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

		// Token: 0x06000129 RID: 297 RVA: 0x00006CE0 File Offset: 0x00004EE0
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

		// Token: 0x0600012A RID: 298 RVA: 0x00006DD4 File Offset: 0x00004FD4
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

		// Token: 0x0600012B RID: 299 RVA: 0x00006FEC File Offset: 0x000051EC
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

		// Token: 0x0600012C RID: 300 RVA: 0x00007129 File Offset: 0x00005329
		public override void Show()
		{
			base.Show();
			base.MenuItemList[base.MenuSelectIndex].Show();
		}

		// Token: 0x04000062 RID: 98
		private bool StageSelect;
	}
}
