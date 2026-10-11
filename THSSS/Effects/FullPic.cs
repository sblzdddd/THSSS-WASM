using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200008B RID: 139
	internal class FullPic : BaseObject
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public FullPic(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.Position = new PointF((float)(base.BoundRect.Left + 250), (float)(base.BoundRect.Top + 150));
			base.Velocity = 2f;
			base.Direction = 1.5707963267948966;
			base.Active = false;
			base.LifeTime = 200;
			base.Accelerate = -0.025f;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Background2.BackgroundList.Add(this);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00020C04 File Offset: 0x0001EE04
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Velocity < 0f)
			{
				base.Velocity = 0f;
			}
			if (base.Time > 40)
			{
				base.TransparentVelocity = -5f;
			}
		}
	}
}
