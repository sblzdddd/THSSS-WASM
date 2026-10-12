using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Shooting
{
	internal class MenuGroup_ReplayStageSelect : BaseMenuGroup
	{
		public MenuGroup_ReplayStageSelect(StageDataPackage StageData, PointF OriginalPosition, DescriptionMenuItem RepMenuItem)
			: base(StageData)
		{
			RepIndex = RepMenuItem.Name;
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			for (int i = 0; i < 6; i++)
			{
				MenuItemList.Add(new DescriptionMenuItem(StageData, string.Format("Stage{0:00}", i + 1))
				{
					Description = "----------"
				});
			}
			MenuItemList.Add(new DescriptionMenuItem(StageData, "StageEx")
			{
				Description = "----------"
			});
			float num = OriginalPosition.X + 12f;
			float num2 = OriginalPosition.Y + 12f;
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.Position = new PointF(num, OriginalPosition.Y + 10f);
				baseMenuItem.DestPoint = new PointF(num, num2);
				num2 += 17f;
			}
			string text = ".\\Replay\\thSSS_" + RepIndex + ".rpy";
			if (File.Exists(text))
			{
				ReplayInfo replayInfo = Replay.ReadTitle(text);
				if (replayInfo.StartStage == "StEx")
				{
					((DescriptionMenuItem)MenuItemList[6]).Description = replayInfo.MyPlaneData[1].Score.ToString().PadLeft(10);
				}
				else
				{
					int num3;
					if (replayInfo.StartStage.Contains("St"))
					{
						num3 = Convert.ToInt32(replayInfo.StartStage.Replace("St", ""));
					}
					else
					{
						num3 = Convert.ToInt32(replayInfo.StartStage.Replace("Bs", ""));
					}
					for (int i = 0; i < replayInfo.MyPlaneData.Count - 1; i++)
					{
						if (num3 - 1 + i < 6)
						{
							((DescriptionMenuItem)MenuItemList[num3 - 1 + i]).Description = replayInfo.MyPlaneData[i + 1].Score.ToString().PadLeft(10);
						}
					}
				}
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			MenuTilte = new DescriptionMenuItem(StageData, RepIndex)
			{
				Description = RepMenuItem.Description,
				OriginalPosition = RepMenuItem.OriginalPosition,
				Selected = true
			};
			MenuTitlePos1 = new PointF(RepMenuItem.OriginalPosition.X, 16f);
			MenuTitlePos2 = RepMenuItem.OriginalPosition;
			TxtureObject = TextureObjectDictionary["MenuBackground"];
			this.OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			AngleDegree = 90.0;
			ColorValue = Color.SkyBlue;
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_Z && LastZ == 0)
			{
				MenuItemList[MenuSelectIndex].Click();
				OnChangeMenu = TimeMain + 1;
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(MenuItemList[0].OriginalPosition.X, MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void ProcessZ()
		{
			if (((DescriptionMenuItem)MenuItemList[MenuSelectIndex]).Description != "----------")
			{
				StageData.SoundPlay("se_ok00.wav");
				string text = ".\\Replay\\thSSS_" + RepIndex + ".rpy";
				StageData.Rep.LoadRpy(text);
				ReplayInfo replayInfo = Replay.ReadTitle(text);
				StageData.RepInfo = replayInfo;
				string text3;
				if (MenuSelectIndex < 6)
				{
					if (replayInfo.StartStage.Contains("St"))
					{
						string text2 = "St";
						int num = MenuSelectIndex + 1;
						text3 = text2 + num.ToString();
					}
					else
					{
						string text4 = "Bs";
						int num = MenuSelectIndex + 1;
						text3 = text4 + num.ToString();
					}
				}
				else
				{
					text3 = "StEx";
				}
				StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = text3,
					NeedInit = true,
					SDPswitch = new StageDataPackage(StageData.GlobalData)
					{
						OnReplay = true,
						Difficulty = replayInfo.Rank
					}
				};
				int num2;
				if (text3 == "StEx")
				{
					num2 = 0;
				}
				else if (replayInfo.StartStage.Contains("St"))
				{
					num2 = MenuSelectIndex - Convert.ToInt32(replayInfo.StartStage.Replace("St", "")) + 1;
				}
				else
				{
					num2 = MenuSelectIndex - Convert.ToInt32(replayInfo.StartStage.Replace("Bs", "")) + 1;
				}
				Point point = new Point(192, 398);
				if (num2 >= 0)
				{
					point = new Point((int)replayInfo.MyPlaneData[num2].PosX, (int)replayInfo.MyPlaneData[num2].PosY);
				}
				string text5 = replayInfo.MyPlaneName;
				BaseMyPlane baseMyPlane;
				switch (text5)
				{
				case "Aya":
					text5 = replayInfo.WeaponType;
					if (text5 != null)
					{
						if (text5 == "A")
						{
							baseMyPlane = new MyPlane_Aya(StageData.StateSwitchData.SDPswitch, point);
							goto IL_0336;
						}
						if (text5 == "B")
						{
							baseMyPlane = new MyPlane_AyaB(StageData.StateSwitchData.SDPswitch, point);
							goto IL_0336;
						}
					}
					baseMyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
					IL_0336:
					goto IL_0442;
				case "Plane":
					text5 = replayInfo.WeaponType;
					if (text5 != null)
					{
						if (text5 == "A")
						{
							baseMyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
							goto IL_03B6;
						}
						if (text5 == "B")
						{
							baseMyPlane = new MyPlane_PlaneB(StageData.StateSwitchData.SDPswitch, point);
							goto IL_03B6;
						}
					}
					baseMyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
					IL_03B6:
					goto IL_0442;
				case "Reimu":
					baseMyPlane = new MyPlane_Reimu(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Sanae":
					baseMyPlane = new MyPlane_Sanae(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Marisa":
					baseMyPlane = new MyPlane_Marisa(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Koishi":
					baseMyPlane = new MyPlane_Koishi(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				}
				baseMyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
				IL_0442:
				if (num2 >= 0)
				{
					baseMyPlane.Life = replayInfo.MyPlaneData[num2].Life;
					baseMyPlane.Spell = replayInfo.MyPlaneData[num2].Spell;
					baseMyPlane.Power = replayInfo.MyPlaneData[num2].Power;
					baseMyPlane.Score = replayInfo.MyPlaneData[num2].Score;
					baseMyPlane.OriginalPosition = new PointF(replayInfo.MyPlaneData[num2].PosX, replayInfo.MyPlaneData[num2].PosY);
					baseMyPlane.Graze = replayInfo.MyPlaneData[num2].Graze;
					baseMyPlane.LifeUpCount = replayInfo.MyPlaneData[num2].LifeUpCount;
					baseMyPlane.LifeChip = replayInfo.MyPlaneData[num2].LifeChip;
					baseMyPlane.SpellChip = replayInfo.MyPlaneData[num2].SpellChip;
					baseMyPlane.StarPoint = replayInfo.MyPlaneData[num2].StarPoint;
					baseMyPlane.HighItemScore = replayInfo.MyPlaneData[num2].HighItemScore;
					baseMyPlane.Rate = replayInfo.MyPlaneData[num2].Rate;
					baseMyPlane.LastColor = replayInfo.MyPlaneData[num2].LastColor;
					StageData.Rep.DataPosition = replayInfo.MyPlaneData[num2].DataPosition;
				}
				StageData.StateSwitchData.SDPswitch.MyPlane = baseMyPlane;
				StageData.StateSwitchData.SDPswitch.RepInfo = replayInfo;
			}
		}

		private const string NullDescription = "----------";

		private string RepIndex;
	}
}
