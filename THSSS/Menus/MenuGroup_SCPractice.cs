using System;
using System.Collections.Generic;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x02000090 RID: 144
	internal class MenuGroup_SCPractice : BaseMenuGroup
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00022CA4 File Offset: 0x00020EA4
		public MenuGroup_SCPractice(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			List<SpellCardHistory> list = StageData.PData.SC_History.FindAll((SpellCardHistory a) => a.MyPlaneFullName == "ReimuA");
			for (int i = 0; i < list.Count; i++)
			{
				base.MenuItemList.Add(new SCMenuItem(StageData, list[i].Index.ToString().PadRight(3))
				{
					Description = (list[i].Rank.ToString().PadLeft(10) + list[i].Recorded.ToString().PadLeft(2) + "/" + list[i].History.ToString().PadLeft(2)).PadLeft(40),
					TxtureObject = StageData.TextureObjectDictionary["欧" + list[i].Name],
					DefaultColor = ((list[i].Recorded > 0) ? Color.SkyBlue : Color.White)
				});
			}
			int num = 100;
			int num2 = 100;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.TransparentValueF = 0f;
				baseMenuItem.Position = new PointF((float)num, 90f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.MaxVelocity = 10000f;
				num2 += this.deltaY;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.MenuTitlePos1 = new PointF(140f, -30f);
			this.MenuTitlePos2 = new PointF(140f, -150f);
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_MusicRoom")
			{
				Position = this.MenuTitlePos2
			};
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 5f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.Scale = 0.625f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.SlateBlue;
			this.DescriptionBox = new DescriptionBox(StageData)
			{
				MaxTransparent = 0,
				OffsetX = 50
			};
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00022FB8 File Offset: 0x000211B8
		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			int num = 100;
			int num2 = 100;
			int num3 = (base.MenuSelectIndex - 8) * this.deltaY;
			if (num3 < 0)
			{
				num3 = 0;
			}
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)(num2 - num3));
				num2 += this.deltaY;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00023058 File Offset: 0x00021258
		public override void Ctrl()
		{
			base.Ctrl();
			this.DescriptionBox.Ctrl();
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.TransparentValueF = 400f - Math.Abs(260f - baseMenuItem.Position.Y) * 2f;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000230EC File Offset: 0x000212EC
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				this.DescriptionBox.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(100f, 60f);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000231F8 File Offset: 0x000213F8
		public override void Show()
		{
			base.Show();
			this.DescriptionBox.Show();
		}

		// Token: 0x0400016B RID: 363
		private DescriptionBox DescriptionBox;

		// Token: 0x0400016C RID: 364
		private int deltaY = 20;
	}
}
