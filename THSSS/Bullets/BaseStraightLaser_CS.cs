using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000A4 RID: 164
	public class BaseStraightLaser_CS : Bullet_StraightLaser
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00026C04 File Offset: 0x00024E04
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00026C1C File Offset: 0x00024E1C
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

		// Token: 0x060004E2 RID: 1250 RVA: 0x00026CA8 File Offset: 0x00024EA8
		public BaseStraightLaser_CS(StageDataPackage StageData)
			: base(StageData)
		{
			base.Active = true;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00026D0A File Offset: 0x00024F0A
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

		// Token: 0x04000192 RID: 402
		private byte[] BulletColorType = new byte[] { 1, 3, 5, 7, 9, 13, 14, 15 };
	}
}
