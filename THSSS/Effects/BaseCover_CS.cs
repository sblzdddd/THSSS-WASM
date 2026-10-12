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
			this.StageData = StageData;
			EventGroupList = new List<EventGroup>();
			EventsExecutionList = new List<Execution>();
			SubEventGroupList = new List<EventGroup>();
			HealthPoint = 1000000f;
			HitEnabled = false;
			Region = 1;
			Angle = 1.5707963267948966;
		}

		public override void Shoot()
		{
			AngleWithDirection = Roundness;
		}

		public override void EventCtrl()
		{
			EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		public override void HitCheckAll()
		{
			if (Time >= StartTime && Time <= StartTime + Duration)
			{
				BulletList.ForEach(delegate(BaseBullet_Touhou b)
				{
					if (b.Cover)
					{
						if (Type == 0 || b.ID == CtrlID)
						{
							if (HitCheck(b))
							{
								SubEventGroupList.ForEach(delegate(EventGroup e)
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
			if (Roundness)
			{
				if (ScaleLength == ScaleWidth)
				{
					if (GetDistance(Target) - (double)Target.Region < (double)((float)Region * Scale))
					{
						return true;
					}
				}
				else
				{
					var num = (AngleWithDirection ? (Direction + Angle) : Angle);
					var distance = GetDistance(Target);
					var direction = GetDirection(Target);
					var num2 = distance * Math.Cos((double)((float)(num - direction)));
					var num3 = distance * Math.Sin((double)((float)(num - direction)));
					var num4 = (double)((float)Region * ScaleWidth);
					var num5 = (double)((float)Region * ScaleLength);
					if (num3 * num3 / num4 / num4 + num2 * num2 / num5 / num5 < 1.0)
					{
						return true;
					}
				}
				flag = false;
			}
			else
			{
				flag = Math.Abs(Target.OriginalPosition.X - OriginalPosition.X) < (float)Region * ScaleWidth && Math.Abs(Target.OriginalPosition.Y - OriginalPosition.Y) < (float)Region * ScaleLength;
			}
			return flag;
		}
	}
}
