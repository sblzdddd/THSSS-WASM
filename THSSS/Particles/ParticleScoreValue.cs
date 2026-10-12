using System.Drawing;

namespace Shooting
{
	internal class ParticleScoreValue : BaseEffect
	{
		private char[] ScoreCharArray { get; set; }

		public ParticleScoreValue(StageDataPackage StageData, PointF OriginalPosition, long ScoreValue)
			: base(StageData, null, new PointF(0f, 0f), 1f, -1.5707963705062866)
		{
			LifeTime = 50;
			Active = false;
			this.OriginalPosition = OriginalPosition;
			ScoreCharArray = ScoreValue.ToString().ToCharArray();
		}

		public override void Ctrl()
		{
			Angle = -Direction + 1.5707963267948966;
			base.Ctrl();
			if (Time > 0)
			{
				TransparentValueF -= (float)(255 / LifeTime);
			}
		}

		public override void Show()
		{
			int num = TextureObjectDictionary["Score_+"].Width;
			for (int i = 0; i < ScoreCharArray.Length; i++)
			{
				num += TextureObjectDictionary[ScoreCharArray[i].ToString()].Width;
			}
			float num2 = 0.6f;
			PointF pointF = new PointF(OriginalPosition.X - (float)num * num2 / 2f, OriginalPosition.Y);
			SpriteMain.Draw2D(TextureObjectDictionary["Score_+"], num2, 0f, pointF, Color.FromArgb(TransparentValue, ColorValue));
			pointF = new PointF(pointF.X + (float)TextureObjectDictionary["Score_+"].Width * num2 / 2f, pointF.Y);
			for (int i = 0; i < ScoreCharArray.Length; i++)
			{
				pointF = new PointF(pointF.X + (float)TextureObjectDictionary["Score_" + ScoreCharArray[i].ToString()].Width * num2 / 2f, pointF.Y);
				SpriteMain.Draw2D(TextureObjectDictionary["Score_" + ScoreCharArray[i].ToString()], num2, 0f, pointF, Color.FromArgb(TransparentValue, ColorValue));
				pointF = new PointF(pointF.X + (float)TextureObjectDictionary["Score_" + ScoreCharArray[i].ToString()].Width * num2 / 2f, pointF.Y);
			}
		}
	}
}
