using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class StageTitle3D : BaseParticle3D
	{
		public StageTitle3D(StageDataPackage StageData, string TextureName)
		{
			base.StageData = StageData;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			if (TextureName != null)
			{
				base.TxtureObject = base.TextureObjectDictionary[TextureName];
			}
			StageData.Particle3D.ParticleList.Add(this);
			this.vertexDecl = new VertexDeclaration(base.DeviceMain, TexturedColoredVertex.VertexElements);
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 2 * this.Length * TexturedColoredVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			this.material = default(Material);
			this.SetupVertex();
			base.LifeTime = 300;
			base.TransparentValueF = 0f;
			base.ColorValue = Color.White;
			base.Active = false;
		}

		public virtual void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			float num = -base.TxtureObject.RotatingCenter.X;
			float num2 = (float)base.TxtureObject.Width - base.TxtureObject.RotatingCenter.X;
			float num3 = -base.TxtureObject.RotatingCenter.Y;
			float num4 = (float)base.TxtureObject.Height - base.TxtureObject.RotatingCenter.Y;
			float num5 = (float)base.TxtureObject.PosRect.X / (float)base.TxtureObject.SrcWidth;
			float num6 = (float)(base.TxtureObject.PosRect.X + base.TxtureObject.Width) / (float)base.TxtureObject.SrcWidth;
			float num7 = (float)base.TxtureObject.PosRect.Y / (float)base.TxtureObject.SrcHeight;
			float num8 = (float)(base.TxtureObject.PosRect.Y + base.TxtureObject.Height) / (float)base.TxtureObject.SrcHeight;
			TexturedColoredVertex[] array = new TexturedColoredVertex[2 * this.Length];
			for (int i = 0; i < this.Length; i++)
			{
				int num9 = -10 * i + base.Time * 3;
				if (num9 < 0)
				{
					num9 = 0;
				}
				else if (num9 > base.TransparentValue)
				{
					num9 = base.TransparentValue;
				}
				array[2 * i] = new TexturedColoredVertex(new Vector3(num, num3 + (num4 - num3) * (float)i / (float)this.Length, 0f), new Vector2(num5, num7 + (num8 - num7) * (float)i / (float)this.Length), Color.FromArgb(num9, base.ColorValue).ToArgb());
				array[2 * i + 1] = new TexturedColoredVertex(new Vector3(num2, num3 + (num4 - num3) * (float)i / (float)this.Length, 0f), new Vector2(num6, num7 + (num8 - num7) * (float)i / (float)this.Length), Color.FromArgb(num9, base.ColorValue).ToArgb());
			}
			dataStream.WriteRange<TexturedColoredVertex>(array);
			this.vertexBuffer.Unlock();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)base.MaxTransparent * 3f / (float)base.LifeTime;
			}
			else if (base.Time > base.LifeTime * 2 / 3)
			{
				base.TransparentValueF -= (float)base.MaxTransparent * 3f / (float)base.LifeTime;
			}
		}

		public override void Show()
		{
			base.DeviceMain.EnableLight(0, false);
			base.DeviceMain.SetRenderState(RenderState.Lighting, false);
			base.DeviceMain.VertexDeclaration = this.vertexDecl;
			base.DeviceMain.VertexFormat = TexturedColoredVertex.Format;
			this.SetupVertex();
			base.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedColoredVertex.SizeBytes);
			base.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
			this.SetupMatrices();
			if (base.Active)
			{
				base.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.One);
			}
			else
			{
				base.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			}
			this.material.Diffuse = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
			this.material.Ambient = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
			base.DeviceMain.Material = this.material;
			base.DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2 * this.Length - 2);
			base.DeviceMain.EnableLight(0, true);
			base.DeviceMain.SetRenderState(RenderState.Lighting, true);
			base.DeviceMain.VertexDeclaration = base.StageData.Particle3D.vertexDecl;
			base.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		protected VertexBuffer vertexBuffer;

		protected VertexDeclaration vertexDecl;

		protected Material material;

		protected int Length = 40;
	}
}
