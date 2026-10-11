using System;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200010A RID: 266
	internal struct TexturedVertex
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00042B94 File Offset: 0x00040D94
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x00042BAB File Offset: 0x00040DAB
		public Vector3 Position { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00042BB4 File Offset: 0x00040DB4
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00042BCB File Offset: 0x00040DCB
		public Vector2 TextureCoordinate { get; set; }

		// Token: 0x0600075B RID: 1883 RVA: 0x00042BD4 File Offset: 0x00040DD4
		public TexturedVertex(Vector3 position, Vector2 textureCoordinate)
		{
			this = default(TexturedVertex);
			this.Position = position;
			this.TextureCoordinate = textureCoordinate;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00042BF0 File Offset: 0x00040DF0
		public static int SizeBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(TexturedVertex));
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00042C14 File Offset: 0x00040E14
		public static VertexFormat Format
		{
			get
			{
				return VertexFormat.Position | VertexFormat.Texture1;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00042C2C File Offset: 0x00040E2C
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
