using SlimDX.Direct3D9;

namespace Shooting
{
	public class BackgroundUUZtree : BackgroundFix
	{
		public BackgroundUUZtree(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			TransparentValueF = 150f;
		}

		public override void Show()
		{
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			StageData.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			StageData.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			StageData.DeviceMain.SetRenderState<BlendOperation>(RenderState.BlendOperation, BlendOperation.ReverseSubtract);
			base.Show();
			base.Show();
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
		}
	}
}
