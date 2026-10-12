using System.Drawing;

namespace Shooting
{
	internal class ScoreItem_Touhou : BaseItem_Touhou
	{
		public ScoreItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-1", OriginalPosition)
		{
			Region = 5;
		}

		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (HitCheck(MyPlane))
			{
				ItemList.Remove(this);
				long num;
				if (Doubled)
				{
					num = (long)((float)MyPlane.HighItemScore * MyPlane.Rate);
					num /= 10L;
					num *= 10L;
					ParticleScoreValue particleScoreValue = new ParticleScoreValue(StageData, OriginalPosition, num);
					particleScoreValue.ColorValue = Color.FromArgb(254, 222, 63);
				}
				else
				{
					num = (long)(10 + (MyPlane.HighItemScore - 10) * (448 - (int)OriginalPosition.Y) / 320);
					num /= 10L;
					num *= 10L;
					ParticleScoreValue particleScoreValue = new ParticleScoreValue(StageData, OriginalPosition, num);
				}
				MyPlane.Score += num;
				MyPlane.StarPoint += 20;
				StageData.SoundPlay("se_item00.wav", OriginalPosition.X / (float)BoundRect.Width);
			}
		}

		public override void Show()
		{
			base.Show();
			if (OriginalPosition.Y < -8f)
			{
				SpriteMain.Draw2D(TextureObjectDictionary["Item-9"], 1f, 0f, new PointF(Position.X, (float)BoundRect.Top), Color.FromArgb(TransparentValue, ColorValue));
			}
		}
	}
}
