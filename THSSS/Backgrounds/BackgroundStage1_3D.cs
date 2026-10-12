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
				return StageData.DeviceMain;
			}
		}

		public BackgroundStage1_3D(StageDataPackage StageData)
			: base(StageData, "Background_Stage1", new PointF(0f, 0f), 0f, 0.0)
		{
			Background3D.BackgroundList.Add(this);
			vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			vertexBuffer = new VertexBuffer(DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			Velocity = 1f;
		}

		public override void Ctrl()
		{
			Time++;
			if (Time == 660)
			{
				Velocity = 5f;
			}
			deltaY += Velocity;
			if (deltaY > 1000f)
			{
				deltaY = 0f;
			}
		}

		public override bool OutBoundary()
		{
			return false;
		}

		private void SetupVertex()
		{
			var dataStream = vertexBuffer.Lock(0, 0, LockFlags.None);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-1000f, -4000f, 0f), new Vector2(0f, 4f)),
				new TexturedVertex(new Vector3(1000f, -4000f, 0f), new Vector2(1f, 4f)),
				new TexturedVertex(new Vector3(-1000f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(1000f, 0f, 0f), new Vector2(1f, 0f))
			});
			vertexBuffer.Unlock();
		}

		private void SetupMatrices()
		{
			DeviceMain.SetTransform(TransformState.World, Matrix.Identity);
			DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(new Vector3(0f, -1400f - deltaY, 150f), new Vector3(0f, -1500f - deltaY, 0f), new Vector3(0f, -1f, 0f)));
		}

		public override void Show()
		{
			SetupVertex();
			SetupMatrices();
			DeviceMain.SetTexture(0, TxtureObject.TXTure);
			DeviceMain.SetStreamSource(0, vertexBuffer, 0, TexturedVertex.SizeBytes);
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		public override void Dispose()
		{
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
				vertexDecl.Dispose();
			}
		}

		private VertexBuffer vertexBuffer;

		private VertexDeclaration vertexDecl;

		private float deltaY = 0f;
	}
}
