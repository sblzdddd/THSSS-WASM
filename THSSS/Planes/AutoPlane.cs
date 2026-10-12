using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class AutoPlane : BaseMyPlane
	{
		public AutoPlane(StageDataPackage StageData, Point Position)
			: base(StageData, Position)
		{
		}

		public override void Ctrl()
		{
			if (Time % CtrlCircle == 0)
			{
				KC = new KeyClass();
				KC.Key_Z = true;
				Vector2 vector = GenerateMove();
				double num = (double)(-(double)vector.X);
				double num2 = (double)(-(double)vector.Y);
				if (vector.Length() < 80f)
				{
					KC.Key_Shift = true;
				}
				if (num > 1.0)
				{
					KC.ArrowLeft = true;
				}
				else if (num < -1.0)
				{
					KC.ArrowRight = true;
				}
				if (num2 > 1.0)
				{
					KC.ArrowUp = true;
				}
				else if (num2 < -1.0)
				{
					KC.ArrowDown = true;
				}
			}
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (x.HitCheck(this, (float)x.Region) || DeadTime > Time)
				{
					KC.Key_X = true;
				}
			});
			StageData.GlobalData.KClass.Hex2Key(KC.Key2Hex());
			base.Ctrl();
		}

		private double GenerateDanger(PointF OriginalPos)
		{
			double tmp = 0.0;
			double d;
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				Vector2 vector = new Vector2(OriginalPos.X, OriginalPos.Y);
				Vector2 vector2 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
				d = (double)((vector - vector2).Length() - (float)Region - (float)x.Region);
				if (d < 200.0)
				{
					tmp += 7000.0 / d / d;
					Vector2 vector3 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector2 += (float)GetDistance(OriginalPos) / 5f * vector3;
					d = (double)((vector - vector2).Length() - (float)Region - (float)x.Region);
					tmp += 3000.0 / d / d;
				}
			});
			EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.X > 0f && x.OriginalPosition.X < (float)BoundRect.Width && x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < (float)BoundRect.Height)
				{
					Vector2 vector4 = new Vector2(OriginalPos.X, OriginalPos.Y);
					Vector2 vector5 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
					d = (double)((vector4 - vector5).Length() - (float)Region - (float)x.Region);
					tmp += 10000.0 / d / d;
				}
			});
			if (Boss != null)
			{
				d = Boss.GetDistance(OriginalPos) - (double)Boss.Region - (double)Region;
				tmp += 10000.0 / d / d;
			}
			return tmp;
		}

		private Vector2 GenerateDestPoint(int div, int count, float x, float y)
		{
			Vector2 zero = Vector2.Zero;
			double num = 1000000000.0;
			double num2 = 0.0;
			x -= (float)(div * (count - 1) / 2);
			y -= (float)(div * (count - 1) / 2);
			if (x < 0f)
			{
				x = 0f;
			}
			else if (x > (float)(BoundRect.Width - div * (count - 1)))
			{
				x = (float)(BoundRect.Width - div * (count - 1));
			}
			if (y < 0f)
			{
				y = 0f;
			}
			else if (y > (float)(BoundRect.Height - div * (count - 1)))
			{
				y = (float)(BoundRect.Height - div * (count - 1));
			}
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					double num3 = GenerateDanger(new PointF(x, y));
					if (num3 < num)
					{
						num = num3;
						zero = new Vector2(x, y);
					}
					if (num3 > num2)
					{
						num2 = num3;
					}
					x += (float)div;
				}
				y += (float)div;
				x -= (float)(div * count);
			}
			Vector2 vector;
			if (num2 > 2.0)
			{
				vector = zero;
			}
			else
			{
				vector = new Vector2(OriginalPosition.X, OriginalPosition.Y);
			}
			return vector;
		}

		private Vector2 GenerateMove()
		{
			Vector2 forceSum = Vector2.Zero;
			float d;
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				Vector2 vector8 = new Vector2(OriginalPosition.X, OriginalPosition.Y);
				Vector2 vector9 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
				Vector2 vector10 = vector8 - vector9;
				d = vector10.Length() - (float)Region - (float)x.Region;
				if (d < 200f)
				{
					vector10.Normalize();
					vector10 *= 5000f / d / d;
					forceSum += vector10;
					Vector2 vector11 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector9 += 3f * vector11;
					vector10 = vector8 - vector9;
					d = vector10.Length() - (float)Region - (float)x.Region;
					vector10.Normalize();
					vector10 *= 5000f / d / d;
					forceSum += vector10;
					vector9 += 6f * vector11;
					vector10 = vector8 - vector9;
					d = vector10.Length() - (float)Region - (float)x.Region;
					vector10.Normalize();
					vector10 *= 2000f / d / d;
					forceSum += vector10;
				}
			});
			EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.X > 0f && x.OriginalPosition.X < (float)BoundRect.Width && x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < (float)BoundRect.Height)
				{
					Vector2 vector12 = new Vector2(OriginalPosition.X, OriginalPosition.Y);
					Vector2 vector13 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
					Vector2 vector14 = vector12 - vector13;
					d = vector14.Length() - (float)Region - (float)x.Region;
					vector14.Normalize();
					vector14 *= 5000f / d / d;
					forceSum += vector14;
					Vector2 vector15 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector13 += 5f * vector15;
					vector14 = vector12 - vector13;
					d = vector14.Length() - (float)Region - (float)x.Region;
					vector14.Normalize();
					vector14 *= 5000f / d / d;
					forceSum += vector14;
				}
			});
			if (Boss != null)
			{
				Vector2 vector = new Vector2(OriginalPosition.X, OriginalPosition.Y);
				Vector2 vector2 = new Vector2(Boss.OriginalPosition.X, Boss.OriginalPosition.Y);
				Vector2 vector3 = vector - vector2;
				d = vector3.Length() - (float)Region - (float)Boss.Region;
				vector3.Normalize();
				vector3 *= 10000f / d / d;
				forceSum += vector3;
			}
			Vector2 vector4 = GenerateDestPoint(40, 4, OriginalPosition.X, OriginalPosition.Y);
			vector4 = GenerateDestPoint(10, 4, vector4.X, vector4.Y);
			vector4 = GenerateDestPoint(2, 5, vector4.X, vector4.Y);
			Vector2 vector5 = new Vector2(DestPoint.X, DestPoint.Y);
			if (Time > 300)
			{
				vector4 = Vector2.Lerp(vector4, vector5, 0.5f);
			}
			Vector2 vector6 = new Vector2(OriginalPosition.X, OriginalPosition.Y);
			Vector2 vector7 = vector4 - vector6;
			forceSum += 1f * vector7;
			DestPoint = new PointF(vector4.X, vector4.Y);
			if (Boss != null)
			{
				forceSum += new Vector2(5f * (Boss.OriginalPosition.X - OriginalPosition.X), 0f);
			}
			else
			{
				forceSum += new Vector2(0.1f * ((float)(BoundRect.Width / 2) - OriginalPosition.X), 0.1f * ((float)BoundRect.Height - OriginalPosition.Y));
				forceSum += GenerateEnemyBeat();
			}
			forceSum += GenerateItemGet();
			return forceSum;
		}

		private Vector2 GenerateEnemyBeat()
		{
			Vector2 zero = Vector2.Zero;
			float tmp = 1000f;
			EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < 300f && x.HealthPoint < 20000f)
				{
					if (Math.Abs(x.OriginalPosition.X - OriginalPosition.X) < tmp)
					{
						tmp = x.OriginalPosition.X;
					}
				}
			});
			if (tmp < 1000f)
			{
				zero = new Vector2(10f * (tmp - OriginalPosition.X), 0f);
			}
			return zero;
		}

		private Vector2 GenerateItemGet()
		{
			Vector2 vector;
			if (BulletList.Count > 10)
			{
				vector = Vector2.Zero;
			}
			else if (Boss != null)
			{
				vector = Vector2.Zero;
			}
			else
			{
				int itemCount = 0;
				ItemList.ForEach(delegate(BaseItem x)
				{
					if (!x.Obtain)
					{
						itemCount++;
					}
				});
				if (itemCount > 5)
				{
					vector = new Vector2(0f, 2f * (110f - OriginalPosition.Y));
				}
				else
				{
					vector = Vector2.Zero;
				}
			}
			return vector;
		}

		public override void Show()
		{
			SpriteMain.Draw2D(TextureObjectDictionary["Region"], 0.1f, 0f, new PointF(DestPoint.X + (float)BoundRect.Left, DestPoint.Y + (float)BoundRect.Top), Color.White);
			base.Show();
		}

		private int CtrlCircle = 1;

		private KeyClass KC = new KeyClass();
	}
}
