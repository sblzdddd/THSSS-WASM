using System;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000140 RID: 320
	public class BackgroundUUZtree : BackgroundFix
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0004D9E2 File Offset: 0x0004BBE2
		public BackgroundUUZtree(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.TransparentValueF = 150f;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0004D9FC File Offset: 0x0004BBFC
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
