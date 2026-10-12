using System.Drawing;

namespace Shooting
{
	internal class PowerItemBig_Touhou : BaseItem_Touhou
	{
		public PowerItemBig_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-3", OriginalPosition)
		{
			base.Region = 10;
		}

		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				float num = (float)base.MyPlane.Power;
				base.MyPlane.Power += 100;
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				if (num != (float)base.MyPlane.Power)
				{
					base.StageData.SoundPlay("se_powerup.wav");
				}
				int num2 = 5000 + base.MyPlane.GreenPoint / 2;
				num2 /= 10;
				num2 *= 10;
				base.MyPlane.Score += (long)num2;
				ParticleScoreValue particleScoreValue = new ParticleScoreValue(base.StageData, base.OriginalPosition, (long)num2);
				if (base.Doubled)
				{
					particleScoreValue.ColorValue = Color.FromArgb(254, 222, 63);
				}
			}
		}

		public override void Show()
		{
			base.Show();
			if (base.OriginalPosition.Y < -8f)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Item-11"], 1f, 0f, new PointF(base.Position.X, (float)base.BoundRect.Top), Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
