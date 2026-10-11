using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E6 RID: 486
	internal class EnemyPlane_TouhouNormal : BaseEnemyPlane_Touhou
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x0006E1AE File Offset: 0x0006C3AE
		public EnemyPlane_TouhouNormal(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: this(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			base.ColorType = ColorType;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0006E1C9 File Offset: 0x0006C3C9
		public EnemyPlane_TouhouNormal(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, null, OriginalPosition, Velocity, Direction)
		{
			base.TxtureObject = base.TextureObjectDictionary[EnemyName];
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0006E1EE File Offset: 0x0006C3EE
		public override void TextureCtrl()
		{
		}
	}
}
