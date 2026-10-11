using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001A7 RID: 423
	internal class MenuGroup_PlayerSelectEx : MenuGroup_PlayerSelect
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x0005FD84 File Offset: 0x0005DF84
		public MenuGroup_PlayerSelectEx(StageDataPackage StageData)
			: base(StageData, false)
		{
			base.MenuItemList = new List<BaseMenuItem>();
			this.PlayerDescription = new List<BaseMenuItem>();
			List<ClearHistory> list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "ReimuA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "MarisaA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "SanaeA");
			if (list.Count > 0)
			{
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
			}
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.ClearTimes > 0);
			list = StageData.PData.C_History.FindAll((ClearHistory x) => x.NoContinueClearTimes > 0 && x.MyPlaneFullName == "KoishiA");
			if (list.Count > 0)
			{
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
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.PlayerDescription[base.MenuSelectIndex].Selected = true;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00060230 File Offset: 0x0005E430
		public override void ProcessZ()
		{
			Point point = new Point(192, 398);
			string name = base.MenuItemList[base.MenuSelectIndex].Name;
			if (name != null)
			{
				if (name == "Plane")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceReimu_me")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Reimu(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceSanae_me")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Sanae(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceMarisa_me")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Marisa(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
				if (name == "FaceKoishi_me")
				{
					base.StageData.StateSwitchData.SDPswitch.MyPlane = new MyPlane_Koishi(base.StageData.StateSwitchData.SDPswitch, point);
					goto IL_0196;
				}
			}
			base.StageData.StateSwitchData.SDPswitch.MyPlane = new BaseMyPlane(base.StageData.StateSwitchData.SDPswitch, point);
			IL_0196:
			base.StageData.Rep.CreatRpy();
			base.StageData.StateSwitchData.NextState = "StEx";
			base.StageData.StateSwitchData.SDPswitch.SetReplayInfo(base.StageData.StateSwitchData.NextState);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00060428 File Offset: 0x0005E628
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
	}
}
