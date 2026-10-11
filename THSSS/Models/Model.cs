using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C9 RID: 201
	public class Model : IModel
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00031894 File Offset: 0x0002FA94
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x000318AB File Offset: 0x0002FAAB
		public string Name { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000318B4 File Offset: 0x0002FAB4
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x000318CB File Offset: 0x0002FACB
		public TextureObject TxtureObject { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000318D4 File Offset: 0x0002FAD4
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000318EB File Offset: 0x0002FAEB
		public Device DeviceMain { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000318F4 File Offset: 0x0002FAF4
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0003190B File Offset: 0x0002FB0B
		public int TransparentValue { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00031914 File Offset: 0x0002FB14
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0003192B File Offset: 0x0002FB2B
		public Color ColorValue { get; set; }

		// Token: 0x060005A4 RID: 1444 RVA: 0x00031934 File Offset: 0x0002FB34
		public Model()
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00031940 File Offset: 0x0002FB40
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

		// Token: 0x060005A6 RID: 1446 RVA: 0x000319C0 File Offset: 0x0002FBC0
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

		// Token: 0x060005A7 RID: 1447 RVA: 0x00031BB8 File Offset: 0x0002FDB8
		public void SetupMaterial()
		{
			this.material.Diffuse = new Color4(Color.FromArgb(this.TransparentValue, this.ColorValue));
			this.material.Ambient = new Color4(Color.FromArgb(this.TransparentValue, this.ColorValue));
			this.DeviceMain.Material = this.material;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00031C1C File Offset: 0x0002FE1C
		public virtual void SetModel()
		{
			this.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			this.DeviceMain.SetTexture(0, this.TxtureObject.TXTure);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00031C50 File Offset: 0x0002FE50
		public void SetVertexFormat()
		{
			this.DeviceMain.VertexDeclaration = this.vertexDecl;
			this.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00031C76 File Offset: 0x0002FE76
		public virtual void Draw()
		{
			this.SetupMaterial();
			this.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00031C90 File Offset: 0x0002FE90
		public virtual void Dispose()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexDecl.Dispose();
			}
		}

		// Token: 0x040001E6 RID: 486
		public VertexBuffer vertexBuffer;

		// Token: 0x040001E7 RID: 487
		public VertexDeclaration vertexDecl;

		// Token: 0x040001E8 RID: 488
		public Material material;
	}
}
