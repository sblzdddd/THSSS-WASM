using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200015E RID: 350
	internal class BackgroundTouhouStage01C_3D : BaseBackgroundTouhouStage_3D
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000545B0 File Offset: 0x000527B0
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000545C7 File Offset: 0x000527C7
		public int Height { get; set; }

		// Token: 0x060008D8 RID: 2264 RVA: 0x000545D0 File Offset: 0x000527D0
		public BackgroundTouhouStage01C_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000545F8 File Offset: 0x000527F8
		public override void Ctrl()
		{
			base.Ctrl();
			base.Velocity = 0.2f * (float)Math.Sin((double)(base.Time / 200));
			if (base.OriginalPosition.X < -1000f)
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X + 1000f, base.OriginalPosition.Y);
			}
			else if (base.OriginalPosition.X > 1000f)
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X - 1000f, base.OriginalPosition.Y);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000546C8 File Offset: 0x000528C8
		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-500f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(-500f, 1000f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(500f, 0f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 1000f, 0f), new Vector2(1f, 1f))
			});
			this.vertexBuffer.Unlock();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000547D4 File Offset: 0x000529D4
		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)this.Height);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0005482C File Offset: 0x00052A2C
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
