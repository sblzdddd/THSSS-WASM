using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B6 RID: 182
	internal class MenuGroup_PlayerData : BaseMenuGroup
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0002A018 File Offset: 0x00028218
		public MenuGroup_PlayerData(StageDataPackage StageData)
			: base(StageData)
		{
			base.TransparentValueF = 0f;
			base.MaxTransparent = 255;
			base.TransparentVelocity = 5f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.FromArgb(30, 110, 150);
			this.MenuTitlePos1 = new PointF(140f, -30f);
			this.MenuTitlePos2 = new PointF(140f, -150f);
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_PlayerData")
			{
				Position = this.MenuTitlePos2
			};
			this.ClearHistoryList = new List<BaseMenuItem>();
			this.MenuPlaneSelectIndex = 0;
			this.MenuItemListPlane = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "History_ReimuA"),
				new BaseMenuItem(StageData, "History_MarisaA"),
				new BaseMenuItem(StageData, "History_SanaeA"),
				new BaseMenuItem(StageData, "History_KoishiA")
			};
			int num = 90;
			int num2 = 90;
			int num3 = 0;
			foreach (BaseMenuItem baseMenuItem in this.MenuItemListPlane)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), (float)num2);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
			}
			this.MenuItemListPlane[base.MenuSelectIndex].Selected = true;
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
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
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.Position = new PointF((float)(num + num3), (float)num2);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.ScorePageList = new List<DescriptionMenuItem>();
			this.AddNewScorePage();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002A35C File Offset: 0x0002855C
		public override void Ctrl()
		{
			base.Ctrl();
			this.MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
			this.ScorePageList.ForEach(delegate(DescriptionMenuItem x)
			{
				x.Ctrl();
			});
			this.ClearHistoryList.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002A3F4 File Offset: 0x000285F4
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				this.ScorePageIndex++;
				int num = ((base.MenuSelectIndex == 2 || base.MenuSelectIndex == 3) ? 5 : 4);
				if (this.ScorePageIndex > num)
				{
					this.ScorePageIndex = 0;
				}
				this.AddNewScorePage();
				base.StageData.SoundPlay("se_ok00.wav");
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in this.MenuItemListPlane)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in this.ClearHistoryList)
				{
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
			if (base.KClass.ArrowRight && (this.LastRight == 0 || this.LastRight > 30))
			{
				this.MenuPlaneSelectIndex++;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowLeft && (this.LastLeft == 0 || this.LastLeft > 30))
			{
				this.MenuPlaneSelectIndex--;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0002A6C0 File Offset: 0x000288C0
		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			if (this.MenuPlaneSelectIndex < 0)
			{
				this.MenuPlaneSelectIndex += this.MenuItemListPlane.Count;
			}
			else if (this.MenuPlaneSelectIndex > this.MenuItemListPlane.Count - 1)
			{
				this.MenuPlaneSelectIndex -= this.MenuItemListPlane.Count;
			}
			this.MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			this.MenuItemListPlane[this.MenuPlaneSelectIndex].Select();
			this.ScorePageIndex = 0;
			this.AddNewScorePage();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002A788 File Offset: 0x00028988
		private void AddNewScorePage()
		{
			this.ScorePageList.Clear();
			List<ScoreHistory> list = base.StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(this.FindScore));
			List<SpellCardHistory> list2 = base.StageData.PData.SC_History.FindAll(new Predicate<SpellCardHistory>(this.FindSC));
			List<ClearHistory> list3 = base.StageData.PData.C_History.FindAll(new Predicate<ClearHistory>(this.FindClear));
			switch (this.ScorePageIndex)
			{
			case 0:
			{
				for (int i = 0; i < list.Count; i++)
				{
					this.ScorePageList.Add(new DescriptionMenuItem(base.StageData, (i + 1).ToString().PadRight(2))
					{
						Description = list[i].Data2DrawString(),
						DefaultColor = Color.FromArgb(230 - 15 * i, 230 - 12 * i, 255)
					});
				}
				int num = 50;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 1:
			{
				for (int i = 0; i < 10; i++)
				{
					if (list2[i].History > 0)
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				int num = 80;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 2:
			{
				for (int i = 10; i < 20; i++)
				{
					if (list2[i].History > 0)
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				int num = 80;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 3:
			{
				for (int i = 20; i < Math.Min(list2.Count, 30); i++)
				{
					if (list2[i].History > 0)
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				int num = 80;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 4:
			{
				for (int i = 30; i < Math.Min(list2.Count, 40); i++)
				{
					if (list2[i].History > 0)
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				int num = 80;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			case 5:
			{
				for (int i = 40; i < Math.Min(list2.Count, 50); i++)
				{
					if (list2[i].History > 0)
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["欧" + list2[i].Name],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
					else
					{
						this.ScorePageList.Add(new SCMenuItem(base.StageData, list2[i].Index.ToString().PadRight(3))
						{
							Description = (list2[i].Recorded.ToString().PadLeft(2) + "/" + list2[i].History.ToString().PadLeft(2)).PadLeft(40),
							TxtureObject = base.StageData.TextureObjectDictionary["???"],
							DefaultColor = ((list2[i].Recorded > 0) ? Color.SkyBlue : Color.White)
						});
					}
				}
				int num = 80;
				int num2 = 130;
				int num3 = 0;
				foreach (BaseMenuItem baseMenuItem in this.ScorePageList)
				{
					baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
					baseMenuItem.Position = new PointF((float)(num + num3), 100f);
					num2 += 20;
				}
				break;
			}
			}
			int num4 = 215;
			int num5 = 350;
			this.ClearHistoryList = new List<BaseMenuItem>();
			this.ClearHistoryList.Add(new SCMenuItem(base.StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = base.StageData.TextureObjectDictionary["History_游戏次数"],
				Description = list3[0].StartTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			long playingTime = list3[0].PlayingTime;
			int num6 = (int)(playingTime / 216000L);
			int num7 = (int)(playingTime % 216000L / 3600L);
			int num8 = (int)(playingTime % 3600L / 60L);
			string text = string.Concat(new string[]
			{
				num6.ToString("d2"),
				":",
				num7.ToString("d2"),
				":",
				num8.ToString("d2")
			});
			this.ClearHistoryList.Add(new SCMenuItem(base.StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = base.StageData.TextureObjectDictionary["History_游戏时间"],
				Description = text.PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			this.ClearHistoryList.Add(new SCMenuItem(base.StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = base.StageData.TextureObjectDictionary["History_通关次数"],
				Description = list3[0].ClearTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			this.ClearHistoryList.Add(new SCMenuItem(base.StageData, "")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				OffX = 0,
				OffY = -5,
				TxtureObject = base.StageData.TextureObjectDictionary["History_无续关通关次数"],
				Description = list3[0].NoContinueClearTimes.ToString().PadLeft(18, ' '),
				DefaultColor = Color.FromArgb(230, 230, 255)
			});
			num5 += 24;
			num4 -= 70;
			this.ClearHistoryList.Add(new BaseMenuItem(base.StageData, "History_切换")
			{
				OriginalPosition = new PointF((float)num4, (float)num5),
				DestPoint = new PointF((float)num4, (float)num5),
				Scale = 1f,
				Selected = true
			});
			this.ClearHistoryList.Add(new BaseMenuItem(base.StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(70f, 95f),
				DestPoint = new PointF(70f, 95f),
				Selected = true
			});
			this.ClearHistoryList.Add(new BaseMenuItem(base.StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(322f, 112f),
				DestPoint = new PointF(322f, 112f),
				Selected = true,
				AngleDegree = 180.0
			});
			this.ClearHistoryList.Add(new BaseMenuItem(base.StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(480f, 89f),
				DestPoint = new PointF(480f, 89f),
				Selected = true,
				AngleDegree = 90.0
			});
			this.ClearHistoryList.Add(new BaseMenuItem(base.StageData, "History_Arrow")
			{
				OriginalPosition = new PointF(464f, 119f),
				DestPoint = new PointF(464f, 119f),
				Selected = true,
				AngleDegree = -90.0
			});
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002BB84 File Offset: 0x00029D84
		public override void Show()
		{
			base.Show();
			this.MenuItemListPlane.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
			this.ScorePageList.ForEach(delegate(DescriptionMenuItem x)
			{
				x.Show();
			});
			this.ClearHistoryList.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002BC1C File Offset: 0x00029E1C
		public bool FindScore(ScoreHistory SH)
		{
			return SH.Rank.ToString() == base.MenuItemList[base.MenuSelectIndex].Name.Replace("History_", string.Empty) && SH.MyPlaneFullName == this.MenuItemListPlane[this.MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002BCA0 File Offset: 0x00029EA0
		public bool FindClear(ClearHistory CH)
		{
			return CH.Rank.ToString() == base.MenuItemList[base.MenuSelectIndex].Name.Replace("History_", string.Empty) && CH.MyPlaneFullName == this.MenuItemListPlane[this.MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002BD24 File Offset: 0x00029F24
		public bool FindSC(SpellCardHistory SCH)
		{
			return SCH.Rank.ToString() == base.MenuItemList[base.MenuSelectIndex].Name.Replace("History_", string.Empty) && SCH.MyPlaneFullName == this.MenuItemListPlane[this.MenuPlaneSelectIndex].Name.Replace("History_", string.Empty);
		}

		// Token: 0x040001B8 RID: 440
		private List<BaseMenuItem> MenuItemListPlane;

		// Token: 0x040001B9 RID: 441
		private int MenuPlaneSelectIndex;

		// Token: 0x040001BA RID: 442
		private List<DescriptionMenuItem> ScorePageList;

		// Token: 0x040001BB RID: 443
		private int ScorePageIndex = 0;

		// Token: 0x040001BC RID: 444
		private List<BaseMenuItem> ClearHistoryList;
	}
}
