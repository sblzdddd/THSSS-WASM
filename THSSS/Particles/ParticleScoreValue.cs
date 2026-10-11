using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001CA RID: 458
	internal class ParticleScoreValue : BaseEffect
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0006646C File Offset: 0x0006466C
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00066483 File Offset: 0x00064683
		private char[] ScoreCharArray { get; set; }

		// Token: 0x06000A8F RID: 2703 RVA: 0x0006648C File Offset: 0x0006468C
		public ParticleScoreValue(StageDataPackage StageData, PointF OriginalPosition, long ScoreValue)
			: base(StageData, null, new PointF(0f, 0f), 1f, -1.5707963705062866)
		{
			base.LifeTime = 50;
			base.Active = false;
			base.OriginalPosition = OriginalPosition;
			this.ScoreCharArray = ScoreValue.ToString().ToCharArray();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000664F0 File Offset: 0x000646F0
		public override void Ctrl()
		{
			base.Angle = -base.Direction + 1.5707963267948966;
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0006654C File Offset: 0x0006474C
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
