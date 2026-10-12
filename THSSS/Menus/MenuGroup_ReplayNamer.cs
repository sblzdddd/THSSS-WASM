using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_ReplayNamer : BaseMenuGroup
	{
		public MenuGroup_ReplayNamer(StageDataPackage StageData, PointF OriginalPosition, DescriptionMenuItem RepMenuItem)
			: base(StageData)
		{
			this.RepMenuItem = RepMenuItem;
			MenuItemList = new List<BaseMenuItem>();
			var stageData = StageData.GlobalData.LastState.StageData;
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
			for (var i = 0; i < 7; i++)
			{
				for (var j = 0; j < 13; j++)
				{
					BaseMenuItem baseMenuItem = new DescriptionMenuItem(StageData, array2[i, j]);
					baseMenuItem.OriginalPosition = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138), 300f);
					baseMenuItem.DestPoint = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138 + j * 22), (float)(270 + i * 22));
					MenuItemList.Add(baseMenuItem);
				}
			}
			MenuSelectIndex = MenuItemList.Count - 1;
			var repInfo = StageData.GlobalData.LastState.StageData.RepInfo;
			RepDescription += DateTime.Now.Date.ToString("yy'/'MM'/'dd").PadRight(9);
			RepDescription += repInfo.MyPlaneName.PadRight(7);
			switch (repInfo.Rank)
			{
			case DifficultLevel.Easy:
				RepDescription += "E  ";
				break;
			case DifficultLevel.Normal:
				RepDescription += "N  ";
				break;
			case DifficultLevel.Hard:
				RepDescription += "H  ";
				break;
			case DifficultLevel.Lunatic:
				RepDescription += "L  ";
				break;
			case DifficultLevel.Ultra:
				RepDescription += "U  ";
				break;
			case DifficultLevel.Extra:
				RepDescription += "EX ";
				break;
			}
			RepDescription += ((repInfo.LastStage == null) ? "   " : repInfo.LastStage.PadRight(4));
			MenuItemList[MenuSelectIndex].Selected = true;
			MenuTilte = new DescriptionMenuItem(StageData, RepMenuItem.Name)
			{
				Description = "        " + RepDescription,
				OriginalPosition = RepMenuItem.OriginalPosition,
				Selected = true
			};
			MenuTitlePos1 = new PointF(OriginalPosition.X, OriginalPosition.Y);
			MenuTitlePos2 = RepMenuItem.OriginalPosition;
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
			if (KClass.Key_ESC)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (var baseMenuItem in MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(MenuItemList[0].OriginalPosition.X, MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void ProcessZ()
		{
			if (MenuSelectIndex == MenuItemList.Count - 1)
			{
				var repInfo = StageData.GlobalData.LastState.StageData.RepInfo;
				repInfo.PlayerName = null;
				for (var i = 0; i < 7; i++)
				{
					var replayInfo = repInfo;
					replayInfo.PlayerName += PlayerName[i];
				}
				StageData.Rep.SaveRpy(".\\Replay\\thSSS_" + RepMenuItem.Name + ".rpy", repInfo);
				RepMenuItem.Description = ((DescriptionMenuItem)MenuTilte).Description;
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (var baseMenuItem in MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(MenuItemList[0].OriginalPosition.X, MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_extend.wav");
			}
			else if (MenuSelectIndex == MenuItemList.Count - 2)
			{
				if (NameIndex > 0)
				{
					NameIndex--;
					PlayerName[NameIndex] = null;
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
			((DescriptionMenuItem)MenuTilte).Description = text.PadRight(8) + RepDescription;
			var stageData = StageData.GlobalData.LastState.StageData;
			stageData.PData.PlayerName = text;
		}

		private int NameIndex = 0;

		private string[] PlayerName = new string[8];

		private string RepDescription = null;

		private DescriptionMenuItem RepMenuItem;
	}
}
