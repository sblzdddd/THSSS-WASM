using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuOption : BaseMenuGroup
	{
		public MenuOption(StageDataPackage StageData)
			: base(StageData)
		{
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_BGM_Vol"),
				new BaseMenuItem(StageData, "Menu_SE_Vol"),
				new BaseMenuItem(StageData, "Menu_Quit")
			};
			int num = 220;
			int num2 = 220;
			int num3 = -200;
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 140f);
				num2 += 24;
				num3 -= 50;
			}
			BGMvolumn = new BaseNum(StageData, new PointF(MenuItemList[0].DestPoint.X + 130f, MenuItemList[0].DestPoint.Y + 16f))
			{
				Num = BGM_Player.Volume,
				Count = 3
			};
			SEvolumn = new BaseNum(StageData, new PointF(MenuItemList[1].DestPoint.X + 130f, MenuItemList[1].DestPoint.Y + 16f))
			{
				Num = StageData.GlobalData.SEVolume,
				Count = 3
			};
			MenuItemList[MenuSelectIndex].Selected = true;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_Z && LastZ == 0)
			{
				string text = MenuItemList[MenuSelectIndex].Name;
				if (text != null)
				{
					if (text == "Menu_Quit")
					{
						OnRemoveMenu = TimeMain + 20;
						TransparentVelocity = -15f;
						foreach (BaseMenuItem baseMenuItem in MenuItemList)
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
				StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "此功能尚未开通",
					NeedInit = true,
					SDPswitch = StageData
				};
				IL_0122:
				StageData.SoundPlay("se_ok00.wav");
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				MenuSelectIndex = MenuItemList.Count - 1;
				SelectItemChanged();
				StageData.SoundPlay("se_cancel00.wav");
			}
			if (KClass.ArrowRight && (LastRight == 0 || LastRight > 30))
			{
				string text = MenuItemList[MenuSelectIndex].Name;
				if (text != null)
				{
					if (!(text == "Menu_BGM_Vol"))
					{
						if (text == "Menu_SE_Vol")
						{
							foreach (XAudio2_Player xaudio2_Player in StageData.SoundDictionary.Values)
							{
								xaudio2_Player.Volume += 5;
								SEvolumn.Num = xaudio2_Player.Volume;
							}
							StageData.SoundPlay("se_ok00.wav");
						}
					}
					else
					{
						BGM_Player.Volume += 5;
						BGMvolumn.Num = BGM_Player.Volume;
					}
				}
				LastSelectTime = TimeMain;
				StageData.GlobalData.BGMVolume = BGM_Player.Volume;
				StageData.GlobalData.SEVolume = SEvolumn.Num;
			}
			if (KClass.ArrowLeft && (LastLeft == 0 || LastLeft > 30))
			{
				string text = MenuItemList[MenuSelectIndex].Name;
				if (text != null)
				{
					if (!(text == "Menu_BGM_Vol"))
					{
						if (text == "Menu_SE_Vol")
						{
							foreach (XAudio2_Player xaudio2_Player in StageData.SoundDictionary.Values)
							{
								xaudio2_Player.Volume -= 5;
								SEvolumn.Num = xaudio2_Player.Volume;
							}
							StageData.SoundPlay("se_ok00.wav");
						}
					}
					else
					{
						BGM_Player.Volume -= 5;
						BGMvolumn.Num = BGM_Player.Volume;
					}
				}
				LastSelectTime = TimeMain;
				StageData.GlobalData.BGMVolume = BGM_Player.Volume;
				StageData.GlobalData.SEVolume = SEvolumn.Num;
			}
		}

		public override void Show()
		{
			base.Show();
			BGMvolumn.Show();
			SEvolumn.Show();
		}

		private BaseNum BGMvolumn;

		private BaseNum SEvolumn;
	}
}
