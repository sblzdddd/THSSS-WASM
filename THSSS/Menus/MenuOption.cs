using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuOption : BaseMenuGroup
	{
		public MenuOption(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_BGM_Vol"),
				new BaseMenuItem(StageData, "Menu_SE_Vol"),
				new BaseMenuItem(StageData, "Menu_Quit")
			};
			int num = 220;
			int num2 = 220;
			int num3 = -200;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 140f);
				num2 += 24;
				num3 -= 50;
			}
			this.BGMvolumn = new BaseNum(StageData, new PointF(base.MenuItemList[0].DestPoint.X + 130f, base.MenuItemList[0].DestPoint.Y + 16f))
			{
				Num = base.BGM_Player.Volume,
				Count = 3
			};
			this.SEvolumn = new BaseNum(StageData, new PointF(base.MenuItemList[1].DestPoint.X + 130f, base.MenuItemList[1].DestPoint.Y + 16f))
			{
				Num = StageData.GlobalData.SEVolume,
				Count = 3
			};
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				string text = base.MenuItemList[base.MenuSelectIndex].Name;
				if (text != null)
				{
					if (text == "Menu_Quit")
					{
						this.OnRemoveMenu = base.TimeMain + 20;
						base.TransparentVelocity = -15f;
						foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
						{
							baseMenuItem.DestPoint = new PointF(60f, 160f);
							baseMenuItem.OnRemove = true;
						}
						goto IL_0122;
					}
					if (text == "Menu_BGM_Vol")
					{
						goto IL_0122;
					}
					if (text == "Menu_SE_Vol")
					{
						goto IL_0122;
					}
				}
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "此功能尚未开通",
					NeedInit = true,
					SDPswitch = base.StageData
				};
				IL_0122:
				base.StageData.SoundPlay("se_ok00.wav");
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				base.MenuSelectIndex = base.MenuItemList.Count - 1;
				this.SelectItemChanged();
				base.StageData.SoundPlay("se_cancel00.wav");
			}
			if (base.KClass.ArrowRight && (this.LastRight == 0 || this.LastRight > 30))
			{
				string text = base.MenuItemList[base.MenuSelectIndex].Name;
				if (text != null)
				{
					if (!(text == "Menu_BGM_Vol"))
					{
						if (text == "Menu_SE_Vol")
						{
							foreach (XAudio2_Player xaudio2_Player in base.StageData.SoundDictionary.Values)
							{
								xaudio2_Player.Volume += 5;
								this.SEvolumn.Num = xaudio2_Player.Volume;
							}
							base.StageData.SoundPlay("se_ok00.wav");
						}
					}
					else
					{
						base.BGM_Player.Volume += 5;
						this.BGMvolumn.Num = base.BGM_Player.Volume;
					}
				}
				base.LastSelectTime = base.TimeMain;
				base.StageData.GlobalData.BGMVolume = base.BGM_Player.Volume;
				base.StageData.GlobalData.SEVolume = this.SEvolumn.Num;
			}
			if (base.KClass.ArrowLeft && (this.LastLeft == 0 || this.LastLeft > 30))
			{
				string text = base.MenuItemList[base.MenuSelectIndex].Name;
				if (text != null)
				{
					if (!(text == "Menu_BGM_Vol"))
					{
						if (text == "Menu_SE_Vol")
						{
							foreach (XAudio2_Player xaudio2_Player in base.StageData.SoundDictionary.Values)
							{
								xaudio2_Player.Volume -= 5;
								this.SEvolumn.Num = xaudio2_Player.Volume;
							}
							base.StageData.SoundPlay("se_ok00.wav");
						}
					}
					else
					{
						base.BGM_Player.Volume -= 5;
						this.BGMvolumn.Num = base.BGM_Player.Volume;
					}
				}
				base.LastSelectTime = base.TimeMain;
				base.StageData.GlobalData.BGMVolume = base.BGM_Player.Volume;
				base.StageData.GlobalData.SEVolume = this.SEvolumn.Num;
			}
		}

		public override void Show()
		{
			base.Show();
			this.BGMvolumn.Show();
			this.SEvolumn.Show();
		}

		private BaseNum BGMvolumn;

		private BaseNum SEvolumn;
	}
}
