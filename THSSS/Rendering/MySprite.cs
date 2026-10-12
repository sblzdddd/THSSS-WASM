using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class MySprite
	{
		public Sprite sprite { get; set; }

		public MySprite(Device device)
		{
			sprite = new Sprite(device);
		}

		public void Begin(SpriteFlags flags)
		{
			sprite.Begin(flags);
		}

		public void End()
		{
			sprite.End();
		}

		public void Draw2D(Texture srcTexture, PointF rotationCenter, float rotationAngle, PointF position, Color color)
		{
			var vector = new Vector3(rotationCenter.X, rotationCenter.Y, 0f);
			var vector2 = new Vector3(position.X, position.Y, 0f);
			var color2 = new Color4(color);
			var vector3 = new Vector2(position.X, position.Y);
			sprite.Transform = Matrix.Transformation2D(vector3, 0f, new Vector2(1f, 1f), vector3, rotationAngle, new Vector2(0f, 0f));
			sprite.Draw(srcTexture, new Vector3?(vector), new Vector3?(vector2), color2);
			sprite.Transform = Matrix.Identity;
		}

		public void Draw2D(Texture srcTexture, Rectangle srcRectangle, SizeF destinationSize, PointF position, Color color)
		{
			var vector = new Vector3(position.X, position.Y, 0f);
			var color2 = new Color4(color);
			var vector2 = new Vector2(position.X, position.Y);
			var vector3 = new Vector2(destinationSize.Width / (float)srcRectangle.Width, destinationSize.Height / (float)srcRectangle.Height);
			sprite.Transform = Matrix.Transformation2D(vector2, 0f, vector3, vector2, 0f, new Vector2(0f, 0f));
			sprite.Draw(srcTexture, new Rectangle?(srcRectangle), new Vector3?(new Vector3(0f, 0f, 0f)), new Vector3?(vector), color2);
			sprite.Transform = Matrix.Identity;
		}

		public void Draw2D(Texture srcTexture, Rectangle srcRectangle, SizeF destinationSize, PointF rotationCenter, float rotationAngle, PointF position, Color color)
		{
			var vector = new Vector3(rotationCenter.X, rotationCenter.Y, 0f);
			var vector2 = new Vector3(position.X, position.Y, 0f);
			var color2 = new Color4(color);
			var vector3 = new Vector2(rotationCenter.X, rotationCenter.Y);
			var vector4 = new Vector2(position.X, position.Y);
			var vector5 = vector3 + vector4;
			var vector6 = new Vector2(destinationSize.Width / (float)srcRectangle.Width, destinationSize.Height / (float)srcRectangle.Height);
			sprite.Transform = Matrix.Transformation2D(vector4, 0f, vector6, vector4, rotationAngle, new Vector2(0f, 0f));
			sprite.Draw(srcTexture, new Rectangle?(srcRectangle), new Vector3?(vector), new Vector3?(vector2), color2);
			sprite.Transform = Matrix.Identity;
		}

		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, int transpanrent)
		{
			Draw2D(txtureObject, scale, rotationAngle, position, Color.FromArgb(transpanrent, Color.White));
		}

		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, Color color)
		{
			Draw2D(txtureObject, scale, rotationAngle, position, txtureObject.RotatingCenter, color);
		}

		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, PointF rotationCenter, Color color)
		{
			Draw2D(txtureObject, scale, rotationAngle, position, rotationCenter, color, false);
		}

		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, PointF rotationCenter, Color color, bool mirrored)
		{
			Draw2D(txtureObject, scale, scale, rotationAngle, position, rotationCenter, color, mirrored);
		}

		public void Draw2D(TextureObject txtureObject, float scaleX, float scaleY, float rotationAngle, PointF position, Color color, bool mirrored)
		{
			Draw2D(txtureObject, scaleX, scaleY, rotationAngle, position, txtureObject.RotatingCenter, color, mirrored);
		}

		public void Draw2D(TextureObject txtureObject, float scaleX, float scaleY, float rotationAngle, PointF position, PointF rotationCenter, Color color, bool mirrored)
		{
			if (txtureObject == null)
			{
				return;
			}
			var num = mirrored ? -scaleX : scaleX;
			sprite.DrawSpriteQuad(txtureObject.TXTure, txtureObject.PosRect, rotationCenter.X, rotationCenter.Y, position.X, position.Y, num, scaleY, rotationAngle, (float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f, (float)color.A / 255f);
		}

		public void Dispose()
		{
			sprite.Dispose();
		}
	}
}
