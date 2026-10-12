using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_GameOver : BaseMenuGroup
	{
		public MenuGroup_GameOver(StageDataPackage StageData)
		{
			this.StageData = StageData;
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			if (!StageData.GlobalData.LastState.StageData.OnReplay)
			{
				MenuItemList.Add(new BaseMenuItem(StageData, "Menu_再试一次"));
				if (StageData.GlobalData.LastState.StageData.ContinueTimes == 0)
				{
					MenuItemList.Add(new BaseMenuItem(StageData, "Menu_保存录像"));
				}
			}
			else
			{
				StageData.OnReplay = false;
			}
			MenuItemList.Add(new BaseMenuItem(StageData, "Menu_返回主菜单"));
			MenuItemList.Add(new BaseMenuItem(StageData, "Menu_从头开始"));
			var num = 62;
			var num2 = 300;
			var num3 = -200;
			foreach (var baseMenuItem in MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 150f);
				num2 += 30;
				num3 -= 50;
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			var backgroundRotate = new BackgroundRotate(StageData, "Menu_图案");
			backgroundRotate.OriginalPosition = new PointF(120f, 340f);
			backgroundRotate.Scale = 1f;
			backgroundRotate.TransparentValueF = 0f;
			backgroundRotate.TransparentVelocity = 10f;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_Z && LastZ == 0)
			{
				MenuItemList[MenuSelectIndex].Click();
				OnChangeMenu = TimeMain + 20;
				StageData.SoundPlay("se_ok00.wav");
			}
		}

		public override void ProcessZ()
		{
			var text = MenuItemList[MenuSelectIndex].Name;
			if (text != null)
			{
				if (!(text == "Menu_继续游戏"))
				{
					if (!(text == "Menu_再试一次"))
					{
						if (!(text == "Menu_保存录像"))
						{
							if (!(text == "Menu_从头开始"))
							{
								if (text == "Menu_返回主菜单")
								{
									StageData.StateSwitchData = new StateSwitchDataPackage
									{
										NextState = "MainMenu",
										NeedInit = true,
										SDPswitch = new StageDataPackage(StageData.GlobalData)
									};
									StageData.Rep.CloseRpy();
								}
							}
							else if (StageData.GlobalData.LastState != null)
							{
								var onReplay = StageData.GlobalData.LastState.StageData.OnReplay;
								var onPractice = StageData.GlobalData.LastState.StageData.OnPractice;
								var repInfo = StageData.GlobalData.LastState.StageData.RepInfo;
								StageData.StateSwitchData = new StateSwitchDataPackage
								{
									NextState = StageData.GlobalData.LastState.StageData.RepInfo.StartStage,
									NeedInit = true,
									SDPswitch = new StageDataPackage(StageData.GlobalData)
									{
										Difficulty = repInfo.Rank,
										OnReplay = onReplay
									}
								};
								var num = 0;
								var point = new Point(192, 398);
								if (num >= 0)
								{
									point = new Point((int)repInfo.MyPlaneData[num].PosX, (int)repInfo.MyPlaneData[num].PosY);
								}
								text = repInfo.MyPlaneName;
								BaseMyPlane baseMyPlane;
								switch (text)
								{
								case "Aya":
									text = repInfo.WeaponType;
									if (text != null)
									{
										if (text == "A")
										{
											baseMyPlane = new MyPlane_Aya(StageData.StateSwitchData.SDPswitch, point);
											goto IL_040B;
										}
										if (text == "B")
										{
											baseMyPlane = new MyPlane_AyaB(StageData.StateSwitchData.SDPswitch, point);
											goto IL_040B;
										}
									}
									baseMyPlane = new MyPlane_Aya(StageData.StateSwitchData.SDPswitch, point);
									IL_040B:
									goto IL_0517;
								case "Plane":
									text = repInfo.WeaponType;
									if (text != null)
									{
										if (text == "A")
										{
											baseMyPlane = new AutoPlane(StageData.StateSwitchData.SDPswitch, point);
											goto IL_048B;
										}
										if (text == "B")
										{
											baseMyPlane = new MyPlane_PlaneB(StageData.StateSwitchData.SDPswitch, point);
											goto IL_048B;
										}
									}
									baseMyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
									IL_048B:
									goto IL_0517;
								case "Reimu":
									baseMyPlane = new MyPlane_Reimu(StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Sanae":
									baseMyPlane = new MyPlane_Sanae(StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Marisa":
									baseMyPlane = new MyPlane_Marisa(StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Koishi":
									baseMyPlane = new MyPlane_Koishi(StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								}
								baseMyPlane = new MyPlane_Aya(StageData.StateSwitchData.SDPswitch, point);
								IL_0517:
								if (num >= 0)
								{
									baseMyPlane.Life = repInfo.MyPlaneData[num].Life;
									baseMyPlane.Spell = repInfo.MyPlaneData[num].Spell;
									baseMyPlane.Power = repInfo.MyPlaneData[num].Power;
									baseMyPlane.Score = repInfo.MyPlaneData[num].Score;
									baseMyPlane.OriginalPosition = new PointF(repInfo.MyPlaneData[num].PosX, repInfo.MyPlaneData[num].PosY);
									StageData.Rep.DataPosition = repInfo.MyPlaneData[num].DataPosition;
								}
								StageData.StateSwitchData.SDPswitch.MyPlane = baseMyPlane;
								StageData.StateSwitchData.SDPswitch.OnPractice = onPractice;
								if (!onReplay)
								{
									StageData.Rep.Dispose();
									StageData.Rep.CreatRpy();
								}
								else
								{
									StageData.Rep.DataPosition = 0L;
								}
								if (!onReplay)
								{
									StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
								}
								else
								{
									StageData.StateSwitchData.SDPswitch.RepInfo = repInfo;
								}
							}
						}
						else
						{
							StageData.MenuGroupList.Add(new MenuGroup_ReplaySaver(StageData, new PointF(36f, 16f)));
						}
					}
					else if (StageData.GlobalData.LastState != null)
					{
						StageData.GlobalData.LastState.StageData.ContinueTimes++;
						StageData.GlobalData.LastState.StageData.MyPlane.RetryClear();
						StageData.StateSwitchData = new StateSwitchDataPackage
						{
							NextState = StageData.GlobalData.LastState.StageName,
							NeedInit = false
						};
						StageData.Rep.Dispose();
						StageData.Rep.CreatRpy();
					}
				}
				else
				{
					StageData.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = StageData.GlobalData.LastState.StageName,
						NeedInit = false
					};
					((BaseGameState)StageData.GlobalData.LastState).TimeSycn();
				}
			}
		}
	}
}
