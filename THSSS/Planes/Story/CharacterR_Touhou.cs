using System.Drawing;

namespace Shooting
{
	internal class CharacterR_Touhou : BaseStoryItem
	{
		public TextureObject TxtureObject2 { get; set; }

		public CharacterR_Touhou(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width - 70), 128f);
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Active = false;
			base.TransparentValueF = 0f;
		}

		public override void Move()
		{
			if (base.Active)
			{
				if (base.OriginalPosition.X > (float)(base.BoundRect.Width - 100))
				{
					base.OriginalPosition = new PointF(base.OriginalPosition.X - 2f, base.OriginalPosition.Y);
				}
			}
			else if (base.OriginalPosition.X < (float)(base.BoundRect.Width - 70))
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X + 2f, base.OriginalPosition.Y);
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
			if (base.Active)
			{
				base.TransparentValueF += 10f;
				base.ColorValue = Color.White;
			}
			else
			{
				base.ColorValue = Color.Gray;
			}
		}

		public override void Show()
		{
			if (this.TxtureObject2 != null)
			{
				base.SpriteMain.Draw2D(this.TxtureObject2, base.ScaleWidth, base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
			}
			base.Show();
		}
	}
}
