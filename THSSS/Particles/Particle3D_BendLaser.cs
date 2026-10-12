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
			Active = true;
			this.StageData = StageData;
			this.ColorType = ColorType;
			TxtureObject = TextureObjectDictionary["Laser0_" + ColorType.ToString()];
			this.Length = Length;
			this.Width = Width;
			vertexBuffer = new VertexBuffer(DeviceMain, 2 * Length * TexturedVertex.SizeBytes, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
			material = default(Material);
		}

		public override void Ctrl()
		{
			Time++;
		}

		private void SetupVertex()
		{
			Length = GhostingCount;
			for (var i = GhostingCount - 1; i > 0; i--)
			{
				var num = Math.Sqrt((double)((LastPoints[i].X - LastPoints[i - 1].X) * (LastPoints[i].X - LastPoints[i - 1].X) + (LastPoints[i].Y - LastPoints[i - 1].Y) * (LastPoints[i].Y - LastPoints[i - 1].Y)));
				if (num >= 1.0)
				{
					break;
				}
				Length--;
			}
			if (Length < 2)
			{
				Length = 2;
			}
			var array = new TexturedVertex[2 * Length];
			var num2 = Math.Atan2((double)(LastPoints[0].Y - LastPoints[1].Y), (double)(LastPoints[0].X - LastPoints[1].X));
			array[0] = new TexturedVertex(new Vector3(LastPoints[0].X - (float)(Width / 2) * (float)Math.Sin(num2), LastPoints[0].Y + (float)(Width / 2) * (float)Math.Cos(num2), 0f), new Vector2(0f, (float)ColorType / 16f));
			array[1] = new TexturedVertex(new Vector3(LastPoints[0].X + (float)(Width / 2) * (float)Math.Sin(num2), LastPoints[0].Y - (float)(Width / 2) * (float)Math.Cos(num2), 0f), new Vector2(0f, (float)(ColorType + 1) / 16f));
			for (var i = 1; i < Length; i++)
			{
				array[2 * i] = new TexturedVertex(new Vector3(LastPoints[i].X - (float)(Width / 2) * (float)Math.Sin(num2), LastPoints[i].Y + (float)(Width / 2) * (float)Math.Cos(num2), 0f), new Vector2((float)i / ((float)Length - 1f), (float)ColorType / 16f));
				array[2 * i + 1] = new TexturedVertex(new Vector3(LastPoints[i].X + (float)(Width / 2) * (float)Math.Sin(num2), LastPoints[i].Y - (float)(Width / 2) * (float)Math.Cos(num2), 0f), new Vector2((float)i / ((float)Length - 1f), (float)(ColorType + 1) / 16f));
				if (LastPoints[i - 1] != LastPoints[i])
				{
					num2 = Math.Atan2((double)(LastPoints[i - 1].Y - LastPoints[i].Y), (double)(LastPoints[i - 1].X - LastPoints[i].X));
				}
			}
			var dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			dataStream.WriteRange<TexturedVertex>(array);
			vertexBuffer.Unlock();
		}

		public override bool OutBoundary()
		{
			return false;
		}

		public override void SetupMatrices()
		{
			var matrix = Matrix.Identity;
			matrix *= Matrix.Translation((float)(-(float)StageData.BoundRect.Width / 2), (float)(-(float)StageData.BoundRect.Height / 2), 0f);
			DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			SetupVertex();
			DeviceMain.SetStreamSource(0, vertexBuffer, 0, TexturedVertex.SizeBytes);
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
		}

		public override void Dispose()
		{
			vertexBuffer.Dispose();
		}

		private VertexBuffer vertexBuffer;

		private Material material;

		private int Length;

		private int Width;

		private int ColorType;
	}
}
