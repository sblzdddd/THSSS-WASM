using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class Model_Mesh : Model, IModel
	{
		public Model_Mesh(Device DeviceMain, string ModelName)
		{
			TransparentValue = 255;
			ColorValue = Color.White;
			this.DeviceMain = DeviceMain;
			Name = ModelName;
			SetupVertex();
		}

		public override void SetupVertex()
		{
			this.mesh = Mesh.FromFile(DeviceMain, ".\\Model\\" + Name + ".x", MeshFlags.Managed);
			materials = this.mesh.GetMaterials();
			if ((this.mesh.VertexFormat & VertexFormat.Normal) == VertexFormat.None)
			{
				Mesh mesh = this.mesh.Clone(DeviceMain, MeshFlags.SystemMemory, this.mesh.VertexFormat | VertexFormat.Normal);
				mesh.ComputeNormals();
				this.mesh.Dispose();
				this.mesh = mesh;
			}
			if (materials.Length >= 1)
			{
				textures = new Texture[materials.Length];
				for (int i = 0; i < materials.Length; i++)
				{
					textures[i] = null;
					if (!string.IsNullOrEmpty(materials[i].TextureFileName))
					{
						textures[i] = Texture.FromFile(DeviceMain, ".\\Model\\" + materials[i].TextureFileName);
					}
				}
			}
		}

		public override void SetModel()
		{
		}

		public override void Draw()
		{
			for (int i = 0; i < materials.Length; i++)
			{
				Material materialD3D = materials[i].MaterialD3D;
				materialD3D.Diffuse = new Color4(Color.FromArgb(TransparentValue, ColorValue));
				materialD3D.Ambient = new Color4(Color.FromArgb(TransparentValue, ColorValue));
				DeviceMain.Material = materialD3D;
				DeviceMain.SetTexture(0, textures[i]);
				mesh.DrawSubset(i);
			}
			DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		public override void Dispose()
		{
			foreach (Texture texture in textures)
			{
				if (texture != null)
				{
					texture.Dispose();
				}
			}
			mesh.Dispose();
		}

		public GlobalDataPackage GlobalData;

		private Mesh mesh;

		private ExtendedMaterial[] materials;

		private Texture[] textures;

		public bool CullMode = false;
	}
}
