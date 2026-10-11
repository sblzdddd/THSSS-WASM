using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000114 RID: 276
	internal class BackgroundBlack : BaseObject
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00043D24 File Offset: 0x00041F24
		public BackgroundBlack(StageDataPackage StageData, PointF Position)
			: base(StageData, "MyBullet0008", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.Position = Position;
			base.Scale = 1f;
			base.LifeTime = 350;
			base.TransparentValueF = 0f;
			base.MaxTransparent = 180;
			base.ColorValue = Color.Black;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00043DB4 File Offset: 0x00041FB4
		public override void Ctrl()
		{
			base.Time++;
			base.Scale += 1f;
			if (base.Time < 40)
			{
				base.TransparentValueF += 6f;
			}
			if (base.Time > base.LifeTime - 40)
			{
				base.TransparentValueF -= 6f;
			}
		}
	}
}
