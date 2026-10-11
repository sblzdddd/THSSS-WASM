using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200009E RID: 158
	public class BossLife_Line : Life_Line
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00024D0F File Offset: 0x00022F0F
		public BossLife_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			base.TransparentVelocity = 0f;
			base.TransparentValueF = 0f;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00024D38 File Offset: 0x00022F38
		public override void Ctrl()
		{
			if (base.Boss != null)
			{
				base.Ctrl();
				if (base.MyPlane.OriginalPosition.X < (float)base.Life * this.size && base.MyPlane.OriginalPosition.Y < this.size + base.OriginalPosition.Y)
				{
					base.TransparentValueF -= 10f;
					if (base.TransparentValueF < 50f)
					{
						base.TransparentValueF = 50f;
					}
				}
				else
				{
					base.TransparentValueF += 10f;
				}
				base.Life = base.Boss.Life;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00024E1C File Offset: 0x0002301C
		public override void Show()
		{
			if (base.Boss != null)
			{
				this.size = 16f;
				SizeF sizeF = new SizeF(this.size, this.size);
				for (int i = 0; i < base.Life - 1; i++)
				{
					if (i < 5)
					{
						PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
						base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
					}
					else
					{
						PointF pointF = new PointF(base.OriginalPosition.X + (float)(i - 5) * this.size, base.OriginalPosition.Y + 16f);
						base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
					}
				}
			}
		}
	}
}
