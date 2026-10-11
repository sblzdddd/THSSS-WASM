using System;
using System.Drawing;

namespace Shooting
{
	public class HP_Line : BaseObject
	{
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

		public int MaxHP { get; set; }

		public HP_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.OriginalPosition = OriginalPosition;
			this.MaxHP = 1;
			base.TransparentValueF = 0f;
		}

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

		private int hp = 0;
	}
}
