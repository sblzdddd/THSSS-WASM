using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BaseBackgroundTouhouStage_3D : BaseObject
	{
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		public BaseBackgroundTouhouStage_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF(0f, 0f);
			base.Background3D.BackgroundList.Add(this);
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
		}

		public void SetupMaterial()
		{
			this.material = default(Material);
			this.material.Diffuse = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
			this.material.Ambient = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
		}

		public override bool OutBoundary()
		{
			return false;
		}

		public virtual void SetupVertex()
		{
		}

		public virtual void SetupMatrices()
		{
			this.DeviceMain.SetTransform(TransformState.World, Matrix.Identity);
		}

		public override void Show()
		{
			this.SetupVertex();
			this.SetupMaterial();
			this.SetupMatrices();
			this.DeviceMain.Material = this.material;
			this.DeviceMain.SetTexture(0, base.TxtureObject.TXTure);
			this.DeviceMain.SetStreamSource(0, this.vertexBuffer, 0, TexturedVertex.SizeBytes);
			this.DeviceMain.VertexDeclaration = this.vertexDecl;
			this.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		public override void Dispose()
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
