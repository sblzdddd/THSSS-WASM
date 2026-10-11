using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000122 RID: 290
	public class BackgroundParticle3D2 : BaseParticle3D
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00047238 File Offset: 0x00045438
		public BackgroundParticle3D2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Background3D.BackgroundParticleList.Add(this);
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00047274 File Offset: 0x00045474
		public override void Ctrl()
		{
			base.Ctrl();
			if (0 < base.Time && base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
			else if (base.LifeTime * 2 / 3 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00047318 File Offset: 0x00045518
		public override void Show()
		{
			base.DeviceMain.SetRenderState(RenderState.FogEnable, true);
			base.DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			base.DeviceMain.SetRenderState(RenderState.FogColor, Color.FromArgb(160, 190, 230).ToArgb());
			base.DeviceMain.SetRenderState(RenderState.FogStart, 0f);
			base.DeviceMain.SetRenderState(RenderState.FogEnd, 400f);
			base.Show();
			base.DeviceMain.SetRenderState(RenderState.FogEnable, false);
		}
	}
}
