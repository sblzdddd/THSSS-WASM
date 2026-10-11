using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000A3 RID: 163
	public class Bullet_StraightLaser : BaseBullet_Touhou
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000262D0 File Offset: 0x000244D0
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x000262E7 File Offset: 0x000244E7
		public bool LaserHead { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000262F0 File Offset: 0x000244F0
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00026307 File Offset: 0x00024507
		private int HeadTransparent { get; set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x00026310 File Offset: 0x00024510
		public Bullet_StraightLaser(StageDataPackage StageData)
			: base(StageData)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0002631C File Offset: 0x0002451C
		public Bullet_StraightLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length)
			: this(StageData, OriginalPosition, Velocity, Direction, ColorType, Length, 16)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00026334 File Offset: 0x00024534
		public Bullet_StraightLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length, int Width)
			: this(StageData, "Laser0_" + ColorType, OriginalPosition, Velocity, Direction, Length)
		{
			base.ColorType = ColorType;
			base.Active = true;
			base.Angle = 1.5707963267948966;
			base.ScaleLength = (float)Width / (float)base.TxtureObject.Height;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002639A File Offset: 0x0002459A
		public Bullet_StraightLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.GhostingCount = Length;
			base.Region = 2;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000263C0 File Offset: 0x000245C0
		public override void Ctrl()
		{
			base.Ctrl();
			base.ScaleWidth = (float)base.GetDistance(base.LastPoints[base.LastPoints.Length - 1]) / (float)base.TxtureObject.Width;
			if (base.Time == 1)
			{
				ParticleBeginBullet particleBeginBullet = new ParticleBeginBullet(base.StageData, base.Position, base.ColorType);
				particleBeginBullet.Scale = 0.5f;
				particleBeginBullet.Active = true;
				particleBeginBullet.LifeTime = base.GhostingCount;
			}
			if (base.Time % 8 == 0)
			{
				base.Grazed = false;
			}
			if (this.LaserHead)
			{
				this.HeadTransparent = base.Ran.Next(150, 255);
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000264A0 File Offset: 0x000246A0
		public override bool OutBoundary()
		{
			bool flag;
			if (base.LifeTime != 0 && base.Time > base.LifeTime)
			{
				flag = true;
			}
			else if (base.Reflect <= 0)
			{
				if (base.Time < 100)
				{
					flag = false;
				}
				else
				{
					int num;
					if (base.Region < 10)
					{
						num = 10;
					}
					else
					{
						num = base.Region;
					}
					flag = ((float)(-(float)num) >= base.LastPoints[base.LastPoints.Length - 1].X || base.LastPoints[base.LastPoints.Length - 1].X >= (float)(base.BoundRect.Width + num) || (float)(-(float)num) >= base.LastPoints[base.LastPoints.Length - 1].Y || base.LastPoints[base.LastPoints.Length - 1].Y >= (float)(base.BoundRect.Height + num)) && ((float)(-(float)num) >= base.OriginalPosition.X || base.OriginalPosition.X >= (float)(base.BoundRect.Width + num) || (float)(-(float)num) >= base.OriginalPosition.Y || base.OriginalPosition.Y >= (float)(base.BoundRect.Height + num));
				}
			}
			else
			{
				if (base.OriginalPosition.X < 0f || base.OriginalPosition.X >= (float)base.BoundRect.Width)
				{
					base.Reflect -= 1;
					Bullet_StraightLaser bullet_StraightLaser = new Bullet_StraightLaser(base.StageData, new PointF(0f, 0f), 0f, 0.0, 0, 0);
					bullet_StraightLaser.Copy(this);
					bullet_StraightLaser.Direction -= 2.0 * (base.Direction - 1.5707963267948966);
				}
				if (base.OriginalPosition.Y < 20f)
				{
					base.Reflect -= 1;
					Bullet_StraightLaser bullet_StraightLaser = new Bullet_StraightLaser(base.StageData, new PointF(0f, 0f), 0f, 0.0, 0, 0);
					bullet_StraightLaser.Copy(this);
					bullet_StraightLaser.Direction = -base.Direction;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002677C File Offset: 0x0002497C
		public override bool HitCheck(BaseObject Target, float Radius)
		{
			for (int i = 3; i < base.LastPoints.Length - 3; i++)
			{
				PointF pointF = base.LastPoints[i];
				double num = Math.Sqrt((double)((pointF.X - Target.OriginalPosition.X) * (pointF.X - Target.OriginalPosition.X) + (pointF.Y - Target.OriginalPosition.Y) * (pointF.Y - Target.OriginalPosition.Y)));
				if (num - (double)((float)Target.Region * Target.Scale) < (double)(Radius * base.ScaleLength))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00026854 File Offset: 0x00024A54
		public override void GiveEndEffect()
		{
			for (int i = 0; i < base.LastPoints.Length; i++)
			{
				if (i % 8 == 0)
				{
					new ParticleEndBullet(base.StageData, base.LastPoints[i], base.ColorType, base.SizeValue);
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000268B4 File Offset: 0x00024AB4
		public override void GiveItems()
		{
			int num = 5;
			int num2 = base.Region / num;
			for (int i = 0; i < base.LastPoints.Length; i += 8)
			{
				for (int j = 0; j <= num2; j++)
				{
					double num3 = base.Direction;
					for (int k = 0; k < j * 5 + 1; k++)
					{
						PointF pointF = new PointF((float)((double)base.LastPoints[i].X + (double)j * Math.Cos(num3)), (float)((double)base.LastPoints[i].Y + (double)j * Math.Sin(num3)));
						new StarItem_Touhou(base.StageData, pointF);
						num3 += 6.283185307179586 / (double)(j * 6);
					}
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00026994 File Offset: 0x00024B94
		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				PointF pointF = new PointF((base.OriginalPosition.X + base.LastPoints[base.LastPoints.Length - 1].X) / 2f, (base.OriginalPosition.Y + base.LastPoints[base.LastPoints.Length - 1].Y) / 2f);
				pointF = new PointF(pointF.X + (float)base.BoundRect.Left, pointF.Y + (float)base.BoundRect.Top);
				base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), pointF, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
				if (this.LaserHead)
				{
					TextureObject textureObject = base.TextureObjectDictionary["光点"];
					if (0 < base.ColorType && base.ColorType <= 2)
					{
						textureObject = base.TextureObjectDictionary["FY_Particle6_0"];
					}
					else if (2 < base.ColorType && base.ColorType <= 4)
					{
						textureObject = base.TextureObjectDictionary["FY_Particle4_0"];
					}
					else if (4 < base.ColorType && base.ColorType <= 8)
					{
						textureObject = base.TextureObjectDictionary["FY_Particle5_0"];
					}
					base.SpriteMain.Draw2D(textureObject, 0.4f, 0.4f, (float)(base.Direction - 1.5707963267948966 + base.Angle + (double)((float)base.Time / 10f)), base.Position, Color.FromArgb(this.HeadTransparent, base.ColorValue), base.Mirrored);
				}
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00026BAC File Offset: 0x00024DAC
		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			for (int i = 3; i < base.LastPoints.Length - 3; i++)
			{
				base.OriginalPosition = base.LastPoints[i];
				base.ShowRegion();
			}
			base.OriginalPosition = originalPosition;
		}
	}
}
