using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000024 RID: 36
	internal class FullPic2 : BaseObject
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000A0BC File Offset: 0x000082BC
		public FullPic2(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.Position = new PointF((float)(base.BoundRect.Left + 440), (float)(base.BoundRect.Top + 200));
			base.Velocity = 9f;
			base.DirectionDegree = 165.0;
			base.Active = false;
			base.LifeTime = 200;
			base.Accelerate = -0.2f;
			this.AccelerateDictionary.Add(90, 1f);
			this.TransparentVelocityDictionary.Add(120, -6f);
			base.AngleWithDirection = false;
			base.AngleDegree = 90.0;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Background2.BackgroundList.Add(this);
			base.OutsideRegion = 500;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A1BC File Offset: 0x000083BC
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
