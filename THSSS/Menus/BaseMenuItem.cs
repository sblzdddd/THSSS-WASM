using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000005 RID: 5
	public class BaseMenuItem : BaseObject
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004BD4 File Offset: 0x00002DD4
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004BEB File Offset: 0x00002DEB
		public bool Selected { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004BF4 File Offset: 0x00002DF4
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004C0B File Offset: 0x00002E0B
		public string Name { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004C14 File Offset: 0x00002E14
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004C2B File Offset: 0x00002E2B
		public bool OnRemove { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004C34 File Offset: 0x00002E34
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004C4B File Offset: 0x00002E4B
		public int TwinkleTime { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004C54 File Offset: 0x00002E54
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004C6B File Offset: 0x00002E6B
		public int VibrateTime { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004C74 File Offset: 0x00002E74
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004C8B File Offset: 0x00002E8B
		public float OffsetX { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004C94 File Offset: 0x00002E94
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004CAB File Offset: 0x00002EAB
		public float OffsetY { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004CCB File Offset: 0x00002ECB
		public bool Vibrateable { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004CD4 File Offset: 0x00002ED4
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004CEB File Offset: 0x00002EEB
		public bool Twinkleable { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004CF4 File Offset: 0x00002EF4
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004D0B File Offset: 0x00002F0B
		public bool UnSelectVisible { get; set; }

		// Token: 0x060000D3 RID: 211 RVA: 0x00004D14 File Offset: 0x00002F14
		public BaseMenuItem()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004D20 File Offset: 0x00002F20
		public BaseMenuItem(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			this.Name = textureName;
			base.MaxVelocity = 10f;
			this.UnSelectVisible = true;
			this.Vibrateable = true;
			this.Twinkleable = true;
			this.Selected = false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004D88 File Offset: 0x00002F88
		public override void Move()
		{
			float num = (float)base.GetDistance(base.DestPoint);
			if (num > 0f)
			{
				base.Velocity = (float)Math.Sqrt((double)num) / 1f;
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Direction = base.GetDirection(base.DestPoint);
			base.Move();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.Selected)
			{
				double num = 1.0 + Math.Sin((double)((float)base.Time / 5f));
				if (!this.Twinkleable)
				{
					num = 2.0;
				}
				base.ColorValue = Color.FromArgb(220 + (int)(12.0 * num), 220 + (int)(12.0 * num), 220 + (int)(12.0 * num));
				base.TransparentValueF = 255f;
			}
			else if (!this.UnSelectVisible)
			{
				base.TransparentVelocity = -25f;
			}
			else
			{
				base.ColorValue = Color.FromArgb(150, 150, 150);
				base.TransparentValueF = 255f;
			}
			if (base.Time < this.VibrateTime)
			{
				this.OffsetX = (float)base.Ran.Next(-2, 3);
				this.OffsetY = (float)base.Ran.Next(-2, 3);
			}
			else
			{
				this.OffsetX = 0f;
				this.OffsetY = 0f;
			}
			if (base.Time < this.TwinkleTime)
			{
				if (base.Time % 4 == 0)
				{
					base.TransparentValueF = 255f;
				}
				else if (base.Time % 4 == 2)
				{
					base.TransparentValueF = 0f;
				}
			}
			if (this.OnRemove)
			{
				base.MaxTransparent -= 15;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004FBC File Offset: 0x000031BC
		public virtual void Select()
		{
			base.Time = 0;
			this.Selected = true;
			if (this.Vibrateable)
			{
				this.VibrateTime = base.Time + 10;
			}
			this.TwinkleTime = 0;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005001 File Offset: 0x00003201
		public virtual void Click()
		{
			base.Time = 0;
			this.VibrateTime = 0;
			this.TwinkleTime = base.Time + 30;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005024 File Offset: 0x00003224
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), (float)base.Angle, new PointF(base.Position.X - this.OffsetX, base.Position.Y - this.OffsetY), Color.FromArgb(base.TransparentValue, base.ColorValue));
		}
	}
}
