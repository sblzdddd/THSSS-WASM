using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BackgroundTouhouStage02_3D : BaseBackgroundTouhouStage_3D
	{
		public BackgroundTouhouStage02_3D(StageDataPackage StageData)
			: base(StageData, " ")
		{
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 14 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-500f, 6000f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(500f, 6000f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(-500f, 5000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 5000f, 0f), new Vector2(1f, 1f)),
				new TexturedVertex(new Vector3(-500f, 4000f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(500f, 4000f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(-500f, 3000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 3000f, 0f), new Vector2(1f, 1f)),
				new TexturedVertex(new Vector3(-500f, 2000f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(500f, 2000f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(-500f, 1000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 1000f, 0f), new Vector2(1f, 1f)),
				new TexturedVertex(new Vector3(-500f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(500f, 0f, 0f), new Vector2(1f, 0f))
			});
			this.vertexBuffer.Unlock();
		}

		public override void Show()
		{
			base.Show();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 12);
		}
	}
}
