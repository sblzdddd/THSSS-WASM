using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000134 RID: 308
	internal class PowerItemBig_Touhou : BaseItem_Touhou
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x0004B820 File Offset: 0x00049A20
		public PowerItemBig_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-3", OriginalPosition)
		{
			base.Region = 10;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0004B83C File Offset: 0x00049A3C
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

		// Token: 0x06000834 RID: 2100 RVA: 0x0004B960 File Offset: 0x00049B60
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
