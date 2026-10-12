using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BackgroundTouhouStage01_3D : BaseBackgroundTouhouStage_3D
	{
		public int Height { get; set; }

		public BackgroundTouhouStage01_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			vertexBuffer = new VertexBuffer(DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

		public override void SetupVertex()
		{
			var dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(-500f, 0f, 0f), new Vector2(0f, 0f)),
				new TexturedVertex(new Vector3(-500f, 1000f, 0f), new Vector2(0f, 1f)),
				new TexturedVertex(new Vector3(500f, 0f, 0f), new Vector2(1f, 0f)),
				new TexturedVertex(new Vector3(500f, 1000f, 0f), new Vector2(1f, 1f))
			});
			vertexBuffer.Unlock();
		}

		public override void SetupMatrices()
		{
			var matrix = Matrix.Identity;
			matrix *= Matrix.Translation(OriginalPosition.X, OriginalPosition.Y, (float)Height);
			DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			base.Show();
			OriginalPosition = new PointF(OriginalPosition.X - 1000f, OriginalPosition.Y);
			SetupMatrices();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			OriginalPosition = new PointF(OriginalPosition.X + 1000f, OriginalPosition.Y);
			SetupMatrices();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			OriginalPosition = new PointF(OriginalPosition.X + 1000f, OriginalPosition.Y);
			SetupMatrices();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			OriginalPosition = new PointF(OriginalPosition.X - 1000f, OriginalPosition.Y);
		}
	}
}
