using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C6 RID: 198
	internal class BackgroundTouhouStage01_3D : BaseBackgroundTouhouStage_3D
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00031280 File Offset: 0x0002F480
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00031297 File Offset: 0x0002F497
		public int Height { get; set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x000312A0 File Offset: 0x0002F4A0
		public BackgroundTouhouStage01_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000312C8 File Offset: 0x0002F4C8
		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-500f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(-500f, 1000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 0f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(500f, 1000f, 0f), new Vector2(1f, 1f))
			});
			this.vertexBuffer.Unlock();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000313D4 File Offset: 0x0002F5D4
		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)this.Height);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0003142C File Offset: 0x0002F62C
		public override void Show()
		{
			base.Show();
			base.OriginalPosition = new PointF(base.OriginalPosition.X - 1000f, base.OriginalPosition.Y);
			this.SetupMatrices();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			base.OriginalPosition = new PointF(base.OriginalPosition.X + 1000f, base.OriginalPosition.Y);
			this.SetupMatrices();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			base.OriginalPosition = new PointF(base.OriginalPosition.X + 1000f, base.OriginalPosition.Y);
			this.SetupMatrices();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			base.OriginalPosition = new PointF(base.OriginalPosition.X - 1000f, base.OriginalPosition.Y);
		}
	}
}
