using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200004F RID: 79
	public class BaseRadialLaser_CS : Bullet_RadialLaser
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00010E3C File Offset: 0x0000F03C
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00010E54 File Offset: 0x0000F054
		public override int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				if (value >= 0 && value < 16)
				{
					base.ColorType = this.BulletColorType[value];
					base.TxtureObject = base.TextureObjectDictionary["Laser0_" + base.ColorType];
					base.GhostingColor = Color.White;
					base.SizeValue = 32;
					base.Region = 3;
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		public BaseRadialLaser_CS(StageDataPackage StageData)
			: base(StageData)
		{
			base.Active = true;
			base.UnmatchedTime = 60;
			base.UnRemoveable = true;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010F53 File Offset: 0x0000F153
		public override void EventCtrl()
		{
			base.EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			base.EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		// Token: 0x040000C3 RID: 195
		private byte[] BulletColorType = new byte[] { 1, 3, 5, 7, 9, 13, 14, 15 };
	}
}
