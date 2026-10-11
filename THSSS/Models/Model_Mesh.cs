using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200020C RID: 524
	public class Model_Mesh : Model, IModel
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0007375C File Offset: 0x0007195C
		public Model_Mesh(Device DeviceMain, string ModelName)
		{
			base.TransparentValue = 255;
			base.ColorValue = Color.White;
			base.DeviceMain = DeviceMain;
			base.Name = ModelName;
			this.SetupVertex();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000737A8 File Offset: 0x000719A8
		public override void SetupVertex()
		{
			this.mesh = Mesh.FromFile(base.DeviceMain, ".\\Model\\" + base.Name + ".x", MeshFlags.Managed);
			this.materials = this.mesh.GetMaterials();
			if ((this.mesh.VertexFormat & VertexFormat.Normal) == VertexFormat.None)
			{
				Mesh mesh = this.mesh.Clone(base.DeviceMain, MeshFlags.SystemMemory, this.mesh.VertexFormat | VertexFormat.Normal);
				mesh.ComputeNormals();
				this.mesh.Dispose();
				this.mesh = mesh;
			}
			if (this.materials.Length >= 1)
			{
				this.textures = new Texture[this.materials.Length];
				for (int i = 0; i < this.materials.Length; i++)
				{
					this.textures[i] = null;
					if (!string.IsNullOrEmpty(this.materials[i].TextureFileName))
					{
						this.textures[i] = Texture.FromFile(base.DeviceMain, ".\\Model\\" + this.materials[i].TextureFileName);
					}
				}
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000738DE File Offset: 0x00071ADE
		public override void SetModel()
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000738E4 File Offset: 0x00071AE4
		public override void Draw()
		{
			for (int i = 0; i < this.materials.Length; i++)
			{
				Material materialD3D = this.materials[i].MaterialD3D;
				materialD3D.Diffuse = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
				materialD3D.Ambient = new Color4(Color.FromArgb(base.TransparentValue, base.ColorValue));
				base.DeviceMain.Material = materialD3D;
				base.DeviceMain.SetTexture(0, this.textures[i]);
				this.mesh.DrawSubset(i);
			}
			base.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000739A0 File Offset: 0x00071BA0
		public override void Dispose()
		{
			foreach (Texture texture in this.textures)
			{
				if (texture != null)
				{
					texture.Dispose();
				}
			}
			this.mesh.Dispose();
		}

		// Token: 0x0400043A RID: 1082
		public GlobalDataPackage GlobalData;

		// Token: 0x0400043B RID: 1083
		private Mesh mesh;

		// Token: 0x0400043C RID: 1084
		private ExtendedMaterial[] materials;

		// Token: 0x0400043D RID: 1085
		private Texture[] textures;

		// Token: 0x0400043E RID: 1086
		public bool CullMode = false;
	}
}
