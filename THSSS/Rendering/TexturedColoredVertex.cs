using System;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000063 RID: 99
	internal struct TexturedColoredVertex
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00016498 File Offset: 0x00014698
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000164AF File Offset: 0x000146AF
		public Vector3 Position { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000164B8 File Offset: 0x000146B8
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000164CF File Offset: 0x000146CF
		public int Color { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000164D8 File Offset: 0x000146D8
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000164EF File Offset: 0x000146EF
		public Vector2 TextureCoordinate { get; set; }

		// Token: 0x0600034C RID: 844 RVA: 0x000164F8 File Offset: 0x000146F8
		public TexturedColoredVertex(Vector3 position, Vector2 textureCoordinate, int color)
		{
			this = default(TexturedColoredVertex);
			this.Position = position;
			this.TextureCoordinate = textureCoordinate;
			this.Color = color;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001651C File Offset: 0x0001471C
		public static int SizeBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(TexturedColoredVertex));
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00016540 File Offset: 0x00014740
		public static VertexFormat Format
		{
			get
			{
				return VertexFormat.Position | VertexFormat.Diffuse | VertexFormat.Texture1;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00016558 File Offset: 0x00014758
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
