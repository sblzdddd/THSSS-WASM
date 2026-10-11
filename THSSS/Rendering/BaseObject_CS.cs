using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BaseObject_CS : BaseObject
	{
		public virtual int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public bool Ghosting
		{
			get
			{
				return base.GhostingCount > 0;
			}
			set
			{
				if (value)
				{
					base.GhostingCount = 20;
					base.GhostingColor = Color.White;
				}
				else
				{
					base.GhostingCount = 0;
				}
			}
		}

		public bool OutBound
		{
			get
			{
				return base.OutsideRegion < 80;
			}
			set
			{
				if (!value)
				{
					base.OutsideRegion = 200;
				}
			}
		}

		public bool BeginningEffect { get; set; }

		public bool EndingEffect { get; set; }

		public PointF CS_Position
		{
			get
			{
				return new PointF(base.OriginalPosition.X + 320f - 192f, base.OriginalPosition.Y + 240f - 224f);
			}
			set
			{
				base.OriginalPosition = new PointF(value.X - 320f + 192f, value.Y - 240f + 224f);
			}
		}

		public int ID { get; set; }

		public int LayerID { get; set; }

		public float AccelerateCS { get; set; }

		public double AccDirection { get; set; }

		public double RanAngle { get; set; }

		public float RanVelocity { get; set; }

		public double RanDirection { get; set; }

		public float RanAccelerate { get; set; }

		public double RanAccDirection { get; set; }

		public bool Cover { get; set; }

		public bool Rebound { get; set; }

		public bool Force { get; set; }

		public List<EventGroup> EventGroupList { get; set; }

		public List<Execution> EventsExecutionList { get; set; }

		public BaseObject_CS()
		{
		}

		public BaseObject_CS(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public BaseObject_CS(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
		}

		public override void Ctrl()
		{
			double num = this.AccDirection / 180.0 * 3.141592653589793;
			base.Ax = this.AccelerateCS * (float)Math.Cos(num);
			base.Ay = this.AccelerateCS * (float)Math.Sin(num);
			this.EventCtrl();
			base.Ctrl();
		}

		public virtual void EventCtrl()
		{
			if (this.EventGroupList != null)
			{
				this.EventGroupList.ForEach(delegate(EventGroup a)
				{
					a.Update(this);
				});
			}
			if (this.EventsExecutionList != null)
			{
				this.EventsExecutionList.ForEach(delegate(Execution a)
				{
					a.Update(this);
				});
			}
		}

		public override object Clone()
		{
			BaseObject_CS b = (BaseObject_CS)base.Clone();
			b.EventsExecutionList = new List<Execution>();
			b.EventGroupList = new List<EventGroup>();
			this.EventGroupList.ForEach(delegate(EventGroup x)
			{
				b.EventGroupList.Add((EventGroup)x.Clone());
			});
			return b;
		}

		public int type;
	}
}
