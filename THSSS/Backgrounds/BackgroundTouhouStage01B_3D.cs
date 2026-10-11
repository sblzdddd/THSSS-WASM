using System;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BackgroundTouhouStage01B_3D : BaseBackgroundTouhouStage_3D
	{
		public int Height { get; set; }

		public BackgroundTouhouStage01B_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 6 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

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

		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)this.Height);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			base.Show();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 4);
		}
	}
}
