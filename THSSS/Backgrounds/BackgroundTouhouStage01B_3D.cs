using System;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200013E RID: 318
	internal class BackgroundTouhouStage01B_3D : BaseBackgroundTouhouStage_3D
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0004CC9C File Offset: 0x0004AE9C
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0004CCB3 File Offset: 0x0004AEB3
		public int Height { get; set; }

		// Token: 0x0600084B RID: 2123 RVA: 0x0004CCBC File Offset: 0x0004AEBC
		public BackgroundTouhouStage01B_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 6 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0004CCE4 File Offset: 0x0004AEE4
		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-2000f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(-2000f, 1000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(0f, 0f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(0f, 1000f, 0f), new Vector2(1f, 1f)),
				new TexturedVertex(new Vector3(2000f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(2000f, 1000f, 0f), new Vector2(0f, 1f))
			});
			this.vertexBuffer.Unlock();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0004CE58 File Offset: 0x0004B058
		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)this.Height);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0004CEAD File Offset: 0x0004B0AD
		public override void Show()
		{
			base.Show();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 4);
		}
	}
}
