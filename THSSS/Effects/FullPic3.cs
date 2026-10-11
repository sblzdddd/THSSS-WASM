using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000200 RID: 512
	internal class FullPic3 : BaseObject
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x000722F4 File Offset: 0x000704F4
		public FullPic3(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.Position = new PointF((float)(base.BoundRect.Left + base.BoundRect.Width / 2), (float)(base.BoundRect.Bottom + 100));
			base.Velocity = 9f;
			base.DirectionDegree = -90.0;
			base.Active = false;
			base.LifeTime = 200;
			base.Accelerate = -0.2f;
			this.AccelerateDictionary.Add(90, 0.1f);
			this.TransparentVelocityDictionary.Add(90, -10f);
			base.AngleWithDirection = false;
			base.AngleDegree = 90.0;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Background2.BackgroundList.Add(this);
			base.OutsideRegion = 500;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000723FC File Offset: 0x000705FC
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Velocity < 1.2f)
			{
				base.Velocity = 1.2f;
			}
		}
	}
}
