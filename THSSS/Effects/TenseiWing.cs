using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001F6 RID: 502
	public class TenseiWing : BaseEffect
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x00070B50 File Offset: 0x0006ED50
		public TenseiWing(StageDataPackage StageData)
			: base(StageData, null, PointF.Empty, 0f, 0.0)
		{
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00070B9C File Offset: 0x0006ED9C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
				float num = 177f + 4f * (float)Math.Sin((double)((float)base.Time / 150f) * 3.141592653589793 * 2.0);
				num = num / 180f * 3.1415927f;
				PointF pointF = new PointF(base.OriginalPosition.X - 4f, base.OriginalPosition.Y);
				float num2 = 1f - 0.2f * (float)Math.Sin((double)((float)base.Time / 150f) * 3.141592653589793 * 2.0);
				BaseObject baseObject = new BaseObject(base.StageData, "beike1", PointF.Empty, 1f, (double)num);
				baseObject.OriginalPosition = pointF;
				baseObject.LifeTime = 20;
				baseObject.TransparentValueF = 30f;
				baseObject.AngleWithDirection = true;
				baseObject.ColorValue = Color.FromArgb(220, 220, 255);
				baseObject.Active = true;
				baseObject.AngleDegree = -90.0;
				baseObject.TransparentVelocity = -baseObject.TransparentValueF / (float)baseObject.LifeTime;
				baseObject.ScaleVelocity = 0.3f / (float)baseObject.LifeTime;
				baseObject.ScaleWidth = num2;
				base.Background2.BackgroundList.Add(baseObject);
				num = 3f - 4f * (float)Math.Sin((double)((float)base.Time / 150f) * 3.141592653589793 * 2.0);
				num = num / 180f * 3.1415927f;
				pointF = new PointF(base.OriginalPosition.X + 4f, base.OriginalPosition.Y);
				num2 = 1f - 0.2f * (float)Math.Sin((double)((float)base.Time / 150f) * 3.141592653589793 * 2.0);
				baseObject = new BaseObject(base.StageData, "beike1", PointF.Empty, 1f, (double)num);
				baseObject.OriginalPosition = pointF;
				baseObject.LifeTime = 20;
				baseObject.TransparentValueF = 30f;
				baseObject.AngleWithDirection = true;
				baseObject.ColorValue = Color.FromArgb(220, 220, 255);
				baseObject.Active = true;
				baseObject.AngleDegree = 90.0;
				baseObject.TransparentVelocity = -baseObject.TransparentValueF / (float)baseObject.LifeTime;
				baseObject.ScaleVelocity = 0.3f / (float)baseObject.LifeTime;
				baseObject.ScaleWidth = num2;
				baseObject.Mirrored = true;
				base.Background2.BackgroundList.Add(baseObject);
				if (base.Boss.Life <= 0)
				{
					base.LifeTime = base.Time + 10;
				}
			}
		}
	}
}
