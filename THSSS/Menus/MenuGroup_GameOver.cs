using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_GameOver : BaseMenuGroup
	{
		public MenuGroup_GameOver(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			if (!StageData.GlobalData.LastState.StageData.OnReplay)
			{
				base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_再试一次"));
				if (StageData.GlobalData.LastState.StageData.ContinueTimes == 0)
				{
					base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_保存录像"));
				}
			}
			else
			{
				StageData.OnReplay = false;
			}
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_返回主菜单"));
			base.MenuItemList.Add(new BaseMenuItem(StageData, "Menu_从头开始"));
			int num = 62;
			int num2 = 300;
			int num3 = -200;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), 150f);
				num2 += 30;
				num3 -= 50;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "Menu_图案");
			backgroundRotate.OriginalPosition = new PointF(120f, 340f);
			backgroundRotate.Scale = 1f;
			backgroundRotate.TransparentValueF = 0f;
			backgroundRotate.TransparentVelocity = 10f;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 20;
				base.StageData.SoundPlay("se_ok00.wav");
			}
		}

		public override void ProcessZ()
		{
			string text = base.MenuItemList[base.MenuSelectIndex].Name;
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
									base.StageData.StateSwitchData = new StateSwitchDataPackage
									{
										NextState = "MainMenu",
										NeedInit = true,
										SDPswitch = new StageDataPackage(base.StageData.GlobalData)
									};
									base.StageData.Rep.CloseRpy();
								}
							}
							else if (base.StageData.GlobalData.LastState != null)
							{
								bool onReplay = base.StageData.GlobalData.LastState.StageData.OnReplay;
								bool onPractice = base.StageData.GlobalData.LastState.StageData.OnPractice;
								ReplayInfo repInfo = base.StageData.GlobalData.LastState.StageData.RepInfo;
								base.StageData.StateSwitchData = new StateSwitchDataPackage
								{
									NextState = base.StageData.GlobalData.LastState.StageData.RepInfo.StartStage,
									NeedInit = true,
									SDPswitch = new StageDataPackage(base.StageData.GlobalData)
									{
										Difficulty = repInfo.Rank,
										OnReplay = onReplay
									}
								};
								int num = 0;
								Point point = new Point(192, 398);
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
											baseMyPlane = new MyPlane_Aya(base.StageData.StateSwitchData.SDPswitch, point);
											goto IL_040B;
										}
										if (text == "B")
										{
											baseMyPlane = new MyPlane_AyaB(base.StageData.StateSwitchData.SDPswitch, point);
											goto IL_040B;
										}
									}
									baseMyPlane = new MyPlane_Aya(base.StageData.StateSwitchData.SDPswitch, point);
									IL_040B:
									goto IL_0517;
								case "Plane":
									text = repInfo.WeaponType;
									if (text != null)
									{
										if (text == "A")
										{
											baseMyPlane = new AutoPlane(base.StageData.StateSwitchData.SDPswitch, point);
											goto IL_048B;
										}
										if (text == "B")
										{
											baseMyPlane = new MyPlane_PlaneB(base.StageData.StateSwitchData.SDPswitch, point);
											goto IL_048B;
										}
									}
									baseMyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
									IL_048B:
									goto IL_0517;
								case "Reimu":
									baseMyPlane = new MyPlane_Reimu(base.StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Sanae":
									baseMyPlane = new MyPlane_Sanae(base.StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Marisa":
									baseMyPlane = new MyPlane_Marisa(base.StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								case "Koishi":
									baseMyPlane = new MyPlane_Koishi(base.StageData.StateSwitchData.SDPswitch, point);
									goto IL_0517;
								}
								baseMyPlane = new MyPlane_Aya(base.StageData.StateSwitchData.SDPswitch, point);
								IL_0517:
								if (num >= 0)
								{
									baseMyPlane.Life = repInfo.MyPlaneData[num].Life;
									baseMyPlane.Spell = repInfo.MyPlaneData[num].Spell;
									baseMyPlane.Power = repInfo.MyPlaneData[num].Power;
									baseMyPlane.Score = repInfo.MyPlaneData[num].Score;
									baseMyPlane.OriginalPosition = new PointF(repInfo.MyPlaneData[num].PosX, repInfo.MyPlaneData[num].PosY);
									base.StageData.Rep.DataPosition = repInfo.MyPlaneData[num].DataPosition;
								}
								base.StageData.StateSwitchData.SDPswitch.MyPlane = baseMyPlane;
								base.StageData.StateSwitchData.SDPswitch.OnPractice = onPractice;
								if (!onReplay)
								{
									base.StageData.Rep.Dispose();
									base.StageData.Rep.CreatRpy();
								}
								else
								{
									base.StageData.Rep.DataPosition = 0L;
								}
								if (!onReplay)
								{
									base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
								}
								else
								{
									base.StageData.StateSwitchData.SDPswitch.RepInfo = repInfo;
								}
							}
						}
						else
						{
							base.StageData.MenuGroupList.Add(new MenuGroup_ReplaySaver(base.StageData, new PointF(36f, 16f)));
						}
					}
					else if (base.StageData.GlobalData.LastState != null)
					{
						base.StageData.GlobalData.LastState.StageData.ContinueTimes++;
						base.StageData.GlobalData.LastState.StageData.MyPlane.RetryClear();
						base.StageData.StateSwitchData = new StateSwitchDataPackage
						{
							NextState = base.StageData.GlobalData.LastState.StageName,
							NeedInit = false
						};
						base.StageData.Rep.Dispose();
						base.StageData.Rep.CreatRpy();
					}
				}
				else
				{
					base.StageData.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = base.StageData.GlobalData.LastState.StageName,
						NeedInit = false
					};
					((BaseGameState)base.StageData.GlobalData.LastState).TimeSycn();
				}
			}
		}
	}
}
