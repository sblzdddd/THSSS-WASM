using System;
using System.Drawing;

namespace Shooting
{
	[Serializable]
	public class Execution
	{
		public void Update(BaseEmitter_CS objects)
		{
			switch (changevalue)
			{
			case 0:
			{
				var x = objects.CS_Position.X;
				objects.CS_Position = new PointF(ChangeValue(x), objects.CS_Position.Y);
				break;
			}
			case 1:
			{
				var y = objects.CS_Position.Y;
				objects.CS_Position = new PointF(objects.CS_Position.X, ChangeValue(y));
				break;
			}
			case 2:
				objects.EmitRadius = ChangeValue(objects.EmitRadius);
				break;
			case 3:
				objects.RadiusDirection = ChangeValue(objects.RadiusDirection);
				break;
			case 4:
				objects.Way = ChangeValue(objects.Way);
				break;
			case 5:
				objects.Circle = ChangeValue(objects.Circle);
				break;
			case 6:
				objects.EmitDirection = ChangeValue(objects.EmitDirection);
				break;
			case 7:
				objects.Range = ChangeValue(objects.Range);
				break;
			case 8:
				objects.Velocity = ChangeValue(objects.Velocity);
				break;
			case 9:
				objects.DirectionDegree = ChangeValue(objects.DirectionDegree);
				break;
			case 10:
				objects.AccelerateCS = ChangeValue(objects.AccelerateCS);
				break;
			case 11:
				objects.AccDirection = ChangeValue(objects.AccDirection);
				break;
			case 12:
				objects.SubBullet.LifeTime = ChangeValue(objects.SubBullet.LifeTime);
				break;
			case 13:
				objects.SubBullet.Type = ChangeValue(objects.SubBullet.Type);
				break;
			case 14:
				objects.SubBullet.ScaleWidth = ChangeValue(objects.SubBullet.ScaleWidth);
				break;
			case 15:
				objects.SubBullet.ScaleLength = ChangeValue(objects.SubBullet.ScaleLength);
				break;
			case 16:
			{
				var num = (int)objects.SubBullet.ColorValue.R;
				num = ChangeValue(num);
				num = ((num < 0) ? 0 : num);
				num = ((num > 255) ? 255 : num);
				objects.SubBullet.ColorValue = Color.FromArgb(num, (int)objects.SubBullet.ColorValue.G, (int)objects.SubBullet.ColorValue.B);
				break;
			}
			case 17:
			{
				var num2 = (int)objects.SubBullet.ColorValue.G;
				num2 = ChangeValue(num2);
				num2 = ((num2 < 0) ? 0 : num2);
				num2 = ((num2 > 255) ? 255 : num2);
				objects.SubBullet.ColorValue = Color.FromArgb((int)objects.SubBullet.ColorValue.R, num2, (int)objects.SubBullet.ColorValue.B);
				break;
			}
			case 18:
			{
				var num3 = (int)objects.SubBullet.ColorValue.B;
				num3 = ChangeValue(num3);
				num3 = ((num3 < 0) ? 0 : num3);
				num3 = ((num3 > 255) ? 255 : num3);
				objects.SubBullet.ColorValue = Color.FromArgb((int)objects.SubBullet.ColorValue.R, (int)objects.SubBullet.ColorValue.G, num3);
				break;
			}
			case 19:
				objects.SubBullet.TransparentValueF = ChangeValue(objects.SubBullet.TransparentValueF);
				break;
			case 20:
				objects.SubBullet.AngleDegree = ChangeValue(objects.SubBullet.AngleDegree);
				break;
			case 21:
				objects.SubBullet.Velocity = ChangeValue(objects.SubBullet.Velocity);
				break;
			case 22:
				objects.SubBullet.DirectionDegree = ChangeValue(objects.SubBullet.DirectionDegree);
				break;
			case 23:
				objects.SubBullet.AccelerateCS = ChangeValue(objects.SubBullet.AccelerateCS);
				break;
			case 24:
				objects.SubBullet.AccDirection = ChangeValue(objects.SubBullet.AccDirection);
				break;
			case 25:
				objects.SubBullet.ScaleX = ChangeValue(objects.SubBullet.ScaleX);
				break;
			case 26:
				objects.SubBullet.ScaleY = ChangeValue(objects.SubBullet.ScaleY);
				break;
			case 27:
				objects.SubBullet.BeginningEffect = value > 0f;
				break;
			case 28:
				objects.SubBullet.EndingEffect = value > 0f;
				break;
			case 29:
				objects.SubBullet.Active = value > 0f;
				break;
			case 30:
				objects.SubBullet.Ghosting = value > 0f;
				break;
			case 31:
				objects.SubBullet.OutBound = value > 0f;
				break;
			case 32:
				objects.SubBullet.UnRemoveable = value > 0f;
				break;
			}
			ctime--;
			if (((changetype == 2) & (ctime == -1)) || ((changetype != 2) & (ctime == 0)))
			{
				objects.EventsExecutionList.Remove(this);
			}
		}

		public void Update(BaseObject_CS objects)
		{
			switch (changevalue)
			{
			case 0:
				objects.LifeTime = ChangeValue(objects.LifeTime);
				break;
			case 1:
				objects.Type = ChangeValue(objects.Type);
				break;
			case 2:
				objects.ScaleWidth = ChangeValue(objects.ScaleWidth);
				break;
			case 3:
				objects.ScaleLength = ChangeValue(objects.ScaleLength);
				break;
			case 4:
			{
				var num = (int)objects.ColorValue.R;
				num = ChangeValue(num);
				num = ((num < 0) ? 0 : num);
				num = ((num > 255) ? 255 : num);
				objects.ColorValue = Color.FromArgb(num, (int)objects.ColorValue.G, (int)objects.ColorValue.B);
				break;
			}
			case 5:
			{
				var num2 = (int)objects.ColorValue.G;
				num2 = ChangeValue(num2);
				num2 = ((num2 < 0) ? 0 : num2);
				num2 = ((num2 > 255) ? 255 : num2);
				objects.ColorValue = Color.FromArgb((int)objects.ColorValue.R, num2, (int)objects.ColorValue.B);
				break;
			}
			case 6:
			{
				var num3 = (int)objects.ColorValue.B;
				num3 = ChangeValue(num3);
				num3 = ((num3 < 0) ? 0 : num3);
				num3 = ((num3 > 255) ? 255 : num3);
				objects.ColorValue = Color.FromArgb((int)objects.ColorValue.R, (int)objects.ColorValue.G, num3);
				break;
			}
			case 7:
				objects.TransparentValueF = ChangeValue(objects.TransparentValueF);
				break;
			case 8:
				objects.AngleDegree = ChangeValue(objects.AngleDegree);
				break;
			case 9:
				objects.Velocity = ChangeValue(objects.Velocity);
				break;
			case 10:
				objects.DirectionDegree = ChangeValue(objects.DirectionDegree);
				break;
			case 11:
				objects.AccelerateCS = ChangeValue(objects.AccelerateCS);
				break;
			case 12:
				objects.AccDirection = ChangeValue(objects.AccDirection);
				break;
			case 13:
				objects.ScaleX = ChangeValue(objects.ScaleX);
				break;
			case 14:
				objects.ScaleY = ChangeValue(objects.ScaleY);
				break;
			case 15:
				objects.BeginningEffect = value > 0f;
				break;
			case 16:
				objects.EndingEffect = value > 0f;
				break;
			case 17:
				objects.Active = value > 0f;
				break;
			case 18:
				objects.Ghosting = value > 0f;
				break;
			case 19:
				objects.OutBound = value > 0f;
				break;
			case 20:
				objects.UnRemoveable = value > 0f;
				break;
			}
			ctime--;
			if (((changetype == 2) & (ctime == -1)) || ((changetype != 2) & (ctime == 0)))
			{
				objects.EventsExecutionList.Remove(this);
			}
		}

		private int ChangeValue(int Value)
		{
			return (int)ChangeValue((double)Value);
		}

		private float ChangeValue(float Value)
		{
			return (float)ChangeValue((double)Value);
		}

		private double ChangeValue(double Value)
		{
			double num;
			if (changetype == 0)
			{
				if (change == 0)
				{
					num = (Value * (double)((float)ctime - 1f) + (double)value) / (double)((float)ctime);
				}
				else if (change == 1)
				{
					num = Value + (double)(value / (float)time);
				}
				else
				{
					num = Value - (double)(value / (float)time);
				}
			}
			else if (changetype == 1)
			{
				if (change == 0)
				{
					num = (double)value;
				}
				else if (change == 1)
				{
					num = Value + (double)value;
				}
				else
				{
					num = Value - (double)value;
				}
			}
			else if (change == 0)
			{
				num = (double)(float.Parse(region) + (value - float.Parse(region)) * (float)Math.Sin(6.283185307179586 / (double)((float)time) * (double)((float)time - (float)ctime)));
			}
			else if (change == 1)
			{
				num = (double)(float.Parse(region) + value * (float)Math.Sin(6.283185307179586 / (double)((float)time) * (double)((float)time - (float)ctime)));
			}
			else
			{
				num = (double)(float.Parse(region) - value * (float)Math.Sin(6.283185307179586 / (double)((float)time) * (double)((float)time - (float)ctime)));
			}
			return num;
		}

		public int parentid;

		public int id;

		public int change;

		public int changetype;

		public int changevalue;

		public string region;

		public float value;

		public int time;

		public int ctime;
	}
}
