using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class MenuGroup_PlayerSelectEx : MenuGroup_PlayerSelect
	{
		public MenuGroup_PlayerSelectEx(StageDataPackage StageData)
			: base(StageData, false)
		{
			MenuItemList = new List<BaseMenuItem>();
			PlayerDescription = new List<BaseMenuItem>();
			var list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "ReimuA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "MarisaA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "SanaeA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "KoishiA");
			if (list.Count > 0)
			{
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
			}
			MenuItemList[MenuSelectIndex].Selected = true;
			PlayerDescription[MenuSelectIndex].Selected = true;
		}

		public override void ProcessZ()
		{
			var point = new Point(192, 398);
			var name = MenuItemList[MenuSelectIndex].Name;
			if (name != null)
			{
				if (name == "Plane")
				{
					StageData.StateSwitchData.SDPswitch.MyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceReimu_me")
				{
					StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Reimu(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceSanae_me")
				{
					StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Sanae(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceMarisa_me")
				{
					StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Marisa(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceKoishi_me")
				{
					StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Koishi(StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
			}
			StageData.StateSwitchData.SDPswitch.MyPlane = new BaseMyPlane(StageData.StateSwitchData.SDPswitch, point);
			IL_0196:
			StageData.Rep.CreatRpy();
			StageData.StateSwitchData.NextState = "StEx";
			StageData.StateSwitchData.SDPswitch.SetReplayInfo(StageData.StateSwitchData.NextState);
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
	}
}
