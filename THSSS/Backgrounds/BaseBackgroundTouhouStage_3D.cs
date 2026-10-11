using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C5 RID: 197
	internal class BaseBackgroundTouhouStage_3D : BaseObject
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00031098 File Offset: 0x0002F298
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000310B8 File Offset: 0x0002F2B8
		public BaseBackgroundTouhouStage_3D(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF(0f, 0f);
			base.Background3D.BackgroundList.Add(this);
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0003112C File Offset: 0x0002F32C
		public void SetupMaterial()
		{
			this.material = default(Material);
			this.material.Diffuse = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
			this.material.Ambient = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0003118C File Offset: 0x0002F38C
		public override bool OutBoundary()
		{
			return false;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0003119F File Offset: 0x0002F39F
		public virtual void SetupVertex()
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000311A2 File Offset: 0x0002F3A2
		public virtual void SetupMatrices()
		{
			this.DeviceMain.SetTransform(TransformState.World, Matrix.Identity);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000311BC File Offset: 0x0002F3BC
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

		// Token: 0x0600057B RID: 1403 RVA: 0x00031248 File Offset: 0x0002F448
		public override void Dispose()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
				this.vertexDecl.Dispose();
			}
		}

		// Token: 0x040001DF RID: 479
		public VertexBuffer vertexBuffer;

		// Token: 0x040001E0 RID: 480
		public VertexDeclaration vertexDecl;

		// Token: 0x040001E1 RID: 481
		public Material material;
	}
}
