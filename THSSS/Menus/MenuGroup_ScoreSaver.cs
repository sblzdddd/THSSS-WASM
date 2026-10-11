using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000A2 RID: 162
	internal class MenuGroup_ScoreSaver : BaseMenuGroup
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00025384 File Offset: 0x00023584
		public MenuGroup_ScoreSaver(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData)
		{
			base.OriginalPosition = OriginalPosition;
			this.ScoreHistorys = new List<BaseMenuItem>();
			for (int i = 0; i < 10; i++)
			{
				this.ScoreHistorys.Add(new DescriptionMenuItem(StageData, string.Format("{0:00}", i + 1)));
			}
			StageDataPackage stageData = StageData.GlobalData.LastState.StageData;
			for (int i = 0; i < stageData.S_History.Count; i++)
			{
				if (stageData.MyPlane.Score > stageData.S_History[i].Score)
				{
					ScoreHistory scoreHistory = new ScoreHistory
					{
						MyPlaneFullName = stageData.MyPlane.FullName,
						Rank = stageData.Difficulty,
						PlayerName = "",
						Score = stageData.MyPlane.Score,
						Date = DateTime.Now.Date.ToString("yyyy'/'MM'/'dd"),
						Time = DateTime.Now.ToShortTimeString(),
						Stage = stageData.CurrentStageName,
						SlowRate = stageData.SlowRate
					};
					for (int j = stageData.S_History.Count - 1; j > i; j--)
					{
						stageData.S_History[j].Copy(stageData.S_History[j - 1]);
					}
					stageData.S_History[i].Copy(scoreHistory);
					this.HistorySelectIndex = i;
					break;
				}
			}
			float num = OriginalPosition.X + 12f;
			float num2 = OriginalPosition.Y + 30f;
			int num3 = 0;
			foreach (BaseMenuItem baseMenuItem in this.ScoreHistorys)
			{
				baseMenuItem.Position = new PointF(num, (float)(base.BoundRect.Top + 40));
				baseMenuItem.DestPoint = new PointF(num, num2);
				num2 += 17f;
				if (stageData.CurrentStageName.Contains("St"))
				{
					((DescriptionMenuItem)baseMenuItem).Description = stageData.S_History[num3++].Data2DrawStringSimple();
				}
				else
				{
					((DescriptionMenuItem)baseMenuItem).Description = stageData.S_History[num3++].Data2DrawString();
				}
			}
			if (this.HistorySelectIndex >= 0)
			{
				this.ScoreHistorys[this.HistorySelectIndex].Selected = true;
			}
			string[,] array = new string[7, 13];
			array[0, 0] = "A";
			array[0, 1] = "B";
			array[0, 2] = "C";
			array[0, 3] = "D";
			array[0, 4] = "E";
			array[0, 5] = "F";
			array[0, 6] = "G";
			array[0, 7] = "H";
			array[0, 8] = "I";
			array[0, 9] = "J";
			array[0, 10] = "K";
			array[0, 11] = "L";
			array[0, 12] = "M";
			array[1, 0] = "N";
			array[1, 1] = "O";
			array[1, 2] = "P";
			array[1, 3] = "Q";
			array[1, 4] = "R";
			array[1, 5] = "S";
			array[1, 6] = "T";
			array[1, 7] = "U";
			array[1, 8] = "V";
			array[1, 9] = "W";
			array[1, 10] = "X";
			array[1, 11] = "Y";
			array[1, 12] = "Z";
			array[2, 0] = "a";
			array[2, 1] = "b";
			array[2, 2] = "c";
			array[2, 3] = "d";
			array[2, 4] = "e";
			array[2, 5] = "f";
			array[2, 6] = "g";
			array[2, 7] = "h";
			array[2, 8] = "i";
			array[2, 9] = "j";
			array[2, 10] = "k";
			array[2, 11] = "l";
			array[2, 12] = "m";
			array[3, 0] = "n";
			array[3, 1] = "o";
			array[3, 2] = "p";
			array[3, 3] = "q";
			array[3, 4] = "r";
			array[3, 5] = "s";
			array[3, 6] = "t";
			array[3, 7] = "u";
			array[3, 8] = "v";
			array[3, 9] = "w";
			array[3, 10] = "x";
			array[3, 11] = "y";
			array[3, 12] = "z";
			array[4, 0] = "0";
			array[4, 1] = "1";
			array[4, 2] = "2";
			array[4, 3] = "3";
			array[4, 4] = "4";
			array[4, 5] = "5";
			array[4, 6] = "6";
			array[4, 7] = "7";
			array[4, 8] = "8";
			array[4, 9] = "9";
			array[4, 10] = "+";
			array[4, 11] = "-";
			array[4, 12] = "=";
			array[5, 0] = ".";
			array[5, 1] = ",";
			array[5, 2] = "!";
			array[5, 3] = "?";
			array[5, 4] = "@";
			array[5, 5] = ":";
			array[5, 6] = ";";
			array[5, 7] = "[";
			array[5, 8] = "]";
			array[5, 9] = "(";
			array[5, 10] = ")";
			array[5, 11] = "_";
			array[5, 12] = "/";
			array[6, 0] = "{";
			array[6, 1] = "}";
			array[6, 2] = "|";
			array[6, 3] = "~";
			array[6, 4] = "^";
			array[6, 5] = "#";
			array[6, 6] = "$";
			array[6, 7] = "%";
			array[6, 8] = "&";
			array[6, 9] = "*";
			array[6, 10] = " ";
			array[6, 11] = "BS";
			array[6, 12] = "OK";
			string[,] array2 = array;
			base.MenuItemList = new List<BaseMenuItem>();
			for (int k = 0; k < 7; k++)
			{
				for (int l = 0; l < 13; l++)
				{
					BaseMenuItem baseMenuItem2 = new DescriptionMenuItem(StageData, array2[k, l]);
					baseMenuItem2.OriginalPosition = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138), 300f);
					baseMenuItem2.DestPoint = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138 + l * 22), (float)(270 + k * 22));
					base.MenuItemList.Add(baseMenuItem2);
				}
			}
			base.MenuSelectIndex = base.MenuItemList.Count - 1;
			if (base.MenuSelectIndex >= 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Selected = true;
			}
			char[] array3 = stageData.PData.PlayerName.ToCharArray();
			while (this.NameIndex < array3.Length)
			{
				this.PlayerName[this.NameIndex] = array3[this.NameIndex].ToString();
				this.NameIndex++;
			}
			this.ShowPlayerName();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00025D40 File Offset: 0x00023F40
		public override void ProcessKeys()
		{
			this.ScoreHistorys.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
			if (base.KClass.ArrowDown && (this.LastDown == 0 || this.LastDown > 30))
			{
				base.MenuSelectIndex += 13;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowUp && (this.LastUp == 0 || this.LastUp > 30))
			{
				base.MenuSelectIndex -= 13;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowLeft && (this.LastLeft == 0 || this.LastLeft > 30))
			{
				base.MenuSelectIndex--;
				if ((base.MenuSelectIndex + 1) % 13 == 0)
				{
					base.MenuSelectIndex += 13;
				}
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowRight && (this.LastRight == 0 || this.LastRight > 30))
			{
				base.MenuSelectIndex++;
				if (base.MenuSelectIndex % 13 == 0)
				{
					base.MenuSelectIndex -= 13;
				}
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 1;
			}
			if (base.KClass.Key_X && this.LastX == 0)
			{
				if (this.NameIndex > 0)
				{
					this.NameIndex--;
					this.PlayerName[this.NameIndex] = null;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
				this.ShowPlayerName();
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00025FCC File Offset: 0x000241CC
		public override void ProcessZ()
		{
			if (base.MenuSelectIndex == base.MenuItemList.Count - 1)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in this.ScoreHistorys)
				{
					baseMenuItem.DestPoint = new PointF(base.OriginalPosition.X + 12f, base.OriginalPosition.Y + 30f);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_ok00.wav");
			}
			else if (base.MenuSelectIndex == base.MenuItemList.Count - 2)
			{
				if (this.NameIndex > 0)
				{
					this.NameIndex--;
					this.PlayerName[this.NameIndex] = " ";
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
			else
			{
				this.PlayerName[this.NameIndex] = base.MenuItemList[base.MenuSelectIndex].Name;
				base.StageData.SoundPlay("se_ok00.wav");
				if (this.NameIndex < 7)
				{
					this.NameIndex++;
				}
			}
			this.ShowPlayerName();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00026170 File Offset: 0x00024370
		private void ShowPlayerName()
		{
			string text = null;
			for (int i = 0; i < 7; i++)
			{
				text += this.PlayerName[i];
			}
			StageDataPackage stageData = base.StageData.GlobalData.LastState.StageData;
			if (this.HistorySelectIndex >= 0)
			{
				stageData.S_History[this.HistorySelectIndex].PlayerName = text;
				stageData.PData.PlayerName = text;
				if (stageData.CurrentStageName.Contains("St"))
				{
					((DescriptionMenuItem)this.ScoreHistorys[this.HistorySelectIndex]).Description = stageData.S_History[this.HistorySelectIndex].Data2DrawStringSimple();
				}
				else
				{
					((DescriptionMenuItem)this.ScoreHistorys[this.HistorySelectIndex]).Description = stageData.S_History[this.HistorySelectIndex].Data2DrawString();
				}
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00026278 File Offset: 0x00024478
		public override void Show()
		{
			base.Show();
			if (this.ScoreHistorys.Count > 0)
			{
				this.ScoreHistorys.ForEach(delegate(BaseMenuItem x)
				{
					x.Show();
				});
			}
		}

		// Token: 0x0400018A RID: 394
		private int NameIndex = 0;

		// Token: 0x0400018B RID: 395
		private string[] PlayerName = new string[8];

		// Token: 0x0400018C RID: 396
		private int HistorySelectIndex = -1;

		// Token: 0x0400018D RID: 397
		private List<BaseMenuItem> ScoreHistorys;
	}
}
