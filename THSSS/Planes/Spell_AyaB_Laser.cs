using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001A3 RID: 419
	public class Spell_AyaB_Laser : BaseSpell
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x0005E164 File Offset: 0x0005C364
		public Spell_AyaB_Laser(StageDataPackage StageData, PointF OriginalPosition, bool Left)
		{
			base.StageData = StageData;
			base.SpellList.Add(this);
			base.OriginalPosition = OriginalPosition;
			base.Damage = 50;
			base.Region = 25;
			base.LifeTime = 20;
			base.Velocity = 50f;
			base.ScaleLength = 2f;
			base.ScaleWidth = 1f;
			base.TxtureObject = base.TextureObjectDictionary["Laser"];
			if (base.Boss != null)
			{
				if (base.Boss.OriginalPosition.X > 0f && base.Boss.OriginalPosition.X < (float)base.BoundRect.Width && base.Boss.OriginalPosition.Y > 0f && base.Boss.OriginalPosition.Y < (float)base.BoundRect.Height)
				{
					this.Target = base.Boss;
				}
			}
			if (this.Target == null)
			{
				double dist = 500.0;
				base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					double distance = this.GetDistance(x);
					if (distance < dist)
					{
						this.Target = x;
						dist = distance;
					}
				});
			}
			if (this.Target == null)
			{
				this.Target = new BaseObject
				{
					OriginalPosition = new PointF(OriginalPosition.X + (float)base.Ran.Next(-80, 80), OriginalPosition.Y + (float)base.Ran.Next(-80, 80))
				};
				if (this.Target.OriginalPosition.Y < 0f)
				{
					this.Target.OriginalPosition = new PointF(this.Target.OriginalPosition.X, 0f);
				}
				else if (this.Target.OriginalPosition.Y > (float)base.BoundRect.Height)
				{
					this.Target.OriginalPosition = new PointF(this.Target.OriginalPosition.X, (float)base.BoundRect.Height);
				}
			}
			base.OriginalPosition = new PointF((float)(Left ? (-150) : (base.BoundRect.Width + 150)), (float)base.Ran.Next((int)this.Target.OriginalPosition.Y - 80, (int)this.Target.OriginalPosition.Y + 80));
			base.Direction = base.GetDirection(this.Target);
			base.SpellList[0].OriginalPosition = this.Target.OriginalPosition;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0005E480 File Offset: 0x0005C680
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.Target != null)
			{
				if (this.Target.HealthPoint <= 0f)
				{
					this.Target = null;
				}
				if (this.OutBoundary())
				{
					this.Target = null;
					base.MyPlane.Time = base.MyPlane.UnmatchedTime - 90;
				}
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0005E4F4 File Offset: 0x0005C6F4
		public override bool HitCheck(BaseObject Target)
		{
			double distance = base.GetDistance(Target);
			double direction = base.GetDirection(Target);
			double num = distance * Math.Cos((double)((float)(base.Direction - direction)));
			double num2 = distance * Math.Sin((double)((float)(base.Direction - direction)));
			double num3 = (double)((float)base.Region * base.ScaleWidth);
			double num4 = (double)((float)base.TxtureObject.Height * base.ScaleLength * 0.45f);
			return num2 * num2 / num3 / num3 + (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) * (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) / num4 / num4 < 1.0;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0005E5D0 File Offset: 0x0005C7D0
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF((float)(base.TxtureObject.Width / 2), 0f), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0005E678 File Offset: 0x0005C878
		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			base.OriginalPosition = new PointF(base.OriginalPosition.X + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Cos(base.Direction), base.OriginalPosition.Y + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Sin(base.Direction));
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], 2f * (float)base.Region / (float)base.TextureObjectDictionary["Region"].Width * base.ScaleWidth, (float)base.TxtureObject.Height * 0.9f / (float)base.TextureObjectDictionary["Region"].Height * base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.White, base.Mirrored);
			base.OriginalPosition = originalPosition;
		}

		// Token: 0x040003B6 RID: 950
		private BaseObject Target;
	}
}
