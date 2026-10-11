using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Model_Mesh : Model, IModel
	{
		public Model_Mesh(Device DeviceMain, string ModelName)
		{
			base.TransparentValue = 255;
			base.ColorValue = Color.White;
			base.DeviceMain = DeviceMain;
			base.Name = ModelName;
			this.SetupVertex();
		}

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

		public override void SetModel()
		{
		}

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

		public GlobalDataPackage GlobalData;

		private Mesh mesh;

		private ExtendedMaterial[] materials;

		private Texture[] textures;

		public bool CullMode = false;
	}
}
