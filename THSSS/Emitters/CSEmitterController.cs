using System;
using System.Drawing;

namespace Shooting
{
	public class CSEmitterController : BaseBulleEmitter
	{
		public bool BossBinding { get; set; }

		public bool OnRoad { get; set; }

		public PointF BaseOriginalPosition { get; set; }

		public CSEmitterController(StageDataPackage StageData, CS_Data CSData)
			: base(StageData)
		{
			this.CSData = CSData;
			this.BaseOriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 130f);
		}

		public override void Shoot()
		{
			if (base.Time >= this.CSData.TimeTotal)
			{
				base.Time -= this.CSData.TimeTotal;
			}
			if (!this.OnRoad || base.Boss == null)
			{
				this.CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
				{
					if (em.BindingID == -1)
					{
						if (em.StartTime == base.Time)
						{
							BaseEmitter_CS baseEmitter_CS = (BaseEmitter_CS)em.Clone();
							if (this.BossBinding && base.Boss != null)
							{
								baseEmitter_CS.OriginalPosition = new PointF(baseEmitter_CS.OriginalPosition.X + base.Boss.OriginalPosition.X - this.BaseOriginalPosition.X, baseEmitter_CS.OriginalPosition.Y + base.Boss.OriginalPosition.Y - this.BaseOriginalPosition.Y);
								baseEmitter_CS.EmitPoint = new PointF(-99998f, -99998f);
								baseEmitter_CS.SetBinding(base.Boss);
							}
							baseEmitter_CS.Time = base.Time;
							base.StageData.EnemyPlaneList.Add(baseEmitter_CS);
						}
					}
				});
			}
		}

		private CS_Data CSData;
	}
}
