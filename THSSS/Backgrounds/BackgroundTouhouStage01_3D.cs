using System;
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
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
		}

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

		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)this.Height);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

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
