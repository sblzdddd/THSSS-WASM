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
			base.TransparentValue = 255;
			base.ColorValue = Color.White;
			base.DeviceMain = DeviceMain;
			base.TxtureObject = TxtureObject;
			base.Name = Name;
			this.vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(DeviceMain, 2 * (this.Length + 1) * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			this.material = default(Material);
			this.SetupVertex();
		}

		public override void SetupVertex()
		{
			float num = (float)base.TxtureObject.PosRect.X / (float)base.TxtureObject.SrcWidth;
			float num2 = (float)(base.TxtureObject.PosRect.X + base.TxtureObject.Width) / (float)base.TxtureObject.SrcWidth;
			TexturedVertex[] array = new TexturedVertex[2 * (this.Length + 1)];
			for (int i = 0; i <= this.Length; i++)
			{
				double num3 = (double)(i * 2) * 3.141592653589793 / (double)this.Length;
				int num4 = this.Radius - this.Width / 2;
				float num5 = (float)(i % (this.Length / 3)) / (float)(this.Length / 3);
				array[2 * i] = new TexturedVertex(new Vector3((float)num4 * (float)Math.Cos(num3), (float)num4 * (float)Math.Sin(num3), 0f), new Vector2(num, num5));
				num4 = this.Radius + this.Width / 2;
				array[2 * i + 1] = new TexturedVertex(new Vector3((float)num4 * (float)Math.Cos(num3), (float)num4 * (float)Math.Sin(num3), 0f), new Vector2(num2, num5));
			}
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(array);
			this.vertexBuffer.Unlock();
		}

		public override void Draw()
		{
			base.SetupMaterial();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2 * this.Length);
		}

		private int Length = 64;

		private int Width = 16;

		private int Radius = 100;
	}
}
