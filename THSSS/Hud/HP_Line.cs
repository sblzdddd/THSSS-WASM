using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000047 RID: 71
	public class HP_Line : BaseObject
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000FD28 File Offset: 0x0000DF28
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public int HP
		{
			get
			{
				return this.hp;
			}
			set
			{
				if (value < 0)
				{
					this.hp = 0;
				}
				else if (value > this.MaxHP)
				{
					this.hp = this.MaxHP;
				}
				else
				{
					this.hp = value;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000FD90 File Offset: 0x0000DF90
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000FDA7 File Offset: 0x0000DFA7
		public int MaxHP { get; set; }

		// Token: 0x06000274 RID: 628 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public HP_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.OriginalPosition = OriginalPosition;
			this.MaxHP = 1;
			base.TransparentValueF = 0f;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000FE04 File Offset: 0x0000E004
		public override void Ctrl()
		{
			if (base.Boss == null)
			{
				base.TransparentValueF -= 25f;
			}
			else
			{
				base.Ctrl();
				this.MaxHP = base.Boss.MaxHP;
				if (this.HP < (int)base.Boss.HealthPoint)
				{
					this.HP += 300;
				}
				else
				{
					this.HP = (int)base.Boss.HealthPoint;
				}
				if (base.MyPlane.OriginalPosition.Y < 30f)
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

		// Token: 0x06000276 RID: 630 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public override void Show()
		{
			int num = base.TxtureObject.Width * this.HP / this.MaxHP;
			num = ((num > 1) ? num : 1);
			Size size = new Size(num, base.TxtureObject.Height);
			Rectangle rectangle = new Rectangle(new Point(0, 0), size);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, rectangle, size, base.TxtureObject.RotatingCenter, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue * 10 / 10, Color.White));
			if (base.Boss != null)
			{
				if (!base.Boss.OnSpell && base.Boss.SpellcardHP != 0)
				{
					Point point = new Point((int)base.OriginalPosition.X - base.TxtureObject.Width / 2 + base.TxtureObject.Width * base.Boss.SpellcardHP / this.MaxHP, (int)base.OriginalPosition.Y);
					base.SpriteMain.Draw2D(base.TextureObjectDictionary["HP_Flag"], 1f, 0f, point, Color.FromArgb(base.TransparentValue, Color.White));
				}
			}
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["HP_Line2"], 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
		}

		// Token: 0x040000BF RID: 191
		private int hp = 0;
	}
}
