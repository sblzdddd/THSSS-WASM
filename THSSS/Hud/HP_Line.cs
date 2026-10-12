using System.Drawing;

namespace Shooting
{
	public class HP_Line : BaseObject
	{
		public int HP
		{
			get
			{
				return hp;
			}
			set
			{
				if (value < 0)
				{
					hp = 0;
				}
				else if (value > MaxHP)
				{
					hp = MaxHP;
				}
				else
				{
					hp = value;
				}
			}
		}

		public int MaxHP { get; set; }

		public HP_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
		{
			this.StageData = StageData;
			TxtureObject = TextureObjectDictionary[textureName];
			this.OriginalPosition = OriginalPosition;
			MaxHP = 1;
			TransparentValueF = 0f;
		}

		public override void Ctrl()
		{
			if (Boss == null)
			{
				TransparentValueF -= 25f;
			}
			else
			{
				base.Ctrl();
				MaxHP = Boss.MaxHP;
				if (HP < (int)Boss.HealthPoint)
				{
					HP += 300;
				}
				else
				{
					HP = (int)Boss.HealthPoint;
				}
				if (MyPlane.OriginalPosition.Y < 30f)
				{
					TransparentValueF -= 10f;
					if (TransparentValueF < 50f)
					{
						TransparentValueF = 50f;
					}
				}
				else
				{
					TransparentValueF += 10f;
				}
			}
		}

		public override void Show()
		{
			var num = TxtureObject.Width * HP / MaxHP;
			num = ((num > 1) ? num : 1);
			var size = new Size(num, TxtureObject.Height);
			var rectangle = new Rectangle(new Point(0, 0), size);
			SpriteMain.Draw2D(TxtureObject.TXTure, rectangle, size, TxtureObject.RotatingCenter, 0f, OriginalPosition, Color.FromArgb(TransparentValue * 10 / 10, Color.White));
			if (Boss != null)
			{
				if (!Boss.OnSpell && Boss.SpellcardHP != 0)
				{
					var point = new Point((int)OriginalPosition.X - TxtureObject.Width / 2 + TxtureObject.Width * Boss.SpellcardHP / MaxHP, (int)OriginalPosition.Y);
					SpriteMain.Draw2D(TextureObjectDictionary["HP_Flag"], 1f, 0f, point, Color.FromArgb(TransparentValue, Color.White));
				}
			}
			SpriteMain.Draw2D(TextureObjectDictionary["HP_Line2"], 1f, 0f, OriginalPosition, Color.FromArgb(TransparentValue, Color.White));
		}

		private int hp = 0;
	}
}
