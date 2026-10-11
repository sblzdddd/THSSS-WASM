using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000AA RID: 170
	public class StageTitle3D2 : StageTitle3D
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00028140 File Offset: 0x00026340
		public StageTitle3D2(StageDataPackage StageData, string TextureName)
			: base(StageData, TextureName)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2 - 80));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002818C File Offset: 0x0002638C
		public override void SetupVertex()
		{
			DataStream dataStream = this.vertexBuffer.Lock(0, 0, LockFlags.Discard);
			float num = -base.TxtureObject.RotatingCenter.X;
			float num2 = (float)base.TxtureObject.Width - base.TxtureObject.RotatingCenter.X;
			float num3 = -base.TxtureObject.RotatingCenter.Y;
			float num4 = (float)base.TxtureObject.Height - base.TxtureObject.RotatingCenter.Y;
			float num5 = (float)base.TxtureObject.PosRect.X / (float)base.TxtureObject.SrcWidth;
			float num6 = (float)(base.TxtureObject.PosRect.X + base.TxtureObject.Width) / (float)base.TxtureObject.SrcWidth;
			float num7 = (float)base.TxtureObject.PosRect.Y / (float)base.TxtureObject.SrcHeight;
			float num8 = (float)(base.TxtureObject.PosRect.Y + base.TxtureObject.Height) / (float)base.TxtureObject.SrcHeight;
			TexturedColoredVertex[] array = new TexturedColoredVertex[2 * this.Length];
			for (int i = 0; i < this.Length; i++)
			{
				int num9 = -10 * i + base.Time * 3;
				if (num9 < 0)
				{
					num9 = 0;
				}
				else if (num9 > base.TransparentValue)
				{
					num9 = base.TransparentValue;
				}
				array[2 * i] = new TexturedColoredVertex(new Vector3(num + (num2 - num) * (float)i / (float)this.Length, num3, 0f), new Vector2(num5 + (num6 - num5) * (float)i / (float)this.Length, num7), Color.FromArgb(num9, base.ColorValue).ToArgb());
				array[2 * i + 1] = new TexturedColoredVertex(new Vector3(num + (num2 - num) * (float)i / (float)this.Length, num4, 0f), new Vector2(num5 + (num6 - num5) * (float)i / (float)this.Length, num8), Color.FromArgb(num9, base.ColorValue).ToArgb());
			}
			dataStream.WriteRange<TexturedColoredVertex>(array);
			this.vertexBuffer.Unlock();
		}
	}
}
