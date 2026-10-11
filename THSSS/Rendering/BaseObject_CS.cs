using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000019 RID: 25
	public class BaseObject_CS : BaseObject
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00008470 File Offset: 0x00006670
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00008488 File Offset: 0x00006688
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00008494 File Offset: 0x00006694
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000084B0 File Offset: 0x000066B0
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000084EC File Offset: 0x000066EC
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00008508 File Offset: 0x00006708
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000852C File Offset: 0x0000672C
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00008543 File Offset: 0x00006743
		public bool BeginningEffect { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000854C File Offset: 0x0000674C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00008563 File Offset: 0x00006763
		public bool EndingEffect { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000856C File Offset: 0x0000676C
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000085B7 File Offset: 0x000067B7
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000085EC File Offset: 0x000067EC
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00008603 File Offset: 0x00006803
		public int ID { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000860C File Offset: 0x0000680C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00008623 File Offset: 0x00006823
		public int LayerID { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000862C File Offset: 0x0000682C
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00008643 File Offset: 0x00006843
		public float AccelerateCS { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000864C File Offset: 0x0000684C
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008663 File Offset: 0x00006863
		public double AccDirection { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000866C File Offset: 0x0000686C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00008683 File Offset: 0x00006883
		public double RanAngle { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000868C File Offset: 0x0000688C
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000086A3 File Offset: 0x000068A3
		public float RanVelocity { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000086AC File Offset: 0x000068AC
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000086C3 File Offset: 0x000068C3
		public double RanDirection { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000086CC File Offset: 0x000068CC
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000086E3 File Offset: 0x000068E3
		public float RanAccelerate { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000086EC File Offset: 0x000068EC
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00008703 File Offset: 0x00006903
		public double RanAccDirection { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000870C File Offset: 0x0000690C
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00008723 File Offset: 0x00006923
		public bool Cover { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000872C File Offset: 0x0000692C
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00008743 File Offset: 0x00006943
		public bool Rebound { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000874C File Offset: 0x0000694C
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00008763 File Offset: 0x00006963
		public bool Force { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000876C File Offset: 0x0000696C
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00008783 File Offset: 0x00006983
		public List<EventGroup> EventGroupList { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000878C File Offset: 0x0000698C
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000087A3 File Offset: 0x000069A3
		public List<Execution> EventsExecutionList { get; set; }

		// Token: 0x0600016A RID: 362 RVA: 0x000087AC File Offset: 0x000069AC
		public BaseObject_CS()
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000087B7 File Offset: 0x000069B7
		public BaseObject_CS(StageDataPackage StageData)
			: base(StageData)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000087C3 File Offset: 0x000069C3
		public BaseObject_CS(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000087D8 File Offset: 0x000069D8
		public override void Ctrl()
		{
			double num = this.AccDirection / 180.0 * 3.141592653589793;
			base.Ax = this.AccelerateCS * (float)Math.Cos(num);
			base.Ay = this.AccelerateCS * (float)Math.Sin(num);
			this.EventCtrl();
			base.Ctrl();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008850 File Offset: 0x00006A50
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

		// Token: 0x0600016F RID: 367 RVA: 0x000088E8 File Offset: 0x00006AE8
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

		// Token: 0x04000066 RID: 102
		public int type;
	}
}
