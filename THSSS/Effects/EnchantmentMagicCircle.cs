using System.Drawing;

namespace Shooting
{
	public class EnchantmentMagicCircle : BaseEffect
	{
		public EnchantmentMagicCircle(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			switch (base.MyPlane.EnchantmentState)
			{
			case EnchantmentType.Red:
				base.TxtureObject = base.TextureObjectDictionary["MC-R"];
				break;
			case EnchantmentType.Blue:
				base.TxtureObject = base.TextureObjectDictionary["MC-B"];
				break;
			case EnchantmentType.Green:
				base.TxtureObject = base.TextureObjectDictionary["MC-G"];
				break;
			}
			base.LifeTime = base.MyPlane.EnchantmentTime;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.MaxTransparent = 160;
			base.AngularVelocityDegree = 5f;
			base.Active = true;
		}

		public override bool OutBoundary()
		{
			return (base.LifeTime != 0 && base.Time > base.LifeTime) || base.MyPlane.EnchantmentState == EnchantmentType.None;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.OriginalPosition = base.MyPlane.OriginalPosition;
			base.Scale = 0.2f + (float)base.MyPlane.EnchantmentTime * 1.5f / (float)base.LifeTime;
		}
	}
}
