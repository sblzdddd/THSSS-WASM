using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class Execution
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x0004417C File Offset: 0x0004237C
		public void Update(BaseEmitter_CS objects)
		{
			switch (this.changevalue)
			{
			case 0:
			{
				float x = objects.CS_Position.X;
				objects.CS_Position = new PointF(this.ChangeValue(x), objects.CS_Position.Y);
				break;
			}
			case 1:
			{
				float y = objects.CS_Position.Y;
				objects.CS_Position = new PointF(objects.CS_Position.X, this.ChangeValue(y));
				break;
			}
			case 2:
				objects.EmitRadius = this.ChangeValue(objects.EmitRadius);
				break;
			case 3:
				objects.RadiusDirection = this.ChangeValue(objects.RadiusDirection);
				break;
			case 4:
				objects.Way = this.ChangeValue(objects.Way);
				break;
			case 5:
				objects.Circle = this.ChangeValue(objects.Circle);
				break;
			case 6:
				objects.EmitDirection = this.ChangeValue(objects.EmitDirection);
				break;
			case 7:
				objects.Range = this.ChangeValue(objects.Range);
				break;
			case 8:
				objects.Velocity = this.ChangeValue(objects.Velocity);
				break;
			case 9:
				objects.DirectionDegree = this.ChangeValue(objects.DirectionDegree);
				break;
			case 10:
				objects.AccelerateCS = this.ChangeValue(objects.AccelerateCS);
				break;
			case 11:
				objects.AccDirection = this.ChangeValue(objects.AccDirection);
				break;
			case 12:
				objects.SubBullet.LifeTime = this.ChangeValue(objects.SubBullet.LifeTime);
				break;
			case 13:
				objects.SubBullet.Type = this.ChangeValue(objects.SubBullet.Type);
				break;
			case 14:
				objects.SubBullet.ScaleWidth = this.ChangeValue(objects.SubBullet.ScaleWidth);
				break;
			case 15:
				objects.SubBullet.ScaleLength = this.ChangeValue(objects.SubBullet.ScaleLength);
				break;
			case 16:
			{
				int num = (int)objects.SubBullet.ColorValue.R;
				num = this.ChangeValue(num);
				num = ((num < 0) ? 0 : num);
				num = ((num > 255) ? 255 : num);
				objects.SubBullet.ColorValue = Color.FromArgb(num, (int)objects.SubBullet.ColorValue.G, (int)objects.SubBullet.ColorValue.B);
				break;
			}
			case 17:
			{
				int num2 = (int)objects.SubBullet.ColorValue.G;
				num2 = this.ChangeValue(num2);
				num2 = ((num2 < 0) ? 0 : num2);
				num2 = ((num2 > 255) ? 255 : num2);
				objects.SubBullet.ColorValue = Color.FromArgb((int)objects.SubBullet.ColorValue.R, num2, (int)objects.SubBullet.ColorValue.B);
				break;
			}
			case 18:
			{
				int num3 = (int)objects.SubBullet.ColorValue.B;
				num3 = this.ChangeValue(num3);
				num3 = ((num3 < 0) ? 0 : num3);
				num3 = ((num3 > 255) ? 255 : num3);
				objects.SubBullet.ColorValue = Color.FromArgb((int)objects.SubBullet.ColorValue.R, (int)objects.SubBullet.ColorValue.G, num3);
				break;
			}
			case 19:
				objects.SubBullet.TransparentValueF = this.ChangeValue(objects.SubBullet.TransparentValueF);
				break;
			case 20:
				objects.SubBullet.AngleDegree = this.ChangeValue(objects.SubBullet.AngleDegree);
				break;
			case 21:
				objects.SubBullet.Velocity = this.ChangeValue(objects.SubBullet.Velocity);
				break;
			case 22:
				objects.SubBullet.DirectionDegree = this.ChangeValue(objects.SubBullet.DirectionDegree);
				break;
			case 23:
				objects.SubBullet.AccelerateCS = this.ChangeValue(objects.SubBullet.AccelerateCS);
				break;
			case 24:
				objects.SubBullet.AccDirection = this.ChangeValue(objects.SubBullet.AccDirection);
				break;
			case 25:
				objects.SubBullet.ScaleX = this.ChangeValue(objects.SubBullet.ScaleX);
				break;
			case 26:
				objects.SubBullet.ScaleY = this.ChangeValue(objects.SubBullet.ScaleY);
				break;
			case 27:
				objects.SubBullet.BeginningEffect = this.value > 0f;
				break;
			case 28:
				objects.SubBullet.EndingEffect = this.value > 0f;
				break;
			case 29:
				objects.SubBullet.Active = this.value > 0f;
				break;
			case 30:
				objects.SubBullet.Ghosting = this.value > 0f;
				break;
			case 31:
				objects.SubBullet.OutBound = this.value > 0f;
				break;
			case 32:
				objects.SubBullet.UnRemoveable = this.value > 0f;
				break;
			}
			this.ctime--;
			if (((this.changetype == 2) & (this.ctime == -1)) || ((this.changetype != 2) & (this.ctime == 0)))
			{
				objects.EventsExecutionList.Remove(this);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00044780 File Offset: 0x00042980
		public void Update(BaseObject_CS objects)
		{
			switch (this.changevalue)
			{
			case 0:
				objects.LifeTime = this.ChangeValue(objects.LifeTime);
				break;
			case 1:
				objects.Type = this.ChangeValue(objects.Type);
				break;
			case 2:
				objects.ScaleWidth = this.ChangeValue(objects.ScaleWidth);
				break;
			case 3:
				objects.ScaleLength = this.ChangeValue(objects.ScaleLength);
				break;
			case 4:
			{
				int num = (int)objects.ColorValue.R;
				num = this.ChangeValue(num);
				num = ((num < 0) ? 0 : num);
				num = ((num > 255) ? 255 : num);
				objects.ColorValue = Color.FromArgb(num, (int)objects.ColorValue.G, (int)objects.ColorValue.B);
				break;
			}
			case 5:
			{
				int num2 = (int)objects.ColorValue.G;
				num2 = this.ChangeValue(num2);
				num2 = ((num2 < 0) ? 0 : num2);
				num2 = ((num2 > 255) ? 255 : num2);
				objects.ColorValue = Color.FromArgb((int)objects.ColorValue.R, num2, (int)objects.ColorValue.B);
				break;
			}
			case 6:
			{
				int num3 = (int)objects.ColorValue.B;
				num3 = this.ChangeValue(num3);
				num3 = ((num3 < 0) ? 0 : num3);
				num3 = ((num3 > 255) ? 255 : num3);
				objects.ColorValue = Color.FromArgb((int)objects.ColorValue.R, (int)objects.ColorValue.G, num3);
				break;
			}
			case 7:
				objects.TransparentValueF = this.ChangeValue(objects.TransparentValueF);
				break;
			case 8:
				objects.AngleDegree = this.ChangeValue(objects.AngleDegree);
				break;
			case 9:
				objects.Velocity = this.ChangeValue(objects.Velocity);
				break;
			case 10:
				objects.DirectionDegree = this.ChangeValue(objects.DirectionDegree);
				break;
			case 11:
				objects.AccelerateCS = this.ChangeValue(objects.AccelerateCS);
				break;
			case 12:
				objects.AccDirection = this.ChangeValue(objects.AccDirection);
				break;
			case 13:
				objects.ScaleX = this.ChangeValue(objects.ScaleX);
				break;
			case 14:
				objects.ScaleY = this.ChangeValue(objects.ScaleY);
				break;
			case 15:
				objects.BeginningEffect = this.value > 0f;
				break;
			case 16:
				objects.EndingEffect = this.value > 0f;
				break;
			case 17:
				objects.Active = this.value > 0f;
				break;
			case 18:
				objects.Ghosting = this.value > 0f;
				break;
			case 19:
				objects.OutBound = this.value > 0f;
				break;
			case 20:
				objects.UnRemoveable = this.value > 0f;
				break;
			}
			this.ctime--;
			if (((this.changetype == 2) & (this.ctime == -1)) || ((this.changetype != 2) & (this.ctime == 0)))
			{
				objects.EventsExecutionList.Remove(this);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00044B14 File Offset: 0x00042D14
		private int ChangeValue(int Value)
		{
			return (int)this.ChangeValue((double)Value);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00044B30 File Offset: 0x00042D30
		private float ChangeValue(float Value)
		{
			return (float)this.ChangeValue((double)Value);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00044B4C File Offset: 0x00042D4C
		private double ChangeValue(double Value)
		{
			double num;
			if (this.changetype == 0)
			{
				if (this.change == 0)
				{
					num = (Value * (double)((float)this.ctime - 1f) + (double)this.value) / (double)((float)this.ctime);
				}
				else if (this.change == 1)
				{
					num = Value + (double)(this.value / (float)this.time);
				}
				else
				{
					num = Value - (double)(this.value / (float)this.time);
				}
			}
			else if (this.changetype == 1)
			{
				if (this.change == 0)
				{
					num = (double)this.value;
				}
				else if (this.change == 1)
				{
					num = Value + (double)this.value;
				}
				else
				{
					num = Value - (double)this.value;
				}
			}
			else if (this.change == 0)
			{
				num = (double)(float.Parse(this.region) + (this.value - float.Parse(this.region)) * (float)Math.Sin(6.283185307179586 / (double)((float)this.time) * (double)((float)this.time - (float)this.ctime)));
			}
			else if (this.change == 1)
			{
				num = (double)(float.Parse(this.region) + this.value * (float)Math.Sin(6.283185307179586 / (double)((float)this.time) * (double)((float)this.time - (float)this.ctime)));
			}
			else
			{
				num = (double)(float.Parse(this.region) - this.value * (float)Math.Sin(6.283185307179586 / (double)((float)this.time) * (double)((float)this.time - (float)this.ctime)));
			}
			return num;
		}

		// Token: 0x04000294 RID: 660
		public int parentid;

		// Token: 0x04000295 RID: 661
		public int id;

		// Token: 0x04000296 RID: 662
		public int change;

		// Token: 0x04000297 RID: 663
		public int changetype;

		// Token: 0x04000298 RID: 664
		public int changevalue;

		// Token: 0x04000299 RID: 665
		public string region;

		// Token: 0x0400029A RID: 666
		public float value;

		// Token: 0x0400029B RID: 667
		public int time;

		// Token: 0x0400029C RID: 668
		public int ctime;
	}
}
