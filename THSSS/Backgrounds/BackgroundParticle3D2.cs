using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class BackgroundParticle3D2 : BaseParticle3D
	{
		public BackgroundParticle3D2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			Background3D.BackgroundParticleList.Add(this);
			TransparentValueF = 0f;
			this.LifeTime = LifeTime;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (0 < Time && Time < LifeTime / 3)
			{
				TransparentValueF += (float)(MaxTransparent * 3 / LifeTime);
			}
			else if (LifeTime * 2 / 3 < Time && Time < LifeTime)
			{
				TransparentValueF -= (float)(MaxTransparent * 3 / LifeTime);
			}
		}

		public override void Show()
		{
			DeviceMain.SetRenderState(RenderState.FogEnable, true);
			DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			DeviceMain.SetRenderState(RenderState.FogColor, Color.FromArgb(160, 190, 230).ToArgb());
			DeviceMain.SetRenderState(RenderState.FogStart, 0f);
			DeviceMain.SetRenderState(RenderState.FogEnd, 400f);
			base.Show();
			DeviceMain.SetRenderState(RenderState.FogEnable, false);
		}
	}
}
