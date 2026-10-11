using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001C5 RID: 453
	public class SpellClock : BaseObject
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x000652C8 File Offset: 0x000634C8
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x000652E0 File Offset: 0x000634E0
		public int TimeRemaining
		{
			get
			{
				return this.timeRemaining;
			}
			set
			{
				if (value < 0)
				{
					this.timeRemaining = 0;
				}
				else if (value > 5940)
				{
					this.timeRemaining = 5940;
				}
				else
				{
					this.timeRemaining = value;
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0006532C File Offset: 0x0006352C
		public SpellClock(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Left + 25), 40f);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00065370 File Offset: 0x00063570
		public override void Ctrl()
		{
			if (base.Boss != null)
			{
				base.Ctrl();
				this.TimeRemaining = base.Boss.SpellTime - base.Boss.Time;
				if (base.Boss.Life > 0)
				{
					if (this.TimeRemaining <= 300)
					{
						base.ColorValue = Color.FromArgb(255, 100, 100);
						if (0 < this.TimeRemaining)
						{
							if (this.TimeRemaining % 60 == 0)
							{
								if (this.TimeRemaining <= 120)
								{
									base.StageData.SoundPlay("se_timeout2.wav");
								}
								else
								{
									base.StageData.SoundPlay("se_timeout.wav");
								}
							}
						}
					}
					else if (this.TimeRemaining <= 600)
					{
						base.ColorValue = Color.FromArgb(255, 200, 200);
					}
					else
					{
						base.ColorValue = Color.White;
					}
				}
				if (base.MyPlane.OriginalPosition.Y < 30f && base.MyPlane.OriginalPosition.X < base.OriginalPosition.X + 60f && base.MyPlane.OriginalPosition.X > base.OriginalPosition.X - 60f)
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
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0006555C File Offset: 0x0006375C
		public override void Show()
		{
			if (base.Boss != null)
			{
				if (base.Boss.Life > 0)
				{
					base.SpriteMain.Draw2D(base.TextureObjectDictionary["球球"], 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
					string text = "tsnum";
					int num = 3;
					int num2 = 9;
					int num3 = 0;
					char[] array = ((float)this.TimeRemaining / 60f).ToString("F1").ToCharArray();
					for (int i = array.Length - 1; i >= 0; i--)
					{
						if (array[i] == '.')
						{
							num2 = 10;
							text = "tnum";
							num3 = 4;
						}
						PointF pointF = new PointF(base.OriginalPosition.X + 18f - (float)((22 - num2) * (array.Length - 1 - i)) + (float)num3, base.OriginalPosition.Y + (float)num);
						base.SpriteMain.Draw2D(base.TextureObjectDictionary[text + array[i].ToString()], base.Scale, 0f, pointF, Color.FromArgb(base.TransparentValue, base.ColorValue));
						if (array[i] == '.')
						{
							num = 0;
							num2 = 3;
							num3 = 20;
						}
					}
				}
			}
		}

		// Token: 0x040003E2 RID: 994
		private int timeRemaining;
	}
}
