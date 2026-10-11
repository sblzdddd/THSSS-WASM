using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000201 RID: 513
	public class BaseBendLaser_CS : Bullet_BendLaser
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00072434 File Offset: 0x00070634
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0007244C File Offset: 0x0007064C
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
					base.Region = 1;
				}
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000724D8 File Offset: 0x000706D8
		public BaseBendLaser_CS(StageDataPackage StageData)
			: base(StageData)
		{
			base.Active = true;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0007253A File Offset: 0x0007073A
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

		// Token: 0x04000438 RID: 1080
		private byte[] BulletColorType = new byte[] { 1, 3, 5, 7, 9, 13, 14, 15 };
	}
}
