using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Model : IModel
	{
		public string Name { get; set; }

		public TextureObject TxtureObject { get; set; }

		public Device DeviceMain { get; set; }

		public int TransparentValue { get; set; }

		public Color ColorValue { get; set; }

		public Model()
		{
		}

		public Model(Device DeviceMain, TextureObject TxtureObject, string Name)
		{
			TransparentValue = 255;
			ColorValue = Color.White;
			this.DeviceMain = DeviceMain;
			this.TxtureObject = TxtureObject;
			this.Name = Name;
			vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			vertexBuffer = new VertexBuffer(DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			material = default(Material);
			SetupVertex();
		}

		public virtual void SetupVertex()
		{
			DataStream dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			float num = -TxtureObject.RotatingCenter.X;
			float num2 = (float)TxtureObject.Width - TxtureObject.RotatingCenter.X;
			float num3 = -TxtureObject.RotatingCenter.Y;
			float num4 = (float)TxtureObject.Height - TxtureObject.RotatingCenter.Y;
			float num5 = (float)TxtureObject.PosRect.X / (float)TxtureObject.SrcWidth;
			float num6 = (float)(TxtureObject.PosRect.X + TxtureObject.Width) / (float)TxtureObject.SrcWidth;
			float num7 = (float)TxtureObject.PosRect.Y / (float)TxtureObject.SrcHeight;
			float num8 = (float)(TxtureObject.PosRect.Y + TxtureObject.Height) / (float)TxtureObject.SrcHeight;
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(num, num3, 0f), new Vector2(num5, num7)),
				new TexturedVertex(new Vector3(num2, num3, 0f), new Vector2(num6, num7)),
				new TexturedVertex(new Vector3(num, num4, 0f), new Vector2(num5, num8)),
				new TexturedVertex(new Vector3(num2, num4, 0f), new Vector2(num6, num8))
			});
			vertexBuffer.Unlock();
		}

		public void SetupMaterial()
		{
			material.Diffuse = new Color4(Color.FromArgb(TransparentValue, ColorValue));
			material.Ambient = new Color4(Color.FromArgb(TransparentValue, ColorValue));
			DeviceMain.Material = material;
		}

		public virtual void SetModel()
		{
			DeviceMain.SetStreamSource(0, vertexBuffer, 0, TexturedVertex.SizeBytes);
			DeviceMain.SetTexture(0, TxtureObject.TXTure);
		}

		public void SetVertexFormat()
		{
			DeviceMain.VertexDeclaration = vertexDecl;
			DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		public virtual void Draw()
		{
			SetupMaterial();
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		public virtual void Dispose()
		{
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
				vertexDecl.Dispose();
			}
		}

		public VertexBuffer vertexBuffer;

		public VertexDeclaration vertexDecl;

		public Material material;
	}
}
