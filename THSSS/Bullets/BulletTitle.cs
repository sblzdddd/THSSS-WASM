using System.Drawing;

namespace Shooting
{
	internal class BulletTitle : MusicTitle
	{
		public BulletTitle(StageDataPackage StageData, string textureName)
			: this(StageData, textureName, new Point(0, 0))
		{
			base.DestPoint = new Point(base.BoundRect.Width - 10, 20);
		}

		public BulletTitle(StageDataPackage StageData, string textureName, Point DestPoint)
			: base(StageData, textureName, DestPoint)
		{
			base.Layer = 1;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 200)
			{
				if (base.Boss != null)
				{
					if (base.Boss.OnSpell && base.Boss.HealthPoint > 0f)
					{
						base.LifeTime++;
					}
				}
			}
			if (base.MyPlane.OriginalPosition.Y < base.OriginalPosition.Y + (float)base.TxtureObject.Height + 16f && base.MyPlane.OriginalPosition.X > base.OriginalPosition.X - (float)base.TxtureObject.Width && base.MyPlane.OriginalPosition.X < base.OriginalPosition.X)
			{
				base.TransparentValueF -= 10f;
				if (base.TransparentValueF < 50f)
				{
					base.TransparentValueF = 50f;
				}
			}
			else if (base.Boss != null && base.Time > 100)
			{
				if (base.Boss.OriginalPosition.Y < base.OriginalPosition.Y + (float)base.TxtureObject.Height + 16f && base.Boss.OriginalPosition.Y > 0f && base.Boss.OriginalPosition.X > base.OriginalPosition.X - (float)base.TxtureObject.Width && base.Boss.OriginalPosition.X < base.OriginalPosition.X)
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
			else
			{
				base.TransparentValueF += 10f;
			}
		}

		public void DrawText(string Text, Point Pos, float Scale)
		{
			int num = 0;
			char[] array = Text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '.' || array[i] == ',')
				{
					num += (int)(8f * Scale);
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '.' || array[i] == ',')
				{
					num -= (int)(5f * Scale);
				}
				PointF pointF = new PointF((float)(Pos.X + 5 + num), (float)(Pos.Y + 9));
				base.SpriteMain.Draw2D(base.TextureObjectDictionary[array[i].ToString() ?? ""], Scale, 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
				num += (int)(14f * Scale);
				if (array[i] == '.' || array[i] == ',')
				{
					num -= (int)(3f * Scale);
				}
			}
		}

		public override void Show()
		{
			TextureObject textureObject = base.StageData.TextureObjectDictionary["tiaotiao2"];
			base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, new SizeF((float)textureObject.Width * base.Scale, (float)textureObject.Height * base.Scale), textureObject.RightTop, 0f, new PointF(base.Position.X, base.Position.Y - 16f), Color.FromArgb(base.TransparentValue, Color.White));
			base.Show();
			if (base.StageData.SCBstring != null)
			{
				this.DrawText(base.StageData.SCBstring, new Point(240, 68), 0.6f);
			}
		}
	}
}
