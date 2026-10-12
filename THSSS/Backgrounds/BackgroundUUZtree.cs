using SlimDX.Direct3D9;

namespace Shooting
{
	public class BackgroundUUZtree : BackgroundFix
	{
		public BackgroundUUZtree(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.TransparentValueF = 150f;
		}

		public override void Show()
		{
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.StageData.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.StageData.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.StageData.DeviceMain.SetRenderState<BlendOperation>(RenderState.BlendOperation, BlendOperation.ReverseSubtract);
			base.Show();
			base.Show();
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
		}
	}
}
