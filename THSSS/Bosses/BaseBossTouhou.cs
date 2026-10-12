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
			this.StageData = StageData;
			Boss = this;
			AngleWithDirection = false;
			Angle = 1.5707963267948966;
			SetBossLifeLineTex();
		}

		public void SetBossLifeLineTex()
		{
		}

		public void MoveToPoint(PointF destOriginalPosition)
		{
			var num = (float)GetDistance(destOriginalPosition);
			if (num > Math.Abs(Velocity))
			{
				Direction = GetDirection(destOriginalPosition);
				Accelerate = -Velocity * Velocity / (2f * num);
				if (Velocity < 0f)
				{
					Velocity = 0f;
				}
			}
			else
			{
				Accelerate = 0f;
				Velocity = 0f;
			}
		}

		public void MoveUpDown()
		{
			if (Velocity < 0.01f)
			{
				if (Time % 80 == 0)
				{
					var num = DestPoint.Y;
					num += 10f;
					Velocity = 0.5f;
					DestPoint = new PointF(DestPoint.X, num);
				}
				else if (Time % 80 == 40)
				{
					var num = DestPoint.Y;
					num -= 10f;
					Velocity = 0.5f;
					DestPoint = new PointF(DestPoint.X, num);
				}
			}
		}

		public void MoveUpDown(int circle)
		{
			if (Velocity < 0.01f)
			{
				if (Time % circle == 0)
				{
					var num = DestPoint.Y;
					num += 10f;
					Velocity = 0.5f;
					DestPoint = new PointF(DestPoint.X, num);
				}
				else if (Time % circle == circle / 2)
				{
					var num = DestPoint.Y;
					num -= 10f;
					Velocity = 0.5f;
					DestPoint = new PointF(DestPoint.X, num);
				}
			}
		}

		public void RandomMove(int T, float velocity)
		{
			if (Time % T == 0)
			{
				DestPoint = new Point(Ran.Next(100, BoundRect.Width - 100), Ran.Next(70, 200));
				Velocity = velocity;
			}
			MoveToPoint(DestPoint);
		}

		public void RandomMove(int T, float velocity, Rectangle RangeRect)
		{
			if (Time % T == 0)
			{
				DestPoint = new Point(Ran.Next(RangeRect.Left, RangeRect.Right), Ran.Next(RangeRect.Top, RangeRect.Bottom));
				Velocity = velocity;
			}
			MoveToPoint(DestPoint);
		}

		public void RandomMove(int T, int Dist, float Velocity, Rectangle RangeRect)
		{
			if (Time % T == 0)
			{
				for (var i = 0; i < 300; i++)
				{
					var num = (double)Ran.Next(360) * 3.141592653589793 / 180.0;
					var num2 = OriginalPosition.X + (float)((double)Dist * Math.Sin(num));
					var num3 = OriginalPosition.Y + (float)((double)Dist * Math.Cos(num));
					if (num2 > (float)RangeRect.Left && num2 < (float)RangeRect.Right && num3 > (float)RangeRect.Top && num3 < (float)RangeRect.Bottom)
					{
						DestPoint = new PointF(num2, num3);
						this.Velocity = Velocity;
						break;
					}
				}
			}
			MoveToPoint(DestPoint);
		}

		public void RandomMove1(int T, float velocity)
		{
			if (Time % T == 0)
			{
				if (OriginalPosition.X < (float)(BoundRect.Width / 2 - 50))
				{
					DestPoint = new PointF((float)Ran.Next(BoundRect.Width / 2 + 50, BoundRect.Width - 110), (float)Ran.Next(BoundRect.Top + 100, BoundRect.Top + 150));
				}
				else if (OriginalPosition.X > (float)(BoundRect.Width / 2 + 50))
				{
					DestPoint = new PointF((float)Ran.Next(110, BoundRect.Width / 2 - 50), (float)Ran.Next(BoundRect.Top + 100, BoundRect.Top + 150));
				}
				else if (MyPlane.OriginalPosition.X < (float)(BoundRect.Width / 2))
				{
					DestPoint = new PointF((float)Ran.Next(110, BoundRect.Width / 2 - 50), (float)Ran.Next(BoundRect.Top + 100, BoundRect.Top + 150));
				}
				else
				{
					DestPoint = new PointF((float)Ran.Next(BoundRect.Width / 2 + 50, BoundRect.Width - 110), (float)Ran.Next(BoundRect.Top + 100, BoundRect.Top + 150));
				}
				Velocity = velocity;
			}
			MoveToPoint(DestPoint);
		}

		public virtual void TextureCtrl()
		{
			var num = (Mirrored ? IndexY : (-IndexY));
			if (OnAction > 0)
			{
				IndexY = 2;
				IndexX += 0.125f;
				OnAction--;
			}
			else if (Vx < -0.05f)
			{
				Mirrored = true;
				IndexY = 1;
				if (Vx < -1f)
				{
					if (IndexX < 2f)
					{
						IndexX += 0.125f;
					}
					else
					{
						IndexX = (float)(2 + Time % 16 / 8);
					}
				}
				else if (Vx < -0.5f)
				{
					IndexX = 1f;
				}
				else
				{
					IndexX = 0f;
				}
			}
			else if (Vx > 0.05f)
			{
				Mirrored = false;
				IndexY = 1;
				if (Vx > 1f)
				{
					if (IndexX < 2f)
					{
						IndexX += 0.125f;
					}
					else
					{
						IndexX = (float)(2 + Time % 16 / 8);
					}
				}
				else if (Vx > 0.5f)
				{
					IndexX = 1f;
				}
				else
				{
					IndexX = 0f;
				}
			}
			else
			{
				Mirrored = false;
				IndexY = 0;
				IndexX = (float)(TimeMain % 32 / 8);
			}
			if (num != (Mirrored ? IndexY : (-IndexY)))
			{
				IndexX = 0f;
			}
			if (IndexX > 3f)
			{
				IndexX = 3f;
			}
			TxtureObject = TxtureObjects[IndexY, (int)IndexX];
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Shoot();
			TextureCtrl();
			if (OutBoundary())
			{
				Boss = null;
			}
		}

		public override bool OutBoundary()
		{
			return Time > LifeTime;
		}

		public override void GiveItems()
		{
			if (MyPlane.Power < 400)
			{
				for (var i = 0; i < 15; i++)
				{
					var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-49, 50), OriginalPosition.Y + (float)Ran.Next(-49, 50));
					new PowerItem_Touhou(StageData, pointF);
				}
				for (var i = 0; i < 15; i++)
				{
					var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-49, 50), OriginalPosition.Y + (float)Ran.Next(-49, 50));
					new ScoreItem_Touhou(StageData, pointF);
				}
			}
			else
			{
				for (var i = 0; i < 30; i++)
				{
					var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-49, 50), OriginalPosition.Y + (float)Ran.Next(-49, 50));
					new ScoreItem_Touhou(StageData, pointF);
				}
			}
		}

		protected virtual void LoadArmon(string fileName)
		{
			if (File.Exists(fileName))
			{
				var array = new char[] { ',' };
				var array2 = File.ReadAllLines(fileName, Encoding.Default);
				ArmonArray = new float[array2.Length];
				for (var i = 0; i < array2.Length; i++)
				{
					ArmonArray[i] = float.Parse(array2[i]);
				}
			}
		}
	}
}
