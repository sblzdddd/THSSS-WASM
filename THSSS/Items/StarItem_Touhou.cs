using System;
using System.Drawing;

namespace Shooting
{
	internal class StarItem_Touhou : BaseItem_Touhou
	{
		public StarItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "SmallStar", OriginalPosition)
		{
			base.Region = 5;
			base.AngularVelocity = 0f;
			base.DirectionDegree += (double)((float)base.Ran.Next(-49, 50) / 10f);
			base.MaxTransparent = 80;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 16f;
			base.Active = true;
			base.AngleWithDirection = false;
		}

		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.StarPoint++;
				base.MyPlane.Score += 100L;
				if (++base.MyPlane.GreenPoint % 2 == 0)
				{
					base.MyPlane.HighItemScore += 10;
				}
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 40)
			{
				base.Obtain = true;
			}
			base.Angle = 0.0;
		}
	}
}
