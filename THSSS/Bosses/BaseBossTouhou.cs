using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Shooting
{
	public class BaseBossTouhou : BaseBoss
	{
		public TextureObject[,] TxtureObjects { get; set; }

		public float IndexX { get; set; }

		public int IndexY { get; set; }

		public int OnAction { get; set; }

		public float[] ArmonArray { get; set; }

		public BaseBossTouhou()
		{
		}

		public BaseBossTouhou(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Boss = this;
			base.AngleWithDirection = false;
			base.Angle = 1.5707963267948966;
			this.SetBossLifeLineTex();
		}

		public void SetBossLifeLineTex()
		{
		}

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

		public void RandomMove(int T, float velocity)
		{
			if (base.Time % T == 0)
			{
				base.DestPoint = new Point(base.Ran.Next(100, base.BoundRect.Width - 100), base.Ran.Next(70, 200));
				base.Velocity = velocity;
			}
			this.MoveToPoint(base.DestPoint);
		}

		public void RandomMove(int T, float velocity, Rectangle RangeRect)
		{
			if (base.Time % T == 0)
			{
				base.DestPoint = new Point(base.Ran.Next(RangeRect.Left, RangeRect.Right), base.Ran.Next(RangeRect.Top, RangeRect.Bottom));
				base.Velocity = velocity;
			}
			this.MoveToPoint(base.DestPoint);
		}

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

		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime;
		}

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
