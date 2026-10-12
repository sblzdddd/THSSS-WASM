using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_PlayerData : BaseMenuGroup
	{
		public MenuGroup_PlayerData(StageDataPackage StageData)
			: base(StageData)
		{
			TransparentValueF = 0f;
			MaxTransparent = 255;
			TransparentVelocity = 5f;
			TxtureObject = TextureObjectDictionary["MenuBackground"];
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			AngleDegree = 90.0;
			ColorValue = Color.FromArgb(30, 110, 150);
			MenuTitlePos1 = new PointF(140f, -30f);
			MenuTitlePos2 = new PointF(140f, -150f);
			MenuTilte = new BaseMenuItem(StageData, "MenuTitle_PlayerData")
			{
				Position = MenuTitlePos2
			};
			ClearHistoryList = new List<BaseMenuItem>();
			MenuPlaneSelectIndex = 0;
			MenuItemListPlane = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "History_ReimuA"),
				new BaseMenuItem(StageData, "History_MarisaA"),
				new BaseMenuItem(StageData, "History_SanaeA"),
				new BaseMenuItem(StageData, "History_KoishiA")
			};
			var num = 90;
			var num2 = 90;
			var num3 = 0;
			foreach (var baseMenuItem in MenuItemListPlane)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), (float)num2);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
			}
			MenuItemListPlane[MenuSelectIndex].Selected = true;
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "History_Easy"),
				new BaseMenuItem(StageData, "History_Normal"),
				new BaseMenuItem(StageData, "History_Hard"),
				new BaseMenuItem(StageData, "History_Lunatic"),
				new BaseMenuItem(StageData, "History_Extra")
			};
			num = 480;
			num2 = 90;
			num3 = 0;
			foreach (var baseMenuItem in MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), (float)num2);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			ScorePageList = new List<DescriptionMenuItem>();
			AddNewScorePage();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
			ScorePageList.ForEach(delegate(DescriptionMenuItem x)
			{
				x.Ctrl();
			});
			ClearHistoryList.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_Z && LastZ == 0)
			{
				ScorePageIndex++;
				var num = ((MenuSelectIndex == 2 || MenuSelectIndex == 3) ? 5 : 4);
				if (ScorePageIndex > num)
				{
					ScorePageIndex = 0;
				}
				AddNewScorePage();
				StageData.SoundPlay("se_ok00.wav");
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (var baseMenuItem in MenuItemList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (var baseMenuItem in MenuItemListPlane)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (var baseMenuItem in ClearHistoryList)
				{
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
			if (KClass.ArrowRight && (LastRight == 0 || LastRight > 30))
			{
				MenuPlaneSelectIndex++;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.ArrowLeft && (LastLeft == 0 || LastLeft > 30))
			{
				MenuPlaneSelectIndex--;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			if (MenuPlaneSelectIndex < 0)
			{
				MenuPlaneSelectIndex += MenuItemListPlane.Count;
			}
			else if (MenuPlaneSelectIndex > MenuItemListPlane.Count - 1)
			{
				MenuPlaneSelectIndex -= MenuItemListPlane.Count;
			}
			MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			MenuItemListPlane[MenuPlaneSelectIndex].Select();
			ScorePageIndex = 0;
			AddNewScorePage();
		}

		private void AddNewScorePage()
		{
			ScorePageList.Clear();
			var list = StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(FindScore));
			var list2 = StageData.PData.SC_History.FindAll(new Predicate<SpellCardHistory>(FindSC));
			var list3 = StageData.PData.C_History.FindAll(new Predicate<ClearHistory>(FindClear));
			switch (ScorePageIndex)
			{
			case 0:
			{
				for (var i = 0; i < list.Count; i++)
				{
					ScorePageList.Add(new DescriptionMenuItem(StageData, (i + 1).ToString().PadRight(2))
					{
						Description = list[i].Data2DrawString(),
						DefaultColor = Color.FromArgb(230 - 15 * i, 230 - 12 * i, 255)
					});
				}
				var num = 50;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 1:
			{
				for (var i = 0; i < 10; i++)
				{
					if (list2[i].History > 0)
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				var num = 80;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 2:
			{
				for (var i = 10; i < 20; i++)
				{
					if (list2[i].History > 0)
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				var num = 80;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 3:
			{
				for (var i = 20; i < Math.Min(list2.Count, 30); i++)
				{
					if (list2[i].History > 0)
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				var num = 80;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 4:
			{
				for (var i = 30; i < Math.Min(list2.Count, 40); i++)
				{
					if (list2[i].History > 0)
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				var num = 80;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 5:
			{
				for (var i = 40; i < Math.Min(list2.Count, 50); i++)
				{
					if (list2[i].History > 0)
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						ScorePageList.Add(new SCMenuItem(StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				var num = 80;
				var num2 = 130;
				var num3 = 0;
				foreach (BaseMenuItem baseMenuItem in ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			}
			var num4 = 215;
			var num5 = 350;
			ClearHistoryList = new List<BaseMenuItem>();
			ClearHistoryList.Add(new SCMenuItem(StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = StageData.TextureObjectDictionary["History_游戏次数"],
				Description = list3[0].StartTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			var playingTime = list3[0].PlayingTime;
			var num6 = (int)(playingTime / 216000L);
			var num7 = (int)(playingTime % 216000L / 3600L);
			var num8 = (int)(playingTime % 3600L / 60L);
			var text = string.Concat(new string[]
			{
				num6.ToString("d2"),
				":",
				num7.ToString("d2"),
				":",
				num8.ToString("d2")
			});
			ClearHistoryList.Add(new SCMenuItem(StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = StageData.TextureObjectDictionary["History_游戏时间"],
				Description = text.PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			ClearHistoryList.Add(new SCMenuItem(StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = StageData.TextureObjectDictionary["History_通关次数"],
				Description = list3[0].ClearTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			ClearHistoryList.Add(new SCMenuItem(StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = StageData.TextureObjectDictionary["History_无续关通关次数"],
				Description = list3[0].NoContinueClearTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			num4 -= 70;
			ClearHistoryList.Add(new BaseMenuItem(StageData, "History_切换")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				Selected = true
			});
			ClearHistoryList.Add(new BaseMenuItem(StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(70f, 95f),
				DestPoint = new PointF(70f, 95f),
				Selected = true
			});
			ClearHistoryList.Add(new BaseMenuItem(StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(322f, 112f),
				DestPoint = new PointF(322f, 112f),
				Selected = true,
				AngleDegree = 180.0
			});
			ClearHistoryList.Add(new BaseMenuItem(StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(480f, 89f),
				DestPoint = new PointF(480f, 89f),
				Selected = true,
				AngleDegree = 90.0
			});
			ClearHistoryList.Add(new BaseMenuItem(StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(464f, 119f),
				DestPoint = new PointF(464f, 119f),
				Selected = true,
				AngleDegree = -90.0
			});
		}

		public override void Show()
		{
			base.Show();
			MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
			ScorePageList.ForEach(delegate(DescriptionMenuItem x)
			{
				x.Show();
			});
			ClearHistoryList.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
		}

		public bool FindScore(ScoreHistory SH)
		{
			return SH.Rank.ToString() == MenuItemList[MenuSelectIndex].Name.Replace("History_", string.Empty) && SH.MyPlaneFullName == MenuItemListPlane[MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		public bool FindClear(ClearHistory CH)
		{
			return CH.Rank.ToString() == MenuItemList[MenuSelectIndex].Name.Replace("History_", string.Empty) && CH.MyPlaneFullName == MenuItemListPlane[MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		public bool FindSC(SpellCardHistory SCH)
		{
			return SCH.Rank.ToString() == MenuItemList[MenuSelectIndex].Name.Replace("History_", string.Empty) && SCH.MyPlaneFullName == MenuItemListPlane[MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		private List<BaseMenuItem> MenuItemListPlane;

		private int MenuPlaneSelectIndex;

		private List<DescriptionMenuItem> ScorePageList;

		private int ScorePageIndex = 0;

		private List<BaseMenuItem> ClearHistoryList;
	}
}
