using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Particle3D_BendLaser : BaseParticle3D
	{
		public Particle3D_BendLaser(StageDataPackage StageData, int Length, int Width, int ColorType)
		{
			StageData.Particle3D.ParticleList.Add(this);
			base.Active = true;
			base.StageData = StageData;
			this.ColorType = ColorType;
			base.TxtureObject = base.TextureObjectDictionary["Laser0_" + ColorType.ToString()];
			this.Length = Length;
			this.Width = Width;
			this.vertexBuffer = new VertexBuffer(base.DeviceMain, 2 * Length * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			this.material = default(Material);
		}

		public override void Ctrl()
		{
			base.Time++;
		}

		private void SetupVertex()
		{
			this.Length = base.GhostingCount;
			for (int i = base.GhostingCount - 1; i > 0; i--)
			{
				double num = Math.Sqrt((double)((base.LastPoints[i].X - base.LastPoints[i - 1].X) * (base.LastPoints[i].X - base.LastPoints[i - 1].X) + (base.LastPoints[i].Y - base.LastPoints[i - 1].Y) * (base.LastPoints[i].Y - base.LastPoints[i - 1].Y)));
				if (num >= 1.0)
				{
					break;
				}
				this.Length--;
			}
			if (this.Length < 2)
			{
				this.Length = 2;
			}
			TexturedVertex[] array = new TexturedVertex[2 * this.Length];
			double num2 = Math.Atan2((double)(base.LastPoints[0].Y - base.LastPoints[1].Y), (double)(base.LastPoints[0].X - base.LastPoints[1].X));
			array[0] = new TexturedVertex(new Vector3(base.LastPoints[0].X - (float)(this.Width / 2) * (float)Math.Sin(num2), base.LastPoints[0].Y + (float)(this.Width / 2) * (float)Math.Cos(num2), 0f), new Vector2(0f, (float)this.ColorType / 16f));
			array[1] = new TexturedVertex(new Vector3(base.LastPoints[0].X + (float)(this.Width / 2) * (float)Math.Sin(num2), base.LastPoints[0].Y - (float)(this.Width / 2) * (float)Math.Cos(num2), 0f), new Vector2(0f, (float)(this.ColorType + 1) / 16f));
			for (int i = 1; i < this.Length; i++)
			{
				array[2 * i] = new TexturedVertex(new Vector3(base.LastPoints[i].X - (float)(this.Width / 2) * (float)Math.Sin(num2), base.LastPoints[i].Y + (float)(this.Width / 2) * (float)Math.Cos(num2), 0f), new Vector2((float)i / ((float)this.Length - 1f), (float)this.ColorType / 16f));
				array[2 * i + 1] = new TexturedVertex(new Vector3(base.LastPoints[i].X + (float)(this.Width / 2) * (float)Math.Sin(num2), base.LastPoints[i].Y - (float)(this.Width / 2) * (float)Math.Cos(num2), 0f), new Vector2((float)i / ((float)this.Length - 1f), (float)(this.ColorType + 1) / 16f));
				if (base.LastPoints[i - 1] != base.LastPoints[i])
				{
					num2 = Math.Atan2((double)(base.LastPoints[i - 1].Y - base.LastPoints[i].Y), (double)(base.LastPoints[i - 1].X - base.LastPoints[i].X));
				}
			}
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(array);
			this.vertexBuffer.Unlock();
		}

		public override bool OutBoundary()
		{
			return false;
		}

		public override void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.Translation((float)(-(float)base.StageData.BoundRect.Width / 2), (float)(-(float)base.StageData.BoundRect.Height / 2), 0f);
			base.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			this.SetupVertex();
			base.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
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
		}

		public override void Dispose()
		{
			this.vertexBuffer.Dispose();
		}

		private VertexBuffer vertexBuffer;

		private Material material;

		private int Length;

		private int Width;

		private int ColorType;
	}
}
