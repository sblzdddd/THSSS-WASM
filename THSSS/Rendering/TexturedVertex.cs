using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal struct TexturedVertex
	{
		public Vector3 Position { get; set; }

		public Vector2 TextureCoordinate { get; set; }

		public TexturedVertex(Vector3 position, Vector2 textureCoordinate)
		{
			this = default(TexturedVertex);
			Position = position;
			TextureCoordinate = textureCoordinate;
		}

		public static int SizeBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(TexturedVertex));
			}
		}

		public static VertexFormat Format
		{
			get
			{
				return VertexFormat.Position | VertexFormat.Texture1;
			}
		}

		public static VertexElement[] VertexElements
		{
			get
			{
				return new VertexElement[]
				{
					new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0),
					new VertexElement(0, 12, DeclarationType.Float2, DeclarationMethod.Default, DeclarationUsage.TextureCoordinate, 0),
					VertexElement.VertexDeclarationEnd
				};
			}
		}
	}
}
