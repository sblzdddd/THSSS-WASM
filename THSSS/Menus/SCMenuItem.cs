using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000172 RID: 370
	public class SCMenuItem : DescriptionMenuItem
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00056B3A File Offset: 0x00054D3A
		public SCMenuItem(StageDataPackage StageData, string Name)
			: base(StageData, Name)
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00056B58 File Offset: 0x00054D58
		public override void Show()
		{
			base.Show();
			if (base.TxtureObject != null)
			{
				base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, 0f, new PointF(base.Position.X + (float)base.TxtureObject.Width * base.Scale / 2f + (float)this.OffX, base.Position.Y + (float)this.OffY + (float)base.TxtureObject.Height * base.Scale / 2f), Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
			}
		}

		// Token: 0x0400035C RID: 860
		public int OffX = 50;

		// Token: 0x0400035D RID: 861
		public int OffY = -5;
	}
}
