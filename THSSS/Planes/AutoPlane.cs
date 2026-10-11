using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020000EF RID: 239
	public class AutoPlane : BaseMyPlane
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0003897A File Offset: 0x00036B7A
		public AutoPlane(StageDataPackage StageData, Point Position)
			: base(StageData, Position)
		{
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000389E0 File Offset: 0x00036BE0
		public override void Ctrl()
		{
			if (base.Time % this.CtrlCircle == 0)
			{
				this.KC = new KeyClass();
				this.KC.Key_Z = true;
				Vector2 vector = this.GenerateMove();
				double num = (double)(-(double)vector.X);
				double num2 = (double)(-(double)vector.Y);
				if (vector.Length() < 80f)
				{
					this.KC.Key_Shift = true;
				}
				if (num > 1.0)
				{
					this.KC.ArrowLeft = true;
				}
				else if (num < -1.0)
				{
					this.KC.ArrowRight = true;
				}
				if (num2 > 1.0)
				{
					this.KC.ArrowUp = true;
				}
				else if (num2 < -1.0)
				{
					this.KC.ArrowDown = true;
				}
			}
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (x.HitCheck(this, (float)x.Region) || base.DeadTime > base.Time)
				{
					this.KC.Key_X = true;
				}
			});
			base.StageData.GlobalData.KClass.Hex2Key(this.KC.Key2Hex());
			base.Ctrl();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00038DC8 File Offset: 0x00036FC8
		private double GenerateDanger(PointF OriginalPos)
		{
			double tmp = 0.0;
			double d;
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				Vector2 vector = new Vector2(OriginalPos.X, OriginalPos.Y);
				Vector2 vector2 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
				d = (double)((vector - vector2).Length() - (float)this.Region - (float)x.Region);
				if (d < 200.0)
				{
					tmp += 7000.0 / d / d;
					Vector2 vector3 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector2 += (float)this.GetDistance(OriginalPos) / 5f * vector3;
					d = (double)((vector - vector2).Length() - (float)this.Region - (float)x.Region);
					tmp += 3000.0 / d / d;
				}
			});
			base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.X > 0f && x.OriginalPosition.X < (float)this.BoundRect.Width && x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < (float)this.BoundRect.Height)
				{
					Vector2 vector4 = new Vector2(OriginalPos.X, OriginalPos.Y);
					Vector2 vector5 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
					d = (double)((vector4 - vector5).Length() - (float)this.Region - (float)x.Region);
					tmp += 10000.0 / d / d;
				}
			});
			if (base.Boss != null)
			{
				d = base.Boss.GetDistance(OriginalPos) - (double)base.Boss.Region - (double)base.Region;
				tmp += 10000.0 / d / d;
			}
			return tmp;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00038E94 File Offset: 0x00037094
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
			else if (x > (float)(base.BoundRect.Width - div * (count - 1)))
			{
				x = (float)(base.BoundRect.Width - div * (count - 1));
			}
			if (y < 0f)
			{
				y = 0f;
			}
			else if (y > (float)(base.BoundRect.Height - div * (count - 1)))
			{
				y = (float)(base.BoundRect.Height - div * (count - 1));
			}
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					double num3 = this.GenerateDanger(new PointF(x, y));
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
				vector = new Vector2(base.OriginalPosition.X, base.OriginalPosition.Y);
			}
			return vector;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00039424 File Offset: 0x00037624
		private Vector2 GenerateMove()
		{
			Vector2 forceSum = Vector2.Zero;
			float d;
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				Vector2 vector8 = new Vector2(this.OriginalPosition.X, this.OriginalPosition.Y);
				Vector2 vector9 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
				Vector2 vector10 = vector8 - vector9;
				d = vector10.Length() - (float)this.Region - (float)x.Region;
				if (d < 200f)
				{
					vector10.Normalize();
					vector10 *= 5000f / d / d;
					forceSum += vector10;
					Vector2 vector11 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector9 += 3f * vector11;
					vector10 = vector8 - vector9;
					d = vector10.Length() - (float)this.Region - (float)x.Region;
					vector10.Normalize();
					vector10 *= 5000f / d / d;
					forceSum += vector10;
					vector9 += 6f * vector11;
					vector10 = vector8 - vector9;
					d = vector10.Length() - (float)this.Region - (float)x.Region;
					vector10.Normalize();
					vector10 *= 2000f / d / d;
					forceSum += vector10;
				}
			});
			base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.X > 0f && x.OriginalPosition.X < (float)this.BoundRect.Width && x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < (float)this.BoundRect.Height)
				{
					Vector2 vector12 = new Vector2(this.OriginalPosition.X, this.OriginalPosition.Y);
					Vector2 vector13 = new Vector2(x.OriginalPosition.X, x.OriginalPosition.Y);
					Vector2 vector14 = vector12 - vector13;
					d = vector14.Length() - (float)this.Region - (float)x.Region;
					vector14.Normalize();
					vector14 *= 5000f / d / d;
					forceSum += vector14;
					Vector2 vector15 = x.Velocity * new Vector2((float)Math.Cos(x.Direction), (float)Math.Sin(x.Direction));
					vector13 += 5f * vector15;
					vector14 = vector12 - vector13;
					d = vector14.Length() - (float)this.Region - (float)x.Region;
					vector14.Normalize();
					vector14 *= 5000f / d / d;
					forceSum += vector14;
				}
			});
			if (base.Boss != null)
			{
				Vector2 vector = new Vector2(base.OriginalPosition.X, base.OriginalPosition.Y);
				Vector2 vector2 = new Vector2(base.Boss.OriginalPosition.X, base.Boss.OriginalPosition.Y);
				Vector2 vector3 = vector - vector2;
				d = vector3.Length() - (float)base.Region - (float)base.Boss.Region;
				vector3.Normalize();
				vector3 *= 10000f / d / d;
				forceSum += vector3;
			}
			Vector2 vector4 = this.GenerateDestPoint(40, 4, base.OriginalPosition.X, base.OriginalPosition.Y);
			vector4 = this.GenerateDestPoint(10, 4, vector4.X, vector4.Y);
			vector4 = this.GenerateDestPoint(2, 5, vector4.X, vector4.Y);
			Vector2 vector5 = new Vector2(base.DestPoint.X, base.DestPoint.Y);
			if (base.Time > 300)
			{
				vector4 = Vector2.Lerp(vector4, vector5, 0.5f);
			}
			Vector2 vector6 = new Vector2(base.OriginalPosition.X, base.OriginalPosition.Y);
			Vector2 vector7 = vector4 - vector6;
			forceSum += 1f * vector7;
			base.DestPoint = new PointF(vector4.X, vector4.Y);
			if (base.Boss != null)
			{
				forceSum += new Vector2(5f * (base.Boss.OriginalPosition.X - base.OriginalPosition.X), 0f);
			}
			else
			{
				forceSum += new Vector2(0.1f * ((float)(base.BoundRect.Width / 2) - base.OriginalPosition.X), 0.1f * ((float)base.BoundRect.Height - base.OriginalPosition.Y));
				forceSum += this.GenerateEnemyBeat();
			}
			forceSum += this.GenerateItemGet();
			return forceSum;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000397FC File Offset: 0x000379FC
		private Vector2 GenerateEnemyBeat()
		{
			Vector2 zero = Vector2.Zero;
			float tmp = 1000f;
			base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				if (x.OriginalPosition.Y > 0f && x.OriginalPosition.Y < 300f && x.HealthPoint < 20000f)
				{
					if (Math.Abs(x.OriginalPosition.X - this.OriginalPosition.X) < tmp)
					{
						tmp = x.OriginalPosition.X;
					}
				}
			});
			if (tmp < 1000f)
			{
				zero = new Vector2(10f * (tmp - base.OriginalPosition.X), 0f);
			}
			return zero;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000398B4 File Offset: 0x00037AB4
		private Vector2 GenerateItemGet()
		{
			Vector2 vector;
			if (base.BulletList.Count > 10)
			{
				vector = Vector2.Zero;
			}
			else if (base.Boss != null)
			{
				vector = Vector2.Zero;
			}
			else
			{
				int itemCount = 0;
				base.ItemList.ForEach(delegate(BaseItem x)
				{
					if (!x.Obtain)
					{
						itemCount++;
					}
				});
				if (itemCount > 5)
				{
					vector = new Vector2(0f, 2f * (110f - base.OriginalPosition.Y));
				}
				else
				{
					vector = Vector2.Zero;
				}
			}
			return vector;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00039960 File Offset: 0x00037B60
		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], 0.1f, 0f, new PointF(base.DestPoint.X + (float)base.BoundRect.Left, base.DestPoint.Y + (float)base.BoundRect.Top), Color.White);
			base.Show();
		}

		// Token: 0x04000231 RID: 561
		private int CtrlCircle = 1;

		// Token: 0x04000232 RID: 562
		private KeyClass KC = new KeyClass();
	}
}
