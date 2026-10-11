namespace SlimDX.Direct3D9
{
	public class Mesh : ComObject
	{
		public VertexFormat VertexFormat { get; set; }

		public static Mesh FromFile(Device device, string fileName, MeshFlags flags)
		{
			return new Mesh
			{
				VertexFormat = VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture1
			};
		}

		public ExtendedMaterial[] GetMaterials()
		{
			return new ExtendedMaterial[]
			{
				new ExtendedMaterial
				{
					MaterialD3D = default(Material),
					TextureFileName = ""
				}
			};
		}

		public Mesh Clone(Device device, MeshFlags flags, VertexFormat format)
		{
			return new Mesh
			{
				VertexFormat = format
			};
		}

		public void ComputeNormals()
		{
		}

		public void DrawSubset(int subset)
		{
		}
	}
}
