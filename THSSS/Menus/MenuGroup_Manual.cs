using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_Manual : BaseMenuGroup
	{
		public MenuGroup_Manual(StageDataPackage StageData)
			: base(StageData)
		{
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Menu_Manual01"),
				new BaseMenuItem(StageData, "Menu_Manual02"),
				new BaseMenuItem(StageData, "Menu_Manual03"),
				new BaseMenuItem(StageData, "Menu_Manual04"),
				new BaseMenuItem(StageData, "Menu_Manual05"),
				new BaseMenuItem(StageData, "Menu_Manual06")
			};
			int num = 20;
			int num2 = 85;
			foreach (BaseMenuItem baseMenuItem in MenuItemList)
			{
				baseMenuItem.Position = new PointF((float)num, 90f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				num2 += 30;
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			MenuItemListManual = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "Manual01"),
				new BaseMenuItem(StageData, "Manual02"),
				new BaseMenuItem(StageData, "Manual03"),
				new BaseMenuItem(StageData, "Manual04"),
				new BaseMenuItem(StageData, "Manual05"),
				new BaseMenuItem(StageData, "Manual06")
			};
			foreach (BaseMenuItem baseMenuItem in MenuItemListManual)
			{
				baseMenuItem.Position = new PointF(0f, 0f);
				baseMenuItem.DestPoint = new PointF(0f, 0f);
				baseMenuItem.UnSelectVisible = false;
				baseMenuItem.Vibrateable = false;
				baseMenuItem.Twinkleable = false;
				baseMenuItem.TransparentValueF = 0f;
			}
			MenuItemListManual[MenuSelectIndex].Selected = true;
			TransparentValueF = 0f;
			MaxTransparent = 255;
			TransparentVelocity = 5f;
			TxtureObject = TextureObjectDictionary["MenuBackground"];
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			AngleDegree = 90.0;
			ColorValue = Color.FromArgb(30, 110, 150);
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Selected = false;
			});
			MenuItemListManual[MenuSelectIndex].Select();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
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
				foreach (BaseMenuItem baseMenuItem in MenuItemList)
				{
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in MenuItemListManual)
				{
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void Show()
		{
			base.Show();
			MenuItemListManual.ForEach(delegate(BaseMenuItem x)
			{
				x.Show();
			});
		}

		private List<BaseMenuItem> MenuItemListManual;
	}
}
