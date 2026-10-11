using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C7 RID: 199
	internal class BackgroundStage1_3D : BaseObject
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0003153C File Offset: 0x0002F73C
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0003155C File Offset: 0x0002F75C
		public BackgroundStage1_3D(StageDataPackage StageData)
			: base(StageData, "Background_Stage1", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background3D.BackgroundList.Add(this);
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(this.DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			base.Velocity = 1f;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000315F0 File Offset: 0x0002F7F0
		public override void Ctrl()
		{
			base.Time++;
			if (base.Time == 660)
			{
				base.Velocity = 5f;
			}
			this.deltaY += base.Velocity;
			if (this.deltaY > 1000f)
			{
				this.deltaY = 0f;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00031664 File Offset: 0x0002F864
		public override bool OutBoundary()
		{
			return false;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00031678 File Offset: 0x0002F878
		private void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.None);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-1000f, -4000f, 0f), new Vector2(0f, 4f)),
				new TexturedVertex(new Vector3(1000f, -4000f, 0f), new Vector2(1f, 4f)),
				new TexturedVertex(new Vector3(-1000f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(1000f, 0f, 0f), new Vector2(1f, 0f))
			});
			this.vertexBuffer.Unlock();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00031780 File Offset: 0x0002F980
		private void SetupMatrices()
		{
			this.DeviceMain.SetTransform(TransformState.World, Matrix.Identity);
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(new Vector3(0f, -1400f - this.deltaY, 150f), new Vector3(0f, -1500f - this.deltaY, 0f), new Vector3(0f, -1f, 0f)));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00031800 File Offset: 0x0002FA00
		public override void Show()
		{
			this.SetupVertex();
			this.SetupMatrices();
			this.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
			this.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			this.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0003185C File Offset: 0x0002FA5C
		public override void Dispose()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexDecl.Dispose();
			}
		}

		// Token: 0x040001E3 RID: 483
		private VertexBuffer vertexBuffer;

		// Token: 0x040001E4 RID: 484
		private VertexDeclaration vertexDecl;

		// Token: 0x040001E5 RID: 485
		private float deltaY = 0f;
	}
}
