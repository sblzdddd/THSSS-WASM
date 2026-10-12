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
			TransparentValue = 255;
			ColorValue = Color.White;
			this.DeviceMain = DeviceMain;
			this.TxtureObject = TxtureObject;
			this.Name = Name;
			this.LeftTop = LeftTop;
			this.RightTop = RightTop;
			this.LeftBottom = LeftBottom;
			this.Width = Width;
			this.Height = Height;
			vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			vertexBuffer = new VertexBuffer(DeviceMain, 3 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			material = default(Material);
			SetupVertex();
		}

		public override void SetupVertex()
		{
			DataStream dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(0f, 0f, 0f), LeftTop),
				new TexturedVertex(new Vector3((float)Width, 0f, 0f), RightTop),
				new TexturedVertex(new Vector3(0f, (float)Height, 0f), LeftBottom)
			});
			vertexBuffer.Unlock();
		}

		public override void SetModel()
		{
			DeviceMain.SetStreamSource(0, vertexBuffer, 0, TexturedVertex.SizeBytes);
			DeviceMain.SetTexture(0, TxtureObject.TXTure);
		}

		public override void Draw()
		{
			SetupMaterial();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
		}

		public Vector2 LeftTop;

		public Vector2 RightTop;

		public Vector2 LeftBottom;

		public int Width;

		public int Height;
	}
}
