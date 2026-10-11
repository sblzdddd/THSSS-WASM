using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B7 RID: 183
	internal class MenuGroup_ReplayNamer : BaseMenuGroup
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		public MenuGroup_ReplayNamer(StageDataPackage StageData, PointF OriginalPosition, DescriptionMenuItem RepMenuItem)
			: base(StageData)
		{
			this.RepMenuItem = RepMenuItem;
			base.MenuItemList = new List<BaseMenuItem>();
			StageDataPackage stageData = StageData.GlobalData.LastState.StageData;
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
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 13; j++)
				{
					BaseMenuItem baseMenuItem = new DescriptionMenuItem(StageData, array2[i, j]);
					baseMenuItem.OriginalPosition = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138), 300f);
					baseMenuItem.DestPoint = new PointF((float)(stageData.BoundRect.Left + stageData.BoundRect.Width / 2 - 138 + j * 22), (float)(270 + i * 22));
					base.MenuItemList.Add(baseMenuItem);
				}
			}
			base.MenuSelectIndex = base.MenuItemList.Count - 1;
			ReplayInfo repInfo = StageData.GlobalData.LastState.StageData.RepInfo;
			this.RepDescription += DateTime.Now.Date.ToString("yy'/'MM'/'dd").PadRight(9);
			this.RepDescription += repInfo.MyPlaneName.PadRight(7);
			switch (repInfo.Rank)
			{
			case DifficultLevel.Easy:
				this.RepDescription += "E  ";
				break;
			case DifficultLevel.Normal:
				this.RepDescription += "N  ";
				break;
			case DifficultLevel.Hard:
				this.RepDescription += "H  ";
				break;
			case DifficultLevel.Lunatic:
				this.RepDescription += "L  ";
				break;
			case DifficultLevel.Ultra:
				this.RepDescription += "U  ";
				break;
			case DifficultLevel.Extra:
				this.RepDescription += "EX ";
				break;
			}
			this.RepDescription += ((repInfo.LastStage == null) ? "   " : repInfo.LastStage.PadRight(4));
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			base.MenuTilte = new DescriptionMenuItem(StageData, RepMenuItem.Name)
			{
				Description = "        " + this.RepDescription,
				OriginalPosition = RepMenuItem.OriginalPosition,
				Selected = true
			};
			this.MenuTitlePos1 = new PointF(OriginalPosition.X, OriginalPosition.Y);
			this.MenuTitlePos2 = RepMenuItem.OriginalPosition;
			char[] array3 = stageData.PData.PlayerName.ToCharArray();
			while (this.NameIndex < array3.Length)
			{
				this.PlayerName[this.NameIndex] = array3[this.NameIndex].ToString();
				this.NameIndex++;
			}
			this.ShowPlayerName();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002C62C File Offset: 0x0002A82C
		public override void ProcessKeys()
		{
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
			if (base.KClass.Key_ESC)
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

		// Token: 0x06000544 RID: 1348 RVA: 0x0002C95C File Offset: 0x0002AB5C
		public override void ProcessZ()
		{
			if (base.MenuSelectIndex == base.MenuItemList.Count - 1)
			{
				ReplayInfo repInfo = base.StageData.GlobalData.LastState.StageData.RepInfo;
				repInfo.PlayerName = null;
				for (int i = 0; i < 7; i++)
				{
					ReplayInfo replayInfo = repInfo;
					replayInfo.PlayerName += this.PlayerName[i];
				}
				base.StageData.Rep.SaveRpy(".\\Replay\\thSSS_" + this.RepMenuItem.Name + ".rpy", repInfo);
				this.RepMenuItem.Description = ((DescriptionMenuItem)base.MenuTilte).Description;
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(base.MenuItemList[0].OriginalPosition.X, base.MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_extend.wav");
			}
			else if (base.MenuSelectIndex == base.MenuItemList.Count - 2)
			{
				if (this.NameIndex > 0)
				{
					this.NameIndex--;
					this.PlayerName[this.NameIndex] = null;
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

		// Token: 0x06000545 RID: 1349 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		private void ShowPlayerName()
		{
			string text = null;
			for (int i = 0; i < 7; i++)
			{
				text += this.PlayerName[i];
			}
			((DescriptionMenuItem)base.MenuTilte).Description = text.PadRight(8) + this.RepDescription;
			StageDataPackage stageData = base.StageData.GlobalData.LastState.StageData;
			stageData.PData.PlayerName = text;
		}

		// Token: 0x040001C4 RID: 452
		private int NameIndex = 0;

		// Token: 0x040001C5 RID: 453
		private string[] PlayerName = new string[8];

		// Token: 0x040001C6 RID: 454
		private string RepDescription = null;

		// Token: 0x040001C7 RID: 455
		private DescriptionMenuItem RepMenuItem;
	}
}
