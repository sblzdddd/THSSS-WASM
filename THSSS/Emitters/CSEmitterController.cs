using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D6 RID: 214
	public class CSEmitterController : BaseBulleEmitter
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00034CC8 File Offset: 0x00032EC8
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00034CDF File Offset: 0x00032EDF
		public bool BossBinding { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00034CE8 File Offset: 0x00032EE8
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x00034CFF File Offset: 0x00032EFF
		public bool OnRoad { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00034D08 File Offset: 0x00032F08
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00034D1F File Offset: 0x00032F1F
		public PointF BaseOriginalPosition { get; set; }

		// Token: 0x060005EF RID: 1519 RVA: 0x00034D28 File Offset: 0x00032F28
		public CSEmitterController(StageDataPackage StageData, CS_Data CSData)
			: base(StageData)
		{
			this.CSData = CSData;
			this.BaseOriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 130f);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00034E80 File Offset: 0x00033080
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

		// Token: 0x0400020B RID: 523
		private CS_Data CSData;
	}
}
