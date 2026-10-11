using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BackgroundStage1_3D : BaseObject
	{
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		public BackgroundStage1_3D(StageDataPackage StageData)
			: base(StageData, "Background_Stage1", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background3D.BackgroundList.Add(this);
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(this.DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			base.Velocity = 1f;
		}

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

		public override bool OutBoundary()
		{
			return false;
		}

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

		private void SetupMatrices()
		{
			this.DeviceMain.SetTransform(TransformState.World, Matrix.Identity);
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(new Vector3(0f, -1400f - this.deltaY, 150f), new Vector3(0f, -1500f - this.deltaY, 0f), new Vector3(0f, -1f, 0f)));
		}

		public override void Show()
		{
			this.SetupVertex();
			this.SetupMatrices();
			this.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
			this.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			this.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		public override void Dispose()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexDecl.Dispose();
			}
		}

		private VertexBuffer vertexBuffer;

		private VertexDeclaration vertexDecl;

		private float deltaY = 0f;
	}
}
