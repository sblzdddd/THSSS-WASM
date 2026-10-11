using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200001A RID: 26
	public class BaseEffect : BaseObject_CS
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008954 File Offset: 0x00006B54
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000896B File Offset: 0x00006B6B
		public int Layer { get; set; }

		// Token: 0x06000174 RID: 372 RVA: 0x00008974 File Offset: 0x00006B74
		public BaseEffect()
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000897F File Offset: 0x00006B7F
		public BaseEffect(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.EffectList.Add(this);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000899F File Offset: 0x00006B9F
		public BaseEffect(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.EffectList.Add(this);
			base.Active = true;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000089C8 File Offset: 0x00006BC8
		public override void Ctrl()
		{
			base.Ctrl();
			this.Shoot();
			if (this.OutBoundary())
			{
				base.EffectList.Remove(this);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008A00 File Offset: 0x00006C00
		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
