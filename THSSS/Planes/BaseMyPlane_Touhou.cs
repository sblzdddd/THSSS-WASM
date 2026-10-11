using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000067 RID: 103
	public class BaseMyPlane_Touhou : BaseMyPlane
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001947C File Offset: 0x0001767C
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00019493 File Offset: 0x00017693
		public float IndexX { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001949C File Offset: 0x0001769C
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000194B3 File Offset: 0x000176B3
		public int IndexY { get; set; }

		// Token: 0x060003BB RID: 955 RVA: 0x000194BC File Offset: 0x000176BC
		public BaseMyPlane_Touhou(StageDataPackage StageData, Point OriginalPosition, string Name)
		{
			base.StageData = StageData;
			base.OriginalPosition = OriginalPosition;
			base.Name = Name;
			base.Region = 1;
			base.Time = base.UnmatchedTime;
			base.DeadTime = -1;
			base.SubPlanePoint = new PointF[4];
			string text = Name + this.IndexY.ToString() + ((int)this.IndexX).ToString();
			base.TxtureObject = base.TextureObjectDictionary[text];
			base.SpellEnabled = true;
			base.HighItemScore = 10000;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00019564 File Offset: 0x00017764
		public override void TextureCtrl()
		{
			int indexY = this.IndexY;
			if (base.Vx > 0.5f)
			{
				this.IndexY = 2;
				if (this.IndexX < 4f)
				{
					this.IndexX += 0.5f;
				}
				else
				{
					this.IndexX = (float)(4 + base.Time % 24 / 6);
				}
			}
			else if (base.Vx < -0.5f)
			{
				this.IndexY = 1;
				if (this.IndexX < 4f)
				{
					this.IndexX += 0.5f;
				}
				else
				{
					this.IndexX = (float)(4 + base.Time % 24 / 6);
				}
			}
			else
			{
				this.IndexY = 0;
				this.IndexX = (float)(base.Time % 48 / 6);
			}
			if (indexY != this.IndexY)
			{
				this.IndexX = 1f;
			}
			if (this.IndexX > 7f)
			{
				this.IndexX = 7f;
			}
			string text = base.Name + this.IndexY.ToString() + ((int)this.IndexX).ToString();
			base.TxtureObject = base.TextureObjectDictionary[text];
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000196D4 File Offset: 0x000178D4
		public override void ShowCenter()
		{
			if (base.SlowMove)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["SlowEffect01"], 1f, -(float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), 120);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["SlowEffect01"], 1f, (float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), 120);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Center"], 1f, (float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), Color.White);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000197EC File Offset: 0x000179EC
		public override void Miss()
		{
			double num = -2.2707963148739676;
			for (int i = 0; i < 8; i++)
			{
				PointF pointF = new PointF((float)((double)base.OriginalPosition.X + 40.0 * Math.Cos(num)), (float)((double)base.OriginalPosition.Y + 40.0 * Math.Sin(num)));
				PowerItem_Touhou powerItem_Touhou = new PowerItem_Touhou(base.StageData, pointF);
				powerItem_Touhou.Direction = num;
				powerItem_Touhou.Velocity = 5f;
				num += 0.1999999965940203;
			}
			base.Miss();
		}
	}
}
