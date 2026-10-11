using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000CA RID: 202
	public class Model_Triangle : Model, IModel
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x00031CC5 File Offset: 0x0002FEC5
		public Model_Triangle()
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00031CD0 File Offset: 0x0002FED0
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

		// Token: 0x060005AE RID: 1454 RVA: 0x00031D78 File Offset: 0x0002FF78
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

		// Token: 0x060005AF RID: 1455 RVA: 0x00031E39 File Offset: 0x00030039
		public override void SetModel()
		{
			base.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			base.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00031E6D File Offset: 0x0003006D
		public override void Draw()
		{
			base.SetupMaterial();
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
		}

		// Token: 0x040001EE RID: 494
		public Vector2 LeftTop;

		// Token: 0x040001EF RID: 495
		public Vector2 RightTop;

		// Token: 0x040001F0 RID: 496
		public Vector2 LeftBottom;

		// Token: 0x040001F1 RID: 497
		public int Width;

		// Token: 0x040001F2 RID: 498
		public int Height;
	}
}
