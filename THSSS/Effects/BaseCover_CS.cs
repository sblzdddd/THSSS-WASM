using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public class BaseCover_CS : BaseEmitter_CS
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00060664 File Offset: 0x0005E864
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0006067B File Offset: 0x0005E87B
		public bool Roundness { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00060684 File Offset: 0x0005E884
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x0006069B File Offset: 0x0005E89B
		public int CtrlID { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000606A4 File Offset: 0x0005E8A4
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x000606BB File Offset: 0x0005E8BB
		public List<EventGroup> SubEventGroupList { get; set; }

		// Token: 0x06000A25 RID: 2597 RVA: 0x000606C4 File Offset: 0x0005E8C4
		public BaseCover_CS(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
			this.SubEventGroupList = new List<EventGroup>();
			base.HealthPoint = 1000000f;
			base.HitEnabled = false;
			base.Region = 1;
			base.Angle = 1.5707963267948966;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00060732 File Offset: 0x0005E932
		public override void Shoot()
		{
			base.AngleWithDirection = this.Roundness;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00060756 File Offset: 0x0005E956
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

		// Token: 0x06000A28 RID: 2600 RVA: 0x00060834 File Offset: 0x0005EA34
		public override void HitCheckAll()
		{
			if (base.Time >= base.StartTime && base.Time <= base.StartTime + base.Duration)
			{
				base.BulletList.ForEach(delegate(BaseBullet_Touhou b)
				{
					if (b.Cover)
					{
						if (this.Type == 0 || b.ID == this.CtrlID)
						{
							if (this.HitCheck(b))
							{
								this.SubEventGroupList.ForEach(delegate(EventGroup e)
								{
									e.Update(b);
								});
							}
						}
					}
				});
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0006088C File Offset: 0x0005EA8C
		public override bool HitCheck(BaseObject Target)
		{
			bool flag;
			if (this.Roundness)
			{
				if (base.ScaleLength == base.ScaleWidth)
				{
					if (base.GetDistance(Target) - (double)Target.Region < (double)((float)base.Region * base.Scale))
					{
						return true;
					}
				}
				else
				{
					double num = (base.AngleWithDirection ? (base.Direction + base.Angle) : base.Angle);
					double distance = base.GetDistance(Target);
					double direction = base.GetDirection(Target);
					double num2 = distance * Math.Cos((double)((float)(num - direction)));
					double num3 = distance * Math.Sin((double)((float)(num - direction)));
					double num4 = (double)((float)base.Region * base.ScaleWidth);
					double num5 = (double)((float)base.Region * base.ScaleLength);
					if (num3 * num3 / num4 / num4 + num2 * num2 / num5 / num5 < 1.0)
					{
						return true;
					}
				}
				flag = false;
			}
			else
			{
				flag = Math.Abs(Target.OriginalPosition.X - base.OriginalPosition.X) < (float)base.Region * base.ScaleWidth && Math.Abs(Target.OriginalPosition.Y - base.OriginalPosition.Y) < (float)base.Region * base.ScaleLength;
			}
			return flag;
		}
	}
}
