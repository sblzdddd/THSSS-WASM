using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000F4 RID: 244
	public class MySprite
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0003A604 File Offset: 0x00038804
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0003A61B File Offset: 0x0003881B
		public Sprite sprite { get; set; }

		// Token: 0x0600068C RID: 1676 RVA: 0x0003A624 File Offset: 0x00038824
		public MySprite(Device device)
		{
			this.sprite = new Sprite(device);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0003A63C File Offset: 0x0003883C
		public void Begin(SpriteFlags flags)
		{
			this.sprite.Begin(flags);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0003A64C File Offset: 0x0003884C
		public void End()
		{
			this.sprite.End();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003A65C File Offset: 0x0003885C
		public void Draw2D(Texture srcTexture, PointF rotationCenter, float rotationAngle, PointF position, Color color)
		{
			Vector3 vector = new Vector3(rotationCenter.X, rotationCenter.Y, 0f);
			Vector3 vector2 = new Vector3(position.X, position.Y, 0f);
			Color4 color2 = new Color4(color);
			Vector2 vector3 = new Vector2(position.X, position.Y);
			this.sprite.Transform = Matrix.Transformation2D(vector3, 0f, new Vector2(1f, 1f), vector3, rotationAngle, new Vector2(0f, 0f));
			this.sprite.Draw(srcTexture, new Vector3?(vector), new Vector3?(vector2), color2);
			this.sprite.Transform = Matrix.Identity;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003A724 File Offset: 0x00038924
		public void Draw2D(Texture srcTexture, Rectangle srcRectangle, SizeF destinationSize, PointF position, Color color)
		{
			Vector3 vector = new Vector3(position.X, position.Y, 0f);
			Color4 color2 = new Color4(color);
			Vector2 vector2 = new Vector2(position.X, position.Y);
			Vector2 vector3 = new Vector2(destinationSize.Width / (float)srcRectangle.Width, destinationSize.Height / (float)srcRectangle.Height);
			this.sprite.Transform = Matrix.Transformation2D(vector2, 0f, vector3, vector2, 0f, new Vector2(0f, 0f));
			this.sprite.Draw(srcTexture, new Rectangle?(srcRectangle), new Vector3?(new Vector3(0f, 0f, 0f)), new Vector3?(vector), color2);
			this.sprite.Transform = Matrix.Identity;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0003A808 File Offset: 0x00038A08
		public void Draw2D(Texture srcTexture, Rectangle srcRectangle, SizeF destinationSize, PointF rotationCenter, float rotationAngle, PointF position, Color color)
		{
			Vector3 vector = new Vector3(rotationCenter.X, rotationCenter.Y, 0f);
			Vector3 vector2 = new Vector3(position.X, position.Y, 0f);
			Color4 color2 = new Color4(color);
			Vector2 vector3 = new Vector2(rotationCenter.X, rotationCenter.Y);
			Vector2 vector4 = new Vector2(position.X, position.Y);
			Vector2 vector5 = vector3 + vector4;
			Vector2 vector6 = new Vector2(destinationSize.Width / (float)srcRectangle.Width, destinationSize.Height / (float)srcRectangle.Height);
			this.sprite.Transform = Matrix.Transformation2D(vector4, 0f, vector6, vector4, rotationAngle, new Vector2(0f, 0f));
			this.sprite.Draw(srcTexture, new Rectangle?(srcRectangle), new Vector3?(vector), new Vector3?(vector2), color2);
			this.sprite.Transform = Matrix.Identity;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003A912 File Offset: 0x00038B12
		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, int transpanrent)
		{
			this.Draw2D(txtureObject, scale, rotationAngle, position, Color.FromArgb(transpanrent, Color.White));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0003A92D File Offset: 0x00038B2D
		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, Color color)
		{
			this.Draw2D(txtureObject, scale, rotationAngle, position, txtureObject.RotatingCenter, color);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003A944 File Offset: 0x00038B44
		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, PointF rotationCenter, Color color)
		{
			this.Draw2D(txtureObject, scale, rotationAngle, position, rotationCenter, color, false);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0003A958 File Offset: 0x00038B58
		public void Draw2D(TextureObject txtureObject, float scale, float rotationAngle, PointF position, PointF rotationCenter, Color color, bool mirrored)
		{
			this.Draw2D(txtureObject, scale, scale, rotationAngle, position, rotationCenter, color, mirrored);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003A97C File Offset: 0x00038B7C
		public void Draw2D(TextureObject txtureObject, float scaleX, float scaleY, float rotationAngle, PointF position, Color color, bool mirrored)
		{
			this.Draw2D(txtureObject, scaleX, scaleY, rotationAngle, position, txtureObject.RotatingCenter, color, mirrored);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		public void Draw2D(TextureObject txtureObject, float scaleX, float scaleY, float rotationAngle, PointF position, PointF rotationCenter, Color color, bool mirrored)
		{
			Vector3 vector = new Vector3(rotationCenter.X, rotationCenter.Y, 0f);
			Vector3 vector2 = new Vector3(position.X, position.Y, 0f);
			Color4 color2 = new Color4(color);
			Vector2 vector3 = new Vector2(rotationCenter.X, rotationCenter.Y);
			Vector2 vector4 = new Vector2(position.X, position.Y);
			Vector2 vector5 = vector3 + vector4;
			Vector2 vector6 = (mirrored ? new Vector2(-scaleX, scaleY) : new Vector2(scaleX, scaleY));
			this.sprite.Transform = Matrix.Transformation2D(vector4, 0f, vector6, vector4, rotationAngle, new Vector2(0f, 0f));
			this.sprite.Draw(txtureObject.TXTure, new Rectangle?(txtureObject.PosRect), new Vector3?(vector), new Vector3?(vector2), color2);
			this.sprite.Transform = Matrix.Identity;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003AAA7 File Offset: 0x00038CA7
		public void Dispose()
		{
			this.sprite.Dispose();
		}
	}
}
