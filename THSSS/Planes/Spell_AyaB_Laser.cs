using System;
using System.Drawing;

namespace Shooting
{
	public class Spell_AyaB_Laser : BaseSpell
	{
		public Spell_AyaB_Laser(StageDataPackage StageData, PointF OriginalPosition, bool Left)
		{
			this.StageData = StageData;
			SpellList.Add(this);
			this.OriginalPosition = OriginalPosition;
			Damage = 50;
			Region = 25;
			LifeTime = 20;
			Velocity = 50f;
			ScaleLength = 2f;
			ScaleWidth = 1f;
			TxtureObject = TextureObjectDictionary["Laser"];
			if (Boss != null)
			{
				if (Boss.OriginalPosition.X > 0f && Boss.OriginalPosition.X < (float)BoundRect.Width && Boss.OriginalPosition.Y > 0f && Boss.OriginalPosition.Y < (float)BoundRect.Height)
				{
					Target = Boss;
				}
			}
			if (Target == null)
			{
				var dist = 500.0;
				EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					var distance = GetDistance(x);
					if (distance < dist)
					{
						Target = x;
						dist = distance;
					}
				});
			}
			if (Target == null)
			{
				Target = new BaseObject
				{
					OriginalPosition = new PointF(OriginalPosition.X + (float)Ran.Next(-80, 80), OriginalPosition.Y + (float)Ran.Next(-80, 80))
				};
				if (Target.OriginalPosition.Y < 0f)
				{
					Target.OriginalPosition = new PointF(Target.OriginalPosition.X, 0f);
				}
				else if (Target.OriginalPosition.Y > (float)BoundRect.Height)
				{
					Target.OriginalPosition = new PointF(Target.OriginalPosition.X, (float)BoundRect.Height);
				}
			}
			this.OriginalPosition = new PointF((float)(Left ? (-150) : (BoundRect.Width + 150)), (float)Ran.Next((int)Target.OriginalPosition.Y - 80, (int)Target.OriginalPosition.Y + 80));
			Direction = GetDirection(Target);
			SpellList[0].OriginalPosition = Target.OriginalPosition;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Target != null)
			{
				if (Target.HealthPoint <= 0f)
				{
					Target = null;
				}
				if (OutBoundary())
				{
					Target = null;
					MyPlane.Time = MyPlane.UnmatchedTime - 90;
				}
			}
		}

		public override bool HitCheck(BaseObject Target)
		{
			var distance = GetDistance(Target);
			var direction = GetDirection(Target);
			var num = distance * Math.Cos((double)((float)(Direction - direction)));
			var num2 = distance * Math.Sin((double)((float)(Direction - direction)));
			var num3 = (double)((float)Region * ScaleWidth);
			var num4 = (double)((float)TxtureObject.Height * ScaleLength * 0.45f);
			return num2 * num2 / num3 / num3 + (num - (double)((float)TxtureObject.Height * ScaleLength / 2f)) * (num - (double)((float)TxtureObject.Height * ScaleLength / 2f)) / num4 / num4 < 1.0;
		}

		public override void Show()
		{
			var sizeF = new SizeF((float)TxtureObject.Width * ScaleWidth, (float)TxtureObject.Height * ScaleLength);
			SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF((float)(TxtureObject.Width / 2), 0f), (float)(Direction - 1.5707963267948966 + Angle), Position, Color.FromArgb(TransparentValue, ColorValue));
		}

		public override void ShowRegion()
		{
			var originalPosition = OriginalPosition;
			OriginalPosition = new PointF(OriginalPosition.X + (float)TxtureObject.Height * ScaleLength * 0.5f * (float)Math.Cos(Direction), OriginalPosition.Y + (float)TxtureObject.Height * ScaleLength * 0.5f * (float)Math.Sin(Direction));
			SpriteMain.Draw2D(TextureObjectDictionary["Region"], 2f * (float)Region / (float)TextureObjectDictionary["Region"].Width * ScaleWidth, (float)TxtureObject.Height * 0.9f / (float)TextureObjectDictionary["Region"].Height * ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), Position, Color.White, Mirrored);
			OriginalPosition = originalPosition;
		}

		private BaseObject Target;
	}
}
