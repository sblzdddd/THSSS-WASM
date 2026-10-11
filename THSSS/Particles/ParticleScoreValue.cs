using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleScoreValue : BaseEffect
	{
		private char[] ScoreCharArray { get; set; }

		public ParticleScoreValue(StageDataPackage StageData, PointF OriginalPosition, long ScoreValue)
			: base(StageData, null, new PointF(0f, 0f), 1f, -1.5707963705062866)
		{
			base.LifeTime = 50;
			base.Active = false;
			base.OriginalPosition = OriginalPosition;
			this.ScoreCharArray = ScoreValue.ToString().ToCharArray();
		}

		public override void Ctrl()
		{
			base.Angle = -base.Direction + 1.5707963267948966;
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}

		public override void Show()
		{
			int num = base.TextureObjectDictionary["Score_+"].Width;
			for (int i = 0; i < this.ScoreCharArray.Length; i++)
			{
				num += base.TextureObjectDictionary[this.ScoreCharArray[i].ToString()].Width;
			}
			float num2 = 0.6f;
			PointF pointF = new PointF(base.OriginalPosition.X - (float)num * num2 / 2f, base.OriginalPosition.Y);
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Score_+"], num2, 0f, pointF, Color.FromArgb(base.TransparentValue, base.ColorValue));
			pointF = new PointF(pointF.X + (float)base.TextureObjectDictionary["Score_+"].Width * num2 / 2f, pointF.Y);
			for (int i = 0; i < this.ScoreCharArray.Length; i++)
			{
				pointF = new PointF(pointF.X + (float)base.TextureObjectDictionary["Score_" + this.ScoreCharArray[i].ToString()].Width * num2 / 2f, pointF.Y);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Score_" + this.ScoreCharArray[i].ToString()], num2, 0f, pointF, Color.FromArgb(base.TransparentValue, base.ColorValue));
				pointF = new PointF(pointF.X + (float)base.TextureObjectDictionary["Score_" + this.ScoreCharArray[i].ToString()].Width * num2 / 2f, pointF.Y);
			}
		}
	}
}
