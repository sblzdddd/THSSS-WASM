using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001FC RID: 508
	internal class PowerItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00071E42 File Offset: 0x00070042
		public PowerItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-0", OriginalPosition)
		{
			base.Region = 5;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00071E5C File Offset: 0x0007005C
		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				float num = (float)base.MyPlane.Power;
				base.MyPlane.Power += 2;
				if (num != (float)base.MyPlane.Power)
				{
					if (base.MyPlane.Power % 100 == 0)
					{
						base.StageData.SoundPlay("se_powerup.wav");
					}
				}
				int num2 = ((base.MyPlane.Power == 400) ? 51200 : 10);
				base.MyPlane.Score += (long)num2;
				base.MyPlane.StarPoint += 20;
				if (base.MyPlane.Power == 400)
				{
					ParticleScoreValue particleScoreValue = new ParticleScoreValue(base.StageData, base.OriginalPosition, (long)num2);
				}
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00071FA0 File Offset: 0x000701A0
		public override void Show()
		{
			base.Show();
			if (base.OriginalPosition.Y < -8f)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Item-8"], 1f, 0f, new PointF(base.Position.X, (float)base.BoundRect.Top), Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
