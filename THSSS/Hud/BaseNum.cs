using System.Drawing;

namespace Shooting
{
	internal class BaseNum : BaseObject
	{
		public int Num { get; set; }

		public int Count { get; set; }

		public BaseNum(StageDataPackage StageData, PointF Position)
			: base(StageData, null, Position, 0f, 0.0)
		{
		}

		public override void Show()
		{
			int num = this.Num;
			for (int i = 0; i < this.Count; i++)
			{
				int num2 = num % 10;
				num /= 10;
				TextureObject textureObject = base.TextureObjectDictionary[num2.ToString()];
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, new SizeF((float)textureObject.Width, (float)textureObject.Height), textureObject.RotatingCenter, 0f, new PointF(base.Position.X + (float)(16 * (this.Count - i - 1)), base.Position.Y), Color.White);
			}
		}

		private const int NumWidth = 16;
	}
}
