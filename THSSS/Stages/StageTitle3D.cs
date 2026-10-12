using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class StageTitle3D : BaseParticle3D
	{
		public StageTitle3D(StageDataPackage StageData, string TextureName)
		{
			this.StageData = StageData;
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			if (TextureName != null)
			{
				TxtureObject = TextureObjectDictionary[TextureName];
			}
			StageData.Particle3D.ParticleList.Add(this);
			vertexDecl = new VertexDeclaration(DeviceMain, TexturedColoredVertex.VertexElements);
			vertexBuffer = new VertexBuffer(DeviceMain, 2 * Length * TexturedColoredVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			material = default(Material);
			SetupVertex();
			LifeTime = 300;
			TransparentValueF = 0f;
			ColorValue = Color.White;
			Active = false;
		}

		public virtual void SetupVertex()
		{
			var dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			var num = -TxtureObject.RotatingCenter.X;
			var num2 = (float)TxtureObject.Width - TxtureObject.RotatingCenter.X;
			var num3 = -TxtureObject.RotatingCenter.Y;
			var num4 = (float)TxtureObject.Height - TxtureObject.RotatingCenter.Y;
			var num5 = (float)TxtureObject.PosRect.X / (float)TxtureObject.SrcWidth;
			var num6 = (float)(TxtureObject.PosRect.X + TxtureObject.Width) / (float)TxtureObject.SrcWidth;
			var num7 = (float)TxtureObject.PosRect.Y / (float)TxtureObject.SrcHeight;
			var num8 = (float)(TxtureObject.PosRect.Y + TxtureObject.Height) / (float)TxtureObject.SrcHeight;
			var array = new TexturedColoredVertex[2 * Length];
			for (var i = 0; i < Length; i++)
			{
				var num9 = -10 * i + Time * 3;
				if (num9 < 0)
				{
					num9 = 0;
				}
				else if (num9 > TransparentValue)
				{
					num9 = TransparentValue;
				}
				array[2 * i] = new TexturedColoredVertex(new Vector3(num, num3 + (num4 - num3) * (float)i / (float)Length, 0f), new Vector2(num5, num7 + (num8 - num7) * (float)i / (float)Length), Color.FromArgb(num9, ColorValue).ToArgb());
				array[2 * i + 1] = new TexturedColoredVertex(new Vector3(num2, num3 + (num4 - num3) * (float)i / (float)Length, 0f), new Vector2(num6, num7 + (num8 - num7) * (float)i / (float)Length), Color.FromArgb(num9, ColorValue).ToArgb());
			}
			dataStream.WriteRange<TexturedColoredVertex>(array);
			vertexBuffer.Unlock();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time < LifeTime / 3)
			{
				TransparentValueF += (float)MaxTransparent * 3f / (float)LifeTime;
			}
			else if (Time > LifeTime * 2 / 3)
			{
				TransparentValueF -= (float)MaxTransparent * 3f / (float)LifeTime;
			}
		}

		public override void Show()
		{
			DeviceMain.EnableLight(0, false);
			DeviceMain.SetRenderState(RenderState.Lighting, false);
			DeviceMain.VertexDeclaration = vertexDecl;
			DeviceMain.VertexFormat = TexturedColoredVertex.Format;
			SetupVertex();
			DeviceMain.SetStreamSource(0, vertexBuffer, 0, TexturedColoredVertex.SizeBytes);
			DeviceMain.SetTexture(0, TxtureObject.TXTure);
			SetupMatrices();
			if (Active)
			{
				DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.One);
			}
			else
			{
				DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			}
			material.Diffuse = new Color4(Color.FromArgb(TransparentValue, ColorValue));
			material.Ambient = new Color4(Color.FromArgb(TransparentValue, ColorValue));
			DeviceMain.Material = material;
			DeviceMain.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2 * Length - 2);
			DeviceMain.EnableLight(0, true);
			DeviceMain.SetRenderState(RenderState.Lighting, true);
			DeviceMain.VertexDeclaration = StageData.Particle3D.vertexDecl;
			DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		protected VertexBuffer vertexBuffer;

		protected VertexDeclaration vertexDecl;

		protected Material material;

		protected int Length = 40;
	}
}
