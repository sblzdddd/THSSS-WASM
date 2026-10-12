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
			BaseOriginalPosition = new PointF((float)(BoundRect.Width / 2), 130f);
		}

		public override void Shoot()
		{
			if (Time >= CSData.TimeTotal)
			{
				Time -= CSData.TimeTotal;
			}
			if (!OnRoad || Boss == null)
			{
				CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
				{
					if (em.BindingID == -1)
					{
						if (em.StartTime == Time)
						{
							var baseEmitter_CS = (BaseEmitter_CS)em.Clone();
							if (BossBinding && Boss != null)
							{
								baseEmitter_CS.OriginalPosition = new PointF(baseEmitter_CS.OriginalPosition.X + Boss.OriginalPosition.X - BaseOriginalPosition.X, baseEmitter_CS.OriginalPosition.Y + Boss.OriginalPosition.Y - BaseOriginalPosition.Y);
								baseEmitter_CS.EmitPoint = new PointF(-99998f, -99998f);
								baseEmitter_CS.SetBinding(Boss);
							}
							baseEmitter_CS.Time = Time;
							StageData.EnemyPlaneList.Add(baseEmitter_CS);
						}
					}
				});
			}
		}

		private CS_Data CSData;
	}
}
