using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Shooting
{
	// Token: 0x0200014F RID: 335
	internal class MenuGroup_ReplayStageSelect : BaseMenuGroup
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x0004F9AC File Offset: 0x0004DBAC
		public MenuGroup_ReplayStageSelect(StageDataPackage StageData, PointF OriginalPosition, DescriptionMenuItem RepMenuItem)
			: base(StageData)
		{
			this.RepIndex = RepMenuItem.Name;
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			for (int i = 0; i < 6; i++)
			{
				base.MenuItemList.Add(new DescriptionMenuItem(StageData, string.Format("Stage{0:00}", i + 1))
				{
					Description = "----------"
				});
			}
			base.MenuItemList.Add(new DescriptionMenuItem(StageData, "StageEx")
			{
				Description = "----------"
			});
			float num = OriginalPosition.X + 12f;
			float num2 = OriginalPosition.Y + 12f;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF(num, OriginalPosition.Y + 10f);
				baseMenuItem.DestPoint = new PointF(num, num2);
				num2 += 17f;
			}
			string text = ".\\Replay\\thSSS_" + this.RepIndex + ".rpy";
			if (File.Exists(text))
			{
				ReplayInfo replayInfo = Replay.ReadTitle(text);
				if (replayInfo.StartStage == "StEx")
				{
					((DescriptionMenuItem)base.MenuItemList[6]).Description = replayInfo.MyPlaneData[1].Score.ToString().PadLeft(10);
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
							((DescriptionMenuItem)base.MenuItemList[num3 - 1 + i]).Description = replayInfo.MyPlaneData[i + 1].Score.ToString().PadLeft(10);
						}
					}
				}
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			base.MenuTilte = new DescriptionMenuItem(StageData, this.RepIndex)
			{
				Description = RepMenuItem.Description,
				OriginalPosition = RepMenuItem.OriginalPosition,
				Selected = true
			};
			this.MenuTitlePos1 = new PointF(RepMenuItem.OriginalPosition.X, 16f);
			this.MenuTitlePos2 = RepMenuItem.OriginalPosition;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.SkyBlue;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 1;
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(base.MenuItemList[0].OriginalPosition.X, base.MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0004FE6C File Offset: 0x0004E06C
		public override void ProcessZ()
		{
			if (((DescriptionMenuItem)base.MenuItemList[base.MenuSelectIndex]).Description != "----------")
			{
				base.StageData.SoundPlay("se_ok00.wav");
				string text = ".\\Replay\\thSSS_" + this.RepIndex + ".rpy";
				base.StageData.Rep.LoadRpy(text);
				ReplayInfo replayInfo = Replay.ReadTitle(text);
				base.StageData.RepInfo = replayInfo;
				string text3;
				if (base.MenuSelectIndex < 6)
				{
					if (replayInfo.StartStage.Contains("St"))
					{
						string text2 = "St";
						int num = base.MenuSelectIndex + 1;
						text3 = text2 + num.ToString();
					}
					else
					{
						string text4 = "Bs";
						int num = base.MenuSelectIndex + 1;
						text3 = text4 + num.ToString();
					}
				}
				else
				{
					text3 = "StEx";
				}
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = text3,
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.StageData.GlobalData)
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
					num2 = base.MenuSelectIndex - Convert.ToInt32(replayInfo.StartStage.Replace("St", "")) + 1;
				}
				else
				{
					num2 = base.MenuSelectIndex - Convert.ToInt32(replayInfo.StartStage.Replace("Bs", "")) + 1;
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
							baseMyPlane = new MyPlane_Aya(base.StageData.StateSwitchData.SDPswitch, point);
							goto IL_0336;
						}
						if (text5 == "B")
						{
							baseMyPlane = new MyPlane_AyaB(base.StageData.StateSwitchData.SDPswitch, point);
							goto IL_0336;
						}
					}
					baseMyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
					IL_0336:
					goto IL_0442;
				case "Plane":
					text5 = replayInfo.WeaponType;
					if (text5 != null)
					{
						if (text5 == "A")
						{
							baseMyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
							goto IL_03B6;
						}
						if (text5 == "B")
						{
							baseMyPlane = new MyPlane_PlaneB(base.StageData.StateSwitchData.SDPswitch, point);
							goto IL_03B6;
						}
					}
					baseMyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
					IL_03B6:
					goto IL_0442;
				case "Reimu":
					baseMyPlane = new MyPlane_Reimu(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Sanae":
					baseMyPlane = new MyPlane_Sanae(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Marisa":
					baseMyPlane = new MyPlane_Marisa(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				case "Koishi":
					baseMyPlane = new MyPlane_Koishi(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0442;
				}
				baseMyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
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
					base.StageData.Rep.DataPosition = replayInfo.MyPlaneData[num2].DataPosition;
				}
				base.StageData.StateSwitchData.SDPswitch.MyPlane = baseMyPlane;
				base.StageData.StateSwitchData.SDPswitch.RepInfo = replayInfo;
			}
		}

		// Token: 0x0400031B RID: 795
		private const string NullDescription = "----------";

		// Token: 0x0400031C RID: 796
		private string RepIndex;
	}
}
