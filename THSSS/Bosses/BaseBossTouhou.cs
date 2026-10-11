using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Shooting
{
	// Token: 0x02000031 RID: 49
	public class BaseBossTouhou : BaseBoss
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000BE14 File Offset: 0x0000A014
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000BE2B File Offset: 0x0000A02B
		public TextureObject[,] TxtureObjects { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000BE34 File Offset: 0x0000A034
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000BE4B File Offset: 0x0000A04B
		public float IndexX { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000BE54 File Offset: 0x0000A054
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000BE6B File Offset: 0x0000A06B
		public int IndexY { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000BE74 File Offset: 0x0000A074
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000BE8B File Offset: 0x0000A08B
		public int OnAction { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000BE94 File Offset: 0x0000A094
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000BEAB File Offset: 0x0000A0AB
		public float[] ArmonArray { get; set; }

		// Token: 0x0600020D RID: 525 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public BaseBossTouhou()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BEBF File Offset: 0x0000A0BF
		public BaseBossTouhou(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Boss = this;
			base.AngleWithDirection = false;
			base.Angle = 1.5707963267948966;
			this.SetBossLifeLineTex();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BEF9 File Offset: 0x0000A0F9
		public void SetBossLifeLineTex()
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		public void MoveToPoint(PointF destOriginalPosition)
		{
			float num = (float)base.GetDistance(destOriginalPosition);
			if (num > Math.Abs(base.Velocity))
			{
				base.Direction = base.GetDirection(destOriginalPosition);
				base.Accelerate = -base.Velocity * base.Velocity / (2f * num);
				if (base.Velocity < 0f)
				{
					base.Velocity = 0f;
				}
			}
			else
			{
				base.Accelerate = 0f;
				base.Velocity = 0f;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000BF94 File Offset: 0x0000A194
		public void MoveUpDown()
		{
			if (base.Velocity < 0.01f)
			{
				if (base.Time % 80 == 0)
				{
					float num = base.DestPoint.Y;
					num += 10f;
					base.Velocity = 0.5f;
					base.DestPoint = new PointF(base.DestPoint.X, num);
				}
				else if (base.Time % 80 == 40)
				{
					float num = base.DestPoint.Y;
					num -= 10f;
					base.Velocity = 0.5f;
					base.DestPoint = new PointF(base.DestPoint.X, num);
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C064 File Offset: 0x0000A264
		public void MoveUpDown(int circle)
		{
			if (base.Velocity < 0.01f)
			{
				if (base.Time % circle == 0)
				{
					float num = base.DestPoint.Y;
					num += 10f;
					base.Velocity = 0.5f;
					base.DestPoint = new PointF(base.DestPoint.X, num);
				}
				else if (base.Time % circle == circle / 2)
				{
					float num = base.DestPoint.Y;
					num -= 10f;
					base.Velocity = 0.5f;
					base.DestPoint = new PointF(base.DestPoint.X, num);
				}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C134 File Offset: 0x0000A334
		public void RandomMove(int T, float velocity)
		{
			if (base.Time % T == 0)
			{
				base.DestPoint = new Point(base.Ran.Next(100, base.BoundRect.Width - 100), base.Ran.Next(70, 200));
				base.Velocity = velocity;
			}
			this.MoveToPoint(base.DestPoint);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public void RandomMove(int T, float velocity, Rectangle RangeRect)
		{
			if (base.Time % T == 0)
			{
				base.DestPoint = new Point(base.Ran.Next(RangeRect.Left, RangeRect.Right), base.Ran.Next(RangeRect.Top, RangeRect.Bottom));
				base.Velocity = velocity;
			}
			this.MoveToPoint(base.DestPoint);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C228 File Offset: 0x0000A428
		public void RandomMove(int T, int Dist, float Velocity, Rectangle RangeRect)
		{
			if (base.Time % T == 0)
			{
				for (int i = 0; i < 300; i++)
				{
					double num = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
					float num2 = base.OriginalPosition.X + (float)((double)Dist * Math.Sin(num));
					float num3 = base.OriginalPosition.Y + (float)((double)Dist * Math.Cos(num));
					if (num2 > (float)RangeRect.Left && num2 < (float)RangeRect.Right && num3 > (float)RangeRect.Top && num3 < (float)RangeRect.Bottom)
					{
						base.DestPoint = new PointF(num2, num3);
						base.Velocity = Velocity;
						break;
					}
				}
			}
			this.MoveToPoint(base.DestPoint);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C328 File Offset: 0x0000A528
		public void RandomMove1(int T, float velocity)
		{
			if (base.Time % T == 0)
			{
				if (base.OriginalPosition.X < (float)(base.BoundRect.Width / 2 - 50))
				{
					base.DestPoint = new PointF((float)base.Ran.Next(base.BoundRect.Width / 2 + 50, base.BoundRect.Width - 110), (float)base.Ran.Next(base.BoundRect.Top + 100, base.BoundRect.Top + 150));
				}
				else if (base.OriginalPosition.X > (float)(base.BoundRect.Width / 2 + 50))
				{
					base.DestPoint = new PointF((float)base.Ran.Next(110, base.BoundRect.Width / 2 - 50), (float)base.Ran.Next(base.BoundRect.Top + 100, base.BoundRect.Top + 150));
				}
				else if (base.MyPlane.OriginalPosition.X < (float)(base.BoundRect.Width / 2))
				{
					base.DestPoint = new PointF((float)base.Ran.Next(110, base.BoundRect.Width / 2 - 50), (float)base.Ran.Next(base.BoundRect.Top + 100, base.BoundRect.Top + 150));
				}
				else
				{
					base.DestPoint = new PointF((float)base.Ran.Next(base.BoundRect.Width / 2 + 50, base.BoundRect.Width - 110), (float)base.Ran.Next(base.BoundRect.Top + 100, base.BoundRect.Top + 150));
				}
				base.Velocity = velocity;
			}
			this.MoveToPoint(base.DestPoint);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C594 File Offset: 0x0000A794
		public virtual void TextureCtrl()
		{
			int num = (base.Mirrored ? this.IndexY : (-this.IndexY));
			if (this.OnAction > 0)
			{
				this.IndexY = 2;
				this.IndexX += 0.125f;
				this.OnAction--;
			}
			else if (base.Vx < -0.05f)
			{
				base.Mirrored = true;
				this.IndexY = 1;
				if (base.Vx < -1f)
				{
					if (this.IndexX < 2f)
					{
						this.IndexX += 0.125f;
					}
					else
					{
						this.IndexX = (float)(2 + base.Time % 16 / 8);
					}
				}
				else if (base.Vx < -0.5f)
				{
					this.IndexX = 1f;
				}
				else
				{
					this.IndexX = 0f;
				}
			}
			else if (base.Vx > 0.05f)
			{
				base.Mirrored = false;
				this.IndexY = 1;
				if (base.Vx > 1f)
				{
					if (this.IndexX < 2f)
					{
						this.IndexX += 0.125f;
					}
					else
					{
						this.IndexX = (float)(2 + base.Time % 16 / 8);
					}
				}
				else if (base.Vx > 0.5f)
				{
					this.IndexX = 1f;
				}
				else
				{
					this.IndexX = 0f;
				}
			}
			else
			{
				base.Mirrored = false;
				this.IndexY = 0;
				this.IndexX = (float)(base.TimeMain % 32 / 8);
			}
			if (num != (base.Mirrored ? this.IndexY : (-this.IndexY)))
			{
				this.IndexX = 0f;
			}
			if (this.IndexX > 3f)
			{
				this.IndexX = 3f;
			}
			base.TxtureObject = this.TxtureObjects[this.IndexY, (int)this.IndexX];
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C800 File Offset: 0x0000AA00
		public override void Ctrl()
		{
			base.Ctrl();
			this.Shoot();
			this.TextureCtrl();
			if (this.OutBoundary())
			{
				base.Boss = null;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C83C File Offset: 0x0000AA3C
		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public override void GiveItems()
		{
			if (base.MyPlane.Power < 400)
			{
				for (int i = 0; i < 15; i++)
				{
					PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-49, 50), base.OriginalPosition.Y + (float)base.Ran.Next(-49, 50));
					new PowerItem_Touhou(base.StageData, pointF);
				}
				for (int i = 0; i < 15; i++)
				{
					PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-49, 50), base.OriginalPosition.Y + (float)base.Ran.Next(-49, 50));
					new ScoreItem_Touhou(base.StageData, pointF);
				}
			}
			else
			{
				for (int i = 0; i < 30; i++)
				{
					PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-49, 50), base.OriginalPosition.Y + (float)base.Ran.Next(-49, 50));
					new ScoreItem_Touhou(base.StageData, pointF);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		protected virtual void LoadArmon(string fileName)
		{
			if (File.Exists(fileName))
			{
				char[] array = new char[] { ',' };
				string[] array2 = File.ReadAllLines(fileName, Encoding.Default);
				this.ArmonArray = new float[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					this.ArmonArray[i] = float.Parse(array2[i]);
				}
			}
		}
	}
}
