using System;
using System.Drawing;

namespace Shooting
{
	internal class GameTitle : BaseEffect
	{
		public GameTitle(StageDataPackage StageData, string textureName, Point Position, Point DestPoint)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Position = Position;
			base.DestPoint = DestPoint;
			base.Velocity = 5f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
			base.TransparentValueF += 2f;
		}
	}
}
