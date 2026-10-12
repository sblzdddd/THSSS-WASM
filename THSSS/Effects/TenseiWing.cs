using System;
using System.Drawing;

namespace Shooting
{
	public class TenseiWing : BaseEffect
	{
		public TenseiWing(StageDataPackage StageData)
			: base(StageData, null, PointF.Empty, 0f, 0.0)
		{
			if (Boss != null)
			{
				OriginalPosition = Boss.OriginalPosition;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Boss != null)
			{
				OriginalPosition = Boss.OriginalPosition;
				var num = 177f + 4f * (float)Math.Sin((double)((float)Time / 150f) * 3.141592653589793 * 2.0);
				num = num / 180f * 3.1415927f;
				var pointF = new PointF(OriginalPosition.X - 4f, OriginalPosition.Y);
				var num2 = 1f - 0.2f * (float)Math.Sin((double)((float)Time / 150f) * 3.141592653589793 * 2.0);
				var baseObject = new BaseObject(StageData, "beike1", PointF.Empty, 1f, (double)num);
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
				Background2.BackgroundList.Add(baseObject);
				num = 3f - 4f * (float)Math.Sin((double)((float)Time / 150f) * 3.141592653589793 * 2.0);
				num = num / 180f * 3.1415927f;
				pointF = new PointF(OriginalPosition.X + 4f, OriginalPosition.Y);
				num2 = 1f - 0.2f * (float)Math.Sin((double)((float)Time / 150f) * 3.141592653589793 * 2.0);
				baseObject = new BaseObject(StageData, "beike1", PointF.Empty, 1f, (double)num);
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
				Background2.BackgroundList.Add(baseObject);
				if (Boss.Life <= 0)
				{
					LifeTime = Time + 10;
				}
			}
		}
	}
}
