using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public class EventGroup
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		public void Update(BaseEmitter_CS CS_Emitter)
		{
			if (CS_Emitter.Time < 1)
			{
				CS_Emitter.Time = 1;
			}
			float[] array = new float[13];
			float[] array2 = new float[33];
			array[0] = (float)(CS_Emitter.Time - CS_Emitter.StartTime + 1);
			array[1] = CS_Emitter.CS_Position.X;
			array[2] = CS_Emitter.CS_Position.Y;
			array[3] = CS_Emitter.EmitRadius;
			array[4] = (float)CS_Emitter.RadiusDirection;
			array[5] = (float)CS_Emitter.Way;
			array[6] = (float)CS_Emitter.Circle;
			array[7] = (float)CS_Emitter.EmitDirection;
			array[8] = (float)CS_Emitter.Range;
			array[9] = CS_Emitter.SubBullet.ScaleWidth;
			array[10] = CS_Emitter.SubBullet.ScaleLength;
			array[11] = CS_Emitter.SubBullet.TransparentValueF;
			array[12] = (float)CS_Emitter.SubBullet.AngleDegree;
			array2[0] = CS_Emitter.CS_Position.X;
			array2[1] = CS_Emitter.CS_Position.Y;
			array2[2] = CS_Emitter.EmitRadius;
			array2[3] = (float)CS_Emitter.RadiusDirection;
			array2[4] = (float)CS_Emitter.Way;
			array2[5] = (float)CS_Emitter.Circle;
			array2[6] = (float)CS_Emitter.EmitDirection;
			array2[7] = (float)CS_Emitter.Range;
			array2[8] = CS_Emitter.Velocity;
			array2[9] = (float)CS_Emitter.DirectionDegree;
			array2[10] = CS_Emitter.AccelerateCS;
			array2[11] = (float)CS_Emitter.AccDirection;
			array2[12] = (float)CS_Emitter.SubBullet.LifeTime;
			array2[13] = (float)CS_Emitter.SubBullet.Type;
			array2[14] = CS_Emitter.SubBullet.ScaleLength;
			array2[15] = CS_Emitter.SubBullet.ScaleLength;
			array2[16] = (float)CS_Emitter.SubBullet.ColorValue.R;
			array2[17] = (float)CS_Emitter.SubBullet.ColorValue.G;
			array2[18] = (float)CS_Emitter.SubBullet.ColorValue.B;
			array2[19] = CS_Emitter.SubBullet.TransparentValueF;
			array2[20] = (float)CS_Emitter.SubBullet.AngleDegree;
			array2[21] = CS_Emitter.SubBullet.Velocity;
			array2[22] = (float)CS_Emitter.SubBullet.DirectionDegree;
			array2[23] = CS_Emitter.SubBullet.AccelerateCS;
			array2[24] = (float)CS_Emitter.SubBullet.AccDirection;
			array2[25] = CS_Emitter.SubBullet.ScaleX;
			array2[26] = CS_Emitter.SubBullet.ScaleY;
			array2[27] = 0f;
			array2[28] = 0f;
			array2[29] = 0f;
			array2[30] = 0f;
			array2[31] = 0f;
			array2[32] = 0f;
			if (this.t < 1)
			{
				this.t = 1;
			}
			if ((CS_Emitter.Time - CS_Emitter.StartTime + 1) % this.t == 0)
			{
				this.loop++;
			}
			foreach (Event @event in this.EventList)
			{
				bool flag = this.CheckCondition(array, @event);
				if (flag)
				{
					if (@event.special == 3)
					{
						if (@event.changevalue == 0)
						{
							@event.res = CS_Emitter.CS_Position.X - 4f;
						}
						if (@event.changevalue == 1)
						{
							@event.res = CS_Emitter.CS_Position.Y + 16f;
						}
						if (@event.changevalue == 6 || @event.changevalue == 24)
						{
							@event.res = this.Twopointangle(CS_Emitter.OriginalPosition.X - 4f, CS_Emitter.OriginalPosition.Y + 16f, CS_Emitter.OriginalPosition.X, CS_Emitter.OriginalPosition.Y) * 180f / 3.1415927f;
						}
					}
					else if (@event.special == 4)
					{
						if (@event.changevalue == 0)
						{
							@event.res = CS_Emitter.MyPlane.OriginalPosition.X + 320f - 192f;
						}
						if (@event.changevalue == 1)
						{
							@event.res = CS_Emitter.MyPlane.OriginalPosition.Y + 240f - 224f;
						}
						if (@event.changevalue == 3 || @event.changevalue == 6 || @event.changevalue == 9 || @event.changevalue == 11 || @event.changevalue == 24)
						{
							@event.res = this.Twopointangle(CS_Emitter.MyPlane.OriginalPosition.X, CS_Emitter.MyPlane.OriginalPosition.Y, CS_Emitter.OriginalPosition.X, CS_Emitter.OriginalPosition.Y) * 180f / 3.1415927f;
						}
					}
					if (@event.special != 1)
					{
						if (@event.special == 2)
						{
							CS_Emitter.ShootBullet();
						}
						else
						{
							Execution execution = new Execution();
							if (!@event.noloop)
							{
								if (@event.time > 0)
								{
									@event.time--;
									if (@event.time == 0)
									{
										@event.noloop = true;
									}
								}
								execution.parentid = CS_Emitter.LayerID;
								execution.id = CS_Emitter.ID;
								execution.change = @event.change;
								execution.changetype = @event.changetype;
								execution.changevalue = @event.changevalue;
								if (@event.rand != 0f)
								{
									execution.value = @event.res + @event.rand * (float)CS_Emitter.Ran.NextPMDouble();
								}
								else
								{
									execution.value = @event.res;
								}
								execution.region = array2[@event.changename].ToString();
								execution.time = @event.times;
								execution.ctime = execution.time;
								CS_Emitter.EventsExecutionList.Add(execution);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		public void Update(BaseObject_CS CS_Bullet)
		{
			float[] array = new float[3];
			float[] array2 = new float[21];
			array[0] = (float)CS_Bullet.Time;
			array[1] = CS_Bullet.CS_Position.X;
			array[2] = CS_Bullet.CS_Position.Y;
			array2[0] = (float)CS_Bullet.LifeTime;
			array2[1] = (float)CS_Bullet.Type;
			array2[2] = CS_Bullet.ScaleWidth;
			array2[3] = CS_Bullet.ScaleLength;
			array2[4] = (float)CS_Bullet.ColorValue.R;
			array2[5] = (float)CS_Bullet.ColorValue.G;
			array2[6] = (float)CS_Bullet.ColorValue.B;
			array2[7] = CS_Bullet.TransparentValueF;
			array2[8] = (float)CS_Bullet.AngleDegree;
			array2[9] = CS_Bullet.Velocity;
			array2[10] = (float)CS_Bullet.DirectionDegree;
			array2[11] = CS_Bullet.AccelerateCS;
			array2[12] = (float)CS_Bullet.AccDirection;
			array2[13] = CS_Bullet.ScaleX;
			array2[14] = CS_Bullet.ScaleY;
			array2[15] = 0f;
			array2[16] = 0f;
			array2[17] = 0f;
			array2[18] = 0f;
			array2[19] = 0f;
			array2[20] = 0f;
			if (this.t < 1)
			{
				this.t = 1;
			}
			if ((CS_Bullet.Time + 1) % this.t == 0)
			{
				this.loop++;
			}
			foreach (Event @event in this.EventList)
			{
				if (@event.special2 == 1)
				{
					array[0] = (float)CS_Bullet.StageData.TimeMain;
				}
				bool flag = this.CheckCondition(array, @event);
				if (flag)
				{
					if (@event.special == 4)
					{
						if (@event.changevalue == 10 || @event.changevalue == 12)
						{
							@event.res = this.Twopointangle(CS_Bullet.MyPlane.OriginalPosition.X, CS_Bullet.MyPlane.OriginalPosition.Y, CS_Bullet.OriginalPosition.X, CS_Bullet.OriginalPosition.Y) * 180f / 3.1415927f;
						}
					}
					Execution execution = new Execution();
					if (!@event.noloop)
					{
						if (@event.time > 0)
						{
							@event.time--;
							if (@event.time == 0)
							{
								@event.noloop = true;
							}
						}
						execution.change = @event.change;
						execution.changetype = @event.changetype;
						execution.changevalue = @event.changevalue;
						if (@event.rand == 0f)
						{
							execution.value = @event.res;
						}
						else
						{
							execution.value = @event.res + @event.rand * (float)CS_Bullet.StageData.Ran.NextPMDouble();
						}
						execution.region = array2[@event.changename].ToString();
						execution.time = @event.times;
						execution.ctime = execution.time;
						CS_Bullet.EventsExecutionList.Add(execution);
					}
				}
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002F26C File Offset: 0x0002D46C
		private bool CheckCondition(float[] conditions, Event ev)
		{
			bool flag = false;
			bool flag2 = false;
			if (ev.opreator == ">")
			{
				flag = conditions[ev.contype] > float.Parse(ev.condition) + (float)(this.loop * this.addtime);
			}
			else if (ev.opreator == "<")
			{
				flag = conditions[ev.contype] < float.Parse(ev.condition) + (float)(this.loop * this.addtime);
			}
			else if (ev.opreator == "=")
			{
				flag = conditions[ev.contype] == float.Parse(ev.condition) + (float)(this.loop * this.addtime);
			}
			if (ev.opreator2 == ">")
			{
				flag2 = conditions[ev.contype2] > float.Parse(ev.condition2) + (float)(this.loop * this.addtime);
			}
			else if (ev.opreator2 == "<")
			{
				flag2 = conditions[ev.contype2] < float.Parse(ev.condition2) + (float)(this.loop * this.addtime);
			}
			else if (ev.opreator2 == "=")
			{
				flag2 = conditions[ev.contype2] == float.Parse(ev.condition2) + (float)(this.loop * this.addtime);
			}
			bool flag3;
			if (ev.collector == "且")
			{
				flag3 = flag && flag2;
			}
			else if (ev.collector == "或")
			{
				flag3 = flag || flag2;
			}
			else
			{
				flag3 = flag;
			}
			return flag3;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002F460 File Offset: 0x0002D660
		private float Twopointangle(float x2, float y2, float x1, float y1)
		{
			float num;
			if (x2 - x1 != 0f)
			{
				num = (float)Math.Atan((double)((y2 - y1) / (x2 - x1)));
			}
			else
			{
				num = (float)Math.Atan((double)((y2 - y1) / (x2 - x1 + 0.1f)));
			}
			if (x2 - x1 < 0f)
			{
				num += 3.1415927f;
			}
			return num;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
		public object Clone()
		{
			EventGroup ev = (EventGroup)base.MemberwiseClone();
			ev.EventList = new List<Event>();
			this.EventList.ForEach(delegate(Event x)
			{
				ev.EventList.Add((Event)x.Clone());
			});
			return ev;
		}

		// Token: 0x040001CB RID: 459
		public int index;

		// Token: 0x040001CC RID: 460
		public string tag = "";

		// Token: 0x040001CD RID: 461
		public int t = 1;

		// Token: 0x040001CE RID: 462
		public int loop;

		// Token: 0x040001CF RID: 463
		public int addtime;

		// Token: 0x040001D0 RID: 464
		public int special;

		// Token: 0x040001D1 RID: 465
		public List<Event> EventList = new List<Event>();
	}
}
