using System;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal struct TexturedColoredVertex
	{
		public Vector3 Position { get; set; }

		public int Color { get; set; }

		public Vector2 TextureCoordinate { get; set; }

		public TexturedColoredVertex(Vector3 position, Vector2 textureCoordinate, int color)
		{
			this = default(TexturedColoredVertex);
			this.Position = position;
			this.TextureCoordinate = textureCoordinate;
			this.Color = color;
		}

		public static int SizeBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(TexturedColoredVertex));
			}
		}

		public static VertexFormat Format
		{
			get
			{
				return VertexFormat.Position | VertexFormat.Diffuse | VertexFormat.Texture1;
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
					new VertexElement(0, 20, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
					VertexElement.VertexDeclarationEnd
				};
			}
		}
	}
}
