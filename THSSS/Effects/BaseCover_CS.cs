using System;
using System.Collections.Generic;

namespace Shooting
{
	[Serializable]
	public class BaseCover_CS : BaseEmitter_CS
	{
		public bool Roundness { get; set; }

		public int CtrlID { get; set; }

		public List<EventGroup> SubEventGroupList { get; set; }

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

		public override void Shoot()
		{
			base.AngleWithDirection = this.Roundness;
		}

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
