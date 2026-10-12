using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_ScoreSaver : BaseMenuGroup
	{
		public MenuGroup_ScoreSaver(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			ScoreHistorys = new List<BaseMenuItem>();
			for (var i = 0; i < 10; i++)
			{
				ScoreHistorys.Add(new DescriptionMenuItem(StageData, string.Format("{0:00}", i + 1)));
			}
			var stageData = StageData.GlobalData.LastState.StageData;
			for (var i = 0; i < stageData.S_History.Count; i++)
			{
				if (stageData.MyPlane.Score > stageData.S_History[i].Score)
				{
					var scoreHistory = new ScoreHistory
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
					for (var j = stageData.S_History.Count - 1; j > i; j--)
					{
						stageData.S_History[j].Copy(stageData.S_History[j - 1]);
					}
					stageData.S_History[i].Copy(scoreHistory);
					HistorySelectIndex = i;
					break;
				}
			}
			var num = OriginalPosition.X + 12f;
			var num2 = OriginalPosition.Y + 30f;
			var num3 = 0;
			foreach (var baseMenuItem in ScoreHistorys)
			{
				baseMenuItem.Position = new PointF(num, (float)(BoundRect.Top + 40));
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
			if (HistorySelectIndex >= 0)
			{
				ScoreHistorys[HistorySelectIndex].Selected = true;
			}
			var array = new string[7, 13];
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
			var array2 = array;
			MenuItemList = new List<BaseMenuItem>();
			for (var k = 0; k < 7; k++)
			{
				for (var l = 0; l < 13; l++)
				{
					BaseMenuItem baseMenuItem2 = new DescriptionMenuItem(StageData, array2[k, l]);
					baseMenuItem2.OriginalPosition = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138), 300f);
					baseMenuItem2.DestPoint = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138 + l * 22), (float)(270 + k * 22));
					MenuItemList.Add(baseMenuItem2);
				}
			}
			MenuSelectIndex = MenuItemList.Count - 1;
			if (MenuSelectIndex >= 0)
			{
				MenuItemList[MenuSelectIndex].Selected = true;
			}
			var array3 = stageData.PData.PlayerName.ToCharArray();
			while (NameIndex < array3.Length)
			{
				PlayerName[NameIndex] = array3[NameIndex].ToString();
				NameIndex++;
			}
			ShowPlayerName();
		}

		public override void ProcessKeys()
		{
			ScoreHistorys.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
			if (KClass.ArrowDown && (LastDown == 0 || LastDown > 30))
			{
				MenuSelectIndex += 13;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.ArrowUp && (LastUp == 0 || LastUp > 30))
			{
				MenuSelectIndex -= 13;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.ArrowLeft && (LastLeft == 0 || LastLeft > 30))
			{
				MenuSelectIndex--;
				if ((MenuSelectIndex + 1) % 13 == 0)
				{
					MenuSelectIndex += 13;
				}
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.ArrowRight && (LastRight == 0 || LastRight > 30))
			{
				MenuSelectIndex++;
				if (MenuSelectIndex % 13 == 0)
				{
					MenuSelectIndex -= 13;
				}
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.Key_Z && LastZ == 0)
			{
				MenuItemList[MenuSelectIndex].Click();
				OnChangeMenu = TimeMain + 1;
			}
			if (KClass.Key_X && LastX == 0)
			{
				if (NameIndex > 0)
				{
					NameIndex--;
					PlayerName[NameIndex] = null;
				}
				StageData.SoundPlay("se_cancel00.wav");
				ShowPlayerName();
			}
		}

		public override void ProcessZ()
		{
			if (MenuSelectIndex == MenuItemList.Count - 1)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (var baseMenuItem in ScoreHistorys)
				{
					baseMenuItem.DestPoint = new PointF(OriginalPosition.X + 12f, OriginalPosition.Y + 30f);
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_ok00.wav");
			}
			else if (MenuSelectIndex == MenuItemList.Count - 2)
			{
				if (NameIndex > 0)
				{
					NameIndex--;
					PlayerName[NameIndex] = " ";
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
			else
			{
				PlayerName[NameIndex] = MenuItemList[MenuSelectIndex].Name;
				StageData.SoundPlay("se_ok00.wav");
				if (NameIndex < 7)
				{
					NameIndex++;
				}
			}
			ShowPlayerName();
		}

		private void ShowPlayerName()
		{
			string text = null;
			for (var i = 0; i < 7; i++)
			{
				text += PlayerName[i];
			}
			var stageData = StageData.GlobalData.LastState.StageData;
			if (HistorySelectIndex >= 0)
			{
				stageData.S_History[HistorySelectIndex].PlayerName = text;
				stageData.PData.PlayerName = text;
				if (stageData.CurrentStageName.Contains("St"))
				{
					((DescriptionMenuItem)ScoreHistorys[HistorySelectIndex]).Description = stageData.S_History[HistorySelectIndex].Data2DrawStringSimple();
				}
				else
				{
					((DescriptionMenuItem)ScoreHistorys[HistorySelectIndex]).Description = stageData.S_History[HistorySelectIndex].Data2DrawString();
				}
			}
		}

		public override void Show()
		{
			base.Show();
			if (ScoreHistorys.Count > 0)
			{
				ScoreHistorys.ForEach(delegate(BaseMenuItem x)
				{
					x.Show();
				});
			}
		}

		private int NameIndex = 0;

		private string[] PlayerName = new string[8];

		private int HistorySelectIndex = -1;

		private List<BaseMenuItem> ScoreHistorys;
	}
}
