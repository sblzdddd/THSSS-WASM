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
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>();
			this.PlayerDescription = new List<BaseMenuItem>();
			base.MenuItemList.Add(new CharacterMenuItem(StageData, "FaceReimu_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			this.PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionReimu")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			base.MenuItemList.Add(new CharacterMenuItem(StageData, "FaceMarisa_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			this.PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionMarisa")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			base.MenuItemList.Add(new CharacterMenuItem(StageData, "FaceSanae_me")
			{
				DestPoint1 = new PointF(0f, 30f),
				DestPoint2 = new PointF(-100f, 30f),
				TransparentValueF = 0f
			});
			this.PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionSanae")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			base.MenuItemList.Add(new CharacterMenuItem(StageData, "FaceKoishi_me")
			{
				DestPoint1 = new PointF(-128f, 10f),
				DestPoint2 = new PointF(-228f, 10f),
				TransparentValueF = 0f
			});
			this.PlayerDescription.Add(new CharacterMenuItem(StageData, "DescriptionKoishi")
			{
				DestPoint1 = new PointF(190f, 10f),
				DestPoint2 = new PointF(260f, 10f),
				TransparentValueF = 0f
			});
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.PlayerDescription[base.MenuSelectIndex].Selected = true;
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_PlayerSelect")
			{
				Position = this.MenuTitlePos2
			};
			base.TransparentValueF = 0f;
			base.MaxTransparent = 255;
			base.TransparentVelocity = 10f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.AngleDegree = 90.0;
			base.Scale = 0.75f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2 + 30 * (StageData.StateSwitchData.SDPswitch.Difficulty - DifficultLevel.Normal)), (float)(base.BoundRect.Height / 2 - 20 * (StageData.StateSwitchData.SDPswitch.Difficulty - DifficultLevel.Normal)));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.PlayerDescription.ForEach(delegate(BaseMenuItem x)
			{
				x.Ctrl();
			});
		}

		public override void ProcessKeys()
		{
			if (base.KClass.ArrowLeft && this.LastLeft == 0)
			{
				base.MenuSelectIndex--;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.ArrowRight && this.LastRight == 0)
			{
				base.MenuSelectIndex++;
				base.StageData.SoundPlay("se_select00.wav");
				this.SelectItemChanged();
			}
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 20;
				base.StageData.SoundPlay("se_ok00.wav");
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.Selected = false;
					baseMenuItem.OnRemove = true;
				}
				foreach (BaseMenuItem baseMenuItem in this.PlayerDescription)
				{
					baseMenuItem.Selected = false;
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			for (int i = 0; i < this.PlayerDescription.Count; i++)
			{
				if (i == base.MenuSelectIndex)
				{
					this.PlayerDescription[i].Select();
				}
				else
				{
					this.PlayerDescription[i].Selected = false;
				}
			}
		}

		public override void ProcessZ()
		{
			Point point = new Point(192, 398);
			string name = base.MenuItemList[base.MenuSelectIndex].Name;
			switch (name)
			{
			case "FaceAya_ct":
				base.StageData.MenuGroupList.Add(new MenuGroup_WeaponSelect(base.StageData, this.StageSelect, 0));
				break;
			case "Plane":
				base.StageData.MenuGroupList.Add(new MenuGroup_WeaponSelect(base.StageData, this.StageSelect, 1));
				break;
			case "FaceReimu_me":
				base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Reimu(base.StageData.StateSwitchData.SDPswitch, point);
				if (!this.StageSelect)
				{
					base.StageData.Rep.CreatRpy();
					base.StageData.StateSwitchData.NextState = "St1";
					base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
				}
				else
				{
					base.StageData.MenuGroupList.Add(new MenuGroup_StageSelect(base.StageData));
				}
				break;
			case "FaceSanae_me":
				base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Sanae(base.StageData.StateSwitchData.SDPswitch, point);
				if (!this.StageSelect)
				{
					base.StageData.Rep.CreatRpy();
					base.StageData.StateSwitchData.NextState = "St1";
					base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
				}
				else
				{
					base.StageData.MenuGroupList.Add(new MenuGroup_StageSelect(base.StageData));
				}
				break;
			case "FaceMarisa_me":
				base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Marisa(base.StageData.StateSwitchData.SDPswitch, point);
				if (!this.StageSelect)
				{
					base.StageData.Rep.CreatRpy();
					base.StageData.StateSwitchData.NextState = "St1";
					base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
				}
				else
				{
					base.StageData.MenuGroupList.Add(new MenuGroup_StageSelect(base.StageData));
				}
				break;
			case "FaceKoishi_me":
				base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Koishi(base.StageData.StateSwitchData.SDPswitch, point);
				if (!this.StageSelect)
				{
					base.StageData.Rep.CreatRpy();
					base.StageData.StateSwitchData.NextState = "St1";
					base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
				}
				else
				{
					base.StageData.MenuGroupList.Add(new MenuGroup_StageSelect(base.StageData));
				}
				break;
			}
		}

		public override void Show()
		{
			base.Show();
			if (base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1] == this)
			{
				this.PlayerDescription.ForEach(delegate(BaseMenuItem x)
				{
					x.Show();
				});
			}
		}

		private bool StageSelect;

		protected List<BaseMenuItem> PlayerDescription;
	}
}
