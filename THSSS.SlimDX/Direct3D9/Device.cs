using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX.Gl;

namespace SlimDX.Direct3D9
{
	public class Device : ComObject
	{
		public static Device Active { get; private set; }

		public int BackBufferWidth = 640;

		public int BackBufferHeight = 480;

		public Viewport Viewport;

		public VertexDeclaration VertexDeclaration { get; set; }

		public VertexFormat VertexFormat { get; set; }

		public Material Material { get; set; }

		public bool AlphaBlendEnable = true;

		public bool DepthEnable = true;

		public bool DepthWrite = true;

		public bool Lighting = true;

		public bool FogEnable;

		public float FogStart;

		public float FogEnd = 1000f;

		public int FogColor;

		public int BlendSrc = (int)Blend.SourceAlpha;

		public int BlendDst = (int)Blend.InverseSourceAlpha;

		public int BlendEquation = (int)BlendOperation.Add;

		public int CullMode = 1;

		private VertexBuffer stream;

		private int streamStride = 20;

		private Texture boundTexture;

		private Matrix world = Matrix.Identity;

		private Matrix view = Matrix.Identity;

		private Matrix projection = Matrix.Identity;

		private Surface backBuffer;

		private Surface currentColor;

		public Direct3D Direct3D { get; private set; }

		public Surface DepthStencilSurface { get; set; }

		public Device(Direct3D direct3D, int adapter, DeviceType deviceType, IntPtr window, CreateFlags createFlags, PresentParameters[] presentParameters)
		{
			Active = this;
			this.Direct3D = direct3D;
			if (presentParameters != null && presentParameters.Length > 0 && presentParameters[0] != null)
			{
				if (presentParameters[0].BackBufferWidth > 0)
				{
					this.BackBufferWidth = presentParameters[0].BackBufferWidth;
				}
				if (presentParameters[0].BackBufferHeight > 0)
				{
					this.BackBufferHeight = presentParameters[0].BackBufferHeight;
				}
			}
			this.Viewport = new Viewport
			{
				X = 0,
				Y = 0,
				Width = this.BackBufferWidth,
				Height = this.BackBufferHeight,
				MinZ = 0f,
				MaxZ = 1f
			};
			this.backBuffer = new Surface
			{
				Width = this.BackBufferWidth,
				Height = this.BackBufferHeight,
				GlTextureId = 0
			};
			this.currentColor = this.backBuffer;
			Material material = default(Material);
			material.Diffuse = new Color4(1f, 1f, 1f, 1f);
			material.Ambient = new Color4(1f, 1f, 1f, 1f);
			this.Material = material;
			GlHost.Need().BindTarget(0);
			this.ApplyViewport();
		}

		public void Clear(ClearFlags flags, Color color, float z, int stencil)
		{
			this.Clear(flags, new Color4(color), z, stencil);
		}

		public void Clear(ClearFlags flags, Color4 color, float z, int stencil)
		{
			this.ApplyTarget();
			this.ApplyViewport();
			GlHost.Need().Clear(color.Red, color.Green, color.Blue, color.Alpha, (flags & ClearFlags.Target) != 0, (flags & ClearFlags.ZBuffer) != 0);
		}

		public void BeginScene()
		{
		}

		public void EndScene()
		{
		}

		public void Present(Present flags)
		{
			this.ApplyTarget();
		}

		public Result TestCooperativeLevel()
		{
			Result result = Result.Success;
			Result.Last = result;
			return result;
		}

		public void Reset(PresentParameters[] presentParameters)
		{
		}

		public void SetRenderState(RenderState state, bool value)
		{
			this.ApplyInt(state, value ? 1 : 0);
		}

		public void SetRenderState(RenderState state, int value)
		{
			this.ApplyInt(state, value);
		}

		public void SetRenderState(RenderState state, float value)
		{
			if (state == RenderState.FogStart)
			{
				this.FogStart = value;
			}
			else if (state == RenderState.FogEnd)
			{
				this.FogEnd = value;
			}
			else
			{
				this.ApplyInt(state, (int)value);
			}
		}

		public void SetRenderState<T>(RenderState state, T value) where T : struct
		{
			this.ApplyInt(state, Convert.ToInt32(value));
		}

		public void SetTextureStageState(int stage, TextureStage type, int value)
		{
		}

		public void SetTextureStageState(int stage, TextureStage type, TextureArgument value)
		{
		}

		public void SetTextureStageState(int stage, TextureStage type, TextureOperation value)
		{
		}

		public void SetTransform(TransformState state, Matrix matrix)
		{
			if (state == TransformState.World)
			{
				this.world = matrix;
			}
			else if (state == TransformState.View)
			{
				this.view = matrix;
			}
			else if (state == TransformState.Projection)
			{
				this.projection = matrix;
			}
		}

		public void SetTexture(int stage, Texture texture)
		{
			this.boundTexture = texture;
		}

		public void SetStreamSource(int streamNumber, VertexBuffer buffer, int offset, int stride)
		{
			this.stream = buffer;
			this.streamStride = stride > 0 ? stride : 20;
		}

		public void SetLight(int index, Light light)
		{
		}

		public void EnableLight(int index, bool enable)
		{
		}

		public void SetRenderTarget(int index, Surface surface)
		{
			this.currentColor = surface ?? this.backBuffer;
			this.ApplyTarget();
		}

		public Surface GetRenderTarget(int index)
		{
			return this.currentColor ?? this.backBuffer;
		}

		public Surface GetBackBuffer(int swapChain, int backBuffer)
		{
			return this.backBuffer;
		}

		public void DrawPrimitives(PrimitiveType primitiveType, int startVertex, int primitiveCount)
		{
			if (this.stream == null || this.stream.Data == null || primitiveCount <= 0)
			{
				return;
			}
			int num = primitiveType == PrimitiveType.TriangleList ? primitiveCount * 3 : primitiveCount + 2;
			List<float> list = new List<float>(num * 9);
			for (int i = 0; i < num; i++)
			{
				this.AppendVertex(list, startVertex + i);
			}
			List<float> list2 = new List<float>();
			if (primitiveType == PrimitiveType.TriangleList || primitiveType == PrimitiveType.TriangleFan)
			{
				list2 = list;
			}
			else
			{
				for (int j = 0; j < primitiveCount; j++)
				{
					this.CopyVert(list, list2, j);
					this.CopyVert(list, list2, j + 1);
					this.CopyVert(list, list2, j + 2);
				}
			}
			if (primitiveType == PrimitiveType.TriangleFan && primitiveCount > 0)
			{
				list2 = new List<float>();
				for (int k = 0; k < primitiveCount; k++)
				{
					this.CopyVert(list, list2, 0);
					this.CopyVert(list, list2, k + 1);
					this.CopyVert(list, list2, k + 2);
				}
			}
			this.ApplyTarget();
			this.ApplyViewport();
			GlHost.Need().SetBlend(this.AlphaBlendEnable, this.BlendSrc, this.BlendDst, this.BlendEquation);
			GlHost.Need().SetDepth(this.DepthEnable, this.DepthWrite);
			GlHost.Need().SetCull(this.CullMode);
			Color4 color = this.Lighting ? this.Material.Diffuse : new Color4(1f, 1f, 1f, 1f);
			float num2 = (float)((this.FogColor >> 16) & 255) / 255f;
			float num3 = (float)((this.FogColor >> 8) & 255) / 255f;
			float num4 = (float)(this.FogColor & 255) / 255f;
			int num5 = this.boundTexture != null && !this.boundTexture.Disposed ? this.boundTexture.GlId : 0;
			GlHost.Need().DrawFixed(num5, list2.ToArray(), this.world.ToArray(), this.view.ToArray(), this.projection.ToArray(), color.Red, color.Green, color.Blue, color.Alpha, this.Lighting ? 1f : 0f, this.FogEnable ? 1f : 0f, this.FogStart, this.FogEnd, num2, num3, num4);
		}

		private void AppendVertex(List<float> dest, int index)
		{
			byte[] data = this.stream.Data;
			int num = index * this.streamStride;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 1f;
			float num9 = 1f;
			float num10 = 1f;
			VertexElement[] array = this.VertexDeclaration != null ? this.VertexDeclaration.Elements : null;
			if (array == null || array.Length == 0)
			{
				num2 = ReadSingle(data, num);
				num3 = ReadSingle(data, num + 4);
				num4 = ReadSingle(data, num + 8);
				num5 = ReadSingle(data, num + 12);
				num6 = ReadSingle(data, num + 16);
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					VertexElement vertexElement = array[i];
					if (vertexElement.Stream == 255)
					{
						break;
					}
					int num11 = num + vertexElement.Offset;
					if (vertexElement.Usage == DeclarationUsage.Position)
					{
						num2 = ReadSingle(data, num11);
						num3 = ReadSingle(data, num11 + 4);
						num4 = ReadSingle(data, num11 + 8);
					}
					else if (vertexElement.Usage == DeclarationUsage.TextureCoordinate)
					{
						num5 = ReadSingle(data, num11);
						num6 = ReadSingle(data, num11 + 4);
					}
					else if (vertexElement.Usage == DeclarationUsage.Color)
					{
						int num12 = ReadInt(data, num11);
						num10 = (float)((num12 >> 24) & 255) / 255f;
						num7 = (float)((num12 >> 16) & 255) / 255f;
						num8 = (float)((num12 >> 8) & 255) / 255f;
						num9 = (float)(num12 & 255) / 255f;
					}
				}
			}
			dest.Add(num2);
			dest.Add(num3);
			dest.Add(num4);
			dest.Add(num5);
			dest.Add(num6);
			dest.Add(num7);
			dest.Add(num8);
			dest.Add(num9);
			dest.Add(num10);
		}

		private void CopyVert(List<float> src, List<float> dst, int index)
		{
			int num = index * 9;
			for (int i = 0; i < 9; i++)
			{
				dst.Add(num + i < src.Count ? src[num + i] : 0f);
			}
		}

		private static float ReadSingle(byte[] data, int offset)
		{
			if (data == null || offset < 0 || offset + 4 > data.Length)
			{
				return 0f;
			}
			return BitConverter.ToSingle(data, offset);
		}

		private static int ReadInt(byte[] data, int offset)
		{
			if (data == null || offset < 0 || offset + 4 > data.Length)
			{
				return -1;
			}
			return BitConverter.ToInt32(data, offset);
		}

		private void ApplyInt(RenderState state, int value)
		{
			if (state == RenderState.AlphaBlendEnable)
			{
				this.AlphaBlendEnable = value != 0;
			}
			else if (state == RenderState.SourceBlend)
			{
				this.BlendSrc = value;
			}
			else if (state == RenderState.DestinationBlend)
			{
				this.BlendDst = value;
			}
			else if (state == RenderState.BlendOperation)
			{
				this.BlendEquation = value;
			}
			else if (state == RenderState.ZEnable)
			{
				this.DepthEnable = value != 0;
			}
			else if (state == RenderState.ZWriteEnable)
			{
				this.DepthWrite = value != 0;
			}
			else if (state == RenderState.Lighting)
			{
				this.Lighting = value != 0;
			}
			else if (state == RenderState.FogEnable)
			{
				this.FogEnable = value != 0;
			}
			else if (state == RenderState.FogColor)
			{
				this.FogColor = value;
			}
			else if (state == RenderState.CullMode)
			{
				this.CullMode = value;
			}
		}

		private void ApplyTarget()
		{
			int num = this.currentColor != null ? this.currentColor.GlTextureId : 0;
			GlHost.Need().BindTarget(num);
		}

		private void ApplyViewport()
		{
			Viewport viewport = this.Viewport;
			int num = viewport.Width > 0 ? viewport.Width : this.BackBufferWidth;
			int num2 = viewport.Height > 0 ? viewport.Height : this.BackBufferHeight;
			GlHost.Need().SetViewport(viewport.X, viewport.Y, num, num2, this.BackBufferWidth, this.BackBufferHeight);
		}
	}
}
