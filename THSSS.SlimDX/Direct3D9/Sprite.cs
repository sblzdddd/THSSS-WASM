using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX.Gl;

namespace SlimDX.Direct3D9
{
	public class Sprite : ComObject
	{
		private sealed class Bucket
		{
			public Texture Texture;

			public int Src;

			public int Dst;

			public int Eq;

			public bool Flip;

			public float[] Data = new float[768];

			public int Count;
		}

		private readonly Dictionary<long, Bucket> buckets = new Dictionary<long, Bucket>();

		private readonly List<Bucket> bucketOrder = new List<Bucket>();

		private float[] batchData = new float[8192];

		private int batchCount;

		private Texture batchTexture;

		private int batchBlendSrc;

		private int batchBlendDst;

		private int batchEquation;

		private bool batchFlip;

		private bool unordered;

		private bool runBlendSet;

		private int runSrc;

		private int runDst;

		private int runEq;

		private Bucket currentBucket;

		private float cachedRotation = float.NaN;

		private float cachedCos = 1f;

		private float cachedSin;

		internal static Sprite Current;

		public Matrix Transform { get; set; }

		public Sprite(Device device)
		{
			this.Transform = Matrix.Identity;
		}

		public void Begin(SpriteFlags flags)
		{
			if (Device.Active != null)
			{
				Device.Active.FlushWorldBatch();
			}
			this.Flush();
			Current = this;
			this.unordered = (flags & SpriteFlags.SortTexture) != 0;
			this.runBlendSet = false;
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
			this.unordered = false;
			if (Current == this)
			{
				Current = null;
			}
		}

		internal void FlushPending()
		{
			this.Flush();
		}

		public void Draw(Texture texture, Vector3? center, Vector3? position, Color4 color)
		{
			Rectangle rectangle = new Rectangle(0, 0, texture != null ? texture.Width : 1, texture != null ? texture.Height : 1);
			this.Draw(texture, rectangle, center, position, color);
		}

		public void DrawSpriteQuad(Texture texture, Rectangle source, float centerX, float centerY, float posX, float posY, float scaleX, float scaleY, float rotation, float red, float green, float blue, float alpha)
		{
			if (texture == null || texture.Disposed || texture.GlId == 0 || texture.Width == 0 || texture.Height == 0)
			{
				return;
			}
			this.EnsureBatch(texture);
			float cos;
			float sin;
			if (rotation == 0f)
			{
				cos = 1f;
				sin = 0f;
			}
			else if (rotation == this.cachedRotation)
			{
				cos = this.cachedCos;
				sin = this.cachedSin;
			}
			else
			{
				cos = MathF.Cos(rotation);
				sin = MathF.Sin(rotation);
				this.cachedRotation = rotation;
				this.cachedCos = cos;
				this.cachedSin = sin;
			}
			float bx = posX * (1f - scaleX);
			float by = posY * (1f - scaleY);
			float m11 = cos * scaleX;
			float m21 = -sin * scaleY;
			float m12 = sin * scaleX;
			float m22 = cos * scaleY;
			float m41 = cos * (bx - posX) - sin * (by - posY) + posX;
			float m42 = sin * (bx - posX) + cos * (by - posY) + posY;
			float left = posX - centerX;
			float top = posY - centerY;
			float right = left + source.Width;
			float bottom = top + source.Height;
			float u0 = source.X / (float)texture.Width;
			float v0 = source.Y / (float)texture.Height;
			float u1 = (source.X + source.Width) / (float)texture.Width;
			float v1 = (source.Y + source.Height) / (float)texture.Height;
			this.Append(m11 * left + m21 * top + m41, m12 * left + m22 * top + m42, u0, v0, red, green, blue, alpha);
			this.Append(m11 * right + m21 * top + m41, m12 * right + m22 * top + m42, u1, v0, red, green, blue, alpha);
			this.Append(m11 * left + m21 * bottom + m41, m12 * left + m22 * bottom + m42, u0, v1, red, green, blue, alpha);
			this.Append(m11 * right + m21 * top + m41, m12 * right + m22 * top + m42, u1, v0, red, green, blue, alpha);
			this.Append(m11 * right + m21 * bottom + m41, m12 * right + m22 * bottom + m42, u1, v1, red, green, blue, alpha);
			this.Append(m11 * left + m21 * bottom + m41, m12 * left + m22 * bottom + m42, u0, v1, red, green, blue, alpha);
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
			if (device != null)
			{
				device.FlushWorldBatch();
			}
			int num = device.BlendSrc;
			int num2 = device.BlendDst;
			int num3 = device.BlendEquation;
			bool flag = texture.IsRenderTarget;
			if (this.unordered)
			{
				if (this.runBlendSet && (this.runSrc != num || this.runDst != num2 || this.runEq != num3))
				{
					this.EmitBuckets();
				}
				this.runSrc = num;
				this.runDst = num2;
				this.runEq = num3;
				this.runBlendSet = true;
				if (this.currentBucket != null && this.currentBucket.Texture.GlId == texture.GlId && this.currentBucket.Flip == flag)
				{
					return;
				}
				long key = ((long)texture.GlId << 1) | (flag ? 1L : 0L);
				Bucket bucket;
				if (!this.buckets.TryGetValue(key, out bucket))
				{
					bucket = new Bucket();
					this.buckets[key] = bucket;
				}
				if (bucket.Count == 0)
				{
					bucket.Texture = texture;
					bucket.Src = num;
					bucket.Dst = num2;
					bucket.Eq = num3;
					bucket.Flip = flag;
					this.bucketOrder.Add(bucket);
				}
				this.currentBucket = bucket;
				return;
			}
			if (this.batchTexture != null && this.batchTexture.GlId == texture.GlId && this.batchBlendSrc == num && this.batchBlendDst == num2 && this.batchEquation == num3 && this.batchFlip == flag)
			{
				return;
			}
			this.Flush();
			this.batchTexture = texture;
			this.batchBlendSrc = num;
			this.batchBlendDst = num2;
			this.batchEquation = num3;
			this.batchFlip = flag;
		}

		private void Push(float x, float y, float u, float v, Color4 color)
		{
			float num = this.Transform.M11 * x + this.Transform.M21 * y + this.Transform.M41;
			float num2 = this.Transform.M12 * x + this.Transform.M22 * y + this.Transform.M42;
			this.Append(num, num2, u, v, color.Red, color.Green, color.Blue, color.Alpha);
		}

		private void Append(float x, float y, float u, float v, float red, float green, float blue, float alpha)
		{
			float[] data;
			int count;
			if (this.unordered)
			{
				Bucket bucket = this.currentBucket;
				if (bucket.Count + 8 > bucket.Data.Length)
				{
					Array.Resize(ref bucket.Data, bucket.Data.Length * 2);
				}
				data = bucket.Data;
				count = bucket.Count;
				bucket.Count = count + 8;
			}
			else
			{
				if (this.batchCount + 8 > this.batchData.Length)
				{
					Array.Resize(ref this.batchData, this.batchData.Length * 2);
				}
				data = this.batchData;
				count = this.batchCount;
				this.batchCount = count + 8;
			}
			data[count] = x;
			data[count + 1] = y;
			data[count + 2] = u;
			data[count + 3] = v;
			data[count + 4] = red;
			data[count + 5] = green;
			data[count + 6] = blue;
			data[count + 7] = alpha;
		}

		private void Flush()
		{
			if (this.unordered)
			{
				this.EmitBuckets();
				return;
			}
			if (this.batchCount == 0)
			{
				this.batchTexture = null;
				return;
			}
			this.Emit(this.batchData, this.batchCount, this.batchTexture, this.batchBlendSrc, this.batchBlendDst, this.batchEquation, this.batchFlip);
			this.batchCount = 0;
			this.batchTexture = null;
			this.RestoreDepth();
		}

		private void EmitBuckets()
		{
			for (int i = 0; i < this.bucketOrder.Count; i++)
			{
				Bucket bucket = this.bucketOrder[i];
				if (bucket.Count > 0)
				{
					this.Emit(bucket.Data, bucket.Count, bucket.Texture, bucket.Src, bucket.Dst, bucket.Eq, bucket.Flip);
					bucket.Count = 0;
				}
			}
			this.bucketOrder.Clear();
			this.currentBucket = null;
			this.runBlendSet = false;
			this.RestoreDepth();
		}

		private void Emit(float[] data, int count, Texture texture, int src, int dst, int equation, bool flip)
		{
			if (count == 0 || texture == null)
			{
				return;
			}
			Device device = Device.Active;
			Viewport viewport = device.Viewport;
			GlHost.Need().SetViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, device.BackBufferWidth, device.BackBufferHeight);
			GlHost.Need().SetBlend(device.AlphaBlendEnable, src, dst, equation);
			GlHost.Need().SetDepth(false, false);
			Effect effect = Effect.Active;
			if (effect != null)
			{
				GlHost.Need().DrawEffect(effect.Name, texture.GlId, flip ? 1f : 0f, data.AsSpan(0, count), effect.Float("BlurLevel"), effect.Float("X"), effect.Float("Y"), effect.Float("Time"), effect.Float("Radius"), effect.Int("ColorKey"));
			}
			else
			{
				GlHost.Need().DrawSprites(texture.GlId, flip ? 1f : 0f, data.AsSpan(0, count));
			}
		}

		private void RestoreDepth()
		{
			Device device = Device.Active;
			if (device == null)
			{
				return;
			}
			GlHost.Need().SetDepth(device.DepthEnable, device.DepthWrite);
		}
	}
}
