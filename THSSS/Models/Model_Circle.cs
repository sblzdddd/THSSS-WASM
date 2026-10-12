using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Model_Circle : Model, IModel
	{
		public Model_Circle(Device DeviceMain, TextureObject TxtureObject, string Name)
		{
			TransparentValue = 255;
			ColorValue = Color.White;
			this.DeviceMain = DeviceMain;
			this.TxtureObject = TxtureObject;
			this.Name = Name;
			vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			vertexBuffer = new VertexBuffer(DeviceMain, 2 * (Length + 1) * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			material = default(Material);
			SetupVertex();
		}

		public override void SetupVertex()
		{
			float num = (float)TxtureObject.PosRect.X / (float)TxtureObject.SrcWidth;
			float num2 = (float)(TxtureObject.PosRect.X + TxtureObject.Width) / (float)TxtureObject.SrcWidth;
			TexturedVertex[] array = new TexturedVertex[2 * (Length + 1)];
			for (int i = 0; i <= Length; i++)
			{
				double num3 = (double)(i * 2) * 3.141592653589793 / (double)Length;
				int num4 = Radius - Width / 2;
				float num5 = (float)(i % (Length / 3)) / (float)(Length / 3);
				array[2 * i] = new TexturedVertex(new Vector3((float)num4 * (float)Math.Cos(num3), (float)num4 * (float)Math.Sin(num3), 0f), new Vector2(num, num5));
				num4 = Radius + Width / 2;
				array[2 * i + 1] = new TexturedVertex(new Vector3((float)num4 * (float)Math.Cos(num3), (float)num4 * (float)Math.Sin(num3), 0f), new Vector2(num2, num5));
			}
			DataStream dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(array);
			vertexBuffer.Unlock();
		}

		public override void Draw()
		{
			SetupMaterial();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2 * Length);
		}

		private int Length = 64;

		private int Width = 16;

		private int Radius = 100;
	}
}
