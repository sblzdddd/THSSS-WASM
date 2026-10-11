using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000130 RID: 304
	internal class EndEnemy : BaseEffect
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00049D60 File Offset: 0x00047F60
		public EndEnemy(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = Velocity;
			base.Direction = Direction;
			base.Active = true;
			base.TransparentVelocity = -15f;
			base.ScaleVelocity = 0.1f;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00049DC4 File Offset: 0x00047FC4
		public override bool OutBoundary()
		{
			return base.TransparentValueF <= 0f;
		}
	}
}
