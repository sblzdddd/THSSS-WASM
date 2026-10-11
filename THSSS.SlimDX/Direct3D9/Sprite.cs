using System.Collections.Generic;
using System.Drawing;
using SlimDX.Gl;

namespace SlimDX.Direct3D9
{
	public class Sprite : ComObject
	{
		private List<float> batch = new List<float>(8192);

		private List<float> held = new List<float>(8192);

		private Texture batchTexture;

		private Texture heldTexture;

		private int batchBlendSrc;

		private int batchBlendDst;

		private int batchEquation;

		private int heldBlendSrc;

		private int heldBlendDst;

		private int heldEquation;

		private bool batchFlip;

		private bool heldFlip;

		private int batchSerial;

		private int heldSerial;

		private int nextSerial;

		public Matrix Transform { get; set; }

		public Sprite(Device device)
		{
			this.Transform = Matrix.Identity;
		}

		public void Begin(SpriteFlags flags)
		{
			this.Flush();
			this.nextSerial = 0;
			if ((flags & SpriteFlags.AlphaBlend) != 0)
			{
				Device.Active.SetRenderState(RenderState.AlphaBlendEnable, true);
				Device.Active.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
				Device.Active.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			}
		}

		public void End()
		{
			this.Flush();
		}

		public void Draw(Texture texture, Vector3? center, Vector3? position, Color4 color)
		{
			Rectangle rectangle = new Rectangle(0, 0, texture != null ? texture.Width : 1, texture != null ? texture.Height : 1);
			this.Draw(texture, rectangle, center, position, color);
		}

		public void Draw(Texture texture, Rectangle? sourceRect, Vector3? center, Vector3? position, Color4 color)
		{
			if (texture == null || texture.Disposed || texture.GlId == 0)
			{
				return;
			}
			Rectangle rectangle = sourceRect ?? new Rectangle(0, 0, texture.Width, texture.Height);
			Vector3 vector = center ?? new Vector3(0f, 0f, 0f);
			Vector3 vector2 = position ?? new Vector3(0f, 0f, 0f);
			this.EnsureBatch(texture);
			float num = vector2.X - vector.X;
			float num2 = vector2.Y - vector.Y;
			float num3 = num + (float)rectangle.Width;
			float num4 = num2 + (float)rectangle.Height;
			float num5 = (float)rectangle.X / (float)texture.Width;
			float num6 = (float)rectangle.Y / (float)texture.Height;
			float num7 = (float)(rectangle.X + rectangle.Width) / (float)texture.Width;
			float num8 = (float)(rectangle.Y + rectangle.Height) / (float)texture.Height;
			this.Push(num, num2, num5, num6, color);
			this.Push(num3, num2, num7, num6, color);
			this.Push(num, num4, num5, num8, color);
			this.Push(num3, num2, num7, num6, color);
			this.Push(num3, num4, num7, num8, color);
			this.Push(num, num4, num5, num8, color);
		}

		public void OnLostDevice()
		{
		}

		public void OnResetDevice()
		{
		}

		private void EnsureBatch(Texture texture)
		{
			Device device = Device.Active;
			int num = device.BlendSrc;
			int num2 = device.BlendDst;
			int num3 = device.BlendEquation;
			bool flag = texture.IsRenderTarget;
			if (Matches(this.batchTexture, this.batchBlendSrc, this.batchBlendDst, this.batchEquation, this.batchFlip, texture, num, num2, num3, flag))
			{
				return;
			}
			if (Matches(this.heldTexture, this.heldBlendSrc, this.heldBlendDst, this.heldEquation, this.heldFlip, texture, num, num2, num3, flag))
			{
				this.Swap();
				return;
			}
			if (this.heldTexture != null)
			{
				this.Flush();
			}
			else if (this.batchTexture != null)
			{
				this.Park();
			}
			this.batchTexture = texture;
			this.batchBlendSrc = num;
			this.batchBlendDst = num2;
			this.batchEquation = num3;
			this.batchFlip = flag;
			this.batchSerial = ++this.nextSerial;
		}

		private static bool Matches(Texture current, int currentSrc, int currentDst, int currentEquation, bool currentFlip, Texture texture, int src, int dst, int equation, bool flip)
		{
			return current != null && current.GlId == texture.GlId && currentSrc == src && currentDst == dst && currentEquation == equation && currentFlip == flip;
		}

		private void Park()
		{
			List<float> list = this.held;
			this.held = this.batch;
			this.batch = list;
			this.batch.Clear();
			this.heldTexture = this.batchTexture;
			this.heldBlendSrc = this.batchBlendSrc;
			this.heldBlendDst = this.batchBlendDst;
			this.heldEquation = this.batchEquation;
			this.heldFlip = this.batchFlip;
			this.heldSerial = this.batchSerial;
			this.batchTexture = null;
			this.batchSerial = 0;
		}

		private void Swap()
		{
			List<float> list = this.batch;
			this.batch = this.held;
			this.held = list;
			Texture texture = this.batchTexture;
			this.batchTexture = this.heldTexture;
			this.heldTexture = texture;
			int num = this.batchBlendSrc;
			this.batchBlendSrc = this.heldBlendSrc;
			this.heldBlendSrc = num;
			num = this.batchBlendDst;
			this.batchBlendDst = this.heldBlendDst;
			this.heldBlendDst = num;
			num = this.batchEquation;
			this.batchEquation = this.heldEquation;
			this.heldEquation = num;
			bool flag = this.batchFlip;
			this.batchFlip = this.heldFlip;
			this.heldFlip = flag;
			num = this.batchSerial;
			this.batchSerial = this.heldSerial;
			this.heldSerial = num;
		}

		private void Push(float x, float y, float u, float v, Color4 color)
		{
			float num = this.Transform.M11 * x + this.Transform.M21 * y + this.Transform.M41;
			float num2 = this.Transform.M12 * x + this.Transform.M22 * y + this.Transform.M42;
			this.batch.Add(num);
			this.batch.Add(num2);
			this.batch.Add(u);
			this.batch.Add(v);
			this.batch.Add(color.Red);
			this.batch.Add(color.Green);
			this.batch.Add(color.Blue);
			this.batch.Add(color.Alpha);
		}

		private void Flush()
		{
			if (this.heldTexture != null && this.batchTexture != null && this.batchSerial < this.heldSerial)
			{
				this.Swap();
			}
			this.Emit(this.held, this.heldTexture, this.heldBlendSrc, this.heldBlendDst, this.heldEquation, this.heldFlip);
			this.held.Clear();
			this.heldTexture = null;
			this.heldSerial = 0;
			this.Emit(this.batch, this.batchTexture, this.batchBlendSrc, this.batchBlendDst, this.batchEquation, this.batchFlip);
			this.batch.Clear();
			this.batchTexture = null;
			this.batchSerial = 0;
		}

		private void Emit(List<float> list, Texture texture, int src, int dst, int equation, bool flip)
		{
			if (list.Count == 0 || texture == null)
			{
				list.Clear();
				return;
			}
			Device device = Device.Active;
			Viewport viewport = device.Viewport;
			GlHost.Need().SetViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, device.BackBufferWidth, device.BackBufferHeight);
			GlHost.Need().SetBlend(device.AlphaBlendEnable, src, dst, equation);
			GlHost.Need().SetDepth(false);
			GlHost.Need().DrawSprites(texture.GlId, flip ? 1f : 0f, list.ToArray());
			GlHost.Need().SetDepth(device.DepthEnable);
		}
	}
}
