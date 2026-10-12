using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class StageTitle3D2 : StageTitle3D
	{
		public StageTitle3D2(StageDataPackage StageData, string TextureName)
			: base(StageData, TextureName)
		{
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2 - 80));
		}

		public override void SetupVertex()
		{
			var dataStream = vertexBuffer.Lock(0, 0, LockFlags.Discard);
			var num = -TxtureObject.RotatingCenter.X;
			var num2 = (float)TxtureObject.Width - TxtureObject.RotatingCenter.X;
			var num3 = -TxtureObject.RotatingCenter.Y;
			var num4 = (float)TxtureObject.Height - TxtureObject.RotatingCenter.Y;
			var num5 = (float)TxtureObject.PosRect.X / (float)TxtureObject.SrcWidth;
			var num6 = (float)(TxtureObject.PosRect.X + TxtureObject.Width) / (float)TxtureObject.SrcWidth;
			var num7 = (float)TxtureObject.PosRect.Y / (float)TxtureObject.SrcHeight;
			var num8 = (float)(TxtureObject.PosRect.Y + TxtureObject.Height) / (float)TxtureObject.SrcHeight;
			var array = new TexturedColoredVertex[2 * Length];
			for (var i = 0; i < Length; i++)
			{
				var num9 = -10 * i + Time * 3;
				if (num9 < 0)
				{
					num9 = 0;
				}
				else if (num9 > TransparentValue)
				{
					num9 = TransparentValue;
				}
				array[2 * i] = new TexturedColoredVertex(new Vector3(num + (num2 - num) * (float)i / (float)Length, num3, 0f), new Vector2(num5 + (num6 - num5) * (float)i / (float)Length, num7), Color.FromArgb(num9, ColorValue).ToArgb());
				array[2 * i + 1] = new TexturedColoredVertex(new Vector3(num + (num2 - num) * (float)i / (float)Length, num4, 0f), new Vector2(num5 + (num6 - num5) * (float)i / (float)Length, num8), Color.FromArgb(num9, ColorValue).ToArgb());
			}
			dataStream.WriteRange<TexturedColoredVertex>(array);
			vertexBuffer.Unlock();
		}
	}
}
