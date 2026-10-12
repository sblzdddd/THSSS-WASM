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
			var num = Num;
			for (var i = 0; i < Count; i++)
			{
				var num2 = num % 10;
				num /= 10;
				var textureObject = TextureObjectDictionary[num2.ToString()];
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, new SizeF((float)textureObject.Width, (float)textureObject.Height), textureObject.RotatingCenter, 0f, new PointF(Position.X + (float)(16 * (Count - i - 1)), Position.Y), Color.White);
			}
		}

		private const int NumWidth = 16;
	}
}
