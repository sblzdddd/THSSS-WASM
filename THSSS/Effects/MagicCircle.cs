using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200008C RID: 140
	internal class MagicCircle : BaseEffect
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00020C5A File Offset: 0x0001EE5A
		public MagicCircle(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00020C84 File Offset: 0x0001EE84
		public override void Move()
		{
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += 0.019999999552965164;
			base.Scale += (float)Math.Sin((double)((float)base.Time * 3f / 180f) * 3.141592653589793) / 80f;
			base.TransparentValueF = (float)(100 + (int)(Math.Sin((double)((float)base.Time * 2f / 180f) * 3.141592653589793) * 75.0));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00020D58 File Offset: 0x0001EF58
		public override bool OutBoundary()
		{
			return base.Boss == null;
		}
	}
}
