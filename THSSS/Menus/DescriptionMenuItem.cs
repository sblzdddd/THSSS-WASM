using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D4 RID: 212
	public class DescriptionMenuItem : BaseMenuItem
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00034698 File Offset: 0x00032898
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000346AF File Offset: 0x000328AF
		public string Description { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000346B8 File Offset: 0x000328B8
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000346CF File Offset: 0x000328CF
		public Color DefaultColor { get; set; }

		// Token: 0x060005DC RID: 1500 RVA: 0x000346D8 File Offset: 0x000328D8
		public DescriptionMenuItem(StageDataPackage StageData, string Name)
		{
			base.StageData = StageData;
			base.Name = Name;
			base.MaxVelocity = 100f;
			base.Selected = false;
			base.UnSelectVisible = true;
			this.DefaultColor = Color.Gray;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00034728 File Offset: 0x00032928
		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				this.Move();
				base.Velocity += base.Accelerate;
				base.TransparentValueF += base.TransparentVelocity;
				if (base.Selected)
				{
					double num = 1.0 + Math.Sin((double)((float)base.Time / 6f));
					base.ColorValue = Color.Yellow;
					base.TransparentVelocity = 25f;
				}
				else
				{
					base.ColorValue = this.DefaultColor;
					if (!base.UnSelectVisible)
					{
						base.TransparentVelocity = -25f;
					}
				}
				if (base.OnRemove)
				{
					base.MaxTransparent -= 15;
				}
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00034811 File Offset: 0x00032A11
		public override void Select()
		{
			base.Time = 1;
			base.Selected = true;
			base.VibrateTime = 0;
			base.TwinkleTime = 0;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00034834 File Offset: 0x00032A34
		public override void Click()
		{
			base.Time = 1;
			base.VibrateTime = 0;
			base.TwinkleTime = base.Time + 30;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00034858 File Offset: 0x00032A58
		public void DrawText(string text, PointF Pos)
		{
			text = text.Replace("\t", "    ");
			text = text.Replace("\r\n", string.Empty);
			int num = 0;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				PointF pointF = new PointF(Pos.X + 5f + (float)num, Pos.Y + 9f);
				if (base.TextureObjectDictionary.ContainsKey(array[i].ToString()))
				{
					base.SpriteMain.Draw2D(base.TextureObjectDictionary[array[i].ToString()], 1f, 0f, pointF, Color.FromArgb(base.TransparentValue, base.ColorValue));
				}
				num += 11;
				if (array[i] == '.')
				{
					num -= 6;
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00034950 File Offset: 0x00032B50
		public override void Show()
		{
			this.DrawText(base.Name + " " + this.Description, base.OriginalPosition);
		}
	}
}
