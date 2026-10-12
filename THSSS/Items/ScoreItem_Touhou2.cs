using System.Drawing;

namespace Shooting
{
	internal class ScoreItem_Touhou2 : BaseItem_Touhou
	{
		public ScoreItem_Touhou2(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-7", OriginalPosition)
		{
			base.Region = 5;
			base.AngularVelocity = 0f;
			base.DirectionDegree += (double)((float)base.Ran.Next(-49, 50) / 10f);
			base.MaxTransparent = 80;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 16f;
			base.Active = true;
		}

		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
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
		}
	}
}
