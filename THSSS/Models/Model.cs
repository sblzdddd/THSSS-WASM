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
			this.TransparentValue = 255;
			this.ColorValue = Color.White;
			this.DeviceMain = DeviceMain;
			this.TxtureObject = TxtureObject;
			this.Name = Name;
			this.vertexDecl = new VertexDeclaration(DeviceMain, TexturedVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(DeviceMain, 4 * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			this.material = default(Material);
			this.SetupVertex();
		}

		public virtual void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			float num = -this.TxtureObject.RotatingCenter.X;
			float num2 = (float)this.TxtureObject.Width - this.TxtureObject.RotatingCenter.X;
			float num3 = -this.TxtureObject.RotatingCenter.Y;
			float num4 = (float)this.TxtureObject.Height - this.TxtureObject.RotatingCenter.Y;
			float num5 = (float)this.TxtureObject.PosRect.X / (float)this.TxtureObject.SrcWidth;
			float num6 = (float)(this.TxtureObject.PosRect.X + this.TxtureObject.Width) / (float)this.TxtureObject.SrcWidth;
			float num7 = (float)this.TxtureObject.PosRect.Y / (float)this.TxtureObject.SrcHeight;
			float num8 = (float)(this.TxtureObject.PosRect.Y + this.TxtureObject.Height) / (float)this.TxtureObject.SrcHeight;
			dataStream.WriteRange<TexturedVertex>(new TexturedVertex[]
			{
				new TexturedVertex(new Vector3(num, num3, 0f), new Vector2(num5, num7)),
				new TexturedVertex(new Vector3(num2, num3, 0f), new Vector2(num6, num7)),
				new TexturedVertex(new Vector3(num, num4, 0f), new Vector2(num5, num8)),
				new TexturedVertex(new Vector3(num2, num4, 0f), new Vector2(num6, num8))
			});
			this.vertexBuffer.Unlock();
		}

		public void SetupMaterial()
		{
			this.material.Diffuse = new Color4(Color.FromArgb(this.TransparentValue, this.ColorValue));
			this.material.Ambient = new Color4(Color.FromArgb(this.TransparentValue, this.ColorValue));
			this.DeviceMain.Material = this.material;
		}

		public virtual void SetModel()
		{
			this.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			this.DeviceMain.SetTexture(0, this.TxtureObject.TXTure);
		}

		public void SetVertexFormat()
		{
			this.DeviceMain.VertexDeclaration = this.vertexDecl;
			this.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		public virtual void Draw()
		{
			this.SetupMaterial();
			this.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		public virtual void Dispose()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexDecl.Dispose();
			}
		}

		public VertexBuffer vertexBuffer;

		public VertexDeclaration vertexDecl;

		public Material material;
	}
}
