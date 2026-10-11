using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class BackgroundParticle3D2 : BaseParticle3D
	{
		public BackgroundParticle3D2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Background3D.BackgroundParticleList.Add(this);
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

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
