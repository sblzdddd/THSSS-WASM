using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Model_Triangle : Model, IModel
	{
		public Model_Triangle()
		{
		}

		public Model_Triangle(Device DeviceMain, TextureObject TxtureObject, string Name, Vector2 LeftTop, Vector2 RightTop, Vector2 LeftBottom, int Width, int Height)
		{
			base.TransparentValue = 255;
			base.ColorValue = Color.White;
			base.DeviceMain = DeviceMain;
			base.TxtureObject = TxtureObject;
			base.Name = Name;
			this.LeftTop = LeftTop;
			this.RightTop = RightTop;
			this.LeftBottom = LeftBottom;
			this.Width = Width;
			this.Height = Height;
			this.vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(DeviceMain, 3 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			this.material = default(Material);
			this.SetupVertex();
		}

		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(0f, 0f, 0f), this.LeftTop),
				new TexturedVertex(new Vector3((float)this.Width, 0f, 0f), this.RightTop),
				new TexturedVertex(new Vector3(0f, (float)this.Height, 0f), this.LeftBottom)
			});
			this.vertexBuffer.Unlock();
		}

		public override void SetModel()
		{
			base.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			base.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
		}

		public override void Draw()
		{
			base.SetupMaterial();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
		}

		public Vector2 LeftTop;

		public Vector2 RightTop;

		public Vector2 LeftBottom;

		public int Width;

		public int Height;
	}
}
