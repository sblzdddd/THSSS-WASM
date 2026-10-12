using System.Drawing;

namespace Shooting
{
	public class EnchantmentMagicCircle : BaseEffect
	{
		public EnchantmentMagicCircle(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			switch (MyPlane.EnchantmentState)
			{
			case EnchantmentType.Red:
				TxtureObject = TextureObjectDictionary["MC-R"];
				break;
			case EnchantmentType.Blue:
				TxtureObject = TextureObjectDictionary["MC-B"];
				break;
			case EnchantmentType.Green:
				TxtureObject = TextureObjectDictionary["MC-G"];
				break;
			}
			LifeTime = MyPlane.EnchantmentTime;
			TransparentValueF = 0f;
			TransparentVelocity = 20f;
			MaxTransparent = 160;
			AngularVelocityDegree = 5f;
			Active = true;
		}

		public override bool OutBoundary()
		{
			return (LifeTime != 0 && Time > LifeTime) || MyPlane.EnchantmentState == EnchantmentType.None;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			OriginalPosition = MyPlane.OriginalPosition;
			Scale = 0.2f + (float)MyPlane.EnchantmentTime * 1.5f / (float)LifeTime;
		}
	}
}
