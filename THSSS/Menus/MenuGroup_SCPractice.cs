using System;
using System.Collections.Generic;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class MenuGroup_SCPractice : BaseMenuGroup
	{
		public MenuGroup_SCPractice(StageDataPackage StageData)
			: base(StageData)
		{
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			List<SpellCardHistory> list = StageData.PData.SC_History.FindAll((SpellCardHistory a) => a.MyPlaneFullName == "ReimuA");
			for (int i = 0; i < list.Count; i++)
			{
				MenuItemList.Add(new SCMenuItem(StageData, list[i].Index.ToString().PadRight(3))
				{
					Description = (list[i].Rank.ToString().PadLeft(10) + list[i].Recorded.ToString().PadLeft(2) + "/" + list[i].History.ToString().PadLeft(2)).PadLeft(40),
					TxtureObject = StageData.TextureObjectDictionary["欧" + list[i].Name],
					DefaultColor = ((list[i].Recorded > 0) ? Color.SkyBlue : Color.White)
				});
			}
			int num = 100;
			int num2 = 100;
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.TransparentValueF = 0f;
				baseMenuItem.Position = new PointF((float)num, 90f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.MaxVelocity = 10000f;
				num2 += deltaY;
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			MenuTitlePos1 = new PointF(140f, -30f);
			MenuTitlePos2 = new PointF(140f, -150f);
			MenuTilte = new BaseMenuItem(StageData, "MenuTitle_MusicRoom")
			{
				Position = MenuTitlePos2
			};
			TransparentValueF = 0f;
			TransparentVelocity = 5f;
			TxtureObject = TextureObjectDictionary["MenuBackground"];
			Scale = 0.625f;
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			AngleDegree = 90.0;
			ColorValue = Color.SlateBlue;
			DescriptionBox = new DescriptionBox(StageData)
			{
				MaxTransparent = 0,
				OffsetX = 50
			};
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			int num = 100;
			int num2 = 100;
			int num3 = (MenuSelectIndex - 8) * deltaY;
			if (num3 < 0)
			{
				num3 = 0;
			}
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)(num2 - num3));
				num2 += deltaY;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			DescriptionBox.Ctrl();
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.TransparentValueF = 400f - Math.Abs(260f - baseMenuItem.Position.Y) * 2f;
			}
		}

		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (KClass.Key_Z && LastZ == 0)
			{
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				DescriptionBox.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(100f, 60f);
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void Show()
		{
			base.Show();
			DescriptionBox.Show();
		}

		private DescriptionBox DescriptionBox;

		private int deltaY = 20;
	}
}
