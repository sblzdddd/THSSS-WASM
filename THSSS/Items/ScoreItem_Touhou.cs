using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E3 RID: 483
	internal class ScoreItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x0006DE7C File Offset: 0x0006C07C
		public ScoreItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-1", OriginalPosition)
		{
			base.Region = 5;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0006DE98 File Offset: 0x0006C098
		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				long num;
				if (base.Doubled)
				{
					num = (long)((float)base.MyPlane.HighItemScore * base.MyPlane.Rate);
					num /= 10L;
					num *= 10L;
					ParticleScoreValue particleScoreValue = new ParticleScoreValue(base.StageData, base.OriginalPosition, num);
					particleScoreValue.ColorValue = Color.FromArgb(254, 222, 63);
				}
				else
				{
					num = (long)(10 + (base.MyPlane.HighItemScore - 10) * (448 - (int)base.OriginalPosition.Y) / 320);
					num /= 10L;
					num *= 10L;
					ParticleScoreValue particleScoreValue = new ParticleScoreValue(base.StageData, base.OriginalPosition, num);
				}
				base.MyPlane.Score += num;
				base.MyPlane.StarPoint += 20;
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		public override void Show()
		{
			base.Show();
			if (base.OriginalPosition.Y < -8f)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Item-9"], 1f, 0f, new PointF(base.Position.X, (float)base.BoundRect.Top), Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
