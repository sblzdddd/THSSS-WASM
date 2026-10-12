using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_PlayerSelect : BaseMenuGroup
	{
		public MenuGroup_PlayerSelect(StageDataPackage StageData, bool StageSelect)
			: base(StageData)
		{
			this.StageSelect = StageSelect;
			MenuSelectIndex = 0;
			MenuItemList = new List<BaseMenuItem>();
			PlayerDescription = new List<BaseMenuItem>();
			MenuItemList.Add(new CharacterMenuItem(StageData, "FaceReimu_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionReimu")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			MenuItemList.Add(new CharacterMenuItem(StageData, "FaceMarisa_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionMarisa")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			MenuItemList.Add(new CharacterMenuItem(StageData, "FaceSanae_me")
			{
				DestPoint1 = new PointF(0f, 30f),
				DestPoint2 = new PointF(-100f, 30f),
				TransparentValueF = 0f
			});
			PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionSanae")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			MenuItemList.Add(new CharacterMenuItem(StageData, "FaceKoishi_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionKoishi")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			MenuItemList[MenuSelectIndex].Selected = true;
			PlayerDescription[MenuSelectIndex].Selected = true;
			MenuTilte = new BaseMenuItem(StageData, "MenuTitle_PlayerSelect")
			{
				Position = MenuTitlePos2
			};
			TransparentValueF = 0f;
			MaxTransparent = 255;
			TransparentVelocity = 10f;
			TxtureObject = TextureObjectDictionary["MenuBackground"];
			AngleDegree = 90.0;
			Scale = 0.75f;
			OriginalPosition = new PointF((float)(BoundRect.Width / 2 + 30 * (StageData.StateSwitchData.SDPswitch.Difficulty - DifficultLevel.Normal)), (float)(BoundRect.Height / 2 - 20 * (StageData.StateSwitchData.SDPswitch.Difficulty - DifficultLevel.Normal)));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			PlayerDescription.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
		}

		public override void ProcessKeys()
		{
			if (KClass.ArrowLeft && LastLeft == 0)
			{
				MenuSelectIndex--;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.ArrowRight && LastRight == 0)
			{
				MenuSelectIndex++;
				StageData.SoundPlay("se_select00.wav");
				SelectItemChanged();
			}
			if (KClass.Key_Z && LastZ == 0)
			{
				MenuItemList[MenuSelectIndex].Click();
				OnChangeMenu = TimeMain + 20;
				StageData.SoundPlay("se_ok00.wav");
			}
			if ((KClass.Key_X || KClass.Key_ESC) && LastX == 0)
			{
				OnRemoveMenu = TimeMain + 20;
				TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in MenuItemList)
				{
					baseMenuItem.Selected = false;
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in PlayerDescription)
				{
					baseMenuItem.Selected = false;
					baseMenuItem.OnRemove = true;
				}
				StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			for (int i = 0; i < PlayerDescription.Count; i++)
			{
				if (i == MenuSelectIndex)
				{
					PlayerDescription[i].Select();
				}
				else
				{
					PlayerDescription[i].Selected = false;
				}
			}
		}

		public override void ProcessZ()
		{
			Point point = new Point(192, 398);
			string name = MenuItemList[MenuSelectIndex].Name;
			switch (name)
			{
			case "FaceAya_ct":
				StageData.MenuGroupList.Add(new MenuGroup_WeaponSelect(StageData, StageSelect, 0));
				break;
			case "Plane":
				StageData.MenuGroupList.Add(new MenuGroup_WeaponSelect(StageData, StageSelect, 1));
				break;
			case "FaceReimu_me":
				StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Reimu(StageData.StateSwitchData.SDPswitch, point);
				if (!StageSelect)
				{
					StageData.Rep.CreatRpy();
					StageData.StateSwitchData.NextState = "St1";
					StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
				}
				else
				{
					StageData.MenuGroupList.Add(new MenuGroup_StageSelect(StageData));
				}
				break;
			case "FaceSanae_me":
				StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Sanae(StageData.StateSwitchData.SDPswitch, point);
				if (!StageSelect)
				{
					StageData.Rep.CreatRpy();
					StageData.StateSwitchData.NextState = "St1";
					StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
				}
				else
				{
					StageData.MenuGroupList.Add(new MenuGroup_StageSelect(StageData));
				}
				break;
			case "FaceMarisa_me":
				StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Marisa(StageData.StateSwitchData.SDPswitch, point);
				if (!StageSelect)
				{
					StageData.Rep.CreatRpy();
					StageData.StateSwitchData.NextState = "St1";
					StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
				}
				else
				{
					StageData.MenuGroupList.Add(new MenuGroup_StageSelect(StageData));
				}
				break;
			case "FaceKoishi_me":
				StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Koishi(StageData.StateSwitchData.SDPswitch, point);
				if (!StageSelect)
				{
					StageData.Rep.CreatRpy();
					StageData.StateSwitchData.NextState = "St1";
					StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
				}
				else
				{
					StageData.MenuGroupList.Add(new MenuGroup_StageSelect(StageData));
				}
				break;
			}
		}

		public override void Show()
		{
			base.Show();
			if (StageData.MenuGroupList[StageData.MenuGroupList.Count - 1] == this)
			{
				PlayerDescription.ForEach(delegate(BaseMenuItem x)
				{
					x.Show();
				});
			}
		}

		private bool StageSelect;

		protected List<BaseMenuItem> PlayerDescription;
	}
}
